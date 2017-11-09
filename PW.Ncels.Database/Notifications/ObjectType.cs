using System.ComponentModel;

namespace PW.Ncels.Database.Notifications
{
    /// <summary>
    /// Перечисление с типами объектов на которые создается уведомление, название элемента перечисления сохраняется в Notification.TableName
    /// </summary>
    /// <remarks>Если нужно чтобы в Notification.TableName сохранялось какое то определеное значение тогда необходимо к элементу добавить атрибут Description</remarks>
    public enum ObjectType
    {
        Unknown,
        TestObject,
        [Description("Contracts")]
        Contract,
        [Description("Documents")]
        Document,
        [Description("PriceProjects")]
        PriceProject,
        [Description("DrugPrimaryFinalDocument")]
        PrimaryFinalDoc,
        [Description("Letter")]
        Letter,
        [Description("Translate")]
        Translate,
        [Description("Commission")]
        Commission,

    }
}