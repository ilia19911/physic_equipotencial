using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using LiveChartsCore.Defaults;

namespace physic_equipotencial.Models
{
    public class Potencial
    {
        static public ObservableCollection<ObservablePoint> GetSeries(double value, double necessaryPotential,ObservableCollection<ObservablePoint> h_edge, ObservableCollection<ObservablePoint> l_edge)
        {
            ObservableCollection<ObservablePoint> result = new ObservableCollection<ObservablePoint>();
            double startPotential = GetPotencial(h_edge[0].X.Value,value, h_edge, l_edge);
            for (int xIndex = 0; xIndex < 8; xIndex++) //h_edge.Count()
            {
                if (xIndex == 7)
                {
                    var yValue = findYCoor(xIndex,startPotential, 0.11,h_edge,l_edge);
                    result.Add(new ObservablePoint{X =h_edge[xIndex].X, Y= yValue });
                }
                
            }
            return result;
        }
        static double getSinglePotential(double x, double y, double x_edge, double y_edge)
        {
            double result = 0;
            double a = Math.Abs(y - y_edge);
            double b = Math.Abs(x - x_edge);
            double c = Math.Sqrt(Math.Pow(a,2) + Math.Pow(b,2));
            result = -c;
            return result;
        }
        /// <summary>
        ///
        ///  |\
        ///  | \
        ///a |  \ c
        ///  |   \
        ///  |   (\alpha
        ///  -------
        ///   b
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="lastPoint"></param>
        /// <param name="CurrentPoint"></param>
        /// <returns></returns>
        static bool checkVertical(ObservablePoint Target, ObservablePoint lastPoint, ObservablePoint CurrentPoint)
        {
            bool result = false;
            double a = lastPoint.Y.Value - CurrentPoint.Y.Value;
            double b = lastPoint.X.Value - CurrentPoint.X.Value;
            double c = Math.Sqrt(Math.Pow(a,2) + Math.Pow(b,2));
            double sin = a / c;
            double angle = (180 / Math.PI) *Math.Asin(sin);
            angle = Math.Abs(angle);
            if (angle == 0)
            {
                if (Target.X >= lastPoint.X.Value && Target.X < CurrentPoint.X.Value)
                {
                    result = true;
                }
            }
            else if (verticalIsLower(Target, lastPoint, angle))
            {
                if (!verticalIsLower(Target, CurrentPoint, angle))
                {
                    result = true;
                }
            }

            return result;
        }

        static bool verticalIsLower(ObservablePoint Target, ObservablePoint Point, double angle)
        {
            angle = 90 - angle;
            var result = false;
            
            double dx = Target.X.Value - Point.X.Value;
            double yProection = 0;
            if (dx == 0)
            {
                yProection = Point.Y.Value;
            }
            else if (angle == 0)
            {
                yProection = 0;
            }
            else
            {
                double verticalLength = dx / Math.Cos(angle) ;
                yProection = Math.Sqrt(Math.Pow(verticalLength,2) - Math.Pow(dx,2)) + Point.Y.Value;
            }
            if (yProection <= Target.Y)
            {
                result = true;
            }

            return result;
        }
        static double GetPotencial(double xValue,double yValue, ObservableCollection<ObservablePoint> hEdge, ObservableCollection<ObservablePoint> lEdge)
        {
            double result = 0;
            double hVectorSumm = 0;
            double l_vector_summ = 0;
            ObservablePoint lastPoint = null;
            foreach (var i in lEdge)
            {
                if (i is ObservablePoint {X: { }, Y: { }} point)
                {
                    if (lastPoint != null)
                    {
                        ///to do возвращать длину вертикали
                        if (checkVertical(new ObservablePoint {X = xValue, Y = yValue}, lastPoint, point))
                        {
                            l_vector_summ +=getSinglePotential(xValue, yValue,((ObservablePoint) i).X.Value, ((ObservablePoint) i).Y.Value );
                        }
                    }
                    lastPoint = point;
                }
            }

            result = hVectorSumm + l_vector_summ;

            return result;
        }
        private static double findYCoor(int XIndex, double necessaryPotential, double maxError,ObservableCollection<ObservablePoint> hEdge, ObservableCollection<ObservablePoint> lEdge)
        {
            double XValue = hEdge[XIndex].X.Value;
            double result = (hEdge[XIndex].Y.Value - lEdge[XIndex].Y.Value)/2 + lEdge[XIndex].Y.Value;
            double potential = GetPotencial(XValue,result, hEdge, lEdge);
            double error = potential - necessaryPotential;
            while (Math.Abs(error) >= Math.Abs(maxError))
            {
                if (error > 0)
                {
                    result += 0.001;
                }
                else
                {
                    result -= 0.001;
                }

                if (Math.Abs(error) < 5)
                {
                    int i = 0;
                }
                potential = GetPotencial(XValue,result, hEdge, lEdge);
                error = potential - necessaryPotential;
            }
            return result;
        }
    }
}