using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlidePuzzle
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Question question = new Question();
            DialogResult result = question.ShowDialog();
            bool scramble = false;
            if (result == DialogResult.Yes) scramble = true;
            Application.Run(new MainForm(scramble));
        }
    }
}
