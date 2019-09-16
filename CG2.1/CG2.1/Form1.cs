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
        private List<Desenho> desenho;
        private Desenho rascunho;
        private Boolean click = false;
        private Color cor;
        private Bitmap map;

        public Form1()
        {
            InitializeComponent();
            rbGeralReta.Checked = true;
             map = ((Bitmap)(pbGraficos.Image));
            cordenadas = new Point(0, 0);
            desenho = new List<Desenho>();
            rascunho = new Desenho();
        }


        private void PrintaPixel(int x, int y, BitmapData data)
        {
            unsafe
            {
                byte* point = (byte*)data.Scan0.ToPointer();

                point += y * data.Stride + (x * 3);

                *(point++) = (byte)cor.B;
                *(point++) = (byte)cor.G;
                *(point++) = (byte)cor.R;

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

        public Bitmap DDA(int x1, int y1, int x2, int y2,Boolean apagavel, Bitmap map)
        {
            int width = pbGraficos.Width;
            int height = pbGraficos.Height;

            //lock dados bitmap origem
            BitmapData data = map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                int Length, I;
                double X, Y, Xinc, Yinc;
                Desenho reta = new Desenho();

                Length = Math.Abs(x2 - x1);

                if (Math.Abs(y2 - y1) > Length)
                    Length = Math.Abs(y2 - y1);
                Xinc = (double)(x2 - x1) / Length;
                Yinc = (double)(y2 - y1) / Length;

                X = x1; Y = y1;
                if (X < x2)
                {
                    while (X < x2)
                    {
                        PrintaPixel((int)Math.Round(X), (int)Math.Round(Y), data);
                        reta.add(new Point((int)Math.Round(X), (int)Math.Round(Y)));
                        X = X + Xinc;
                        Y = Y + Yinc;
                    }
                }
                else
                {
                    while (X > x2)
                    {
                        PrintaPixel((int)Math.Round(X), (int)Math.Round(Y), data);
                        reta.add(new Point((int)Math.Round(X), (int)Math.Round(Y)));
                        X = X + Xinc;
                        Y = Y + Yinc;
                    }
                }

                desenho.Add(reta);
            }
            map.UnlockBits(data);
            return map;
        }

        public Bitmap eqReta(int x1, int y1, int x2, int y2,Boolean apagavel,Bitmap map)
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

                Desenho reta = new Desenho();

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
                        PrintaPixel(x,y, data);
                        if (!apagavel)
                            reta.add(new Point(x, y));
                        else
                            rascunho.add(new Point(x, y));

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
                        PrintaPixel(x, y, data);
                        if (!apagavel)
                            reta.add(new Point(x, y));
                        else
                            rascunho.add(new Point(x, y));
                    }
                }

                desenho.Add(reta);
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
                    //writePixel(x, y, apagavel);
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
                    //writePixel(x, -y, apagavel);
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
                    pbGraficos.Image = DDA(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,false,map);
                }
                if (rbGeralReta.Checked == true)
                {
                    
                    pbGraficos.Image = eqReta(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,false,map);
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
            }
        }

        private void RbDDA_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PbGraficos_MouseMove(object sender, MouseEventArgs e)
        {
           
            if(click)
            {
                limpa_tela();
                int x = ((MouseEventArgs)e).Location.X;
                int y = ((MouseEventArgs)e).Location.Y;
                if (rbDDA.Checked == true)
                {
                    pbGraficos.Image = DDA(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,true, map);
                    
                }
                if (rbGeralReta.Checked == true)
                {
                    if (((MouseEventArgs)e).Location.X != cordenadas.X && ((MouseEventArgs)e).Location.Y != cordenadas.Y)
                        pbGraficos.Image = eqReta(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y,true,map);
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

      
        public void limpa_tela()
        {
            int width = pbGraficos.Width;
            int height = pbGraficos.Height;
            //lock dados bitmap origem
            BitmapData data = map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                if(rascunho.getPixels().Count > 0)
                {
                    List<Point> aux = rascunho.getPixels();
                    for (int i = 0; i < aux.Count; i++)
                    {
                        byte* point = (byte*)data.Scan0.ToPointer();

                        point += aux[i].Y * data.Stride + (aux[i].X * 3);

                        *(point++) = (byte)255;
                        *(point++) = (byte)255;
                        *(point++) = (byte)255;
                    }
                    //desenho.Remove();
                }
            }
            map.UnlockBits(data);
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
            Bitmap m = ((Bitmap)(pbGraficos.Image));
            m.SetPixel(x, y, Color.FromArgb(0, 0, 0));
            pbGraficos.Image = m;
        }

        private void BtnCor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            cor = colorDialog1.Color;
        }

    }
}
