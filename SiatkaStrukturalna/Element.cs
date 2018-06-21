using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SiatkaStrukturalna
{
    //Represents single element made from 3 nodes
    public class Element
    {        
            //a, b, c - Nodes/Vertices of element
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
    
}
