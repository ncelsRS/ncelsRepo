using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Enums.PriceProject{

    public enum RequestOrderType{

        /// <summary>
        /// Список ЕД
        /// </summary>
        [Display(Name = "Список ЕД")]
        Ed = 1,

        /// <summary>
        /// Список АЛО
        /// </summary>
        [Display(Name = "Список АЛО")]
        Alo = 2,

        /// <summary>
        /// Список КНФ
        /// </summary>
        [Display(Name = "Список КНФ")]
        Knf = 3,

        /// <summary>
        /// Другие
        /// </summary>
        [Display(Name = "Другие")]
        Other = 4

    }
}
