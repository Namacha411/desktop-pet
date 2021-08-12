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
	public static class PreferenceIO
	{
		public static string MainFolderPath = @".\preference";
		public static string SoundsPath = $@"{MainFolderPath}\sounds";
		public static string ImagesPath = $@"{MainFolderPath}\images";
		public static string PreferencePath = $@"{MainFolderPath}\preference.json";

		public readonly static Preference defaultPreference = new Preference()
		{
			IconPath = @".\images\icon.ico",
			MainImagesPaths = new List<string>() { @".\images\MainImge.png" },
			DraggingImagesPaths = new List<string>() { @".\images\DraggingImage.png" },
			TimeSignelPaths = new List<string>()
			{
				@".\sounds\午前0時.mp3",
				@".\sounds\午前1時.mp3",
				@".\sounds\午前2時.mp3",
				@".\sounds\午前3時.mp3",
				@".\sounds\午前4時.mp3",
				@".\sounds\午前5時.mp3",
				@".\sounds\午前6時.mp3",
				@".\sounds\午前7時.mp3",
				@".\sounds\午前8時.mp3",
				@".\sounds\午前9時.mp3",
				@".\sounds\午前10時.mp3",
				@".\sounds\午前11時.mp3",
				@".\sounds\午後0時.mp3",
				@".\sounds\午後1時.mp3",
				@".\sounds\午後2時.mp3",
				@".\sounds\午後3時.mp3",
				@".\sounds\午後4時.mp3",
				@".\sounds\午後5時.mp3",
				@".\sounds\午後6時.mp3",
			},
			StartSoundPath = @".\sounds\startsound.mp3",
			ExitSoundPath = @".\sounds\exitsound.mp3",
			LowBattelySoundPath = @".\sounds\lowbattelysound.mp3"
		};

		/// <summary>
		/// read preference file.
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
		/// Write default preference file.
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
		/// create directory for preference file, sounds, images.
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
			/// serialize json file.
			/// </summary>
			/// <returns>string json data.</returns>
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
			/// deserialize json file.
			/// </summary>
			/// <param name="json">string json data.</param>
			/// <returns>deserialized preference data.</returns>
			public Preference Deserialize(string json)
			{
				return JsonSerializer.Deserialize<Preference>(json);
			}
		}
	}
}
