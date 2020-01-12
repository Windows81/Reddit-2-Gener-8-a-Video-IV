using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Reddit2Gener8aVideo4
{
	class Text2Speech : Storable
	{
		string Text;
		public Text2Speech(string path, string text)
		{
			FilePath = path;
			Text = text;
			SaveFile();
		}
		protected override void SaveFile()
		{
			App.Http.DefaultRequestHeaders.Add("Referer", "https://www.vocalware.com/index/demo");
			App.Http.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36");
			App.Http.DefaultRequestHeaders.Add("Origin", "https://www.vocalware.com");
			App.Http.DefaultRequestHeaders.Clear();

			var url = $"https://cache-a.oddcast.com/tts/gen.php?EID=4&LID=1&VID=5&TXT={Uri.EscapeUriString(Text)}&IS_UTF8=1&HTTP_ERR=1&ACC=3314795&API=2292376&vwApiVersion=2&CB=vw_mc.vwCallback";
			App.Http.GetAsync(url).Result.Content.CopyToAsync(new FileStream(FilePath, FileMode.Create)).Wait();
		}
	}
}
