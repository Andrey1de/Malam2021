using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021.Models
{

    //טבלת כיסויים 
    public class PolicyDetailscs
    {
        public int ID { get; set; }     //מספר רץ
        int CustomerId  { get; set; } // מפתח לטבלת הלקוחות
        int StateCode { get; set; } //סטטוס 4= פעיל
    decimal InsuranceAmt { get; set; } //סכום ביטוח

}
}
