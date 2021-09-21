using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Action;
namespace Domain
{
    public class Logic
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
                    {
                        addProjectresults.IsPositiveResult = false;
                        addProjectresults.Message = "\nAdding Project Details is not Successful\nProject already exists with id - " + projectProperty.ProjectId;
                    }
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
                displayProjectResults.results = projectDetails;
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
                    {
                        addEmployeeResult.IsPositiveResult = false;
                        addEmployeeResult.Message = "\nAdding Employee Details is not Successful\nEmployee already exists with id - " + employeeProperty.EmployeeId;
                    }
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
                displayEmployeeResults.results = employeeDetails;
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
                    {
                        addRoleResult.IsPositiveResult = false;
                        addRoleResult.Message = "\nAdding Role Details is not Successful\nRole already exists with id - " + roleProperty.RoleId;
                    }
                    else
                    {
                        roleDetails.Add(roleProperty);
                        addRoleResult.Message = "\nRole Added Details to the Existing Role List";
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
                displayRoleResults.results = roleDetails;
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
                {
                    employeeClassRoleIdResult.IsPositiveResult = false;
                    employeeClassRoleIdResult.Message = "\nThe given Role id Doesn't Exists - " + employeeClassRoleIdProperty.EmployeeRoleId;
                }
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
                {
                    EmployeeIdResult.IsPositiveResult = false;
                    EmployeeIdResult.Message = "\nThe given employee id Doesn't Exists - " + employeeIdProperty.EmployeeId;
                }
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
                {
                    projectIdResult.IsPositiveResult = false;
                    projectIdResult.Message = "\nThe given Project id Doesn't Exists - " + _projectId;
                }
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
                if (projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).listEmployee == null)
                    projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).listEmployee = new List<Employee>();
                if (projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).listEmployee.Exists(e => e.EmployeeId == employeeIdProperty.EmployeeId))
                    addEmployeeToProjectResult.Message = "\nAdding Employee details by Role into Project not Successful\nProject id " + _projectId + " Already contains this Employee id - " + employeeIdProperty.EmployeeId;
                else
                {
                    projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).listEmployee.Add(employeeIdProperty);
                    addEmployeeToProjectResult.Message = "\nEmployee details by Role Successfully added to the project";
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
                    project.listEmployee = projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).listEmployee;
         return project;
        }

        public static ActionResult DeleteEmployeeFromProject(Employee employeeIdProperty, int _projectId)
        {
            ActionResult deleteEmployeeFromProjectResult = new () { IsPositiveResult = true };
            try
            {
                if (projectDetails.Exists(projectProperties => projectProperties.ProjectId == _projectId))
                {
                    if (projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).listEmployee.Exists(employeeProperties => employeeProperties.EmployeeId == employeeIdProperty.EmployeeId))
                    {
                        var removableItem = projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).listEmployee.Single(employeeProperties => employeeProperties.EmployeeId == employeeIdProperty.EmployeeId);
                        projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).listEmployee.Remove(removableItem);
                        deleteEmployeeFromProjectResult.Message = "\nSuccessfully deleted Employee id is - " + employeeIdProperty.EmployeeId;
                    }
                    else
                    {
                        deleteEmployeeFromProjectResult.IsPositiveResult = false;
                        deleteEmployeeFromProjectResult.Message = "\nProject id - " + _projectId + " Doesn't contain the given Employee id - " + employeeIdProperty.EmployeeId;
                    }
                }
                else
                {
                    deleteEmployeeFromProjectResult.IsPositiveResult = false;
                    deleteEmployeeFromProjectResult.Message = "\nThe given Project id Doesn't Exists - " + _projectId;
                }
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
