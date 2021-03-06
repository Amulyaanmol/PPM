using System;
using Model;
using Domain;
using System.Text.RegularExpressions;

namespace Output
{
    public class Display
    {
        protected Display()
        {
        }
        public static int DisplayMainMenu()
        {
            Console.WriteLine("\n---Select any option:---");
            Console.WriteLine(" 1. Add Project\n 2. View Projects\n 3. Add Employee \n 4. View Employees \n 5. Add Role \n " +
                "6. View Roles\n 7. Add Employee to Project\n 8. Delete Employee from Project\n 9. View Project Detail\n 10. Quit\n");
            int option = 0;
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Some Error Occured");
            }
            return option;
        }

        public static void MainCall(int option)
        {
            switch (option)
            {
                case 1:
                    AddProject();
                    break;
                case 2:
                    DisplayProjectList();
                    int option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                case 3:
                    AddEmployee();
                    break;
                case 4:
                    DisplayEmployeeList();
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                case 5:
                    AddRole();
                    break;
                case 6:
                    DisplayRoleList();
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                case 7:
                    AddEmployeeToProject();
                    break;
                case 8:
                    DeleteEmployeeFromProject();
                    break;
                case 9:
                    DisplayProjectDetails();
                    option1  = DisplayMainMenu();
                    MainCall(option1);
                    break;
                case 10:
                    Console.WriteLine("Byeeeeeeeeee!!!!!!!!!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input!!!!!!Re-Enter");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
        }

        public static bool AddProject()
        {
            Project project = new();
            try
            {
                Console.Write("\nEnter following information to add new Project: \nEnter Project ID (Only Numeric) - ");
                project.ProjectId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Project Name - ");
                project.ProjectName = Console.ReadLine();
                while (int.TryParse(project.ProjectName, out _) || string.IsNullOrWhiteSpace(project.ProjectName))
                {
                    if (string.IsNullOrWhiteSpace(project.ProjectName))
                    {
                        Console.Write("\nProject Name Can't be Empty or WhiteSpace...! Input Project Name again...\nEnter Project Name - ");
                        project.ProjectName = Console.ReadLine();
                    }
                    else
                    {
                        Console.Write("Project Name Can't be a Number...! Input Project Name again...\nEnter Project Name - ");
                        project.ProjectName = Console.ReadLine();
                    }
                }
               Console.Write("Enter Project Start Date - ");
               project.OpenDate = Convert.ToDateTime(Console.ReadLine());
               Console.Write("Enter Project End Date - ");
               project.CloseDate = Convert.ToDateTime(Console.ReadLine());
               Console.Write("Enter the Budget - ");
               project.Budget = Convert.ToInt64(Console.ReadLine());
                while (project.Budget < 0)
                {
                    Console.Write("Budget Can't be Negative...! Input Project Name again...\nEnter the Budget - ");
                    project.Budget = Convert.ToInt64(Console.ReadLine());
                }    
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                int option1 = DisplayMainMenu();
                MainCall(option1);
            }
            var addProjectResult = Logic.AddProject(project);
            if (!addProjectResult.IsPositiveResult)
            {
                Console.WriteLine("\nAdding Project Details is not Successful\nProject already exists with id - " + project.ProjectId);
            }
            else
                Console.WriteLine(addProjectResult.Message);
            Console.WriteLine(@"Do you want to add more Projects? Y\N");
            char choice = Console.ReadKey().KeyChar;
            Console.Write("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    AddProject();
                    break;
                case 'N':
                    int option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
            return addProjectResult.IsPositiveResult;
        }

        public static void DisplayProjectList()
        {
            Console.WriteLine("--Projects Details are--\n");
            var displayProjects = Logic.DisplayProjects();
            if (displayProjects.IsPositiveResult)
            {
                Console.WriteLine("Project ID - Project Name -  Project Start Date - Project End Date - Project Budget\n" +
                                  "--------------------------------------------------------------------------------------");
                foreach (Project projectProperties in displayProjects.Results)
                    Console.WriteLine(projectProperties.ProjectId + "\t\t" + projectProperties.ProjectName + "\t\t" + projectProperties.OpenDate.ToShortDateString() + "\t" +
                        projectProperties.CloseDate.ToShortDateString() + "\t" + projectProperties.Budget) ;
            }
            else
                Console.WriteLine(displayProjects.Message);
        }

        public static bool AddEmployee()
        {
            Employee employee = new();
            try
            {
                Console.WriteLine("\nEnsuring Role List is there or not before adding an Employee to the Employee List...");
                var displayRoles = Logic.DisplayRoles();
                if (displayRoles.IsPositiveResult)
                {
                    Console.WriteLine("Available Roles in the List are --- \nID - Name:\n------------");
                    foreach (Role roleProperties in displayRoles.Results)
                        Console.WriteLine(roleProperties.RoleId + " - " + roleProperties.RoleName);
                }
                else
                {
                    Console.Write("\nPlease Enter Roles List First... Quiting from option 3. Add Employee\nRedirecting you to Add Role Option\n");
                    AddRole();
                }
                Console.Write("\nEnter following information to add new Employee:");
                Console.Write("\nEnter Employee Name - ");
                employee.EmployeeName = Console.ReadLine();
                while (int.TryParse(employee.EmployeeName, out _) || string.IsNullOrWhiteSpace(employee.EmployeeName))
                {
                    if (string.IsNullOrWhiteSpace(employee.EmployeeName))
                    {
                        Console.Write("Employee Name Can't be Empty or WhiteSpace...! Input Employee Name again...\nEnter Employee Name - ");
                        employee.EmployeeName = Console.ReadLine();
                    }
                    else
                    {
                        Console.Write("Employee Name Mustn't be a Number...! Input Employee Name again...\nEnter Employee Name - ");
                        employee.EmployeeName = Console.ReadLine();
                    }
                }
                Console.Write("Enter Employee ID (Only Numeric) - ");
                employee.EmployeeId = Convert.ToInt32(Console.ReadLine());
                loop: Console.Write("Enter 10 digit Mobile Number - ");
                employee.Contact = Console.ReadLine();
                var count = employee.Contact.Length;
                if (employee.Contact != null)
                {
                    if (count != 10)
                    {
                        Console.WriteLine("Mobile number must be within the range...");
                        goto loop;
                    }
                    else if (!Regex.Match(employee.Contact, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success)
                    {
                        Console.WriteLine("Invalid Mobile number..");
                        goto loop;
                    }
                }
                Console.Write("Enter Role id (Only Numeric) from above Role list - ");
                employee.EmployeeRoleId = Convert.ToInt32(Console.ReadLine());       
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                int option1 = DisplayMainMenu();
                MainCall(option1);
            }
            var employeeClassRoleIdResult = Logic.CheckEmployeeClassRoleId(employee);
            if (employeeClassRoleIdResult.IsPositiveResult)
            {
                var addEmployeeResult = Logic.AddEmployee(employee);
                if (!addEmployeeResult.IsPositiveResult)
                {
                    Console.WriteLine("\nAdding Employee Details is not Successful\nEmployee already exists with id - " + employee.EmployeeId);
                }
                else
                    Console.WriteLine(addEmployeeResult.Message);
            }
            else
                Console.WriteLine("\nAdding Employee Details is not Successful\nThe given Role id Doesn't Exists - " + employee.EmployeeRoleId);
            Console.WriteLine(@"Do you want to add more Employee? Y\N");
            char choice = Console.ReadKey().KeyChar;
            Console.Write("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    AddEmployee();
                    break;
                case 'N':
                    int option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
            return employeeClassRoleIdResult.IsPositiveResult;
        }

        public static void DisplayEmployeeList()
        {
            Console.WriteLine("--Employees Details are--\n");
            var displayEmployees = Logic.DisplayEmployees();
            if (displayEmployees.IsPositiveResult)
            {
                Console.WriteLine("Employee ID - Employee Name - Employee Contact\n" +
                    "--------------------------------------------------");
                foreach (Employee employeeProperties in displayEmployees.Results)
                    Console.WriteLine(employeeProperties.EmployeeId + "\t\t" + employeeProperties.EmployeeName + "\t\t" + employeeProperties.Contact);
            }
            else
                Console.WriteLine(displayEmployees.Message);
        }

         public static bool AddRole()
        {
            Role role = new();
            try
            {
                Console.Write("\nEnter following information to add new Role:\n");
                Console.Write("Enter Role Name - ");
                role.RoleName = Console.ReadLine();
                while (int.TryParse(role.RoleName, out _) || string.IsNullOrWhiteSpace(role.RoleName))
                {
                    if (string.IsNullOrWhiteSpace(role.RoleName))
                    {
                        Console.Write("Role Name Can't be Empty or WhiteSpace...! Input Role name again...\nEnter Role Name - ");
                        role.RoleName = Console.ReadLine();
                    }
                    else
                    {
                        Console.Write("Role Name Mustn't be Number.. Input Role name again...\nEnter Role Name - ");
                        role.RoleName = Console.ReadLine();
                    }
                }
            Console.Write("Enter Role ID (Only Numeric) - "); 
            role.RoleId = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                int option1 = DisplayMainMenu();
                MainCall(option1);
            }
            var addRoleResult = Logic.AddRole(role);
            if (!addRoleResult.IsPositiveResult)
            {
                Console.WriteLine("\nAdding Role Details is not Successful\nRole already exists with id - " + role.RoleId);
            }
            else
                Console.WriteLine(addRoleResult.Message);
            Console.WriteLine(@"Do you want to add more Roles? Y\N");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    AddRole();
                    break;
                case 'N':
                    int option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
            return addRoleResult.IsPositiveResult;
        }

        public static void DisplayRoleList()
        {
            Console.WriteLine("--Roles Details are--\n");
            var displayRoles = Logic.DisplayRoles();
            if (displayRoles.IsPositiveResult)
            {
                Console.WriteLine("Role ID - Role Name \n----------------------");
                foreach (Role roleProperties in displayRoles.Results)
                    Console.WriteLine(roleProperties.RoleId + "\t\t" + roleProperties.RoleName);
            }
            else
                Console.WriteLine(displayRoles.Message);
        }

        public static bool AddEmployeeToProject()
        {
            Employee employeeId = new();
            var displayEmployees = Logic.DisplayEmployees();
            var displayProjects = Logic.DisplayProjects();
            try
            {
                if (displayEmployees.IsPositiveResult && displayProjects.IsPositiveResult)
                {
                    Console.WriteLine("\nProjects in the List are --- \nID - Name\n-----------");
                    foreach (Project projectProperties in displayProjects.Results)
                        Console.WriteLine(projectProperties.ProjectId + " - " + projectProperties.ProjectName);
                    Console.Write("Select Project Id in which you want to add Employee - ");
                    var projectId = Convert.ToInt32(Console.ReadLine());
                    var projectIdResult = Logic.CheckProjectId(projectId);
                    if (projectIdResult.IsPositiveResult)
                    {
                        Console.WriteLine("Employees in the List are --- \nID - Name\n-------------");
                        foreach (Employee employeeProperties in displayEmployees.Results)
                            Console.WriteLine(employeeProperties.EmployeeId + " - " + employeeProperties.EmployeeName);
                        Console.Write("Choose Employee Id (Only Numeric) for this Project - ");
                        employeeId.EmployeeId = Convert.ToInt32(Console.ReadLine());
                        var EmployeeIdResult = Logic.CheckEmployeeId(employeeId);
                        if (EmployeeIdResult.IsPositiveResult)
                        {
                            var employeeRoleIdResult = Logic.CheckEmployeeRoleId(employeeId);
                            employeeId.EmployeeRoleId = employeeRoleIdResult.EmployeeRoleId;
                            employeeId.EmployeeName = employeeRoleIdResult.EmployeeName;
                            var addEmployeeToProjectResult = Logic.AddEmployeeToProject(projectId, employeeId);
                            if (!addEmployeeToProjectResult.IsPositiveResult)
                            {
                                Console.WriteLine("Adding Employee details by Role into Project not Successful\nProject id " + projectId + " Already contains this Employee id - " + employeeId.EmployeeId);
                            }
                            else
                               Console.WriteLine("Employee details by Role Successfully added to the project"); 
                        }
                        else
                            Console.WriteLine("The given employee id Doesn't Exists - " + employeeId.EmployeeId);
                    }
                    else
                        Console.WriteLine("The given Project id Doesn't Exists - " + projectId);
                }
                else
                {
                    Console.WriteLine("Employee or Project List might be Empty...!\n" +
                        "Quiting from Option 1. Add Employee to Project......");
                    if (!displayEmployees.IsPositiveResult) 
                        AddEmployee();
                    else  
                        AddProject();
                }   
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                int option1 = DisplayMainMenu();
                MainCall(option1);
            }
            Console.WriteLine(@"Do you want to add more Employee to the Project List? Y\N");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    AddEmployeeToProject();
                    break;
                case 'N':
                    int option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
            return displayEmployees.IsPositiveResult;
        }
        //public static bool ListEmployee()
        //{
        //    Project project = new();
        //    if (project.ListEmployee.Count > 0)
        //        return true;
        //    else
        //        return false;
        //}
        public static bool DeleteEmployeeFromProject()
        {
            Employee employeeId = new();
            var displayProjects = Logic.DisplayProjects();
            if (displayProjects.IsPositiveResult)
            {
                Console.WriteLine("List of Available Project details are...\nProject ID - Project Name\n" +
                "-----------------------------------");
                foreach (Project projectProperties in displayProjects.Results)
                    Console.WriteLine(projectProperties.ProjectId + "\t" + projectProperties.ProjectName);
                Console.Write("Select Project Id (Only Numeric) from which you want to delete Employee details - ");
                var projectId = Convert.ToInt32(Console.ReadLine());
                var projectIdResult = Logic.CheckProjectId(projectId);
                if (projectIdResult.IsPositiveResult)
                {
                    var getProjectIdResult = Logic.GetProjectId(projectId);
                    if (getProjectIdResult.ListEmployee!=null)
                    {
                        Console.Write("List of Available Employees in the given project id - " + projectId + "\nEmployee ID - Employee Name\n" +
                                "-----------------------------------\n");
                        
                            foreach (Employee employeeProperties in getProjectIdResult.ListEmployee)
                                Console.WriteLine(employeeProperties.EmployeeId + "\t" + employeeProperties.EmployeeName);
                            Console.Write("\nInput the Employee Id (Only Numeric) to delete - ");
                            employeeId.EmployeeId = Convert.ToInt32(Console.ReadLine());
                            var deleteEmployeeFromProjectResult = Logic.DeleteEmployeeFromProject(employeeId, projectId);
                            if (!deleteEmployeeFromProjectResult.IsPositiveResult)
                            {

                                Console.WriteLine(deleteEmployeeFromProjectResult.Message);
                            }
                            else
                                Console.WriteLine(deleteEmployeeFromProjectResult.Message);    
                    }
                    else
                    {
                        Console.WriteLine("\nThe given Project id Doesn't Contain Employee Details to Delete - " + employeeId.EmployeeId);
                        int option1 = DisplayMainMenu();
                        MainCall(option1);
                    }
                }
                else
                {
                    Console.WriteLine("\nThe given Project id Doesn't Exists - " + projectId);
                    int option1 = DisplayMainMenu();
                    MainCall(option1);
                }
            }
            else
            {
                Console.WriteLine("Noting to Delete.....!!!!! "+displayProjects.Message);
                int option1 = DisplayMainMenu();
                MainCall(option1);
            }
            Console.WriteLine(@"Do you want to Delete more Employees from Project? Y\N");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    DeleteEmployeeFromProject();
                    break;
                case 'N':
                    int option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
            return displayProjects.IsPositiveResult;
        }
        public static void DisplayProjectDetails()
        {
            var displayProjects = Logic.DisplayProjects();
            if (displayProjects.IsPositiveResult)
            {
                Console.WriteLine("Project Details with Employee Assigned by Role ......");
                foreach (Project projectProperties in displayProjects.Results)
                {
                        Console.WriteLine("\nProject ID - Project Name - Start Date - End Date - Budget\n" +
                            "--------------------------------------------------------------");
                        Console.WriteLine(projectProperties.ProjectId + "\t" + projectProperties.ProjectName + "\t" + projectProperties.OpenDate.ToShortDateString() + " " +
                            projectProperties.CloseDate.ToShortDateString() + "\t" + projectProperties.Budget);
                    if (projectProperties.ListEmployee != null)
                    {
                        Console.WriteLine("Assigned Employee details are.....\nEmployee Name - Employee Id - Role Id\n" +
                            "------------------------------------------");
                        foreach (Employee employeeProperties in projectProperties.ListEmployee)
                            Console.WriteLine(employeeProperties.EmployeeName + "\t\t" + employeeProperties.EmployeeId + "\t\t" + employeeProperties.EmployeeRoleId);
                    }
                    else
                        Console.WriteLine("\nEmployee list is empty....");
                }
            }
            else
                Console.WriteLine("Noting to View Project Details.....!!!!! "+displayProjects.Message);   
        }
        
    }
}
