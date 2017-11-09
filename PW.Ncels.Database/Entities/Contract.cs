namespace PW.Ncels.Database.DataModel
{
    public partial class Contract
    {
        public static readonly string StatusNew = "0";
        public static readonly string StatusOnCorrection = "1";
        public static readonly string StatusOnAgreement = "2";
        public static readonly string StatusOnSigning = "3";
        public static readonly string StatusOnApplicantSigningt = "4";
        public static readonly string StatusActive = "5";
        public static readonly string StatusExpired = "6";
        public static readonly string StatusInQueue = "7";
        public static readonly string StatusInWork = "8";
        public static readonly string StatusCorrected = "9";
        public static readonly string StatusApproved = "10";
        public static readonly string StatusOnRegistration = "11";
        public static readonly string[] StatusesInWork = { "0", "1", "2", "3", "4", "7", "8", "9", "10", "11" };

        public string HolderTypeCode { get; set; }
    }
}