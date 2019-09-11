using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        private Color cor;

        public Form1()
        {
            InitializeComponent();
            rbGeralReta.Checked = true;
            cordenadas = new Point(0, 0);
            cor = Color.FromArgb(0, 0, 0);
        }


        private static void PrintaPixel(int x, int y, Color color, BitmapData data)
        {
            unsafe
            {
                byte* point = (byte*)data.Scan0.ToPointer();

                point += y * data.Stride + (x * 3);

                *(point++) = (byte)color.B;
                *(point++) = (byte)color.G;
                *(point++) = (byte)color.R;

            }
        }

        private static int GetOctante(int x0, int x1, int y0, int y1)
        {
            int octant = 0;

            if (x1 > x0 && y0 > y1)//1º quadrante
            {
                if ((x1 - x0) > (y1 - y0))//8º octante
                    octant = 8;
                else//7º octante
                    octant = 7;
            }
            else if (x0 > x1 && y0 > y1)//2º quadrante
            {
                if ((x1 - x0) < (y1 - y0))//6º octante
                    octant = 6;
                else//5º octante
                    octant = 5;
            }
            else if (x0 > x1 && y1 > y0)//3º quadrante
            {
                if ((x1 - x0) > (y1 - y0))//4º octante
                    octant = 4;
                else//3º octante
                    octant = 3;
            }
            else//4º quadrante
            {
                if ((x1 - x0) < (y1 - y0))//2º octante
                    octant = 2;
                else//1º octante
                    octant = 1;
            }

            return octant;
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

        public Bitmap eqReta(int x1, int y1, int x2, int y2,Boolean apagavel,Color cor,Bitmap map)
        {

            int width = pbGraficos.Width;
            int height = pbGraficos.Height;

            //lock dados bitmap origem
            BitmapData data = map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                Boolean isX;
                double dy = y2 - y1;
                double dx = x2 - x1;
                double m = dx != 0 && dy != 0 ? dy / dx : 0;
                int y;
                int x;

                if (dx < dy)
                    isX = false;
                else
                    isX = true;

                int inicio, fim;

                if (isX)
                {
                    if (x1 > x2)
                    {
                        inicio = x2; fim = x1;
                    }
                    else
                    {
                        inicio = x1; fim = x2;
                    }


                    for (x = inicio; x <= fim; x++)
                    {

                        y = Convert.ToInt32(y1 + m * (x - x1));
                        PrintaPixel(x,y,cor, data);
                        //writePixel(x, y, apagavel);
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

                    for (y = inicio; y <= fim; y++)
                    {
                        x = Convert.ToInt32(x1 + ((y - y1) / m));
                        PrintaPixel(x, y, cor, data);
                        // writePixel(x, y, apagavel);
                    }
                }
            }
                //unlock imagem origem
                map.UnlockBits(data);
            return map;
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
                    Bitmap map = ((Bitmap)(pbGraficos.Image));
                    pbGraficos.Image = eqReta(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,false,cor,map);
                }
                if (rbPMReta.Checked == true)
                {
                    bresenham(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,false);
                }
                if(rbPMCirc.Checked == true)
                {
                    //eqReta(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y, false);
                    int raio = (int)Math.Sqrt((((MouseEventArgs)e).Location.X + cordenadas.X) + (((MouseEventArgs)e).Location.Y + cordenadas.Y));
                    pontomedio(raio, 1, cordenadas.X, cordenadas.Y,false);
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
                    Bitmap map = ((Bitmap)(pbGraficos.Image));
                    if (((MouseEventArgs)e).Location.X != cordenadas.X && ((MouseEventArgs)e).Location.Y != cordenadas.Y)
                        pbGraficos.Image = eqReta(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,true,cor,map);
                }
                if (rbPMReta.Checked == true)
                {
                    if (((MouseEventArgs)e).Location.X != cordenadas.X && ((MouseEventArgs)e).Location.Y != cordenadas.Y)
                    {
                        bresenham(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y, true);
                    }   
                }
                if (rbGeralCirc.Checked == true)
                {
                    //int raio = (int)Math.Sqrt(cordenadas.X + cordenadas.Y);
                    //pontomedio(raio, 1, cordenadas.X, cordenadas.Y,true);
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

        void pontomedio(int raio,int valor, int cx, int cy, Boolean apagavel)
        {
            int x = 0;
            int y = raio;
            double d = 1 - raio;

            PontosCircunferência(x, y, cx, cy, valor,apagavel);
            while(y > x)
            {
                if (d < 0)
                    d += 2 * x + 3;
                else
                {
                    d += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                PontosCircunferência(x, y, cx, cy, valor,apagavel);
            }
        }

        void PontosCircunferência(int x, int y,int cx, int cy, int valor,Boolean apagavel)
        {
            PintaPixel(cx + x, cy + y, valor, apagavel);
            PintaPixel(cx + y, cy + x, valor, apagavel);
            PintaPixel(cx + y, cy - x, valor, apagavel);
            PintaPixel(cx + x, cy - y, valor, apagavel);
            PintaPixel(cx - x, cy - y, valor, apagavel);
            PintaPixel(cx - y, cy - x, valor, apagavel);
            PintaPixel(cx - y, cy + x, valor, apagavel);
            PintaPixel(cx - x, cy + y, valor, apagavel);
        }

        private void PintaPixel(int x, int y, int valor,Boolean apagavel)
        {
            if (apagavel)
                list.Add(new Point(x, y));
            Bitmap m = ((Bitmap)(pbGraficos.Image));
            m.SetPixel(x, y, Color.FromArgb(0, 0, 0));
            pbGraficos.Image = m;
        }
    }
}
