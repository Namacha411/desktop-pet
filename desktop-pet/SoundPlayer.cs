using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using WMPLib;

namespace desktop_pet
{
	public static class SoundPlayer
	{
		private static readonly WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();

		/// <summary>
		/// play sound.
		/// </summary>
		/// <param name="filePath">sound file path</param>
		public static void Play(string filePath)
		{
			if (File.Exists(filePath))
			{
				mediaPlayer.URL = filePath;
				mediaPlayer.controls.play();
			}
		}

		/// <summary>
		/// when pc become low battely, play low battely sound.
		/// </summary>
		/// <param name="SoundPath">sound file path</param>
		public static void PlayLowBattelySingal(string SoundPath)
		{
			if (isLowBattely())
			{
				Play(SoundPath);
			}
		}

		/// <summary>
		/// when battely become low, return true.
		/// </summary>
		/// <returns>is battely low?</returns>
		private static bool isLowBattely()
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
		/// BUG: Can't play sound.
		/// </summary>
		/// <param name="exitSoundPath"></param>
		public static void PlayExitSound(string exitSoundPath)
		{
			if (File.Exists(exitSoundPath))
			{
				mediaPlayer.URL = exitSoundPath;
				mediaPlayer.controls.play();
			}
		}

		/// <summary>
		/// play time signal.
		/// </summary>
		/// <param name="SoundPathList">time signal sound paths list. len is 24.</param>
		public static void PlayTimeSignal(List<string> SoundPathList)
		{
			var hour = DateTime.Now.Hour;
			var soundPath = SoundPathList[hour];
			Play(soundPath);
		}
		
		public static bool IsTime(int hour, int min)
		{
			return DateTime.Now.Hour == hour && DateTime.Now.Minute == min && DateTime.Now.Second == 0;
		}

		public static bool IsTime(int min)
		{
			return DateTime.Now.Minute == min && DateTime.Now.Second == 0;
		}
    }
}