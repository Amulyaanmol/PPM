using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Action;
namespace Domain
{
    public static class Logic
    {
        
        static readonly List<Project> projectDetails = new ();
        static readonly List<Employee> employeeDetails = new ();
        static readonly List<Role> roleDetails = new ();

        public static ActionResult AddProject(Project projectProperty)
        {
            ActionResult addProjectresults = new() { IsPositiveResult = true };
            try
            {
                if (projectDetails.Count > 0)
                {
                    if (projectDetails.Exists(projectProperties => projectProperties.ProjectId == projectProperty.ProjectId))
                        addProjectresults.IsPositiveResult = false; 
                    else
                    {
                        projectDetails.Add(projectProperty);
                        addProjectresults.Message = "\nProject Details Added to the Existing Project List";
                    }
                }
                else
                {
                    projectDetails.Add(projectProperty);
                    addProjectresults.Message = "\nProject Details Successfully Added";
                }
            }
            catch (Exception)
            {
                addProjectresults.IsPositiveResult = false;
                addProjectresults.Message = "\nSome Error Occured!! Please select right option";
            }
            return addProjectresults;
        }

        public static DataResults<Project> DisplayProjects()
        {
            DataResults<Project> displayProjectResults = new() { IsPositiveResult = true };
            if (projectDetails.Count > 0)
                displayProjectResults.Results = projectDetails;
            else
            {
                displayProjectResults.Message = ".....Project List is Empty.....";
                displayProjectResults.IsPositiveResult = false;
            }
            return displayProjectResults;
        }

