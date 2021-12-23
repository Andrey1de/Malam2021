using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021.Models
{
    //טבלת  לקוחות 
    public class Department
    {
        public int DepartmentId { get; set; }     //מספר רץ
        public string DepartmentName { get; set; } // שם חלקה 
        public int ParentID { get; set; } // מחלקת אב      

    }
}
