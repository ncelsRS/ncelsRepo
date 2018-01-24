namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentDeclarationReportView
    {
        [Column("Номер заявки")]
        [StringLength(500)]
        public string Номер_заявки { get; set; }

        [Key]
        [Column("Тип услуги", Order = 0)]
        [StringLength(255)]
        public string Тип_услуги { get; set; }

        [Key]
        [Column("Наименование лекарственного перепарата на казахском", Order = 1)]
        [StringLength(255)]
        public string Наименование_лекарственного_перепарата_на_казахском { get; set; }

        [Column("Код статуса")]
        [StringLength(50)]
        public string Код_статуса { get; set; }

        [Column("Наименование статуса")]
        [StringLength(2000)]
        public string Наименование_статуса { get; set; }

        [Column("Дата подачи заявки")]
        public DateTime? Дата_подачи_заявки { get; set; }

        [Column("Дата окончания заявки")]
        public DateTime? Дата_окончания_заявки { get; set; }

        [Column("Наименование продукции")]
        public string Наименование_продукции { get; set; }

        [Column("Наименование производителя")]
        public string Наименование_производителя { get; set; }

        [Key]
        [Column("Тип заявки", Order = 2)]
        public string Тип_заявки { get; set; }
    }
}
