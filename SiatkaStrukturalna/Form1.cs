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
      

        Graphics Graphics;

        List<Point> Nodes;
        List<Element> ElementsList;


        public Form1()
        {
            InitializeComponent();

            //Prepare bitmap
            Graphics = pictureBox1.CreateGraphics();

        }

        //Generate grid button
        private void generateButton_Click(object sender, EventArgs e)
        {
            int elementSize = elementSizeTrackBar.Value;
            
            pictureBox1.Refresh();
            Nodes = GeneratePoints(elementSize);
            GenerateElements(Nodes, elementSize);
            GenerateGrid();
        }

        ////Generate grid of nodes
        private List<Point> GeneratePoints(int distance)
        {
            Nodes = new List<Point>(); //List for nodes
            int size = pictureBox1.Width; //Drawing area size  

            //distance - distance between nodes based on element size
            for (int i = 0; i < size ; i = i + distance)
            {
                Point px = new Point(i, 0); //Start node for each column
                Nodes.Add(px);
                               
                for (int j = distance; j < size ; j = j + distance)
                {
                    Point py = new Point(i, j); //Create column of nodes
                    Nodes.Add(py);                    
                }

            }
         
            return Nodes;
        }

        //Generate elements using nodes
        private void GenerateElements(List<Point> nodes, int distance)
        {
            //List for elements
            ElementsList = new List<Element>();


            for (int i = 0; i < nodes.Count; i++)
            {                               
                    //First triangle from 4 nodes group
                    var a = new Point(nodes[i].X, nodes[i].Y);
                    var b = new Point(nodes[i].X + distance, nodes[i].Y);
                    var c = new Point(nodes[i].X, nodes[i].Y + distance);

                    //Check if helicity of element is correct (it have to be the same for all elements)
                    if (Helicity(a, b, c) && nodes[i].X + distance < pictureBox1.Width && nodes[i].Y + distance < pictureBox1.Height )
                    {
                        Element element = new Element(a, b, c);
                        ElementsList.Add(element);
                    }
                
                    //Seckond triangle from 4 nodes group
                    var d = new Point(nodes[i].X, nodes[i].Y + distance);
                    var e = new Point(nodes[i].X + distance, nodes[i].Y);
                    var f = new Point(nodes[i].X + distance, nodes[i].Y + distance);

                    if (Helicity(d, e, f) && nodes[i].X + distance < pictureBox1.Width && nodes[i].Y + distance < pictureBox1.Height)
                    {
                        Element element2 = new Element(d, e, f);
                        ElementsList.Add(element2);

                    }
                
                
            }
        }

        //Check helicity of triangle element
        static bool Helicity(Point a, Point b, Point c)
        {

            if ((a.X * b.Y + b.X * c.Y + c.X * a.Y - c.X * b.Y - a.X * c.Y - b.X * a.Y) > 0)
            {
                return true;
            }
            return false;
        }

        //Draw grid
        public void GenerateGrid()
        {
            var blackPen = new Pen(Color.FromArgb(255,0,0,0));

            for (int i = 0; i < ElementsList.Count; i++)
            {
                Graphics.DrawLine(blackPen, ElementsList[i].a, ElementsList[i].b);
                Graphics.DrawLine(blackPen, ElementsList[i].b, ElementsList[i].c);
                Graphics.DrawLine(blackPen, ElementsList[i].c, ElementsList[i].a);
            }
        }

        //Show TrackBar value
        private void elementSizeTrackBar_Scroll(object sender, EventArgs e)
        {

            elementSizeLabel.Text = "Element size: " + (elementSizeTrackBar.Value);
        }

        //Results/import option, saves files with information about nodes and elements
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Import(ElementsList, Nodes);
        }


        //Save informations about elements and nodes as txt in choosen folder
        private void Import(List<Element> Elementslist, List<Point> NodesList)
        {
            
        
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Save files in:";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                //Save elements and coordinates of their nodes
                using (StreamWriter file = new StreamWriter(string.Concat(folderBrowserDialog.SelectedPath,"\\Elements.txt") ))
                {
                    int number = 0;
                    foreach (Element E in Elementslist)
                    {
                        file.WriteLine("Element {0}: a={1} b={2} c={3}\n", number, E.a, E.b, E.c);
                        number++;

                     }
                }
                //List of nodes
                using (StreamWriter file = new StreamWriter(string.Concat(folderBrowserDialog.SelectedPath, "\\Nodes.txt") ))
                {
                    int number = 0;
                    foreach (Point p in NodesList)
                    {
                        file.WriteLine("Point {0}: {1}", number, p);
                        number++;                        
                    }
                }
            }                     
        }




        

        
    }
}
