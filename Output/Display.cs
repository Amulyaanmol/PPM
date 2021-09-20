using System;
using Model;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Output
{
    public class Display
    {
        public static int DisplayMainMenu()
        {
            Console.WriteLine("\n---Select any option:---");
            Console.WriteLine(" 1. Add Project\n 2. View Projects\n 3. Add Employee \n 4. View Employees \n 5. Add Role \n 6. View Roles\n 7. Add Employee to Project\n 8. Delete Employee from Project\n 9. View Project Detail\n 10. Quit\n");
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
                    AddEmpToProject();
                    break;
                case 8:
                    DelEmpFromProject();
                    break;
                case 9:
                    DisplayProDetails();
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
            Project pro = new();
            try
            {
                Console.Write("\nEnter following information to add new Project: \nEnter Project ID (Only Numeric) - ");
                pro.proid = Convert.ToInt32(Console.ReadLine());
                loop: Console.Write("Enter Project Name - ");
                pro.name = Console.ReadLine();
                while (int.TryParse(pro.name, out _) || string.IsNullOrWhiteSpace(pro.name))
                {
                    if (string.IsNullOrWhiteSpace(pro.name))
                    {
                        Console.WriteLine("Project Name Can't be Empty...! Input your Project name again...");
                        goto loop;
                    }
                    Console.WriteLine("Project Name Mustn't be Number.. Input your Project name again...");
                    goto loop;
                }
                Console.Write("Enter Project Start Date - ");
                pro.sDate = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Enter Project End Date - ");
                pro.eDate = Convert.ToDateTime(Console.ReadLine());
                loop1:  Console.Write("Enter the Budget - ");
                pro.budget = Convert.ToInt64(Console.ReadLine());
                while (pro.budget < 0)
                {
                    Console.WriteLine("Budget Can't be Negative...! Input your Project name again...");
                    goto loop1;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                int option1 = DisplayMainMenu();
                MainCall(option1);
            }
            var r = Logic.AddProject(pro);
            if (!r.IsPositiveResult)
            {
                Console.WriteLine("Adding Project is not Successful");
                Console.WriteLine(r.Message);
            }
            else
                Console.WriteLine(r.Message);
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
            return r.IsPositiveResult;
        }
        public static void DisplayProjectList()
        {
            Console.WriteLine("--Projects Details are--\n");
            var r = Logic.DisplayProjects();
            if (r.IsPositiveResult)
            {
                Console.WriteLine("Project ID - Project Name -  Project Start Date - Project End Date - Project Budget\n" +
                                  "--------------------------------------------------------------------------------------");
                foreach (Project proj in r.results)
                    Console.WriteLine(proj.proid + "\t\t" + proj.name + "\t\t" + proj.sDate.ToShortDateString() + "\t\t" + 
                        proj.eDate.ToShortDateString() + "\t\t" + proj.budget) ;
            }
            else
                Console.WriteLine(r.Message);
        }
        public static bool AddEmployee()
        {
            Employee emp = new();
            try
            {
                Console.WriteLine("\nEnsuring Role List is there or not before adding an Employee to the Employee List...");
                var r1 = Logic.DisplayRoles();
                if (r1.IsPositiveResult)
                {
                    Console.WriteLine("Available Roles in the List are --- \nID - Name:\n------------");
                    foreach (Role R in r1.results)
                        Console.WriteLine(R.roleid + " - " + R.name);
                }
                else
                {
                    Console.WriteLine("Please Enter Roles List First... Quiting from option 3. Add Employee\nRedirecting you to Add Role Option");
                    AddRole();
                }
                Console.Write("\nEnter following information to add new Employee:");
                loop:  Console.Write("\nEnter Employee Name - ");
                emp.name = Console.ReadLine();
                while (int.TryParse(emp.name, out _) || string.IsNullOrWhiteSpace(emp.name))
                {
                    if (string.IsNullOrWhiteSpace(emp.name))
                    {
                        Console.WriteLine("Employee Name Can't be Empty...! Input your Employee name again...");
                        goto loop;
                    }
                    Console.WriteLine("Employee Name Mustn't be Number.. Input your Employee name again...");
                    goto loop;
                }
                Console.Write("Enter Emp ID (Only Numeric) - ");
                emp.empid = Convert.ToInt32(Console.ReadLine());
                loop1: Console.Write("Enter 10 digit Mobile Number - ");
                emp.contact = Console.ReadLine();
                if (emp.contact != null)
                {
                    var count = emp.contact.Length;
                    if (count != 10)
                    {
                        Console.WriteLine("mobile number must be within the range...");
                        goto loop1;
                    }
                    else if(!Regex.Match(emp.contact, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success)
                    {
                        Console.WriteLine("Invalid Mobile number..");
                        goto loop1;
                    }
                }
                Console.Write("Enter Role id (Only Numeric) from above Role list - ");
                emp.rol = Convert.ToInt32(Console.ReadLine());       
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                int option1 = DisplayMainMenu();
                MainCall(option1);
            }
            var ch = Logic.CheckRole(emp);
            if (ch.IsPositiveResult)
            {
                var r = Logic.AddEmployee(emp);
                if (!r.IsPositiveResult)
                {
                    Console.WriteLine("Adding Employee is not Successful");
                    Console.WriteLine(r.Message);
                }
                else Console.WriteLine(r.Message);
            }
            else
                Console.WriteLine(ch.Message);
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
            return ch.IsPositiveResult;
        }
        public static void DisplayEmployeeList()
        {
            Console.WriteLine("--Employees Details are--\n");
            var r = Logic.DisplayEmployees();
            if (r.IsPositiveResult)
            {
                Console.WriteLine("Employee ID - Employee Name - Employee Contact\n" +
                    "--------------------------------------------------");
                foreach (Employee emp in r.results)
                    Console.WriteLine(emp.empid + "\t\t" + emp.name + "\t\t" + emp.contact);
            }
            else
                Console.WriteLine(r.Message);
        }
         public static bool AddRole()
        {
            Role role = new();
            try
            {
                Console.Write("\nEnter following information to add new Role:\n");
                loop: Console.Write("Enter Role Name - ");
                role.name = Console.ReadLine();
                while (int.TryParse(role.name, out _) || string.IsNullOrWhiteSpace(role.name))
                {
                    if (string.IsNullOrWhiteSpace(role.name))
                    {
                        Console.WriteLine("Role Name Can't be Empty...! Input your Role name again...");
                        goto loop;
                    }
                    Console.WriteLine("Role Name Mustn't be Number.. Input your Role name again...");
                    goto loop;
                }
            Console.Write("Enter Role ID (Only Numeric) - "); 
            role.roleid = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                int option1 = DisplayMainMenu();
                MainCall(option1);
            }
            var r = Logic.AddRole(role);
            if (!r.IsPositiveResult)
            {
                Console.WriteLine("Adding Role is not Successful");
                Console.WriteLine(r.Message);
            }
            else
                Console.WriteLine(r.Message);
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
            return r.IsPositiveResult;
        }
        public static void DisplayRoleList()
        {
            Logic rolem = new();
            Console.WriteLine("--Roles Details are--\n");
            var r = Logic.DisplayRoles();
            if (r.IsPositiveResult)
            {
                Console.WriteLine("Role ID - Role Name \n----------------------");
                foreach (Role role in r.results)
                    Console.WriteLine(role.roleid + "\t\t" + role.name);
            }
            else
                Console.WriteLine(r.Message);
        }
        public static bool AddEmpToProject()
        {
            Employee eid = new();
            var r = Logic.DisplayEmployees();
            var r1 = Logic.DisplayProjects();
            try
            {
                if (r.IsPositiveResult && r1.IsPositiveResult)
                {
                    Console.WriteLine("\nProjects in the List are --- \nID - Name\n-----------");
                    foreach (Project P in r1.results)
                        Console.WriteLine(P.proid + " - " + P.name);
                    Console.Write("Select Project Id in which you want to add Employee - ");
                    var project_id = Convert.ToInt32(Console.ReadLine());
                    var cp = Logic.CheckPro(project_id);
                    if (cp.IsPositiveResult)
                    {
                        Console.WriteLine("Employees in the List are --- \nID - Name\n-------------");
                        foreach (Employee E in r.results)
                            Console.WriteLine(E.empid + " - " + E.name);
                        Console.Write("Choose employee for this Project - ");
                        eid.empid = Convert.ToInt32(Console.ReadLine());
                        var ce = Logic.CheckEmp(eid);
                        if (ce.IsPositiveResult)
                        {
                                var ger = Logic.GetEmpRole(eid);
                                eid.rol = ger.rol;
                                eid.name = ger.name;
                                var aep = Logic.AddEmpToProject(project_id,eid);
                                if (!aep.IsPositiveResult)
                                {
                                    Console.WriteLine("Adding Employee into project not Successful");
                                    Console.WriteLine(aep.Message);
                                }
                                else
                                    Console.WriteLine(aep.Message); 
                        }
                        else
                            Console.WriteLine(ce.Message);
                    }
                    else
                        Console.WriteLine(cp.Message);
                }
                else
                {
                    Console.WriteLine("Employee or Project List might be empty...!\n" +
                        "Quiting from Option 1. Add Employee to Project......");
                    if (!r.IsPositiveResult) AddEmployee();
                    else  AddProject();
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
                    AddEmpToProject();
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
            return r.IsPositiveResult;
        }
        public static bool DelEmpFromProject()
        {
            Employee employee = new();
            Console.WriteLine("List of Available Project details are...\nProject ID - Project Name\n" +
                "-----------------------------------");
            var dp = Logic.DisplayProjects();
            if (dp.IsPositiveResult)
               foreach (Project d in dp.results)
                    Console.WriteLine(d.proid + "\t" + d.name );
            else
               Console.WriteLine(dp.Message);
            Console.Write("Select Project id from which you want to delete Employee details- ");
            var project_id = Convert.ToInt32(Console.ReadLine());
            Console.Write("List of Available Employees in the given project id - "+project_id+ "\nEmployee ID - Employee Name\n" +
                "-----------------------------------\n");
            var del = Logic.GetProId(project_id);
            foreach (Employee e in del.Emp)
             if (del.Emp != null)
                    Console.WriteLine(e.empid + "\t" + e.name);
             else
                    Console.WriteLine("empty employee list...");
            Console.Write("\nInput the employee id to delete - ");
            employee.empid = Convert.ToInt32(Console.ReadLine());
            var r = Logic.DelEmpFromProject(employee,project_id);
            if (!r.IsPositiveResult)
                Console.WriteLine(r.Message);
             else
                Console.WriteLine(r.Message);
            Console.WriteLine(@"Do you want to add more Employee? Y\N");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    DelEmpFromProject();
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
            return r.IsPositiveResult;
        }
       public static void DisplayProDetails()
       {
            Console.WriteLine("Project Details with Employee Assigned by Role ......");
            var c = Logic.DisplayProjects();
            if (c.IsPositiveResult)
            {
                foreach (Project p in c.results)
                {
                        Console.WriteLine("\nProject ID - Project Name - Start Date - End Date - Budget\n" +
                            "--------------------------------------------------------------");
                        Console.WriteLine(p.proid + "\t" + p.name + "\t" + p.sDate.ToShortDateString() + " " +
                            p.eDate.ToShortDateString() + "\t" + p.budget);
                    if (p.Emp != null)
                    {
                        Console.WriteLine("Assigned Employee details are.....\nEmployee Name - Employee Id - Role Id\n" +
                            "------------------------------------------");
                        foreach (Employee e in p.Emp)
                            Console.WriteLine(e.name + "\t\t" + e.empid + "\t\t" + e.rol);
                    }
                    else
                        Console.WriteLine("\nEmployee list is empty....");
                }
            }
            else
                Console.WriteLine(c.Message);   
       }
    }
}
