using System;

namespace PW.Ncels.Database.DataModel {
	public partial class Experience
	{

		public Guid? Kod
		{
			get { return Id == Guid.Empty ? (Guid?) null : Id; }
			set
			{
				if (value.HasValue)
				{
					Id = value.Value;
				}
			}
		}
	}
}