using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Reddit2Gener8aVideo4
{
	abstract class Storable
	{
		protected static Dictionary<string, Storable> instances = new Dictionary<string, Storable>();
		private string path;
		public virtual string FilePath
		{
			get => path; 
			set {
				if (instances.ContainsKey(value))
					throw new IOException();
				else if (isDone)
					File.Move(path, path = value);
				else
					instances[path = value] = this;
			}
		}
		private bool isDone = false;
		public bool IsDone { get => isDone; }
		protected void MarkDone() => isDone = true;
		protected abstract void SaveFile();
	}
}
