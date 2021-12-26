using Malam2021.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021.Services
{

    public interface IDataAccessService 
    {
        public ConcurrentDictionary<int, Employee> Employees { get; }

        public ConcurrentDictionary<int, Department> Departments { get; }

        public ConcurrentDictionary<int, TaskDo> Tasks { get; }

        public Department GetDepartment(int id);
        public Employee GetEmployee(int id);
        public TaskDo GetTaskDo(int id);
        

    }


    public class DataMokService : IDataAccessService
    {
        IWebFileMapper mapper;
        public DataMokService(IWebFileMapper _mapper)
        {
            mapper = _mapper;
        }
        
        #region Department
        ConcurrentDictionary<int, Department> _dicDepartments = null;
        public ConcurrentDictionary<int, Department> Departments
        {
            get
            {
                var file = mapper.MapPath("~/data/json/departments.json");
                var hlp = new JsonHelper<Department>(file);
                _dicDepartments = _dicDepartments ??
                    new ConcurrentDictionary<int, Department>
                        (hlp.Data.ToDictionary(k => k.DepartmentId, v => v));
                return _dicDepartments;
            }
        }
        public Department GetDepartment(int id)
        {
            Department dept = null;
            Departments.TryGetValue(id, out dept);
            return dept;
        }
            #endregion

        #region Employees

        ConcurrentDictionary<int, Employee> _dicEmployees = null;

        public ConcurrentDictionary<int, Employee> Employees
        {
            get
            {
                var file = mapper.MapPath("~/data/json/employees.json");
                var hlp = new JsonHelper<Employee>(file);
                _dicEmployees = _dicEmployees ??
                    new ConcurrentDictionary<int, Employee>
                        (hlp.Data.ToDictionary(k => k.EmployeeId, v => v));
                return _dicEmployees;
            }
        }

        public Employee GetEmployee(int id)
        {
            Employee emp = null;
            Employees.TryGetValue(id, out emp);
            return emp;
        }


        #endregion
        
        #region TaskDo

        ConcurrentDictionary<int, TaskDo> _dicTasks = null;
        public ConcurrentDictionary<int, TaskDo> Tasks
        {
            get
            {
                var file = mapper.MapPath("~/data/json/tasks.json");
                 var hlp = new JsonHelper<TaskDo>(file);
                _dicTasks = _dicTasks ??
                    new ConcurrentDictionary<int, TaskDo>
                        (hlp.Data.ToDictionary(k => k.TaskNumber, v =>
                        {
                            v.EmployeeName = GetEmployee(v.EmployeeId)?.EmployeeName ?? "???";
                            v.DepartmentName = GetDepartment(v.DepartmentId)?.DepartmentName ?? "???"; 


                            return v;
                        }));
                return _dicTasks;
            }
        }

        public TaskDo GetTaskDo(int id)
        {
            TaskDo task = null;
            Tasks.TryGetValue(id, out task);
            return task;
        } 
        #endregion


    }

    internal class JsonHelper<T> where T : class
    {
        public readonly List<T> Data  = new List<T>();

        //      static readonly string appDataPath = Server.MapPath("~/App_Data");
    
        internal JsonHelper(string fileNameFull)
        {

            try
            {
                
                
                string json = File.ReadAllText(fileNameFull);
                T[] arr = JsonConvert.DeserializeObject<T[]>(json);
               
                 Data.AddRange(arr);

            }
            catch (Exception)
            {

                Data.Clear();
            }
            
        }

    }

    public class readobly
    {
    }
}
