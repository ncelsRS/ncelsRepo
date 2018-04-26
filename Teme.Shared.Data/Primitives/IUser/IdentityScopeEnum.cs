using System.ComponentModel;

namespace Teme.Shared.Data.Primitives.IUser
{
    /// <summary>
    /// Описание всех АПИ для использования в конфигурациях IdentityServer
    /// </summary>
    public class IdentityScopeEnum
    {
        [Description("ОБК")] public const string Obk = "obk";

        [Description("ТЭМИ внешний портал")] public const string TemeExt = "ext";

        [Description("ТЭМИ Группа валидации")] public const string TemeGv = "gv";

        [Description("ТЭМИ Центр обработки заявителей")]
        public const string TemeCoz = "coz";
    }
}