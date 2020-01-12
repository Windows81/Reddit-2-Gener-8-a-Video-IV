using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Reddit2Gener8aVideo4
{
	class Demux : Storable
	{
		List<Storable> Files;
		public Demux(List<Storable> files)
		{
			Files = files;
			SaveFile();
		}
		protected override void SaveFile(){
			var lines = Files.Select(v => $"file '{v.FilePath}'");
			var text = string.Join('\n', lines);
			File.WriteAllText(FilePath, text);
			MarkDone();
		}
	}
}
