using System;
using System.Drawing;
using System.Windows.Forms;

namespace desktop_pet
{
	public partial class Form1 : Form
	{
		private bool isDragging;
		private Point mousePointerOffset;

		private Bitmap MainImage;
		private Bitmap DraggingImage;

		public Form1()
		{
			// Form1 init
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.None;
			TopMost = true;
			ClientSize = new Size(255, 255);
			TransparencyKey = BackColor;
			ShowInTaskbar = false;

			// event handler init
			MouseDown += new MouseEventHandler(Form1_LeftClickDown);
			MouseDown += new MouseEventHandler(Form1_RightClickDown);
			MouseUp += new MouseEventHandler(Form1_LeftClickUp);
			MouseMove += new MouseEventHandler(Form1_MouseMove);

			// variable init
			isDragging = false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender, EventArgs e)
		{
			// init images
			MainImage = Properties.Resources.sleep_cat;
			DraggingImage = Properties.Resources.kowai_cat;
			SetImage(MainImage);

			// init sounds
			// todo
		}

		/// <summary>
		/// set back ground image.
		/// image transparent and zoom.
		/// </summary>
		/// <param name="img"></param>
		private void SetImage(Bitmap img)
		{
			img.MakeTransparent();
			BackgroundImageLayout = ImageLayout.Zoom;
			BackgroundImage = img;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EventTimer_Tick(object sender, EventArgs e)
		{
			if (SoundPlayer.IsTime(min: 0))
			{
				// SoundPlayer.PlayTimeSignal();
			}
		}

		/// <summary>
		/// application exit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		//////////////////
		// Mouse Events //
		//////////////////

		/// <summary>
		/// Calls when the left mouse button is down.
		/// isDragging flag makes true.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_LeftClickDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.isDragging = true;
				this.mousePointerOffset = new Point(e.X, e.Y);
				SetImage(DraggingImage);
			}
		}

		/// <summary>
		/// Calls when the left mouse button is down.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_RightClickDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				RightClickMenu.Show(this, new Point(e.X, e.Y));
			}
		}

		/// <summary>
		/// Calls when the mouse is dragging.
		/// Form1 is following mouse.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.isDragging)
			{
				var nextPoint = PointToScreen(new Point(e.X, e.Y));
				nextPoint.Offset(-1 * mousePointerOffset.X, -1 * (mousePointerOffset.Y + SystemInformation.CaptionHeight) + 24);
				Location = nextPoint;
			}
		}

		/// <summary>
		/// Calls when the left mouse button is up.
		/// isDragging flag makes false.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_LeftClickUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.isDragging = false;
				SetImage(MainImage);
			}
		}

	}
}
