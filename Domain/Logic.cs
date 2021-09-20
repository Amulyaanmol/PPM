using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Action;
namespace Domain
{
    public class Logic
    {
        static readonly List<Project> proDetail = new ();
        static readonly List<Employee> empDetail = new ();
        static readonly List<Role> roleDetail = new ();
        public static ActionResult AddProject(Project pro)
        {
            ActionResult result = new() { IsPositiveResult = true };
            try
            {
                if (proDetail.Count > 0)
                {
                    if (proDetail.Exists(proj => proj.proid == pro.proid))
                    {
                        result.IsPositiveResult = false;
                        result.Message = "Project already exists with id:" + pro.proid;
                    }
                    else
                    {
                        proDetail.Add(pro);
                        result.Message = "\nProject Added to the Existing Project List";
                    }
                }
                else
                {
                    proDetail.Add(pro);
                    result.Message = "\nProject successfully Added";
                }
            }
            catch (Exception)
            {
                result.IsPositiveResult = false;
                result.Message = "Some Error Occured!! Please select right option";
            }
            return result;
        }
        public static DataResults<Project> DisplayProjects()
        {
            DataResults<Project> dataResults = new() { IsPositiveResult = true };
            if (proDetail.Count > 0)
                dataResults.results = proDetail;
            else
            {
                dataResults.Message = ".....Project List is Empty.....";
                dataResults.IsPositiveResult = false;
            }
            return dataResults;
        }
        public static ActionResult AddEmployee(Employee emp)
        {
            ActionResult result = new() { IsPositiveResult = true };
            try
            {
                if (empDetail.Count > 0)
                {
                    if (empDetail.Exists(em => em.empid == emp.empid))
                    {
                        result.IsPositiveResult = false;
                        result.Message = "Employee already exists with id:" + emp.empid;
                    }
                    else
                    {
                        empDetail.Add(emp);
                        result.Message = "\nEmployee Added to the Existing Employee List";
                    }
                }
                else
                {
                    empDetail.Add(emp);
                    result.Message = "\nEmployee successfully Added";
                }
            }
            catch (Exception)
            {
                result.IsPositiveResult = false;
                result.Message = "Some Error Occured!! Please select right option";
            }
            return result;
        }
        public static DataResults<Employee> DisplayEmployees()
        {
            DataResults<Employee> dataResults = new() { IsPositiveResult = true };
            if (empDetail.Count > 0)
                dataResults.results = empDetail;
            else
            {
                dataResults.IsPositiveResult = false;
                dataResults.Message = ".....Employee List is Empty.....";
            }
            return dataResults;
        }
        public static ActionResult AddRole(Role role)
        {
            ActionResult result = new() { IsPositiveResult = true };
            try
            {
                if (roleDetail.Count > 0)
                {
                    if (roleDetail.Exists(r => r.roleid == role.roleid))
                    {
                        result.IsPositiveResult = false;
                        result.Message = "Role already exists with id:" + role.roleid;
                    }
                    else
                    {
                        roleDetail.Add(role);
                        result.Message = "\nRole Added to the Existing Role List";
                    }
                }
                else
                {
                    roleDetail.Add(role);
                    result.Message = "\nRole successfully Added";
                }
            }
            catch (Exception)
            {
                result.IsPositiveResult = false;
                result.Message = "Some Error Occured!! Please select right option";
            }
            return result;
        }
        public static DataResults<Role> DisplayRoles()
        {
            DataResults<Role> dataResults = new (){ IsPositiveResult = true };
            if (roleDetail.Count > 0)
                dataResults.results = roleDetail;
            else
            {
                dataResults.IsPositiveResult = false;
                dataResults.Message = ".....Role List is Empty.....";
            }
            return dataResults;
        }
   
