using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;

namespace WPF_Company_Employees
{
    /// <summary>
    /// Класс для создания и использования тестовых данных
    /// </summary>
    public class Test
    {
        public SqlConnection connection = new SqlConnection();
        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();

        #region Variables
        ObservableCollection<string> employeesListString = new ObservableCollection<string>();

        public Test()
        {
            //CreateTestData();
            // SQL db Connection
            connectionString.DataSource = @"(LocalDB)\MSSQLLocalDB";
            connectionString.InitialCatalog = @"X:\GIT\Turchin_Michael_HomeWork_WPF\WPF_Company_Employees\WPF_Company_Employees\nedDB1\HWWPF.mdf"; // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=X:\GIT\Turchin_Michael_HomeWork_WPF\WPF_Company_Employees\WPF_Company_Employees\nedDB1\HWWPF.mdf;Integrated Security=True;Connect Timeout=30
            connectionString.IntegratedSecurity = true;
            connectionString.Pooling = false;

            connection = new SqlConnection(connectionString.ToString());

            connection.Open();
        }

        /// <summary>
        /// Сотрудник
        /// </summary>
        private Employee
            employee = new Employee(),
            employee2 = new Employee(),
            employee3 = new Employee(),
            employee4 = new Employee(),
            employee5 = new Employee(),
            employee6 = new Employee(),
            employee7 = new Employee();

        /// <summary>
        /// Список сотрудников, выходящих в отдел
        /// </summary>
        private ObservableCollection<Employee>
            employeesForDepartment1 = new ObservableCollection<Employee>(),
            employeesForDepartment2 = new ObservableCollection<Employee>(),
            employeesForDepartment3 = new ObservableCollection<Employee>();

        /// <summary>
        /// Отдел
        /// </summary>
        private Department
            department1 = new Department(),
            department2 = new Department(),
            department3 = new Department();

        ObservableCollection<Department> departments = new ObservableCollection<Department>();
                
        /// <summary>
        /// Список всех отделов
        /// </summary>
        public ObservableCollection<Department> Departments
        {
            get => departments;
        }
        #endregion

        /// <summary>
        /// Метод создающий тестовые данные
        /// </summary>
        public void CreateTestData()
        {
            employee = new Employee(Gender.M, new Full_Name("Михаил", "Турчин", "Николаевич"), DateTime.Today, new DateTime(1993, 8, 21), new Position(PositionName.Programmer, 125000), new Address("Россия", "Москва", "Москва", "Советская", 24, 26), "8-963-777-39-97", Status.Work);
            employee2 = new Employee(Gender.M, new Full_Name("Сергей", "Окребун", "Инистратович"), DateTime.Today, new DateTime(1983, 5, 8), new Position(PositionName.Manager, 100000), new Address("Россия", "Москва", "Москва", "Союзная", 45, 35), "8-634-434-43-64", Status.Trial_Period);
            employee3 = new Employee(Gender.M, new Full_Name("Агван", "Амансиев", "Анзавурович"), DateTime.Today, new DateTime(1973, 4, 17), new Position(PositionName.Secretary, 95000), new Address("Россия", "Москва", "Москва", "ВДНХ", 64, 22), "8-433-450-75-54", Status.Work);
            employee4 = new Employee(Gender.W, new Full_Name("Люсьен", "Котина", "Александровна"), DateTime.Today, new DateTime(1981, 3, 1), new Position(PositionName.Director, 250000), new Address("Россия", "Москва", "Москва", "Контемировская", 45, 64), "8-943-143-32-46", Status.Vacation);
            employee5 = new Employee(Gender.W, new Full_Name("Адела", "Смит"), DateTime.Today, new DateTime(1991, 3, 11), new Position(PositionName.Accountant, 105000), new Address("Россия", "Москва", "Москва", "Свиблово", 5, 25), "8-963-777-39-97", Status.Work);
            employee6 = new Employee(Gender.M, new Full_Name("Леорио", "Паладинайт"), DateTime.Today, new DateTime(1989, 12, 6), new Position(PositionName.Programmer, 125000), new Address("Россия", "Москва", "Москва", "Выставочная", 42, 35), "8-453-567-44-21", Status.Sick_Leave);
            employee7 = new Employee(Gender.M, new Full_Name("Айзек", "Кларк"), DateTime.Today, new DateTime(1986, 6, 27), new Position(PositionName.Programmer, 125000), new Address("Россия", "Москва", "Москва", "Сталинская", 65, 3), "8-965-374-35-87", Status.Work);

            int z = (int)employee.StatusNow;

            employeesForDepartment1 = new ObservableCollection<Employee>() { employee1, employee2, employee3 };
            employeesForDepartment2 = new ObservableCollection<Employee>() { employee6, employee7, employee4 };
            employeesForDepartment3 = new ObservableCollection<Employee>() { employee5 };

            departments.Add(new Department("First Department", employeesForDepartment1));
            departments.Add(new Department("Second Department", employeesForDepartment2));
            departments.Add(new Department("Third Department", employeesForDepartment3));


        }

