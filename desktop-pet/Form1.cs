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

		private readonly List<Bitmap> MainImages;
		private readonly List<Bitmap> DraggingImages;

		private bool isDragging;
		private Point mousePointerOffset;
		private int imagesFrame;

		public Form1()
		{
			InitializeComponent();
			Preference = NewPreference();
			notifyIcon = NewNotifyIcon();
			MainImages = NewMainImages();
			DraggingImages = NewDraggingImages();

			// event handler init
			MouseDown += new MouseEventHandler(Form1_LeftClickDown);
			MouseDown += new MouseEventHandler(Form1_RightClickDown);
			MouseUp += new MouseEventHandler(Form1_LeftClickUp);
			MouseMove += new MouseEventHandler(Form1_MouseMove);

			// variable init
			isDragging = false;
			imagesFrame = 0;

			// init Form
			FormBorderStyle = FormBorderStyle.None;
			TopMost = true;
			ClientSize = new Size(255, 255);
			TransparencyKey = BackColor;
			ShowInTaskbar = false;
			EventTimer.Start();
			AnimationFrameRate.Start();
			DraggingAnimationFrameRate.Start();

			// start sound
			SoundPlayer.PlayStartSound(Preference.StartSoundPath);
		}

		public Preference NewPreference()
		{
			// read preference file
			if (!File.Exists(PreferenceIO.PreferencePath))
			{
				PreferenceIO.CreateDirectory();
				PreferenceIO.FileWrite();
			}
			var json = PreferenceIO.FileRead();
			return Preference.Deserialize(json);
		}

		public NotifyIcon NewNotifyIcon()
		{
			var iconMenu = new ContextMenuStrip();
			iconMenu.Items.AddRange(new ToolStripItem[]{
				new ToolStripMenuItem(
					text: "🚪Exit",
					image: null,
					onClick: (s, e) => { ApplicationExit(); },
					name: "Exit"
					),
				new ToolStripMenuItem(
					text: "🔙Reset position",
					image: null,
					onClick: (s, e) => { Location = new Point(0, 0); },
					name: "Reset position"
					),
				new ToolStripMenuItem(
					text: "⚙Preference",
					image: null,
					onClick: (s, e) => { OpenPreferenceInExplorer(); },
					name: "Preference"
					),
				new ToolStripMenuItem(
					text: "📄About",
					image: null,
					onClick: (s, e) => { MessageBox.Show(GetVersion()); },
					name: "About"
					),
				new ToolStripMenuItem(
					text: "📃Source code",
					image: null,
					onClick: (s, e) => { OpenRepoPage(); },
					name: "Source code"
					)
			});
			var icon = Properties.Resources.pet_folder_icon;
			if (File.Exists(Preference.IconPath))
			{
				icon = new Icon(Preference.IconPath);
			}

			return new NotifyIcon
			{
				Icon = icon,
				Text = "Desktop pet",
				Visible = true,
				ContextMenuStrip = iconMenu
			};
		}

		public List<Bitmap> NewMainImages()
        {
			return Preference.MainImagesPaths
				.Where((path) => File.Exists(path))
				.Select((path) => new Bitmap(path))
				.ToList();
		}

		public List<Bitmap> NewDraggingImages()
		{
			return Preference.DraggingImagesPaths
				.Where((path) => File.Exists(path))
				.Select((path) => new Bitmap(path))
				.ToList();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// 背景画像の設定
		/// 画像は可能なら透過し拡大
		/// </summary>
		/// <param name="img"></param>
		private void SetImage(Bitmap img)
		{
			img.MakeTransparent();
			BackgroundImageLayout = ImageLayout.Zoom;
			BackgroundImage = img;
		}

		/// <summary>
		/// 毎時0分になったら時報を再生する
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

		#region menu event
		/// <summary>
		/// アプリケーション終了用のメニューアイテム
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ApplicationExit();
		}

		/// <summary>
		/// アプリケーションの終了
		/// </summary>
		public void ApplicationExit()
		{
			SoundPlayer.PlayExitSound(Preference.ExitSoundPath);
			Application.Exit();
		}

		/// <summary>
		/// エクスプローラーでプリファレンスを開く
		/// </summary>
		private void OpenPreferenceInExplorer()
		{
			Process.Start(PreferenceIO.PreferencePath);
		}

		/// <summary>
		/// このアプリケーションのバージョンを取得
		/// </summary>
		/// <returns>varsion info</returns>
		private string GetVersion()
		{
			var var = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			return var.ToString();
		}

		/// <summary>
		/// ブラウザでこのアプリのリポジトリのページを開く
		/// </summary>
		private void OpenRepoPage()
		{
			const string link = "https://github.com/Namacha411/desktop-pet";
			var sourcecode = new ProcessStartInfo(link);
			Process.Start(sourcecode);
		}
		#endregion

		#region animation
		private void Animation(List<Bitmap> images)
		{
			if (images.Count == 0) return;
			imagesFrame = (imagesFrame + 1) % images.Count;
			SetImage(images[imagesFrame]);
		}

		private void AnimationFrameRate_Tick(object sender, EventArgs e)
		{
			if (!this.isDragging)
			{
				Animation(MainImages);
			}
		}

		private void DraggingAnimationFrameRate_Tick(object sender, EventArgs e)
		{
			if (this.isDragging)
			{
				Animation(DraggingImages);
			}
		}
		#endregion

		#region mouse event
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
			}
		}
		#endregion
	}
}