        public static ActionResult CheckRole(Employee rid)
        {
            ActionResult res = new() { IsPositiveResult = true };
            try
            {
                if (roleDetail.Exists(role => role.roleid == rid.rol))
                    res.IsPositiveResult = true;
                else
                {
                    res.IsPositiveResult = false;
                    res.Message = "The given Role id Doesn't Exists - " + rid.rol;
                }
            }
            catch (Exception)
            {
                res.IsPositiveResult = false;
                res.Message = "Some Error Occured!! Please select right option";
            }
            return res;
        }
        public static ActionResult CheckEmp(Employee eid)
        {
            ActionResult res = new() { IsPositiveResult = true };
            try
            {
                if (empDetail.Exists(e => e.empid == eid.empid))
                    res.IsPositiveResult = true;
                else
                {
                    res.IsPositiveResult = false;
                    res.Message = "The given employee id Doesn't Exists - " + eid.empid;
                }
            }
            catch (Exception)
            {
                res.IsPositiveResult = false;
                res.Message = "Some Error Occured!! Please select right option";
            }
            return res;
        }
        public static ActionResult CheckPro(int pid)
        {
            ActionResult res = new() { IsPositiveResult = true };
            try
            {
                if (proDetail.Exists(p => p.proid == pid))
                    res.IsPositiveResult = true;
                else
                {
                    res.IsPositiveResult = false;
                    res.Message = "The given Project id Doesn't Exists - " + pid;
                }
            }
            catch (Exception)
            {
                res.IsPositiveResult = false;
                res.Message = "Some Error Occured!! Please select right option";
            }
            return res;
        }
        public static ActionResult AddEmpToProject(int id,Employee employ)
        {
            ActionResult res = new() { IsPositiveResult = true };
            try
            {  
                if (proDetail.Single(pr => pr.proid == id).Emp == null)
                    proDetail.Single(pr => pr.proid == id).Emp = new List<Employee>();
                if (proDetail.Single(pr => pr.proid == id).Emp.Exists(e => e.empid == employ.empid))
                    res.Message = "Project id " + id + " Already contains this Employee id - " + employ.empid;
                else
                {
                    proDetail.Single(pr => pr.proid == id).Emp.Add(employ);
                    res.Message = "Employee Successfully added to the project";
                }     
            }
            catch (Exception)
            {
                res.IsPositiveResult = false;
                res.Message = "Some Error Occured!! Please select right option";
            }
            return res;
        } 
        public static Employee GetEmpRole(Employee ei)
        {
            Employee employee = new();
            employee.rol = empDetail.Single(e => e.empid == ei.empid).rol;
            employee.name = empDetail.Single(e => e.empid == ei.empid).name;
            return employee;
        }
        public static Project GetProId(int id)
        {
            Project p = new();
            ActionResult r = new() { IsPositiveResult = true };
            try
            {
                p.Emp = proDetail.Single(p => p.proid == id).Emp;
            }
            catch (Exception)
            {
                r.IsPositiveResult = false;
                r.Message = "Some Error Occured!! Please select right option";
            }
            return p;
        }
        public static ActionResult DelEmpFromProject(Employee emp,int id)
        {
            Project pr = new ();
            ActionResult r = new () { IsPositiveResult = true };
            try
            {
                if (proDetail.Exists(p => p.proid == id))
                {
                    if (proDetail.Single(p => p.proid == id).Emp.Exists(e => e.empid == emp.empid))
                    {
                        var opr = proDetail.Single(p => p.proid == id).Emp.Single(e => e.empid == emp.empid);
                        proDetail.Single(p => p.proid == id).Emp.Remove(opr);
                        r.Message = "\nSuccessfully deleted Employee id is - " + emp.empid;
                    }
                    else
                    {
                        r.IsPositiveResult = false;
                        r.Message = "Project id - "+ id +"Doesn't contain the given Employee id - " + emp.empid;
                    }
                }
                else
                {
                    r.IsPositiveResult = false;
                    r.Message = "The given Project id Doesn't Exists - " + id;
                }
            }
            catch (Exception)
            {
                r.IsPositiveResult = false;
                r.Message = "Some Error Occured!! Please select right option";
            }
                return r;
        }

    }
}
