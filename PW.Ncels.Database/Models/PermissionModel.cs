namespace PW.Ncels.Database.Models
{
	public class PermissionModel
	{
		public int Id { get; set; }
		public string GroupName { get; set; }
		public string KeyName { get; set; }
		public string KeyType { get; set; }
		public string KeyValue { get; set; }
		public string KeyPermission { get; set; }

		public string KeyDescription { get; set; }


	}
}