        public static ActionResult AddEmployee(Employee employeeProperty)
        {
            ActionResult addEmployeeResult = new() { IsPositiveResult = true };
            try
            {
                if (employeeDetails.Count > 0)
                {
                    if (employeeDetails.Exists(employeeProperties => employeeProperties.EmployeeId == employeeProperty.EmployeeId))
                        addEmployeeResult.IsPositiveResult = false;   
                    else
                    {
                        employeeDetails.Add(employeeProperty);
                        addEmployeeResult.Message = "\nEmployee Details Added to the Existing Employee List";
                    }
                }
                else
                {
                    employeeDetails.Add(employeeProperty);
                    addEmployeeResult.Message = "\nEmployee Details Successfully Added";
                }
            }
            catch (Exception)
            {
                addEmployeeResult.IsPositiveResult = false;
                addEmployeeResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return addEmployeeResult;
        }

        public static DataResults<Employee> DisplayEmployees()
        {
            DataResults<Employee> displayEmployeeResults = new() { IsPositiveResult = true };
            if (employeeDetails.Count > 0)
                displayEmployeeResults.Results = employeeDetails;
            else
            {
                displayEmployeeResults.IsPositiveResult = false;
                displayEmployeeResults.Message = ".....Employee List is Empty.....";
            }
            return displayEmployeeResults;
        }

        public static ActionResult AddRole(Role roleProperty)
        {
            ActionResult addRoleResult = new() { IsPositiveResult = true };
            try
            {
                if (roleDetails.Count > 0)
                {
                    if (roleDetails.Exists(roleProperties => roleProperties.RoleId == roleProperty.RoleId))
                         addRoleResult.IsPositiveResult = false;
                    else
                    {
                        roleDetails.Add(roleProperty);
                        addRoleResult.Message = "\nRole Details Added to the Existing Role List";
                    }
                }
                else
                {
                    roleDetails.Add(roleProperty);
                    addRoleResult.Message = "\nRole Details Successfully Added";
                }
            }
            catch (Exception)
            {
                addRoleResult.IsPositiveResult = false;
                addRoleResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return addRoleResult;
        }

        public static DataResults<Role> DisplayRoles()
        {
            DataResults<Role> displayRoleResults = new (){ IsPositiveResult = true };
            if (roleDetails.Count > 0)
                displayRoleResults.Results = roleDetails;
            else
            {
                displayRoleResults.IsPositiveResult = false;
                displayRoleResults.Message = ".....Role List is Empty.....";
            }
            return displayRoleResults;
        }

        public static ActionResult CheckEmployeeClassRoleId(Employee employeeClassRoleIdProperty)
        {
            ActionResult employeeClassRoleIdResult = new() { IsPositiveResult = true };
            try
            {
                if (roleDetails.Exists(roleProperties => roleProperties.RoleId == employeeClassRoleIdProperty.EmployeeRoleId))
                    employeeClassRoleIdResult.IsPositiveResult = true;
                else
                    employeeClassRoleIdResult.IsPositiveResult = false;        
            }
            catch (Exception)
            {
                employeeClassRoleIdResult.IsPositiveResult = false;
                employeeClassRoleIdResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return employeeClassRoleIdResult;
        }

        public static ActionResult CheckEmployeeId(Employee employeeIdProperty)
        {
            ActionResult EmployeeIdResult = new() { IsPositiveResult = true };
            try
            {
                if (employeeDetails.Exists(employeeProperties => employeeProperties.EmployeeId == employeeIdProperty.EmployeeId))
                    EmployeeIdResult.IsPositiveResult = true;
                else
                    EmployeeIdResult.IsPositiveResult = false;       
            }
            catch (Exception)
            {
                EmployeeIdResult.IsPositiveResult = false;
                EmployeeIdResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return EmployeeIdResult;
        }

        public static ActionResult CheckProjectId(int _projectId)
        {
            ActionResult projectIdResult = new() { IsPositiveResult = true };
            try
            {
                if (projectDetails.Exists(projectProperties => projectProperties.ProjectId == _projectId))
                    projectIdResult.IsPositiveResult = true;
                else
                    projectIdResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                projectIdResult.IsPositiveResult = false;
                projectIdResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return projectIdResult;
        }

        public static ActionResult AddEmployeeToProject(int _projectId,Employee employeeIdProperty)
        {
            ActionResult addEmployeeToProjectResult = new() { IsPositiveResult = true };
            try
            {  
                if (projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee == null)
                    projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee = new List<Employee>();
                if (projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Exists(e => e.EmployeeId == employeeIdProperty.EmployeeId))
                    addEmployeeToProjectResult.IsPositiveResult = false;
                else
                {
                    addEmployeeToProjectResult.IsPositiveResult = true;
                    projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Add(employeeIdProperty); 
                }     
            }
            catch (Exception)
            {
                addEmployeeToProjectResult.IsPositiveResult = false;
                addEmployeeToProjectResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return addEmployeeToProjectResult;
        } 

        public static Employee CheckEmployeeRoleId(Employee employeeIdProperty)
        {
            Employee employee = new();
            employee.EmployeeRoleId = employeeDetails.Single(employeeProperties => employeeProperties.EmployeeId == employeeIdProperty.EmployeeId).EmployeeRoleId;
            employee.EmployeeName = employeeDetails.Single(employeeProperties => employeeProperties.EmployeeId == employeeIdProperty.EmployeeId).EmployeeName;
            return employee;
        }

        public static Project GetProjectId(int _projectId)
        {
            Project project = new();
            if (projectDetails.Exists(projectProperties => projectProperties.ProjectId == _projectId))
                project.ListEmployee = projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee;
            return project;
        }
        public static ActionResult DeleteEmployeeFromProject(Employee employeeIdProperty, int _projectId)
        {
            ActionResult deleteEmployeeFromProjectResult = new() { IsPositiveResult = true };
            try
            {
                if (projectDetails.Exists(projectProperties => projectProperties.ProjectId == _projectId))
                {
                    if (projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Exists(employeeProperties => employeeProperties.EmployeeId == employeeIdProperty.EmployeeId))
                    {
                        var removableItem = projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Single(employeeProperties => employeeProperties.EmployeeId == employeeIdProperty.EmployeeId);
                        projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Remove(removableItem);
                        deleteEmployeeFromProjectResult.Message = "\nSuccessfully deleted Employee id is - " + employeeIdProperty.EmployeeId;
                    }
                    else
                    {
                        deleteEmployeeFromProjectResult.IsPositiveResult = false;
                        deleteEmployeeFromProjectResult.Message = "\nProject id - " + _projectId + " Doesn't contain the given Employee id - " + employeeIdProperty.EmployeeId;
                    }
                }
                else
                  deleteEmployeeFromProjectResult.IsPositiveResult = false;    
            }
            catch (Exception)
            {
                deleteEmployeeFromProjectResult.IsPositiveResult = false;
                deleteEmployeeFromProjectResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return deleteEmployeeFromProjectResult;
        }
        

    }
}

    