        #region Methods for employees
        /// <summary>
        /// Метод добавляющий нового сотрудника в отдел
        /// </summary>
        /// <param name="numberDepartment">Номер отдела, в который необходимо добавить сотрудника</param>
        /// <param name="employee">Сотрудник</param>
        public void AddNewEmployee(int numberDepartment, Employee employee)
        {
            departments[numberDepartment].Employees.Add(employee);
        }

        /// <summary>
        /// Метод удаляющий сотрудника
        /// </summary>
        /// <param name="depComboSelectedIndex">Индекс выбранного в главном окне отдела</param>
        /// <param name="emplListSelectedIndex">Индекс выбранного в списке сотрудника из глановного окна</param>
        public void DeleteEmployee(int depComboSelectedIndex, int emplListSelectedIndex)
        {
            if (depComboSelectedIndex >= 0 && emplListSelectedIndex >= 0)
            {
                departments[depComboSelectedIndex].Employees.RemoveAt(emplListSelectedIndex);// departments_Combo.SelectedIndex
            }
        }

        /// <summary>
        /// Метод изменяющий информацию о сотруднике
        /// </summary>
        /// <param name="depComboSelectedIndex">Индекс выбранного в главном окне отдела</param>
        /// <param name="emplListSelectedIndex">Индекс выбранного в списке сотрудника из глановного окна</param>
        /// <param name="employee">Сотрудник, с измененной информацией</param>
        public void ChangeEmployeeInformation(int depComboSelectedIndex, int emplListSelectedIndex, Employee employee)
        {
            if (depComboSelectedIndex >= 0 && emplListSelectedIndex >= 0)
                departments[depComboSelectedIndex].Employees[emplListSelectedIndex] = employee;
        }

        /// <summary>
        /// Заполнение списка сотрудников
        /// </summary>
        /// <param name="depComboSelectedIndex">Индекс выбранного отдела из главного окна, список сотрудников которого, будет возвращен</param>
        /// <returns></returns>
        public ObservableCollection<string> EmployeesListString(int depComboSelectedIndex)
        {
            employeesListString.Clear();
            for (int i = 0; i < departments[depComboSelectedIndex].Employees.Count; i++)
            {
                employeesListString.Add(departments[depComboSelectedIndex].Employees[i].Full_Name.ToString());
            }
            return employeesListString;
        }
        #endregion

        #region Methods for departments
        /// <summary>
        /// Метод добавляющий новый отдел
        /// </summary>
        /// <param name="nameDepartment">Наименование отдела</param>
        public void AddNewDepartment(string nameDepartment)
        {
            departments.Add(new Department(nameDepartment));
        }

        /// <summary>
        /// Метод меняющий наименование отдела
        /// </summary>
        /// <param name="departmentNumber">Номер отдела, который необходимо переименовать</param>
        /// <param name="departmentName">Новое наименование отдела</param>
        public void ChangeDepartmentName(int departmentNumber, string departmentName)
        {
            departments[departmentNumber].DepartmentName = departmentName;
        }
        #endregion


        /// <summary>
        /// Метод переводящий сотрудников между отделами
        /// </summary>
        /// <param name="changEmplDepComboSelectedIndex">Индекс выбранного в окне информации о сотруднике отдела (отдел,  в который необходимо перевести сотрудника)</param>
        /// <param name="depComboSelectedIndex">Индекс выбранного в главном окне отдела (отдел,  из которого необходимо перевести сотрудника)</param>
        /// <param name="emplListSelectedIndex">Индекс выбранного в списке сотрудника из глановного окна (сотрудника, которого необходимо перевести в другой отдел)</param>
        public void transferEmployeeBetweenDepartments(int changEmplDepComboSelectedIndex, int depComboSelectedIndex, int emplListSelectedIndex)
        {
            departments[changEmplDepComboSelectedIndex].Employees.Add(departments[depComboSelectedIndex].Employees[emplListSelectedIndex]);
            departments[depComboSelectedIndex].Employees.RemoveAt(emplListSelectedIndex);
        }
    }
}