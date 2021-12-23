using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021.Models
{
    //טבלת  לקוחות 
    public class Employee
    {
        public int EmployeeId { get; set; }     //מספר רץ
        public string EmployeeName { get; set; } // מספר ת.ז
        public string Email { get; set; } //שם מלא
        public string Phone { get; set; } //שם מלא
        public DateTime DateOB { get; set; } //תאריך יצירה
        public string employeePicture { get; set; }


    }
}
