using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Employee
    {
        [Required]
        public string name { get; set; }
        public int empid { get; set; }
        [Phone]
        public string contact { get; set; }
        public int rol{ get; set; }

    }
    public class Role
    {
        public string name { get; set; }
        public int roleid { get; set; }
    }
    public class Project
    {
        public int proid { get; set; }
        public string name { get; set; }
        public DateTime sDate { get; set; }
        public DateTime eDate { get; set; }
        public decimal budget { get; set; }
        public  List<Employee> Emp;
    }
}
