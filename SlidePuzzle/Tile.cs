using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlidePuzzle
{
    public class Tile
    {  
        public int X { get; private set; }
        public int Y { get; private set; }
        public Bitmap Texture { get; private set; }

        public Tile(Bitmap texture, int x, int y)
        {
            Texture = texture;
            X = x;
            Y = y;
        }
    }
}
