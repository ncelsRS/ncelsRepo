using System.Collections.Generic;

namespace Teme.Shared.Data.Primitives.Workflow.Enums
{
    public class ScopeEnum
    {
        /// <summary>
        /// Корневая область
        /// </summary>
        public const string Root = "Root";
        /// <summary>
        /// Выполнение в Группе Валидации
        /// </summary>
        public const string Gv = "Gv";
        /// <summary>
        /// Выполнение в ЦОЗ
        /// </summary>
        public const string Coz = "Coz";
        /// <summary>
        /// Выполнение в ДЭФ
        /// </summary>
        public const string Def = "Def";
        /// <summary>
        /// Руководитель ЦОЗ
        /// </summary>
        public const string CozBoss = "CozBoss";
        /// <summary>
        /// ЗамГенДир
        /// </summary>
        public const string Ceo = "Ceo";


        public static readonly List<string> OneToMoreListScope = new List<string>()
        {
            Coz,
            CozBoss,
            Ceo
        };
    }
}
