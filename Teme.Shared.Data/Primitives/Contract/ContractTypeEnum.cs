using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Primitives.Contract
{
    public enum ContractTypeEnum
    {
        /// <summary>
        /// Нац процедура 1 к 1
        /// </summary>
        OneToOne = 1,

        /// <summary>
        /// Нац процедура 1 ко многим
        /// </summary>
        OneToMore = 2
    }
}
