
namespace desktop_pet
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.EventTimer = new System.Windows.Forms.Timer(this.components);
			this.AnimationFrameRate = new System.Windows.Forms.Timer(this.components);
			this.DraggingAnimationFrameRate = new System.Windows.Forms.Timer(this.components);
			this.RightClickMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// RightClickMenu
			// 
			this.RightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
			this.RightClickMenu.Name = "RightClickMenu";
			this.RightClickMenu.Size = new System.Drawing.Size(103, 28);
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.Size = new System.Drawing.Size(102, 24);
			this.ExitToolStripMenuItem.Text = "Exit";
			this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// EventTimer
			// 
			this.EventTimer.Interval = 1000;
			this.EventTimer.Tick += new System.EventHandler(this.EventTimer_Tick);
			// 
			// AnimationFrameRate
			// 
			this.AnimationFrameRate.Interval = 500;
			this.AnimationFrameRate.Tick += new System.EventHandler(this.AnimationFrameRate_Tick);
			// 
			// DraggingAnimationFrameRate
			// 
			this.DraggingAnimationFrameRate.Interval = 500;
			this.DraggingAnimationFrameRate.Tick += new System.EventHandler(this.DraggingAnimationFrameRate_Tick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.RightClickMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip RightClickMenu;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.Timer EventTimer;
		private System.Windows.Forms.Timer AnimationFrameRate;
		private System.Windows.Forms.Timer DraggingAnimationFrameRate;
	}
}

