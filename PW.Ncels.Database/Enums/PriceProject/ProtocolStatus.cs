using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Enums.PriceProject{

    public enum ProtocolStatus{

        /// <summary>
        /// Черновик
        /// </summary>
        [Display(Name = "Черновик")]
        Draft = 0,

        /// <summary>
        /// В работе
        /// </summary>
        [Display(Name = "В работе")]
        InWork = 1,

        /// <summary>
        /// Завершен
        /// </summary>
        [Display(Name = "Завершен")]
        Finished = 2


    }
}
