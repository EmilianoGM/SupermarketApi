using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SupermarketAPI.Domain.Models
{
    public enum EUnitOfMeasurement : byte
    {
        /*
         * An attribute is a way to define metadata over classes, interfaces, properties and other components of the C# language.
        */
        [Description("UN")]
        Unity = 1,
        [Description("MG")]
        Milligram = 2,
        [Description("G")]
        Gram = 3,
        [Description("KG")]
        Kilogram = 4,
        [Description("L")]
        Liter = 5
    }
}
