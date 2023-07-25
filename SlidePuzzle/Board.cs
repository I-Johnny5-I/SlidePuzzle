using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidePuzzle
{
    public class Board
    {
        public Dictionary<Point, Bitmap> Tiles { get; private set; }
        public Point[] Exceptions { get; private set; }
        public int count;

        public Board(Dictionary<Point, Bitmap> tiles, Point[] exceptions)
        {
            Tiles = tiles;
            Exceptions = exceptions;
            count = 0;
        }

        public void Move(Point clickPos)
        {
            if (Exceptions.Contains(clickPos)) return;
            int x = Exceptions[0].X;
            int y = Exceptions[0].Y;
            if (clickPos.X == x)
            {
                if (clickPos.Y > y) 
                { 
                    for(int i = y + 1; i < clickPos.Y + 1; i++)
                    {
                        if(Exceptions.Contains(new Point(x, i))) return;
                    }
                    for (int i = y + 1; i < clickPos.Y + 1; i++)
                    {
                        Tiles.Add(new Point(x, i - 1), Tiles[new Point(x, i)]);
                        Tiles.Remove(new Point(x, i));
                    }
                    Exceptions[0].X = clickPos.X;
                    Exceptions[0].Y = clickPos.Y;
                }
                if (clickPos.Y < y)
                {
                    for (int i = y - 1; i > clickPos.Y - 1; i--)
                    {
                        if (Exceptions.Contains(new Point(x, i))) return;
                    }
                    for (int i = y - 1; i > clickPos.Y - 1; i--)
                    {
                        Tiles.Add(new Point(x, i + 1), Tiles[new Point(x, i)]);
                        Tiles.Remove(new Point(x, i));
                    }
                    Exceptions[0].X = clickPos.X;
                    Exceptions[0].Y = clickPos.Y;
                }
            }
            if (clickPos.Y == y)
            {
                if (clickPos.X < x)
                {
                    for (int i = x - 1; i > clickPos.X - 1; i--)
                    {
                        if (Exceptions.Contains(new Point(i, y))) return;
                    }
                    for (int i = x - 1; i > clickPos.X - 1; i--)
                    {
                        Tiles.Add(new Point(i + 1, y), Tiles[new Point(i, y)]);
                        Tiles.Remove(new Point(i, y));
                    }
                    Exceptions[0].X = clickPos.X;
                    Exceptions[0].Y = clickPos.Y;
                }
                if (clickPos.X > x)
                {
                    for (int i = x + 1; i < clickPos.X + 1; i++)
                    {
                        if (Exceptions.Contains(new Point(i, y))) return;
                    }
                    for (int i = x + 1; i < clickPos.X + 1; i++)
                    {
                        Tiles.Add(new Point(i - 1, y), Tiles[new Point(i, y)]);
                        Tiles.Remove(new Point(i, y));
                    }
                    Exceptions[0].X = clickPos.X;
                    Exceptions[0].Y = clickPos.Y;
                }
            }
        }
    }
}
