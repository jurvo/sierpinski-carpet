using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SierpinskiCarpet
{
	struct PointS
	{
		public short X;
		public short Y;

		public PointS(short x, short y)
		{
			X = x;
			Y = y;
		}
	}

	struct SizeS
	{
		public short Width;
		public short Height;

		public SizeS(short width, short height)
		{
			Width = width;
			Height = height;
		}
	}

	struct SierpinskiCarpet
	{
		public SierpinskiCarpet[] underlying;
		public SizeS Size;
		public PointS TopLeft;

		public SierpinskiCarpet(PointS topLeft, short width, int layer, int maxLayer)
		{
			Size = new SizeS(width, width);
			TopLeft = topLeft;
			if (layer < maxLayer)
			{
				short w = (short)(width / 3);
				underlying = new SierpinskiCarpet[9];
				underlying[0] = new SierpinskiCarpet(topLeft, w, layer + 1, maxLayer);
				underlying[1] = new SierpinskiCarpet(new PointS((short)(topLeft.X + w), topLeft.Y), w, layer + 1, maxLayer);
				underlying[2] = new SierpinskiCarpet(new PointS((short)(topLeft.X + w + w), topLeft.Y), w, layer + 1, maxLayer);

				underlying[3] = new SierpinskiCarpet(new PointS(topLeft.X, (short)(topLeft.Y + w)), w, layer + 1, maxLayer);
				underlying[4] = new SierpinskiCarpet(new PointS((short)(topLeft.X + w), (short)(topLeft.Y + w)), w, layer + 1, maxLayer);
				underlying[5] = new SierpinskiCarpet(new PointS((short)(topLeft.X + w + w), (short)(topLeft.Y + w)), w, layer + 1, maxLayer);

				underlying[6] = new SierpinskiCarpet(new PointS(topLeft.X, (short)(topLeft.Y + w + w)), w, layer + 1, maxLayer);
				underlying[7] = new SierpinskiCarpet(new PointS((short)(topLeft.X + w), (short)(topLeft.Y + w + w)), w, layer + 1, maxLayer);
				underlying[8] = new SierpinskiCarpet(new PointS((short)(topLeft.X + w + w), (short)(topLeft.Y + w + w)), w, layer + 1, maxLayer);
			}
			else
				underlying = new SierpinskiCarpet[0];
		}
	}

	public partial class Form1 : Form
	{
		SierpinskiCarpet m;
		public Form1()
		{
			InitializeComponent();
			ClientSize = new Size(729, 729);

			m = new SierpinskiCarpet(new PointS(0, 0), (short)ClientSize.Width, 0, 7);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			DrawSponge(m, e.Graphics);
		}

		private void DrawSponge(SierpinskiCarpet m, Graphics g)
		{
			if (m.underlying.Length > 0)
			{
				var x = m.underlying[4];
				g.FillRectangle(Brushes.Black, new Rectangle(x.TopLeft.X, x.TopLeft.Y, x.Size.Width, x.Size.Height));
				for (int i = 0; i < m.underlying.Length; i++)
				{
					if (i != 4)
						DrawSponge(m.underlying[i], g);
				}
			}
		}
	}
}