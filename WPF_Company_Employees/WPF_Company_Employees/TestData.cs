using System;
using System.Collections.Generic;

namespace WPF_Company_Employees
{
    /// <summary>
    /// Класс для создания и использования тестовых данных
    /// </summary>
    class Test
    {
        Employee
            employee1 = new Employee(),
            employee2 = new Employee(),
            employee3 = new Employee(),
            employee4 = new Employee(),
            employee5 = new Employee(),
            employee6 = new Employee(),
            employee7 = new Employee();

        List<Employee>
            employeesForDepartment1 = new List<Employee>(),
            employeesForDepartment2 = new List<Employee>(),
            employeesForDepartment3 = new List<Employee>();

        Department 
            department1 = new Department(),
            department2 = new Department(),
            department3 = new Department();

        public List<Department> departments = new List<Department>();
        
        /// <summary>
        /// Метод для создания тестовых данных
        /// </summary>
        public void CreateTestData()
        {
            employee1 = new Employee(Gender.M, new Full_Name("Михаил", "Турчин", "Николаевич"), DateTime.Today, new DateTime(1993, 8, 21), new Position(PositionName.Programmer, 125000), new Address("Россия", "Москва", "Москва", "Советская", 24, 26), "8-963-777-39-97", Status.Work);
            employee2 = new Employee(Gender.M, new Full_Name("Сергей", "Окребун", "Инистратович"), DateTime.Today, new DateTime(1983, 5, 8), new Position(PositionName.Manager, 100000), new Address("Россия", "Москва", "Москва", "Союзная", 45, 35), "8-634-434-43-64", Status.Trial_Period);
            employee3 = new Employee(Gender.M, new Full_Name("Агван", "Амансиев", "Анзавурович"), DateTime.Today, new DateTime(1973, 4, 17), new Position(PositionName.Secretary, 95000), new Address("Россия", "Москва", "Москва", "ВДНХ", 64, 22), "8-433-450-75-54", Status.Work);
            employee4 = new Employee(Gender.W, new Full_Name("Люсьен", "Котина", "Александровна"), DateTime.Today, new DateTime(1981, 3, 1), new Position(PositionName.Director, 250000), new Address("Россия", "Москва", "Москва", "Контемировская", 45, 64), "8-943-143-32-46", Status.Vacation);
            employee5 = new Employee(Gender.W, new Full_Name("Адела", "Смит"), DateTime.Today, new DateTime(1991, 3, 11), new Position(PositionName.Accountant, 105000), new Address("Россия", "Москва", "Москва", "Свиблово", 5, 25), "8-963-777-39-97", Status.Work);
            employee6 = new Employee(Gender.M, new Full_Name("Леорио", "Паладинайт"), DateTime.Today, new DateTime(1989, 12, 6), new Position(PositionName.Programmer, 125000), new Address("Россия", "Москва", "Москва", "Выставочная", 42, 35), "8-453-567-44-21", Status.Sick_Leave);
            employee7 = new Employee(Gender.M, new Full_Name("Айзек", "Кларк"), DateTime.Today, new DateTime(1986, 6, 27), new Position(PositionName.Programmer, 125000), new Address("Россия", "Москва", "Москва", "Сталинская", 65, 3), "8-965-374-35-87", Status.Work);

            employeesForDepartment1 = new List<Employee>() { employee1, employee2, employee3 };
            employeesForDepartment2 = new List<Employee>() { employee6, employee7, employee4 };
            employeesForDepartment3 = new List<Employee>() { employee5 };

            department1 = new Department("First Department", employeesForDepartment1);
            department2 = new Department("Second Department", employeesForDepartment2);
            department3 = new Department("Third Department", employeesForDepartment3);

            departments.Add(department1);
            departments.Add(department2);
            departments.Add(department3);
        }
    }
}