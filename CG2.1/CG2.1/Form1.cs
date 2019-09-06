using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG2._1
{
    public partial class Form1 : Form
    {
        private Point cordenadas;
        public Form1()
        {
            InitializeComponent();
            rbGeralReta.Checked = true;
            cordenadas = new Point(0, 0);
        }

        public void DDA(int x1, int y1, int x2, int y2)
        {
            int Length, I;
            double X, Y, Xinc, Yinc;

            Length = Math.Abs(x2 - x1);

            if (Math.Abs(y2 - y1) > Length)
                Length = Math.Abs(y2 - y1);
            Xinc = (double)(x2 - x1) / Length;
            Yinc = (double)(y2 - y1) / Length;

            X = x1; Y = y1;
            if(X< x2)
            {
                while (X < x2)
                {
                    writePixel((int)Math.Round(X), (int)Math.Round(Y));
                    X = X + Xinc;
                    Y = Y + Yinc;
                }
            }
            else
            {
                while (X > x2)
                {
                    writePixel((int)Math.Round(X), (int)Math.Round(Y));
                    X = X + Xinc;
                    Y = Y + Yinc;
                }
            }
        }

        public void eqReta(int x1, int y1, int x2, int y2)
        {
            int aux;
            Boolean isX;
            double dy = y2 - y1;
            double dx = x2 - x1;
            double m = dy / dx;

            if (dx < dy)
                isX = false;
            else
                isX = true;

            int inicio, fim;

            if(isX)
            {
                if(x1 > x2)
                {
                    inicio = x2; fim = x1;
                }
                else
                {
                    inicio = x1; fim = x2;
                }

                double y;
                for (int x = inicio; x <= fim; x++)
                {
                    
                    y = y1 + m * (x - x1);
                    writePixel(x, Convert.ToInt32(Math.Round(y)));
                }
            }
            else
            {
                if (y1 > y2)
                {
                    inicio = y2; fim = y1;
                }
                else
                {
                    inicio = y1; fim = y2;
                }

                double x;
                for (int y = inicio; y <= fim; y++)
                {
                    x = x1 + ((y - y1) / m);
                    writePixel((int)Math.Round(x), y);
                }
            }       
        }

        void bresenham(int x1, int y1, int x2, int y2)
        {
            int declive = 1;
            int dx, dy, incE, incNE, d, x, y;
            dx = x2 - x1;
            dy = y2 - y1;

            // Constante de Bresenham 
            incE = 2 * dy;
            incNE = 2 * dy - 2 * dx;
            d = 2 * dy - dx;
            y = y1;
            for (x = x1; x <= x2; x++)
            {
                writePixel(x, y);
                if (d <= 0)
                {
                    d += incE;
                }
                else
                {
                    d += incNE;
                    y += declive;
                }
            }
        }


        private void writePixel(int x, int y)
        {
            Bitmap m = ((Bitmap)(pbGraficos.Image));
            if (y < 0)
                Console.WriteLine("erro");
            m.SetPixel(x, y, Color.FromArgb(0, 0, 0));
            pbGraficos.Image = m;
        }

        private void PbGraficos_Click(object sender, EventArgs e)
        {
            if (cordenadas.X == 0 && cordenadas.Y == 0)
                cordenadas = ((MouseEventArgs)e).Location;
            else 
            {
                int x1, x2, y1, y2,aux;
                x1 = cordenadas.X;
                y1 = cordenadas.Y;
                x2 = ((MouseEventArgs)e).Location.X;
                y2 = ((MouseEventArgs)e).Location.Y;

                

                if (rbDDA.Checked == true)
                {
                    DDA(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y);
                    cordenadas = new Point(0, 0);
                }
                if(rbGeralReta.Checked == true)
                {
                    eqReta(x1, y1, x2, y2);
                    cordenadas = new Point(0, 0);
                }
                if(rbPMReta.Checked == true)
                {
                    bresenham(x1, y1, x2, y2);
                }
            }
        }

        private void RbDDA_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
