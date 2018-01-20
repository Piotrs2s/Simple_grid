using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SiatkaStrukturalna
{
    public partial class Form1 : Form
    {
      

        private readonly Graphics _graphics;

        Brush brush = new SolidBrush(Color.Black);
        Pen pen = new Pen(Color.Black);

        private List<Point> Vertices;
        private List<Element> ElementsList;

        private int distance = 50;

        public Form1()

        {
            InitializeComponent();
            _graphics = pictureBox1.CreateGraphics();
            _graphics.Clear(Color.White);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Vertices = GeneratePoints(distance);
            GenerateElements(Vertices, distance, pen);
            GenerateNet();
        }

        public List<Point> GeneratePoints(int distanse)
        {
            Vertices = new List<Point>();
            int size = pictureBox1.Width;  

            for (int i = 0; i < size; i = i + distanse)
            {
                Point px = new Point(i, 0);
                Vertices.Add(px);
                for (int j = distance; j < size; j = j + distanse)
                {
                    Point py = new Point(i, j);
                    Vertices.Add(py);
                }

            }
            
            //foreach (Point point in _vertices)
            //{
            //    _graphics.FillRectangle(brush, point.X, point.Y, 1, 1);
            //}

           
            return Vertices;
        }


        public void GenerateElements(List<Point> vertices, int distanse, Pen pen)
        {
            ElementsList = new List<Element>();

            for (int i = 0; i < vertices.Count; i++)
            {
                Point a = new Point(vertices[i].X, vertices[i].Y);
                Point b = new Point(vertices[i].X + distanse, vertices[i].Y);
                Point c = new Point(vertices[i].X, vertices[i].Y + distanse);

                if (Helicity(a.X, a.Y, b.X, b.Y, c.X, c.Y))
                {
                    Element element = new Element(a, b, c);

                    ElementsList.Add(element);
                }

                a = new Point(vertices[i].X, vertices[i].Y + distanse);
                b = new Point(vertices[i].X + distanse, vertices[i].Y);
                c = new Point(vertices[i].X + distanse, vertices[i].Y + distanse);

                if (Helicity(a.X, a.Y, b.X, b.Y, c.X, c.Y))
                {
                    Element element = new Element(a, b, c);

                    ElementsList.Add(element);

                }
            }
        }


        private static bool Helicity(int ax, int ay, int bx, int by, int cx, int cy)
        {

            if ((ax * by + bx * cy + cx * ay - cx * by - ax * cy - bx * ay) > 0)
            {
                return true;
            }
            return false;
        }


        public void GenerateNet()
        {
            for (int i = 0; i < ElementsList.Count; i++)
            {
                _graphics.DrawLine(pen, ElementsList[i].a, ElementsList[i].b);
                _graphics.DrawLine(pen, ElementsList[i].b, ElementsList[i].c);
                _graphics.DrawLine(pen, ElementsList[i].c, ElementsList[i].a);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Import(ElementsList, Vertices);
        }

      

        private void Import(List<Element> Elementslist, List<Point> VerticesList)
        {

            using (System.IO.StreamWriter file =
         new System.IO.StreamWriter("C:\\Users\\Piotrek\\Desktop\\Results\\Verticies.txt"))
            {
                int o = 0;
                foreach (Point p in VerticesList)
                {

                    file.WriteLine("Point {0}: {1}",o, p);
                    o++;
                }
            }

            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter("C:\\Users\\Piotrek\\Desktop\\Results\\Elements.txt"))
            {                             
                int o = 0;
                foreach (Element E in Elementslist)
                {                                     
                    file.WriteLine("Element {0}: a={1} b={2} c={3}\n", o, E.a, E.b, E.c);
                    o++;
                }
            }
        }
    }
}
