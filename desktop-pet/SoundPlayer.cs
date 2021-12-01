using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using WMPLib;

namespace desktop_pet
{
	/// <summary>
	/// 音源の再生
	/// </summary>
	public static class SoundPlayer
	{
		private static readonly WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();

		/// <summary>
		/// windwos media playerで音源を再生
		/// </summary>
		/// <param name="filePath">音源のファイルパス</param>
		public static void Play(string filePath)
		{
			if (File.Exists(filePath))
			{
				mediaPlayer.URL = filePath;
				mediaPlayer.controls.play();
			}
		}

		/// <summary>
		/// パソコンのバッテリーが低下したときに低下用の音源の再生
		/// </summary>
		/// <param name="SoundPath">音源のファイルパス</param>
		public static void PlayLowBattelySingal(string SoundPath)
		{
			if (IsLowBattely())
			{
				Play(SoundPath);
			}
		}

		/// <summary>
		/// バッテリーが低いかどうか
		/// </summary>
		/// <returns>バッテリーが低下したときにtrueそれ以外でfalse</returns>
		private static bool IsLowBattely()
		{
			var pls = SystemInformation.PowerStatus.PowerLineStatus;
			var blp = SystemInformation.PowerStatus.BatteryLifePercent;
			if (pls != PowerLineStatus.Offline) return false;
			if (blp != 0.1) return false;
			return true;
		}

		public static void PlayStartSound(string startSoundPath)
		{
			Play(startSoundPath);
		}

		/// <summary>
		/// BUG:　再生できない
		/// </summary>
		/// <param name="exitSoundPath">音源のファイルパス</param>
		public static void PlayExitSound(string exitSoundPath)
		{
			if (File.Exists(exitSoundPath))
			{
				mediaPlayer.URL = exitSoundPath;
				mediaPlayer.controls.play();
			}
		}

		/// <summary>
		/// 時報の再生
		/// </summary>
		/// <param name="SoundPathList">時報の音源ファイルのパスのリスト。長さは24でなければいけない</param>
		public static void PlayTimeSignal(List<string> SoundPathList)
		{
			var hour = DateTime.Now.Hour;
			var soundPath = SoundPathList[hour];
			Play(soundPath);
		}
		
		/// <summary>
		/// 指定の時間になったかどうか
		/// </summary>
		/// <param name="hour">時</param>
		/// <param name="min">分</param>
		/// <returns>その時間だったらtrue</returns>
		public static bool IsTime(int hour, int min)
		{
			return DateTime.Now.Hour == hour && DateTime.Now.Minute == min && DateTime.Now.Second == 0;
		}

		/// <summary>
		/// 指定の時間になったかどうか
		/// </summary>
		/// <param name="min">分</param>
		/// <returns>その分だったらtrue</returns>
		public static bool IsTime(int min)
		{
			return DateTime.Now.Minute == min && DateTime.Now.Second == 0;
		}
	}
}
