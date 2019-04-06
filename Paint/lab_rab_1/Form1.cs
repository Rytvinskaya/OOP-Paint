using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using CircleSpace;
using EllipseSpace;
using LineSpace;
using PoligonSpace;
using RectangleSpace;
using ShapeSpace;
using SquareSpace;


namespace lab_rab_1
{
    public partial class fMain : Form
    {
        
        public PictureBox pb;
        
        public Shape fig;
        public int thickness = 2; 
        public bool drLine, drRect, drCir, drEll, drSqu, drPol, drPolProc, drPolEnd, press;
        public Button curb;
        
        public Point pos1, pos2;
        public Color curColor;
        public List<Point> ps;
        MyImage myIm = new MyImage();
        public fMain()
        {
            InitializeComponent();
            press = false;
            bColor.BackColor = Color.Black;
        }

        private void bLine_Click(object sender, EventArgs e)
        {
            PolEndDraw();
            FigFalse();
            drLine = true;
        }

        private void bRectangle_Click(object sender, EventArgs e)
        {
            PolEndDraw();
            FigFalse();
            drRect = true;
        }

        private void bEllipse_Click(object sender, EventArgs e)
        {
            PolEndDraw();
            FigFalse();
            drEll = true;
        }

        private void bTriangle_Click(object sender, EventArgs e)
        {
            //drPolProc = true
            PolEndDraw();
            FigFalse();
            drPol = true;

        }

        private void bSquare_Click(object sender, EventArgs e)
        {
            PolEndDraw();
            FigFalse();
            drSqu = true;        
        }

        private void bCircle_Click(object sender, EventArgs e)
        {
            PolEndDraw();
            FigFalse();
            drCir = true;
        }

        private void pbHolst_MouseUp(object sender, MouseEventArgs e)
        {
            if ((fig != null)&&(!drPolProc)&&(!drPolEnd))
            {
                Pen pen = new Pen(curColor, thickness);
                press = false;
                Graphics g = Graphics.FromImage(myIm.bmp);
                g.DrawImage(myIm.bmp, 0, 0);
                g.DrawImage(myIm.bmp_2, 0, 0);
                pb.Image = myIm.bmp;
            }
            if (drPolEnd)
            {
                drPolEnd = false;
                Pen pen = new Pen(curColor, thickness);
                press = false;
                Graphics g = Graphics.FromImage(myIm.bmp);
                fig = new Poligon(ps, pen, pos1, pos2);
                fig.mypen = pen;
                fig.Draw(g);
                g.DrawImage(myIm.bmp, 0, 0);
                myIm.figures.Add(fig);
                pb.Image = myIm.bmp;
                pb.Refresh();
                g.Save();
            }
        }
        public void FigFalse()
        {
            drLine = false;
            drRect = false;
            drCir = false;
            drEll = false; 
            drSqu = false;
            drPol = false;
        }

        private void cbThickness_SelectedIndexChanged(object sender, EventArgs e)
        {
            PolEndDraw();
            thickness = Convert.ToInt32(cbThickness.Text);
        }

