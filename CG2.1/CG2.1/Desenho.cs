using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG2._1
{
    class Desenho
    {
        private List<Point> pixels;
        private Color cor;

        public Desenho(List<Point> pixels, Color cor)
        {
            this.pixels = pixels;
            this.cor = cor;
        }

        public Desenho()
        {
            pixels = new List<Point>();

        }

        public Desenho(Color cor)
        {
            this.cor = cor;
        }

        public void add(Point p)
        {
            pixels.Add(p);
        }

        public void remove()
        {
            pixels.RemoveAt(pixels.Count - 1);
        }

        public List<Point> getPixels()
        {
            return pixels;
        }

        public void setPixel(List<Point>p)
        {
            pixels = p;
        }

        public void setColor(Color c)
        {
            cor = c;
        }

        public Color getColor()
        {
            return cor;
        }
    }
}
