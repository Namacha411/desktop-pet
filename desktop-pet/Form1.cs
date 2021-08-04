using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desktop_pet
{
	public partial class Form1 : Form
	{
		private bool isDragging;
		private Point mousePointerOffset;

		public Form1()
		{
			// Form1 init
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.None;
			TopMost = true;
			ClientSize = new Size(255, 255);

			// event handler init
			MouseDown += new MouseEventHandler(Form1_LeftClickDown);
			MouseUp += new MouseEventHandler(Form1_LeftClickUp);
			MouseMove += new MouseEventHandler(Form1_MouseMove);

			// variable init
			isDragging = false;
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void Form1_LeftClickDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.isDragging = true;
				this.mousePointerOffset = new Point(e.X, e.Y);
			}
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.isDragging)
			{
				var nextPoint = PointToScreen(new Point(e.X, e.Y));
				nextPoint.Offset(-1 * mousePointerOffset.X, -1 * (mousePointerOffset.Y + SystemInformation.CaptionHeight) + 24);
				Location = nextPoint;
			}
		}

		private void Form1_LeftClickUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.isDragging = false;
			}
		}

	}
}