        private void pbHolst_MouseDown(object sender, MouseEventArgs e) 
        {
            Pen pen = new Pen(curColor, thickness);
            pos1 = e.Location;
            press = true;
            if (drCir)
            {
                fig = new Circle(pen, pos1, pos2);               
            }
            else if (drEll)
            {
                fig = new Ellipse(pen, pos1, pos2);
            }
            else if (drLine)
            {
                fig = new Line(pen, pos1, pos2);
            }
            else if (drRect)
            {
                fig = new RectangleSpace.Rectangle(pen, pos1, pos2);
            }
            else if (drSqu)
            {
                fig = new Square(pen, pos1, pos2);
            }
            else if (drPolProc)
            {
                if (((Math.Abs(ps[0].X-pos1.X)<5)&&(Math.Abs(ps[0].Y-pos1.Y)<5)&&(ps.Count>1)))
                {
                    ps.Add(ps[0]);
                    drPolProc = false;
                    drPolEnd = true;
                }
                else
                {
                    Graphics g = Graphics.FromImage(pb.Image);
                    Pen p = new Pen(curColor, thickness);
                    g.DrawLine(p,ps[ps.Count-1],pos1);
                    pb.Refresh();
                    g.Save();
                    ps.Add(pos1);
                    p.Dispose();
                }
            }
            else if (drPol)
            {
                ps = new List<Point>();
                ps.Add(pos1);
                drPol = false;
                drPolProc = true;
            }
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            pb = pbHolst;
            myIm.bmp = new Bitmap(pbHolst.Width, pbHolst.Height);
            myIm.bmp_2 = new Bitmap(pbHolst.Width, pbHolst.Height);
            myIm.figures = new List<Shape>();
            OpenFromFile();
            pb.Image = myIm.bmp;
            curColor = Color.Black;
            Pen pen = new Pen(curColor, thickness);
            Graphics g = Graphics.FromImage(pb.Image);
        }
        public void SaveInFile()
        {
            ImageConverter converter = new ImageConverter();
            myIm.image_byte = (byte[])converter.ConvertTo(myIm.bmp, typeof(byte[]));
            XmlSerializer holst = new XmlSerializer(typeof(byte[]));
            Stream writer = new FileStream("myImage.xml", FileMode.Create);
            holst.Serialize(writer, myIm.image_byte);
            writer.Close();
        }
        public void OpenFromFile()
        {
            XmlSerializer holst = new XmlSerializer(typeof(byte[]));
            Stream reader = new FileStream("myImage.xml", FileMode.Open);  //представляет байтовый поток и является базовым для всех остальных классов потоков
            myIm.image_byte = (byte[])holst.Deserialize(reader);
            reader.Close();
            myIm.bmp = byteArrayToBmp(myIm.image_byte);
            
        }
        private Bitmap byteArrayToBmp(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);  //запись содержимого потока памяти в другой поток
            Bitmap returnbmp = new Bitmap(ms);
            return returnbmp;
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveInFile();
        }
        public void PolEndDraw()
        {
            if (drPolProc)
            {
                drPolProc = false;
                Pen pen = new Pen(curColor, thickness);
                press = false;
                Graphics g = Graphics.FromImage(pb.Image);
                fig = new Poligon(ps, pen, pos1, pos2);
                fig.mypen = pen;
                fig.Draw(g);
                myIm.figures.Add(fig);
                pb.Refresh();
                g.Save();
            }
        }

        private void pbHolst_MouseMove(object sender, MouseEventArgs e)
        {           
            if ((fig!=null)&&(press)&&(!(drPol || drPolEnd || drPolProc))&&(drRect || drEll || drLine || drCir || drSqu))
            {
                pb.Image = myIm.bmp;
                Bitmap bmp2 = new Bitmap(pbHolst.Width, pbHolst.Height);
                Graphics gr = Graphics.FromImage(bmp2);
                gr.DrawImage(myIm.bmp, 0, 0);
                pos2 = e.Location;
                fig.pos2 = pos2;
                fig.Draw(gr);
                myIm.bmp_2 = bmp2;
                pb.Image = myIm.bmp_2;
                pb.Refresh();
            }
        }

        private void bColor_Click(object sender, EventArgs e)
        {
            if (cdDraw.ShowDialog() == DialogResult.Cancel)
                return;
            curColor = cdDraw.Color;
            bColor.BackColor = curColor;
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pb.Image);
            g.Clear(Color.White);
            pb.Refresh();
            g.Save();
        }
       
    }
    [Serializable]
    public class MyImage
    {
        public MyImage(){}
        [NonSerialized]
        public List<Shape> figures;
        [NonSerialized]
        public Bitmap bmp,bmp_2;
        public byte[] image_byte;
    }
}