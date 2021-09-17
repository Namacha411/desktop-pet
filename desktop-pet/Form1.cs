using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace desktop_pet
{
	public partial class Form1 : Form
	{
		private readonly Preference Preference;

		private readonly Bitmap MainImage;
		private readonly Bitmap DraggingImage;

		private readonly List<Bitmap> MainImages;
		private readonly List<Bitmap> DraggingImages;

		private bool isDragging;
		private Point mousePointerOffset;

		public Form1()
		{
			// read preference file
			if (!File.Exists(PreferenceIO.PreferencePath))
			{
				PreferenceIO.FileWrite();
			}
			var json = PreferenceIO.FileRead();
			this.Preference = Preference.Deserialize(json);

			// init Form
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.None;
			TopMost = true;
			ClientSize = new Size(255, 255);
			TransparencyKey = BackColor;
			ShowInTaskbar = false;
			EventTimer.Start();

			// init notify icon
			var iconMenu = new ContextMenuStrip();
			iconMenu.Items.AddRange(new ToolStripItem[]{
				new ToolStripMenuItem(
					text: "Exit",
					image: null,
					onClick: (s, e) => { ApplicationExit(); },
					name: "Exit"
					),
				new ToolStripMenuItem(
					text: "Reset position",
					image: null,
					onClick: (s, e) => { Location = new Point(0, 0); },
					name: "Reset position"
					),
				new ToolStripMenuItem(
					text: "Preference",
					image: null,
					onClick: (s, e) => { OpenPreferenceInExplorer(); },
					name: "Preference"
					),
				new ToolStripMenuItem(
					text: "About",
					image: null,
					onClick: (s, e) => { MessageBox.Show(GetVersion()); },
					name: "About"
					),
				new ToolStripMenuItem(
					text: "Source code",
					image: null,
					onClick: (s, e) => { OpenRepoPage(); },
					name: "Source code"
					)
			});

			notifyIcon.Icon = new Icon(Preference.IconPath);
			notifyIcon.Text = "Destop pet";
			notifyIcon.Visible = true;
			notifyIcon.ContextMenuStrip = iconMenu;

			// init images
			MainImage = Properties.Resources.sleep_cat;
			DraggingImage = Properties.Resources.kowai_cat;
			SetImage(MainImage);

			MainImages =
				Preference.MainImagesPaths
				.Where((path) => File.Exists(path))
				.Select((path) => new Bitmap(path))
				.ToList();
			DraggingImages =
				Preference.DraggingImagesPaths
				.Where((path) => File.Exists(path))
				.Select((path) => new Bitmap(path))
				.ToList();

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
			SoundPlayer.PlayStartSound(Preference.StartSoundPath);
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
				SoundPlayer.PlayTimeSignal(Preference.TimeSignelPaths);
			}
		}

		/// <summary>
		/// application exit.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ApplicationExit();
		}

		/// <summary>
		/// application exit.
		/// </summary>
		private void ApplicationExit()
		{
			SoundPlayer.PlayExitSound(Preference.ExitSoundPath);
			Application.Exit();
		}

		/// <summary>
		/// open preference file
		/// </summary>
		private void OpenPreferenceInExplorer()
		{
			Process.Start(PreferenceIO.PreferencePath);
		}

		/// <summary>
		/// get this application varsion.
		/// </summary>
		/// <returns>varsion info</returns>
		private string GetVersion()
		{
			var var = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			return var.ToString();
		}

		/// <summary>
		/// open this application github repository in browser.
		/// </summary>
		private void OpenRepoPage()
		{
			const string link = "https://github.com/Namacha411/desktop-pet";
			var sourcecode = new ProcessStartInfo(link);
			Process.Start(sourcecode);
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
