using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Enums{

    /// <summary>
	/// Тип документа
	/// </summary>
    public enum DocumentType{
	
		/// <summary>
		/// Входящий документ
		/// </summary>
		TemplateIncoming = -1,

		/// <summary>
		/// Исходящий документ
		/// </summary>
		TemplateOutgoing = -2,

		/// <summary>
		/// Обращение граждан
		/// </summary>
		TemplateCitizen = -3,

		/// <summary>
		/// ОРД
		/// </summary>
		TemplateAdministrative = -4,
		
		/// <summary>
		/// Входящий документ
		/// </summary>
		Incoming = 0,

		/// <summary>
		/// Исходящий документ
		/// </summary>
		Outgoing = 1,

		/// <summary>
		/// Обращение граждан
		/// </summary>
		Citizen = 2,

		/// <summary>
		/// ОРД
		/// </summary>
		Administrative = 3,
		
		/// <summary>
		/// Поект 
		/// </summary>
		Project = 4,
		
		/// <summary>
		/// Внутренния кореспонденция 
		/// </summary>
		Correspondence = 5,

        SixthValue = 6

    }

}
