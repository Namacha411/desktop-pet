using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using System.Text;

namespace desktop_pet
{
	public static class PreferenceIO
	{
		public const string PreferencePath = @".\";
		public readonly static Preference defaultPreference = new Preference()
		{
			IconPath = $@"{PreferencePath}\icon.ico",
			MainImagesPaths = new List<string>() { $@"{PreferencePath}MainImge.png" },
			DraggingImagesPaths = new List<string>() { $@"{PreferencePath}DraggingImage.png" },
			TimeSignelPaths = new List<string>()
			{
				$@"{PreferencePath}午前0時.mp3",
				$@"{PreferencePath}午前1時.mp3",
				$@"{PreferencePath}午前2時.mp3",
				$@"{PreferencePath}午前3時.mp3",
				$@"{PreferencePath}午前4時.mp3",
				$@"{PreferencePath}午前5時.mp3",
				$@"{PreferencePath}午前6時.mp3",
				$@"{PreferencePath}午前7時.mp3",
				$@"{PreferencePath}午前8時.mp3",
				$@"{PreferencePath}午前9時.mp3",
				$@"{PreferencePath}午前10時.mp3",
				$@"{PreferencePath}午前11時.mp3",
				$@"{PreferencePath}午後0時.mp3",
				$@"{PreferencePath}午後1時.mp3",
				$@"{PreferencePath}午後2時.mp3",
				$@"{PreferencePath}午後3時.mp3",
				$@"{PreferencePath}午後4時.mp3",
				$@"{PreferencePath}午後5時.mp3",
				$@"{PreferencePath}午後6時.mp3",
			},
			StartSoundPath = $@"{PreferencePath}startsound.mp3",
			ExitSoundPath = $@"{PreferencePath}exitsound.mp3",
			LowBattelySoundPath = $@"{PreferencePath}lowbattelysound.mp3"
		};

		public static string FileRead()
		{
			using (var streamReader = new StreamReader(PreferencePath, Encoding.UTF8))
			{
				return streamReader.ReadToEnd();
			}
		}

		public static void FileWrite()
		{
			using (var streamWriter	 = new StreamWriter(PreferencePath, true, Encoding.UTF8))
			{
				streamWriter.Write(defaultPreference.Serialize());
				streamWriter.Flush();
			}
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
