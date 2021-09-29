using System;
using Model;
using Domain;
using System.Text.RegularExpressions;

namespace Output
{
    public static class Display
    {
        
        public static int DisplayMainMenu()
        {
            Console.WriteLine("\n---SELECT ANY OPTION FROM BELOW---\n 1. Project Module\n 2. Employee Module \n 3. Role Module\n 4. Quit\n");
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
            int option1;
            switch (option)
            {
                case 1:
                    try
                    {
                        Console.Write("\n---PROJECT MODULE---\n 1.1 Add\n 1.2 List All\n 1.3 List By Id\n 1.4 Delete\n 1.5 Return to Main Menu\n\nEnter Your Choice - ");
                        double option2 = Convert.ToDouble(Console.ReadLine());
                        if (option2 == 1.1)
                            AddProject();
                        else if (option2 == 1.2)
                        {
                            DisplayProjectList();
                            MainCall(1);
                        }
                        else if (option2 == 1.3)
                        {
                            DisplayProjectListById();
                            MainCall(1);
                        }
                        else if (option2 == 1.4)
                            DeleteProject();
                        else if (option2 == 1.5)
                        {
                            Console.WriteLine("Redirecting you to Main Menu...");
                            option1 = DisplayMainMenu();
                            MainCall(option1);
                        }
                        else
                        {
                            Console.WriteLine("Oops! Incorrect Choice...");
                            MainCall(1);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Oops! Incorrect Choice...");
                        MainCall(1);
                    }
                    break;
                case 2:
                    try
                    {
                        Console.Write("\n---EMPLOYEE MODULE---\n 2.1 Add\n 2.2 List All\n 2.3 List By Id\n 2.4 Delete\n 2.5 Return to Main Menu\n\nEnter Your Choice - ");
                        double option3 = Convert.ToDouble(Console.ReadLine());
                        if (option3 == 2.1)
                            AddEmployee();
                        else if (option3 == 2.2)
                        {
                            DisplayEmployeeList();
                            MainCall(2);
                        }
                        else if(option3==2.3)
                        {
                            DisplayEmployeeListById();
                            MainCall(2);
                        }
                        else if (option3 == 2.4)
                            DeleteEmployee();
                        else if (option3 == 2.5)
                        {
                            Console.WriteLine("Redirecting you to Main Menu...");
                            option1 = DisplayMainMenu();
                            MainCall(option1);
                        }
                        else
                        {
                            Console.WriteLine("Oops! Incorrect Choice...");
                            MainCall(2);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Oops! Incorrect Choice...");
                        MainCall(2);
                    }
                     break;
                case 3:
                    try
                    {
                        Console.Write("\n---ROLE MODULE---\n 3.1 Add\n 3.2 List All\n 3.3 List By Id\n 3.4 Delete\n 3.5 Return to Main Menu\n\nEnter Your Choice - ");
                        double option4 = Convert.ToDouble(Console.ReadLine());
                        if (option4 == 3.1)
                            AddRole();
                        else if (option4 == 3.2)
                        {
                            DisplayRoleList();
                            MainCall(3);
                        }
                        else if (option4 == 3.3)
                        {
                            DisplayRoleListById();
                            MainCall(3);
                        }
                        else if (option4 == 3.4)
                            DeleteRole();
                        else if (option4 == 3.5)
                        {
                            Console.WriteLine("Redirecting you to Main Menu...");
                            option1 = DisplayMainMenu();
                            MainCall(option1);
                        }
                        else
                        {
                            Console.WriteLine("Oops! Incorrect Choice...");
                            MainCall(3);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Oops! Incorrect Choice...");
                        MainCall(3);
                    }
                    break;
                case 4:
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

        public static bool AddEmployeeToProject(int _projectId)
        {
            Employee employeeId = new();
            var projectIdResult = Logic.CheckProjectId(_projectId);
            try
            {
                if (projectIdResult.IsPositiveResult)
                {
                    Console.Write("Choose Employee Id (Only Numeric) for this Project - ");
                    employeeId.EmployeeId = Convert.ToInt32(Console.ReadLine());
                    var EmployeeIdResult = Logic.CheckEmployeeId(employeeId);
                    if (EmployeeIdResult.IsPositiveResult)
                    {
                        var employeeRoleIdResult = Logic.CheckEmployeeRoleId(employeeId);
                        employeeId.EmployeeRoleId = employeeRoleIdResult.EmployeeRoleId;
                        employeeId.EmployeeName = employeeRoleIdResult.EmployeeName;
                        var addEmployeeToProjectResult = Logic.AddEmployeeToProject(_projectId, employeeId);
                        if (!addEmployeeToProjectResult.IsPositiveResult)
                            Console.WriteLine("Adding Employee details by Role into Project not Successful\nProject id " + _projectId + " Already contains this Employee id - " + employeeId.EmployeeId);
                        else
                            Console.WriteLine("Employee Successfully added to the project");
                    }
                    else
                        Console.WriteLine("The given employee id Doesn't Exists - " + employeeId.EmployeeId);
                }
                else
                    Console.WriteLine("The given Project id Doesn't Exists - " + _projectId);
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(1);
            }
            return projectIdResult.IsPositiveResult;
        }

        public static bool AddProject()
        {
            Project project = new();
            var displayEmployees = Logic.DisplayEmployees();
            try
            {
                if (displayEmployees.IsPositiveResult)
                {
                    Console.Write("\nEnter following information to add new Project:\nEnter Project ID (Only Numeric) - ");
                    project.ProjectId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Project Name - ");
                    project.ProjectName = Console.ReadLine();
                    while (int.TryParse(project.ProjectName, out _) || string.IsNullOrWhiteSpace(project.ProjectName))
                    {
                        if (string.IsNullOrWhiteSpace(project.ProjectName))
                        {
                            Console.Write("Project Name Can't be Empty or WhiteSpace...! Input Project Name again...\nEnter Project Name - ");
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
                    Console.Write(@"Do you want to add Employee to the Project List? Y\N - ");
                    char choice = Console.ReadKey().KeyChar;
                    Console.WriteLine("\n");
                    switch (char.ToUpper(choice))
                    {
                        case 'Y':
                                Console.WriteLine("Available Employees in the List are --- \nID - Name\n------------");
                                foreach (Employee employeeProperties in displayEmployees.Results)
                                    Console.WriteLine(employeeProperties.EmployeeId + " - " + employeeProperties.EmployeeName);
                                Console.Write("How Many Employees you want add to this Project - ");
                                int count = Convert.ToInt32(Console.ReadLine());
                                var countResult = Logic.CheckCount(count);
                                if (countResult.IsPositiveResult)
                                {
                                    var addProjectResult = Logic.AddProject(project);
                                    if (addProjectResult.IsPositiveResult)
                                    {
                                        for (int i = 1; i <= count; i++)
                                            AddEmployeeToProject(project.ProjectId);
                                        Console.WriteLine(addProjectResult.Message + " With Employee details by Role\n");
                                    }
                                    else
                                        Console.WriteLine("\nAdding Project Details is not Successful\nProject already exists with id - " + project.ProjectId);
                                }
                                else
                                    Console.WriteLine("\nAdding Project Details is not Successful\nEnter the Correct Count to add Employee...!!!!!");
                            break;
                        case 'N':
                            var addProjectResults = Logic.AddProject(project);
                            if (addProjectResults.IsPositiveResult)
                                Console.WriteLine(addProjectResults.Message + " Without Employee details...");
                            else
                                Console.WriteLine("\nAdding Project Details is not Successful\nProject already exists with id - " + project.ProjectId);
                            break;
                        default:
                            Console.WriteLine("Some Error Occured!! Please select right option");
                            MainCall(1);
                            break;
                    }
                } 
                else Console.WriteLine("Empty Employee List....\nAdding Project Details with Employee details is not Successful");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to Project Module");
                MainCall(1);
            }
            Console.WriteLine(@"Do you want to add more Projects? Y\N");
            char choice1 = Console.ReadKey().KeyChar;
            Console.Write("\n");
            switch (char.ToUpper(choice1))
            {
                case 'Y':
                    AddProject();
                    break;
                case 'N':
                    MainCall(1);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(1);
                    break;
            }
            return true;
        }

        public static void DisplayProjectList()
        {  
            var displayProjects = Logic.DisplayProjects();
            if (displayProjects.IsPositiveResult)
            {
                    Console.WriteLine("--Projects Details with Employee Assigned by Role are--\n");
                foreach (Project projectProperties in displayProjects.Results)
                {
                    Console.WriteLine("Project ID - Project Name -  Project Start Date - Project End Date - Project Budget\n" +
                                  "--------------------------------------------------------------------------------------");
                    Console.WriteLine(projectProperties.ProjectId + "\t\t" + projectProperties.ProjectName + "\t\t" + projectProperties.OpenDate.ToShortDateString() + "\t" +
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
                Console.WriteLine("Noting to View Project Details.....!!!!! ");
        }
        
        public static void DisplayProjectListById()
        {
            try
            {
                var displayProjects = Logic.DisplayProjects();
                if (displayProjects.IsPositiveResult)
                {
                    Console.WriteLine("Project ID\n---------------");
                    foreach (Project projectProperties in displayProjects.Results)
                        Console.WriteLine(projectProperties.ProjectId);
                    Console.Write("\nSelect Project Id to Display Full details of Respective Id from the List - ");
                    var projectId = Convert.ToInt32(Console.ReadLine());
                    var projectIdResult = Logic.CheckProjectId(projectId);
                    if (projectIdResult.IsPositiveResult)
                    {
                        var projectsByIdResult = Logic.DisplayProjectsById(projectId);
                        Console.WriteLine("\nProject ID - Project Name -  Project Start Date - Project End Date - Project Budget\n" +
                                              "--------------------------------------------------------------------------------------");
                        Console.WriteLine(projectsByIdResult.ProjectId + "\t\t" + projectsByIdResult.ProjectName + "\t\t" + projectsByIdResult.OpenDate.ToShortDateString() + "\t" +
                                            projectsByIdResult.CloseDate.ToShortDateString() + "\t" + projectsByIdResult.Budget);
                        var getProjectIdResult = Logic.GetProjectId(projectId);
                        if (getProjectIdResult.ListEmployee != null)
                        {
                            Console.WriteLine("\n\nEmployee ID - Employee Name - Role Id\n" +
                                    "-----------------------------------");
                            foreach (Employee employeeProperties in getProjectIdResult.ListEmployee)
                                Console.WriteLine(employeeProperties.EmployeeId + "\t\t" +  employeeProperties.EmployeeName + "\t\t" + employeeProperties.EmployeeRoleId);
                        }
                        else
                            Console.WriteLine("\nEmployee list is empty....");
                    }
                    else
                        Console.WriteLine("Project Id Doesn't Exists....");
                }
                else
                    Console.WriteLine("\n.....Project List is Empty.....");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(1);
            }
        }

        public static bool DeleteProject()
        {
            Project project = new();
            var displayProjects = Logic.DisplayProjects();
            if (displayProjects.IsPositiveResult)
            {
                Console.WriteLine("List of Available Project details are...\nProject ID - Project Name\n" +
                "-----------------------------------");
                foreach (Project projectProperties in displayProjects.Results)
                    Console.WriteLine(projectProperties.ProjectId + "\t" + projectProperties.ProjectName);
                Console.Write("Input the Project Id (Only Numeric) to Delete - ");
                project.ProjectId = Convert.ToInt32(Console.ReadLine());
                var projectIdResult = Logic.CheckProjectId(project.ProjectId);
                if (projectIdResult.IsPositiveResult)
                {
                    var deleteProjectResult = Logic.DeleteProject(project);
                    if (!deleteProjectResult.IsPositiveResult)
                    {
                        Console.WriteLine(deleteProjectResult.Message);  
                    }
                    else
                        Console.WriteLine(deleteProjectResult.Message);
                }
                else
                    Console.WriteLine("Project Id Doesn't Exists....");
            }
            else
                Console.WriteLine("\n.....Project List is Empty.....\n");
            Console.WriteLine(@"Do you want to Delete more Projects? Y\N");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    DeleteProject();
                    break;
                case 'N':
                    MainCall(1);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(1);
                    break;
            }
            return displayProjects.IsPositiveResult;
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
                    Console.WriteLine("Available Roles in the List are --- \nID - Name\n------------");
                    foreach (Role roleProperties in displayRoles.Results)
                        Console.WriteLine(roleProperties.RoleId + " - " + roleProperties.RoleName);
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
                    Console.Write("Enter 10 digit Mobile Number - ");
                    employee.Contact = Console.ReadLine();
                    if (employee.Contact != null)
                    {
                        while ((!Regex.Match(employee.Contact, @"^\d[0-9]{10}\d$").Success) && (!Regex.Match(employee.Contact, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success))
                        {
                            Console.WriteLine("Invalid Mobile number...");
                            Console.Write("Enter 10 digit Mobile Number - ");
                            employee.Contact = Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Mobile number...");
                        Console.Write("Enter 10 digit Mobile Number - ");
                        employee.Contact = Console.ReadLine();
                    }
                    Console.Write("Enter Role id (Only Numeric) from above Role list - ");
                    employee.EmployeeRoleId = Convert.ToInt32(Console.ReadLine());
                }
                else
                    Console.Write("Empty Role List....");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(2);
            }
            var employeeClassRoleIdResult = Logic.CheckEmployeeClassRoleId(employee);
            if (employeeClassRoleIdResult.IsPositiveResult)
            {
                var addEmployeeResult = Logic.AddEmployee(employee);
                if (!addEmployeeResult.IsPositiveResult)
                    Console.WriteLine("\nAdding Employee Details is not Successful\nEmployee already exists with id - " + employee.EmployeeId);
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
                    MainCall(2);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(2);
                    break;
            }
            return employeeClassRoleIdResult.IsPositiveResult;
        }

        public static void DisplayEmployeeList()
        {  
            var displayEmployees = Logic.DisplayEmployees();
            if (displayEmployees.IsPositiveResult)
            {
                Console.WriteLine("--Employees Details are--\nEmployee ID - Employee Name - Employee Contact\n" +
                        "--------------------------------------------------");
                foreach (Employee employeeProperties in displayEmployees.Results)
                    Console.WriteLine(employeeProperties.EmployeeId + "\t\t" + employeeProperties.EmployeeName + "\t\t" + employeeProperties.Contact);
            }
            else
                Console.WriteLine("\n.....Employee List is Empty.....");
        }

        public static void DisplayEmployeeListById()
        {
            Employee employee = new();
            try
            {
                var displayEmployees = Logic.DisplayEmployees();
                if (displayEmployees.IsPositiveResult)
                {
                    Console.WriteLine("Employee ID\n------------");
                    foreach (Employee employeeProperties in displayEmployees.Results)
                        Console.WriteLine(employeeProperties.EmployeeId);
                    Console.Write("\nSelect Employee Id to Display Full details of Respective Id from the List - ");
                    employee.EmployeeId = Convert.ToInt32(Console.ReadLine());
                    var employeeIdResult = Logic.CheckEmployeeId(employee);
                    if (employeeIdResult.IsPositiveResult)
                    {
                        var employeesByIdResult = Logic.DisplayEmployeesById(employee);
                        Console.WriteLine("\nEmployee ID - Employee Name - Employee Contact\n" +
                          "--------------------------------------------------");
                        Console.WriteLine(employeesByIdResult.EmployeeId + "\t\t" + employeesByIdResult.EmployeeName + "\t\t" + employeesByIdResult.Contact);
                    }
                    else
                        Console.WriteLine("Employee Id Doesn't Exists....");
                }
                else
                    Console.WriteLine("\n.....Employee List is Empty.....");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(2);
            }
        }

        public static bool DeleteEmployee()
        {
            Employee employee = new();
            var displayEmployees = Logic.DisplayEmployees();
            if (displayEmployees.IsPositiveResult)
            {
                Console.WriteLine("List of Available Employee details are...\nEmployee ID - Employee Name\n" +
                "-----------------------------------");
                foreach (Employee employeeProperties in displayEmployees.Results)
                    Console.WriteLine(employeeProperties.EmployeeId + "\t" + employeeProperties.EmployeeName);
                Console.Write("Input the Employee Id (Only Numeric) to Delete - ");
                employee.EmployeeId = Convert.ToInt32(Console.ReadLine());
                var employeeIdResult = Logic.CheckEmployeeId(employee);
                if (employeeIdResult.IsPositiveResult)
                {
                    var deleteemployeeResult = Logic.DeleteEmployee(employee);
                    if (!deleteemployeeResult.IsPositiveResult)
                    {

                        Console.WriteLine(deleteemployeeResult.Message);
                    }
                    else
                        Console.WriteLine(deleteemployeeResult.Message);
                }
                else
                    Console.WriteLine("Employee Id Doesn't Exists....");
            }
            else
                Console.WriteLine("\n.....Employee List is Empty.....\n");
            Console.WriteLine(@"Do you want to Delete more Employees? Y\N");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    DeleteEmployee();
                    break;
                case 'N':
                    MainCall(2);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(2);
                    break;
            }
            return displayEmployees.IsPositiveResult;
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
                MainCall(3);
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
                    MainCall(3);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(3);
                    break;
            }
            return addRoleResult.IsPositiveResult;
        }

        public static void DisplayRoleList()
        {
            var displayRoles = Logic.DisplayRoles();
            if (displayRoles.IsPositiveResult)
            {
                Console.WriteLine("--Roles Details are--\nRole ID - Role Name \n----------------------");
                foreach (Role roleProperties in displayRoles.Results)
                    Console.WriteLine(roleProperties.RoleId + "\t\t" + roleProperties.RoleName);
            }
            else
                Console.WriteLine("\n.....Role List is Empty.....");
        }

        public static void DisplayRoleListById()
        {
            try
            {
                var displayRoles = Logic.DisplayRoles();
                if (displayRoles.IsPositiveResult)
                {
                    Console.WriteLine("Role ID\n---------");  
                    foreach (Role roleProperties in displayRoles.Results)
                        Console.WriteLine(roleProperties.RoleId);
                    Console.Write("\nSelect Role Id to Display Full details of Respective Id from the List - ");
                    var roleId = Convert.ToInt32(Console.ReadLine());
                    var roleIdResult = Logic.CheckRoleId(roleId);
                    if (roleIdResult.IsPositiveResult)
                    {
                        var rolesByIdResult = Logic.DisplayRolesById(roleId);
                        Console.WriteLine("\nRole ID - Role Name \n----------------------");
                        Console.WriteLine(rolesByIdResult.RoleId + "\t\t" + rolesByIdResult.RoleName);
                    }
                    else
                        Console.WriteLine("Role Id Doesn't Exists....");
                }
                else
                    Console.WriteLine("\n.....Role List is Empty.....");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(3);
            }
        }

        public static bool DeleteRole()
        {
            Role role = new();
            var displayRoles = Logic.DisplayRoles();
            if (displayRoles.IsPositiveResult)
            {
                Console.WriteLine("List of Available Role details are...\nRole ID - Role Name\n" +
                "-----------------------------------");
                foreach (Role roleProperties in displayRoles.Results)
                    Console.WriteLine(roleProperties.RoleId + "\t" + roleProperties.RoleName);
                Console.Write("Input the Role Id (Only Numeric) to Delete - ");
                role.RoleId = Convert.ToInt32(Console.ReadLine());
                var roleIdResult = Logic.CheckRoleId(role.RoleId);
                if (roleIdResult.IsPositiveResult)
                {
                    var deleteRoleResult = Logic.DeleteRole(role);
                    if (!deleteRoleResult.IsPositiveResult)
                    {
                        Console.WriteLine(deleteRoleResult.Message);
                    }
                    else
                        Console.WriteLine(deleteRoleResult.Message);
                }
                else
                    Console.WriteLine("Role Id Doesn't Exists....");
            }
            else
                Console.WriteLine("\n.....Role List is Empty.....\n");
            Console.WriteLine(@"Do you want to Delete more Roles? Y\N");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    DeleteRole();
                    break;
                case 'N':
                    MainCall(3);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(3);
                    break;
            }
            return displayRoles.IsPositiveResult;
        }

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

    }
}
