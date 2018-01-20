using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SiatkaStrukturalna
{
    public class Element
    {        
            public Point a { get; set; }
            public Point b { get; set; }
            public Point c { get; set; }


           public Element(Point A, Point B, Point C)
           {
            a = A;
            b = B;
            c = C;
           }
        
    }

    //public class Point
    //{
    //    public int X { get; set; }
    //    public int Y { get; set; }
    //    public int Index { get; set; }

    //    public Point(int x, int y, int index)
    //    {
    //        X = x;
    //        Y = y;
    //        Index = index;
    //    }
    //}
}
