using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021.Models
{
    //טבלת  לקוחות 
    public class Customer
    {
        public int ID { get; set; }     //מספר רץ
        string IdentityNo { get; set; } // מספר ת.ז
        string FullName { get; set; } //שם מלא
        DateTime CreateDate { get; set; } //תאריך יצירה

    }
}
