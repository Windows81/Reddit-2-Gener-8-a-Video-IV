using System.IO;
using System.Text.RegularExpressions;

namespace Reddit2Gener8aVideo4
{
	class SubRedditIcon : Storable
	{
		string sub;
		protected override void SaveFile()
		{
			var url = $"https://www.reddit.com/r/{sub}/about.json";
			var result = App.Http.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
			var match = Regex.Match(result, "\"community_icon\": \"(https://[^\"]+\\.png)\"").Groups[1].Value;
			App.Http.GetAsync(match).Result.Content.CopyToAsync(new FileStream(FilePath, FileMode.OpenOrCreate)).Wait();
		}
		public SubRedditIcon(string sub)
		{
			FilePath = $"sub-{this.sub = sub}.png";
			SaveFile();
		}
	}
}
