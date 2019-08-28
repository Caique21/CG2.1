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
            int Length, I;
            double X, Y;
            int x,y;
            Boolean flag;

            Length = Math.Abs(x2 - x1);

            if (Math.Abs(y2 - y1) > Length)
            {
                Length = Math.Abs(y2 - y1);
                flag = false;
            }
            else
                flag = true;

            if(flag)
            {
                for(x = x1; x <= x2; x++)
                {
                    y = y1 + Length * (x - x1);
                    writePixel(x, y);
                }
            }
            else
                for (y = y1; y <= y2; y++)
                {
                    x = x1 + Length * (y - y1);
                    writePixel(x, y);
                }
        }

        private void writePixel(int x, int y)
        {
            Bitmap m = ((Bitmap)(pbGraficos.Image));
            m.SetPixel(x, y, Color.FromArgb(0, 0, 0));
            pbGraficos.Image = m;
        }

        private void PbGraficos_Click(object sender, EventArgs e)
        {
            if (cordenadas.X == 0 && cordenadas.Y == 0)
                cordenadas = ((MouseEventArgs)e).Location;
            else
            {
                if (rbDDA.Checked == true)
                {
                    DDA(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y);
                    cordenadas = new Point(0, 0);
                }
                if(rbGeralReta.Checked == true)
                {
                    eqReta(cordenadas.X, cordenadas.Y, ((MouseEventArgs)e).Location.X, ((MouseEventArgs)e).Location.Y);
                    cordenadas = new Point(0, 0);
                }
            }
        }

        private void RbDDA_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
