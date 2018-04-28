namespace Teme.Shared.Data.Primitives.Permissions
{
    /// <summary>
    /// Permissions для внешнего портала
    /// </summary>
    public class Permissions
    {
        /// <summary>
        /// Разрешения для внешнего портала
        /// </summary>
        public class ExtPortal
        {
            /// <summary>
            /// Просмотр своих договоров для заявителя
            /// </summary>
            public const string IsDeclarant = "IsDeclarant";
        }

        /// <summary>
        /// Разрешения дл внутреннего портала
        /// </summary>
        public class IntPortal
        {
            /// <summary>
            /// 
            /// </summary>
            public const string IsChief = "IsChief";
        }
        
    }
}