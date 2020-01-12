using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Reddit2Gener8aVideo4
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static HttpClient Http = new HttpClient();
		public static void GenerateVideo()
		{
			var tts = new Text2Speech("tts_test.mp3", "This is evil MLG.");
			var image = new RedditImage("image_test.jpg", 6969, "sb_hacks", "askreddit", new DateTime(2008, 11, 30), PostType.COMMENT);
			new RedditSlide("slide_test.webm", image, tts);
		}
	}
}
