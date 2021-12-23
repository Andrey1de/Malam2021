using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021.Models
{
 

    public class TaskDo
    {
        public int TaskNumber { get; set; }    
        public  string TaskName { get; set; } 
        public int StatusId { get; set; }
        public int EmployeeId { get; set; } //תאריך יצירה
        public string EmployeeName { get; set; } //תאריך יצירה
        public int DepartmentId { get; set; } //
        public string DepartmentName { get; set; } //
        public DateTime DueDate { get; set; } //תאריך יצירה

    }


}


//	[{“TaskNumber”:number,”TaskName”:”name”,”StatusID”:Number,” EmployeeName “:”Employee Name”,” DepartmentID “:number,”DueDate”:date}]