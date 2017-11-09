namespace PW.Ncels.Database.DataModel
{
    public partial class FileLinksCategoryComRecord
    {
        public string CreateDateStr => CreateDate.ToString("dd/MM/yyyy HH:mm");
        public string OwnerName => Employee?.DisplayName;
    }
}
