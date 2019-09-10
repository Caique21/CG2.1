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
        private Boolean click = false;
        private List<Point> list = new List<Point>();

        public Form1()
        {
            InitializeComponent();
            rbGeralReta.Checked = true;
            cordenadas = new Point(0, 0);
        }

        public void DDA(int x1, int y1, int x2, int y2,Boolean apagavel)
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
                    writePixel((int)Math.Round(X), (int)Math.Round(Y),apagavel);
                    X = X + Xinc;
                    Y = Y + Yinc;
                }
            }
            else
            {
                while (X > x2)
                {
                    writePixel((int)Math.Round(X), (int)Math.Round(Y),apagavel);
                    X = X + Xinc;
                    Y = Y + Yinc;
                }
            }
        }

        public void eqReta(int x1, int y1, int x2, int y2,Boolean apagavel)
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
                    writePixel(x, Convert.ToInt32(Math.Round(y)),apagavel);
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
                    writePixel((int)Math.Round(x), y,apagavel);
                }
            }       
        }

        void bresenham(int x1, int y1, int x2, int y2,Boolean apagavel)
        {
            int declive = 1;
            int dx, dy, incE, incNE, d, x, y;
            int xfim;
            dx = x2 - x1;
            dy = y2 - y1;

            // Constante de Bresenham 
            incE = 2 * dy;
            incNE = 2 * (dy - dx);
            d = 2 * dy - dx;
            if(x2 > x1)
            {
                int aux = incE;
                incE = incNE;
                incNE = aux;
                x = x1; xfim = x2; y = y2;
            }
            else
            {
                x = x2; xfim = x1; y = y1;
            }

            if(y2 >= y1)
            {
                for (; x <= xfim; x++)
                {
                    writePixel(x, y, apagavel);
                    if (d <= 0)
                    {
                        d += incE;
                    }
                    else
                    {
                        d += incNE;
                        y++;
                    }
                }
            }
            else
            {
                y1 = -y1;
                y2 = -y2;
                for (; x <= xfim; x++)
                {
                    writePixel(x, -y, apagavel);
                    if (d <= 0)
                    {
                        d += incE;
                    }
                    else
                    {
                        d += incNE;
                        y++;
                    }
                }
            }
        }


        private void writePixel(int x, int y,Boolean apagavel)
        {
            if(apagavel)
                list.Add(new Point(x, y));
            Bitmap m = ((Bitmap)(pbGraficos.Image));
            m.SetPixel(x, y, Color.FromArgb(0, 0, 0));
            pbGraficos.Image = m;
        }

        private void PbGraficos_Click(object sender, EventArgs e)
        {
            if (!click)
            {
                cordenadas = ((MouseEventArgs)e).Location;
                click = true;
            }
            else
            {
                if (rbDDA.Checked == true)
                {
                    DDA(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,false);
                }
                if (rbGeralReta.Checked == true)
                {
                    eqReta(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,false);
                }
                if (rbPMReta.Checked == true)
                {

                    bresenham(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,false);
                    
                }
                click = false;
                list = new List<Point>();
            }
        }

        private void RbDDA_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PbGraficos_MouseMove(object sender, MouseEventArgs e)
        {
            limpa_reta();
            if(click)
            {
                int x = ((MouseEventArgs)e).Location.X;
                int y = ((MouseEventArgs)e).Location.Y;
                if (rbDDA.Checked == true)
                {
                    DDA(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,true);
                    
                }
                if (rbGeralReta.Checked == true)
                {
                    if (((MouseEventArgs)e).Location.X != cordenadas.X && ((MouseEventArgs)e).Location.Y != cordenadas.Y)
                        eqReta(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,true);
                }
                if (rbPMReta.Checked == true)
                {
                    if (((MouseEventArgs)e).Location.X != cordenadas.X && ((MouseEventArgs)e).Location.Y != cordenadas.Y)
                    {
                        bresenham(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y, true);
                    }
                        
                        
                }
            }
        }

        private void limpa_reta()
        {
            if(list.Count > 0)
            {
                for(int i = 0; i < list.Count; i++)
                {
                    Bitmap m = ((Bitmap)(pbGraficos.Image));
                    m.SetPixel(list[i].X, list[i].Y, Color.FromArgb(255, 255, 255));
                    pbGraficos.Image = m;
                }
            }
        }
    }
}
