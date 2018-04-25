namespace Teme.Shared.Data.Context
{
    public class Role : BaseEntity
    {
        /// <summary>
        /// Наименование роли, которое будет переводиться ангуларом
        /// </summary>
        public string RoleName { get; set; }
    }
}