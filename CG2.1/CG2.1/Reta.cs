using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG2._1
{
    class Reta : Desenho
    {
        private List<Point> pixels;
        private Color cor;

        public Reta(List<Point> pixels, Color cor)
        {
            this.pixels = pixels;
            this.cor = cor;
        }

        public Reta()
        {
            pixels = new List<Point>();
        }

        public Reta(Color cor)
        {
            this.cor = cor;
        }

        public override string ToString()
        {
            return "Reta";
        }


    }
}
