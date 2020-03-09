using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessCenterWebApp.Models
{
    public static class Static
    {
        public static int MonthDifference(this DateTime lValue, DateTime rValue)
        {
            int monthResult = (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
            return monthResult;
        }
    }
}
