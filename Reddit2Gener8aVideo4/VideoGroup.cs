using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Reddit2Gener8aVideo4
{
	class VideoGroup : Storable
	{
		protected List<VideoFile> Slides = new List<VideoFile>();
		public VideoGroup() { }
		public void AddPiece(VideoFile video) => Slides.Add(video);
		protected override void SaveFile()
		{
			var demuxFile = new Demux(Slides.Cast<Storable>().ToList()).FilePath;
			Process.Start("ffmpeg", $"-f concat -i {demuxFile} -c copy video.webm");
		}
	}
}
