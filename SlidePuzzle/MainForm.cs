using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlidePuzzle
{
    public partial class MainForm : Form
    {
        private Dictionary<string, Bitmap> Textures { get; set; }
        private string[] Paths { get; set; }
        private string CurrentDirectory { get; set; }
        private Bitmap Background { get; set; }
        private Board Board { get; set; }
        public MainForm(bool scramble)
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            Paths = Directory.GetFiles(CurrentDirectory + "\\textures");
            Textures = new Dictionary<string, Bitmap>();
            for (int i = 0; i < Paths.Length; i++)
            {
                Bitmap texture = new Bitmap(Paths[i]);
                Textures.Add(new FileInfo(Paths[i]).Name, new Bitmap(texture));
                texture.Dispose();
            }
            Background = new Bitmap(Textures["background.png"]);
            Board = new Board(new Dictionary<Point, Bitmap>
            {
                { new Point(1, 0), Textures["yellow_br.png"] },
                { new Point(2, 0), Textures["yellow_b.png"] },
                { new Point(3, 0), Textures["yellow_bl.png"] },
                { new Point(1, 1), Textures["yellow_r.png"] },
                { new Point(1, 2), Textures["yellow_r.png"] },
                { new Point(3, 1), Textures["yellow_l_blue_br.png"] },
                { new Point(3, 2), Textures["yellow_l_blue_r.png"] },
                { new Point(1, 3), Textures["yellow_tp_purple_b.png"] },
                { new Point(0, 3), Textures["purple_br.png"] },
                { new Point(0, 4), Textures["purple_r.png"] },
                { new Point(0, 5), Textures["purple_tr.png"] },
                { new Point(1, 5), Textures["purple_t.png"] },
                { new Point(2, 5), Textures["purple_t.png"] },
                { new Point(3, 5), Textures["purple_tl_red_r.png"] },
                { new Point(3, 4), Textures["purple_l_red_r.png"] },
                { new Point(2, 3), Textures["yellow_t_purple_b.png"] },
                { new Point(3, 6), Textures["red_tr.png"] },
                { new Point(5, 6), Textures["red_tl.png"] },
                { new Point(4, 6), Textures["red_t.png"] },
                { new Point(5, 4), Textures["red_l.png"] },
                { new Point(5, 5), Textures["red_l.png"] },
                { new Point(4, 3), Textures["blue_t_red_b.png"] },
                { new Point(5, 3), Textures["blue_t_red_bl.png"] },
                { new Point(4, 1), Textures["blue_b.png"] },
                { new Point(5, 1), Textures["blue_b.png"] },
                { new Point(6, 1), Textures["blue_bl.png"] },
                { new Point(6, 2), Textures["blue_l.png"] },
                { new Point(6, 3), Textures["blue_tl.png"] },
            },
            new Point[]
            {
                // Empty point
                new Point(3, 3),

                // Empty corners
                new Point(0,0),
                new Point(0,1),
                new Point(0,2),

                new Point(0,6),
                new Point(1,6),
                new Point(2,6),

                new Point(4,0),
                new Point(5,0),
                new Point(6,0),

                new Point(6,4),
                new Point(6,5),
                new Point(6,6),

                // Static long pieces
                new Point(2,1),
                new Point(2,2),
                new Point(4,2),
                new Point(5,2),
                new Point(4,4),
                new Point(4,5),
                new Point(2,4),
                new Point(1,4),
            }
            );
            Random random = new Random();
            if (scramble)
            {
                for (int i = 0; i < 10000000; i++)
                {
                    Board.Move(new Point(random.Next(0, 7), random.Next(0, 7)));
                }
            }
            InitializeComponent();
            Icon = Icon.FromHandle(Textures["icon.png"].GetHicon());
            timer1.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Bitmap frame = new Bitmap(Background);
            Graphics g = Graphics.FromImage(frame);
            foreach(var tile in Board.Tiles)
            {
                g.DrawImage(tile.Value, 27 + tile.Key.X * 136, 27 + tile.Key.Y * 136, 130, 130);           
            }
            g.Dispose();
            pictureBox1.Image = frame;
            pictureBox1.Refresh();
            frame.Dispose();
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            int x = (int)Math.Floor((decimal)(e.X - 27) / 136);
            int y = (int)Math.Floor((decimal)(e.Y - 27) / 136);
            x = x < 0 ? 0 : ((x > 6) ? 6 : x);
            y = y < 0 ? 0 : ((y > 6) ? 6 : y);
            Board.Move(new Point(x, y));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
