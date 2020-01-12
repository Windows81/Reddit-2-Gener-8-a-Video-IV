using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Reddit2Gener8aVideo4
{
	class RedditSlide : VideoFile
	{
		public RedditImage Image { get; set; }
		public Text2Speech Audio { get; set; }
		public RedditSlide(string path, RedditImage image, Text2Speech tts)
		{
			FilePath = path;
			Image = image;
			Audio = tts;
			SaveFile();
		}
		protected override void SaveFile()
		{
			var args = $"-loop 1 -y -i {Image.FilePath} -i {Audio.FilePath} -shortest -acodec copy -vcodec mjpeg {FilePath}";
			Process.Start("ffmpeg", args).WaitForExit();
			MarkDone();
		}
	}
}
