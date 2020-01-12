using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Reddit2Gener8aVideo4
{
	enum PostType
	{
		REGULAR,
		COMMENT
	}
	class RedditImage : Storable
	{
		static readonly string gimpDir = $@"{Environment.GetEnvironmentVariable("LocalAppData")}\Programs\GIMP 2";
		static readonly string gimpPath = $@"{gimpDir}\bin\gimp-2.10.exe";
		public uint Count { get; set; }
		public string Username { get; set; }
		public string SubReddit { get; set; }
		public DateTime Timestamp { get; set; }
		public PostType PostType { get; set; }
		protected override void SaveFile()
		{
			var dt = Timestamp;
			var icon = new SubRedditIcon(SubReddit);
			string[] argTable;
			string cmd;
			switch (PostType)
			{
				case PostType.COMMENT:
					argTable = new string[]{
						FilePath,
						Username,
						$"{AbbreviateNum(Count)} points",
						$"• {RelativeTime(dt)}",
						$"The time is {DateTime.UtcNow.ToString("HH:mm:ss")} UTC.",
					};
					cmd = "reddit-comment-gen";
					break;
				default:
					argTable = new string[]{
						FilePath,
						AbbreviateNum(Count),
						icon.FilePath,
						$"r/{SubReddit}",
						$"• Posted by u/{Username} {RelativeTime(dt)}",
						$"The time is {DateTime.UtcNow.ToString("HH:mm:ss")} UTC.",
					};
					cmd = "reddit-post-gen";
					break;
			}
			var argString = $"-b \"({cmd} 666 666 {string.Join(' ', argTable.Select(v => $"\\\"{v}\\\""))})'";
			Process.Start(new ProcessStartInfo() { Arguments = argString, FileName = gimpPath });
			MarkDone();
		}
		public RedditImage(string path, uint count, string user, string sub, DateTime time, PostType type = PostType.REGULAR)
		{
			Count = count;
			Username = user;
			SubReddit = sub;
			Timestamp = time;
			PostType = type;

			foreach (var item in Directory.GetFiles(".", "*.ttf"))
				File.Copy(item, $@"{gimpDir}\share\gimp\2.0\fonts{item}", true);
			foreach (var item in Directory.GetFiles(".", "*.scm"))
				File.Copy(item, $@"{gimpDir}\share\gimp\2.0\scripts\{item}", true);

			FilePath = path;
			SaveFile();
		}
		static string AbbreviateNum(uint num)
		{
			if (num == 0) return "•";

			int digits = 1 + (int)Math.Log10(num);
			string ns = num.ToString();
			switch (digits)
			{
				case 1:
				case 2:
				case 3:
					return ns;
				case 4:
					return $"{ns[0]}.{ns[1]}k";
				case 5:
					return $"{ns[0]}{ns[1]}.{ns[2]}k";
				case 6:
					return $"{ns[0]}{ns[1]}{ns[2]}k";
				case 7:
					return $"{ns[0]}{ns[1]}{ns[2]}k";
				default:
					return ns;
			}
		}
		static string RelativeTime(DateTime dt)
		{
			var ts = DateTime.UtcNow - dt;
			double delta = ts.TotalSeconds;

			if (delta < 60)
				return "just now";
			if (delta < 120)
				return "1 minute ago";
			if (delta < 3600)
				return ts.Minutes + " minutes ago";
			if (delta < 7200)
				return "1 hour ago";
			if (delta < 86400)
				return ts.Hours + " hours ago";
			if (delta < 172800)
				return "1 day ago";
			if (delta < 2592000)
				return ts.Days + " days ago";
			if (delta < 31557600)
			{
				int months = Convert.ToInt32(Math.Floor(ts.Days / 30d));
				return months > 1 ? months + " months ago" : "1 month ago";
			}
			else
			{
				int years = Convert.ToInt32(Math.Floor(ts.Days / 365.25));
				return years > 1 ? years + " years ago" : "1 year ago";
			}
		}
	}
}
