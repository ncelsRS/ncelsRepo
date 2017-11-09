using System;

namespace PW.Ncels.Database.Models
{
	public class UploadInitialFile
	{
		public Guid Id
		{
			get { return Guid.NewGuid(); }
		}

		public UploadInitialFile()
		{
			
		}

	
		public string name { get; set; }
		public long size { get; set; }
		public string extension { get; set; }

		public string documentId { get; set; }
		public UploadInitialFile(string name, long size, string extension) {
			this.name = name;
			this.size = size;
			this.extension = extension;
		}

		public string sizeStr { get { return GetFileSize(size); } }

		private string GetFileSize(long size) {
			const int byteConversion = 1024;
			double bytes = Convert.ToDouble(size);

			if (bytes >= Math.Pow(byteConversion, 3)) //GB Range
			{
				return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), " Гб");
			}
			if (bytes >= Math.Pow(byteConversion, 2)) //MB Range
			{
				return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), " Мб");
			}
			if (bytes >= byteConversion) //KB Range
			{
				return string.Concat(Math.Round(bytes / byteConversion, 2), " Кб");
			}
			return string.Concat(bytes, " Байт");
		}
	}
}