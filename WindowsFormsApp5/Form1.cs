using GraphicsLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Screen = GraphicsLibrary.Screen;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Screen s = new Screen(500, 200, 8);
            Canvas c = new Canvas(200, 500, 8);
            //s.SetWindow(-1.5, 1.5, -1.5, 1.5);
            //s.FillRectangle(0, 0, Width, Height, Color.Gold.ToArgb());            
            int a_x = 0;
            int a_y = 0;
            Bitmap imagee = new Bitmap(c.width, c.height);
            //Добавляется прямоугольник голубого цвета
            c.DrawCanvasCol(0, 0, c, 0x780000FF);
            for (int y = 0; y < c.height; y++)
                for (int x = 0; x < c.width; x++)
                {
                    int h = c.data[x+y * c.width];
                    imagee.SetPixel(x, y, Color.FromArgb(h));
                }
            //Добавляется прямоугольник зеленого цвета от точки 50x50
            c.DrawCanvasCol(50, 50, c, 0x7800FF00);
            for (int y = 0; y < c.height; y++)
                for (int x = 0; x < c.width; x++)
                {
                    int h = c.data[x+y * c.width];
                    imagee.SetPixel(x, y, Color.FromArgb(h));
                }
            //!!!Если раскоментировать строку ниже, создастся два слоя, накладывающиеся друг на друга
           e.Graphics.DrawImage(imagee, 0, 0);



            double centerX = 0 / 2;
            double centerY = 0 / 2;
            double radius = 1;
            double angle = 0;
            double step = 1.0;
            Model m = new Model(Color.Blue.ToArgb());

            while (angle < 360.0)
            {
                m.AddVertex(Convert.ToSingle(centerX + radius * Math.Cos(angle * Math.PI / 180)), Convert.ToSingle(centerY + radius * Math.Sin(angle * Math.PI / 180)));

                s.DrawLine(centerX, centerY, Convert.ToSingle(centerX + radius * Math.Cos(angle * Math.PI / 180)),
                Convert.ToSingle(centerY + radius * Math.Sin(angle * Math.PI / 180)), Color.Black.ToArgb());
                angle += step;
            }
            s.DrawModel(m);
            m.Translate(0.2, 0.1);
            s.DrawModel(m);
            s.DrawLine(0.0, 0.0, 1.5, 1.5, Color.Blue.ToArgb());
            m.Scale(0.5, 0.3);
            s.DrawModel(m);
            angle = 0;
            while (angle < 360.0)
            {
                m.Rotate(angle * Math.PI / 180);
                s.DrawModel(m);
                angle += step;
            }

            //c.DrawLine(0, 600, 100, 100, Color.Black.ToArgb());
            // c.DrawLine(0, 0, 400, 300, Color.Black.ToArgb());

            //Выполняется проверка на прозрачность и вывод окончательного рисунка
            c.DrawCanvas(25, 25, c, 0x7800FF00);
            for (int y = 0; y < c.height; y++)
                for (int x = 0; x < c.width; x++)
                {
                    int h = c.dataPriz[x + y * c.width];
                    imagee.SetPixel(x, y, Color.FromArgb(h));
                }
            e.Graphics.DrawImage(imagee, 0, 0);
        }



    }
}
