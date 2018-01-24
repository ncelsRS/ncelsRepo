namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractReportView
    {
        [Key]
        [Column("Id договора", Order = 0)]
        public Guid Id_договора { get; set; }

        [Column("Код статуса")]
        [StringLength(50)]
        public string Код_статуса { get; set; }

        [Column("Наименование статуса")]
        [StringLength(2000)]
        public string Наименование_статуса { get; set; }

        [Column("Статус оплаты")]
        public bool? Статус_оплаты { get; set; }

        [Column("Статус не полной оплаты")]
        public bool? Статус_не_полной_оплаты { get; set; }

        [Key]
        [Column("ПОдписано ЭЦП", Order = 1)]
        [StringLength(1)]
        public string ПОдписано_ЭЦП { get; set; }

        [Column("Дата подачи договора")]
        public DateTime? Дата_подачи_договора { get; set; }

        [Column("№ заявки")]
        [StringLength(500)]
        public string C__заявки { get; set; }

        [Column("Код статуса заявки")]
        [StringLength(50)]
        public string Код_статуса_заявки { get; set; }

        [Column("Наименование статуса заявки")]
        [StringLength(2000)]
        public string Наименование_статуса_заявки { get; set; }

        [Key]
        [Column("Тип договора", Order = 2)]
        public string Тип_договора { get; set; }

        [Column("Экспертиная организация")]
        [StringLength(4000)]
        public string Экспертиная_организация { get; set; }
    }
}
