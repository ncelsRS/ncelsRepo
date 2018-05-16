namespace PW.Ncels.Database.Enums{

    public enum PriceProjectType{

        /// <summary>
        /// Заявление на регистрацию цен на ЛС
        /// </summary>
        PriceLs = 0,

        /// <summary>
        /// Заявление на регистрацию цен на ИМН
        /// </summary>
        PriceImn = 1,

        /// <summary>
        /// Заявление на регистрацию изменений на цену ЛС
        /// </summary>
        RePriceLs = 2,

        /// <summary>
        /// Заявление на регистрацию изменений на цену ИМН
        /// </summary>
        RePriceImn = 3
    }
}
