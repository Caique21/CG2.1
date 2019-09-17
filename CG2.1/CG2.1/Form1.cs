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
        private Boolean click = false;
        private Color cor;
        private Bitmap map;
        

        public Form1()
        {
            InitializeComponent();
            rbGeralReta.Checked = true;
            cordenadas = new Point(0, 0);
            desenho = new List<Desenho>();
            map = ((Bitmap)(pbGraficos.Image));
            pbGraficos.Enabled = true;
        }


        public void PrintaPixel(int x, int y, BitmapData data)
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

        private static int GetOctante(int x0, int y0, int x1, int y1)
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
                Desenho reta = new Reta();

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
                        if(!apagavel)
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
                        if (!apagavel)
                            reta.add(new Point((int)Math.Round(X), (int)Math.Round(Y)));
                        X = X + Xinc;
                        Y = Y + Yinc;
                    }
                }
                if(reta.getPixels().Count > 0)
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

                Desenho reta = new Reta(cor);

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
                    }
                }
                if(reta.getPixels().Count > 0)
                    desenho.Add(reta);
            }
                //unlock imagem origem
            map.UnlockBits(data);
            return map;
        }

        Bitmap bresenham(int x1, int y1, int x2, int y2,Boolean apagavel,BitmapData data)
        {
            unsafe
            {
                int declive;
                int dx, dy, incE, incNE, d, x, y;
                dx = x2 - x1;
                dy = y2 - y1;

                if (dx < 0)
                    return bresenham(x2, y2, x1, y1, apagavel, data);

                if (dy < 0)
                    declive = -1;
                else
                    declive = 1;

                Desenho reta = new Reta(cor);
                x = x1;
                y = y1;
                PrintaPixel(x, y, data);
                if (!apagavel)
                    reta.add(new Point(x, y));
                if(dx > declive * dy)
                {
                    if (dy < 0)
                    {
                        d = 2 * dy + dx;
                        while (x < x2)
                        {
                            if (d < 0)
                            {
                                d += 2 * (dy + dx);
                                x++;
                                y--;
                            }
                            else
                            {
                                d += 2 * dy;
                                x++;
                            }
                            PrintaPixel(x, y, data);
                            if (!apagavel)
                                reta.add(new Point(x, y));
                        }
                    }
                    else
                    { 
                        d = 2 * dy - dx;
                        while (x < x2)
                        {
                            if (d < 0)
                            { // escolhido é o I
                                d += 2 * dy;
                                x++; // varia apenas no eixo x
                            }
                            else
                            { // escolhido é o S
                                d += 2 * (dy - dx);
                                x++;
                                y++;
                            }
                            PrintaPixel(x, y, data);
                            if (!apagavel)
                                reta.add(new Point(x, y));
                        }
                    }
                }
                else
                {
                    if (dy < 0)
                    { // caso y2<y1
                        d = dy + 2 * dx;
                        while (y > y2)
                        {
                            if (d < 0)
                            {
                                d += 2 * dx;
                                y--; // varia apenas no eixo y
                            }
                            else
                            {
                                d += 2 * (dy + dx);
                                x++;
                                y--;
                            }
                            PrintaPixel(x, y, data);
                            if (!apagavel)
                                reta.add(new Point(x, y));
                        }
                    }
                    else
                    { // caso y1<y2
                        d = dy - 2 * dx;
                        while (y < y2)
                        {
                            if (d < 0)
                            {
                                d += 2 * (dy - dx);
                                x++;
                                y++;
                            }
                            else
                            {
                                d += -2 * dx;
                                y++; // varia apenas no eixo y
                            }
                            PrintaPixel(x, y, data);
                            if (!apagavel)
                                reta.add(new Point(x, y));
                        }
                    }
                }
                PrintaPixel(x, y, data);
                if (!apagavel)
                    reta.add(new Point(x, y));
            }
            map.UnlockBits(data);
            return map;
            /*
            // Constante de Bresenham 
            incE = 2 * dy;
            incNE = 2 * dy - 2 * dx;
            d = 2 * dy - dx;
            y = y1;
            for (x = x1; x <= x2; x++)
            {
                PrintaPixel(x, y, data);
                if (d <= 0)
                {
                    d += incE;
                }
                else
                {
                    d += incNE;
                    y += declive;
                }
            }*/
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
                    int width = pbGraficos.Width;
                    int height = pbGraficos.Height;
                    BitmapData data = map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                    pbGraficos.Image = bresenham(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y, false, data);
                            
                }

                if(rbPMCirc.Checked == true)
                {
                    int raio = (int)Math.Sqrt(Math.Pow((((MouseEventArgs)e).Location.X - cordenadas.X), 2) + Math.Pow((((MouseEventArgs)e).Location.Y - cordenadas.Y), 2));
                    pontomedio(raio, 1, cordenadas.X, cordenadas.Y,false);
                }

                if(rbGeralCirc.Checked == true)
                {
                    int raio = (int)Math.Sqrt(Math.Pow((((MouseEventArgs)e).Location.X - cordenadas.X), 2) + Math.Pow((((MouseEventArgs)e).Location.Y - cordenadas.Y), 2));
                    eqCirc(raio, cordenadas.X, cordenadas.Y);
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
                        int width = pbGraficos.Width;
                        int height = pbGraficos.Height;
                        BitmapData data = map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                        pbGraficos.Image = bresenham(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y, true, data);

                    }
                }
                if (rbPMCirc.Checked == true)
                {
                    int raio = (int)Math.Sqrt(Math.Pow((((MouseEventArgs)e).Location.X - cordenadas.X), 2) + Math.Pow((((MouseEventArgs)e).Location.Y - cordenadas.Y), 2));
                    //pontomedio(raio, 1, cordenadas.X, cordenadas.Y, true);
                }
            }
        }

      
        public void limpa_tela()
        {
            int width = pbGraficos.Width;
            int height = pbGraficos.Height;
            //lock dados bitmap origem
            BitmapData data = map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            List<Point> aux = new List<Point>();

            for (int i = 0; i < desenho.Count; i++)
                aux.AddRange(desenho[i].getPixels());

            unsafe
            {
                for(int x = 0; x < width && aux.Count > 0; x++)
                {
                    for(int y = 0; y < height; y++)
                    {
                        byte* point = (byte*)data.Scan0.ToPointer();

                        point += y * data.Stride + (x * 3);
                        if (!aux.Contains(new Point(x, y)))
                        {
                            *(point++) = (byte)255;
                            *(point++) = (byte)255;
                            *(point++) = (byte)255;
                        }
                    }
                }
                /*if(rascunho.getPixels().Count > 0)
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
                }*/
            }
            map.UnlockBits(data);
        }

        void eqCirc(int raio, int cx, int cy)
        {
            double x = cx;
            double y = cy + raio;
            Desenho circulo = new Circunferencia(cor);

            PintaPixel((int)x, (int)y, 0, false, circulo);
            while (x < cx + raio)
            {
                y = y - 1;

                int a, b, c;
                a = 1;
                b = -2 * cx;
                c = cx * cx + (int)Math.Pow(y, 2) + 2 * ((int)y * cy) + (int)Math.Pow(cy, 2);

                int delta = b * b - (4 * a * c);
                int baskhara1 = -b + (int)Math.Sqrt(delta) / 2 * a;
                int baskhara2 = -b - (int)Math.Sqrt(delta) / 2 * a;

                if (Math.Pow(baskhara1, 2) + (b * baskhara1) == Math.Pow(raio, 2) + c)
                {
                    PintaPixel((int)baskhara1, (int)y, 0, false, circulo);
                    x = baskhara1;
                }

                if (Math.Pow(baskhara2, 2) + (b * baskhara2) == Math.Pow(raio, 2) + c)
                {
                    PintaPixel((int)baskhara2, (int)y, 0, false, circulo);
                    x = baskhara2;
                }
            }
        }

        void pontomedio(int raio,int valor, int cx, int cy, Boolean apagavel)
        {
            int x = 0;
            int y = raio;
            double d = 1 - raio;
            Desenho circulo = new Circunferencia(cor);

            PontosCircunferência(x, y, cx, cy, valor,apagavel,circulo);
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
                PontosCircunferência(x, y, cx, cy, valor,apagavel,circulo);
            }
            desenho.Add(circulo);
        }

        void PontosCircunferência(int x, int y,int cx, int cy, int valor,Boolean apagavel,Desenho circ)
        {
            PintaPixel(cx + x, cy + y, valor, apagavel,circ);
            PintaPixel(cx + y, cy + x, valor, apagavel, circ);
            PintaPixel(cx + y, cy - x, valor, apagavel, circ);
            PintaPixel(cx + x, cy - y, valor, apagavel, circ);
            PintaPixel(cx - x, cy - y, valor, apagavel, circ);
            PintaPixel(cx - y, cy - x, valor, apagavel, circ);
            PintaPixel(cx - y, cy + x, valor, apagavel, circ);
            PintaPixel(cx - x, cy + y, valor, apagavel, circ);
        }

        private void PintaPixel(int x, int y, int valor,Boolean apagavel,Desenho c)
        {
            if(x >=0 && x < pbGraficos.Image.Width && y >= 0 && y < pbGraficos.Image.Height)
            {
                if (!apagavel)
                    c.add(new Point(x, y));
                Bitmap m = ((Bitmap)(pbGraficos.Image));
                m.SetPixel(x, y, cor);
                pbGraficos.Image = m;
            }
        }

        private void BtnCor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            cor = colorDialog1.Color;
        }

    }
}
