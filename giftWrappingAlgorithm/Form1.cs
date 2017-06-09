using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace giftWrappingAlgorithm
{
    public partial class Form1 : Form
    {
        private  Pen linijePen;
        private  Pen tackePen;
        private  Color pozadina;
        private Graphics graphics;

        private readonly LinkedList<Point> tacke;

        private bool isDrawn;

        public Form1()
        {
            linijePen = new Pen(Color.Red);
            tackePen = new Pen(Color.Black);
            pozadina = Color.LightYellow;

            tacke = new LinkedList<Point>();
            isDrawn = false;

            InitializeComponent();
        }

        private void OcistiPictureBox()
        {
            using (var brush = new SolidBrush(pozadina))
                graphics.FillRectangle
                    (brush, 0, 0, pictureBox.Width, pictureBox.Height);
            pictureBox.Invalidate();
        }

        private void NacrtajTacke()
        {
            OcistiPictureBox();
            foreach (var p in tacke)
                graphics.DrawRectangle(tackePen, p.X, p.Y, 1, 1);
            pictureBox.Invalidate();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox.BackColor = Color.LightYellow;
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);

            graphics = Graphics.FromImage(pictureBox.Image);
        }

       

        private void obrisiButton_Click(object sender, EventArgs e)
        {
            OcistiPictureBox();
            tacke.Clear();
            isDrawn = false;
        }

        private void nacrtajButton_Click(object sender, EventArgs e)
        {
            if (tacke.Count < 3)
                return;

            if (isDrawn)
                NacrtajTacke();
            else isDrawn = true;

            var hull = giftWrappingAlgorithm.CrtanjeTacaka.Calculate(tacke);

            var node = hull.First;
            while (node.Next != null)
            {
                graphics.DrawLine(linijePen, node.Value, node.Next.Value);
                node = node.Next;
            }
            graphics.DrawLine(linijePen, node.Value, hull.First.Value);
            pictureBox.Invalidate();
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var tacka = new Point(e.X, e.Y);
            if (tacke.Contains(tacka))
                return;

            tacke.AddLast(tacka);

            graphics.DrawRectangle(tackePen, tacka.X, tacka.Y, 1, 1);
            pictureBox.Invalidate();
        }
    }
}
