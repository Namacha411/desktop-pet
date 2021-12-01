using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace desktop_pet
{
	/// <summary>
	/// プリファレンスの入出力
	/// </summary>
	public static class PreferenceIO
	{
		public static string MainFolderPath { get; private set; } = @".\preference";
		public static string SoundsPath { get; private set; } = $@"{MainFolderPath}\sounds";
		public static string ImagesPath { get; private set; } = $@"{MainFolderPath}\images";
		public static string PreferencePath { get; private set; } = $@"{MainFolderPath}\preference.json";

		public readonly static Preference defaultPreference = new Preference()
		{
			IconPath = $@"{ImagesPath}\icon.ico",
			MainImagesPaths = new List<string>() { $@"{ImagesPath}\MainIamge.png" },
			DraggingImagesPaths = new List<string>() { $@"{ImagesPath}\DraggingImage.png" },
			TimeSignelPaths = new List<string>()
			{
				$@"{SoundsPath}\午前0時.mp3",
				$@"{SoundsPath}\午前1時.mp3",
				$@"{SoundsPath}\午前2時.mp3",
				$@"{SoundsPath}\午前3時.mp3",
				$@"{SoundsPath}\午前4時.mp3",
				$@"{SoundsPath}\午前5時.mp3",
				$@"{SoundsPath}\午前6時.mp3",
				$@"{SoundsPath}\午前7時.mp3",
				$@"{SoundsPath}\午前8時.mp3",
				$@"{SoundsPath}\午前9時.mp3",
				$@"{SoundsPath}\午前10時.mp3",
				$@"{SoundsPath}\午前11時.mp3",
				$@"{SoundsPath}\午後0時.mp3",
				$@"{SoundsPath}\午後1時.mp3",
				$@"{SoundsPath}\午後2時.mp3",
				$@"{SoundsPath}\午後3時.mp3",
				$@"{SoundsPath}\午後4時.mp3",
				$@"{SoundsPath}\午後5時.mp3",
				$@"{SoundsPath}\午後6時.mp3",
				$@"{SoundsPath}\午後7時.mp3",
				$@"{SoundsPath}\午後8時.mp3",
				$@"{SoundsPath}\午後9時.mp3",
				$@"{SoundsPath}\午後10時.mp3",
				$@"{SoundsPath}\午後11時.mp3",
			},
			StartSoundPath = $@"{SoundsPath}\startsound.mp3",
			ExitSoundPath = $@"{SoundsPath}\exitsound.mp3",
			LowBattelySoundPath = $@"{SoundsPath}\lowbattelysound.mp3"
		};

		/// <summary>
		/// プリファレンスファイルの読み込み
		/// </summary>
		/// <returns>preference file. string json</returns>
		public static string FileRead()
		{
			using (var streamReader = new StreamReader(PreferencePath, Encoding.UTF8))
			{
				return streamReader.ReadToEnd();
			}
		}

		/// <summary>
		/// デフォルトプリファレンスファイルの書き出し
		/// </summary>
		public static void FileWrite()
		{
			using (var streamWriter = new StreamWriter(PreferencePath, true, Encoding.UTF8))
			{
				streamWriter.Write(defaultPreference.Serialize());
				streamWriter.Flush();
			}
		}

		/// <summary>
		/// プリファレンスファイル、サウンド、イメージ用のフォルダの生成
		/// </summary>
		public static void CreateDirectory()
		{
			try
			{
				Directory.CreateDirectory(MainFolderPath);
				Directory.CreateDirectory(SoundsPath);
				Directory.CreateDirectory(ImagesPath);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}
	}

	/// <summary>
	/// プリファレンス
	/// </summary>
	public class Preference
	{
		public string IconPath { get; set; }
		public List<string> MainImagesPaths { get; set; }
		public List<string> DraggingImagesPaths { get; set; }

		public List<string> TimeSignelPaths { get; set; }

		public string StartSoundPath { get; set; }
		public string ExitSoundPath { get; set; }
		public string LowBattelySoundPath { get; set; }

		public Preference() { }

		/// <summary>
		/// プリファレンスファイルのjsonのシリアライズ
		/// </summary>
		/// <returns>シリアライズされたjsonの文字列</returns>
		public string Serialize()
		{
			string json = JsonSerializer.Serialize(
				value: this,
				options: new JsonSerializerOptions
				{
					Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
					AllowTrailingCommas = true,
					WriteIndented = true
				}
				);
			return json;
		}

		/// <summary>
		/// プリファレンスファイルのjsonのデシリアライズ
		/// </summary>
		/// <param name="json">文字列のjson</param>
		/// <returns>デシリアライズされたデータのPreferenceクラス</returns>
		public static Preference Deserialize(string json)
		{
			return JsonSerializer.Deserialize<Preference>(json);
		}
	}
}
