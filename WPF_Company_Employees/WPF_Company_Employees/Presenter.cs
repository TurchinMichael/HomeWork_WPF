using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WPF_Company_Employees
{
    public class Presenter : INotifyPropertyChanged
    {

        public Test test;
        private IView view;
        private IViewForNewEmployee addNewEmployee;
        private IViewNewDepartment addNewDepartment;

        public Presenter(IView View)
        {
            this.view = View;
            test = new Test();

            // Заполнение стандартной информацией
            fillCommonInformation();
            fillDepartmentCombo();
        }

        /// <summary>
        /// Заполнение списка departmentsNames именами отделов
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> departmentsNames
        {
            get
            {
                ObservableCollection<string> temp = new ObservableCollection<string>();

                foreach (var obj in test.Departments)
                {
                    temp.Add(obj.DepartmentName);
                }
                //this.NotifyPropertyChanged("departmentsNames");
               // NotifyPropertyChanged(nameof(this.departmentsNames));

                return temp;
            }
        }

        /// <summary>
        /// Заполнение списка отделов в главном окне
        /// </summary>
        public void fillDepartmentCombo()
        {
            view.departmentList = usualMoves($@"select * from [Department Name]", 1);
        }

        /// <summary>
        /// Заполнение списка отделов в окне добавления нового сотрудника
        /// </summary>
        public void fillDepartmentComboAddEmployee(/*AddEmployee AddEmployeeForm*/)
        {
            addNewEmployee.departmentList = usualMoves($@"select * from [Department Name]", 1);
        }

        /// <summary>
        /// Возвращает коллекцию создавая её из строк в БД
        /// </summary>
        /// <param name="s">Запрос возвращающий необходимые строки</param>
        /// <param name="columnIndex">Номер необходимой колонки</param>
        ICollection<string> usualMoves(string s, int columnIndex)
        {
            ICollection<string> t = new List<string>();
            SqlCommand commandRevInformation = new SqlCommand(s, test.connection);
            SqlDataReader reader = commandRevInformation.ExecuteReader();

            while (reader.Read())
            {
                t.Add(reader.GetString(columnIndex));
            }
            reader.Close();
            return t;
        }

        public class MyItem
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        /// <summary>
        /// Возвращает коллекцию создавая её из строк в БД
        /// </summary>
        /// <param name="s">Запрос возвращающий необходимые строки</param>
        void usualMovesForString(string s)
        {
            view.employeesViewItems.Clear();
            //List<string> t = new List<string>();
            SqlCommand commandRevInformation = new SqlCommand(s, test.connection);
            SqlDataReader reader = commandRevInformation.ExecuteReader();

            while (reader.Read())
            {
                if (!reader.IsDBNull(3)) // т.к. отчество может быть Null
                {
                    //t.Add(reader.GetString(1) + reader.GetString(2) + reader.GetString(3));
                    view.employeesViewItems.Add(new MyItem { Id = reader.GetInt32(0), Name = (reader.GetString(1) + reader.GetString(2) + reader.GetString(3)) });
                }
                else
                {
                    //t.Add(reader.GetString(1) + reader.GetString(2));
                    view.employeesViewItems.Add(new MyItem { Id = reader.GetInt32(0), Name = (reader.GetString(1) + reader.GetString(2)) });
                }
            }
            reader.Close();
            //return t;
        }

        /// <summary>
        /// Заполнение стандартной информации для Combo Box
        /// </summary>
        public void fillCommonInformation()
        {
            view.statusList = usualMoves($@"select * from Status", 1);
            view.genderList = usualMoves($@"select * from Gender", 1);
            view.positionList = usualMoves($@"select * from PositionName", 1);
        }

        /// <summary>
        /// Заполнение стандартной информации для Combo Box
        /// </summary>
        public void fillCommonInformationAddEmployee()
        {
            addNewEmployee.statusList = usualMoves($@"select * from Status", 1);
            addNewEmployee.genderList = usualMoves($@"select * from Gender", 1);
            addNewEmployee.positionList = usualMoves($@"select * from PositionName", 1);
        }


        /// <summary>
        /// Метод заполняющий лист сотрудников выбранного отдела
        /// </summary>
        public void fillEmployeesList()
        {
            if (view.SelectedDepartment >= 0)
            {
                /*view.employeeList = */
                usualMovesForString($@"select Employee.Id, FullName.Last_Name, FullName.First_Name, FullName.Patronymic from Department inner join Employee on Department.Employee = Employee.Id inner join[Department Name] on Department.[Department Name] = [Department Name].Id inner join FullName on FullName.Id = Employee.[Full Name] where[Department Name] = {view.SelectedDepartment + 1}");
            }
        }

        /// <summary>
        /// Метод вызываемый при смене отдела в окне сотрудника
        /// Метод переводящий сотрудника между отделами
        /// </summary>
        public void Change_Employee_Department()
        {
            if (view.SelectedEmployee >= 0
                && view.SelectedDepartment >= 0
                && view.Selected_Change_Employee_Department_Combo >= 0
                && view.SelectedDepartment != view.Selected_Change_Employee_Department_Combo)
            {
                var sqlRevTranferEmployee = $"UPDATE Department SET [Department Name] = {view.Selected_Change_Employee_Department_Combo + 1} WHERE (Employee = {(view.employeesViewItems.GetItemAt(view.SelectedEmployee) as MyItem).Id})";


                SqlCommand command = new SqlCommand(sqlRevTranferEmployee, test.connection);

                command.ExecuteNonQuery();


                //test.transferEmployeeBetweenDepartments(view.Selected_Change_Employee_Department_Combo, view.SelectedDepartment, view.SelectedEmployee);

                view.SelectedDepartment = view.Selected_Change_Employee_Department_Combo;

                //view.SelectedEmployee = test.Departments[view.Selected_Change_Employee_Department_Combo].Employees.Count - 1;
            }
        }

        void GetEmployee(string s)
        {
            //Employee tempEmployee;
            SqlCommand commandRevInformation = new SqlCommand(s, test.connection);
            SqlDataReader reader = commandRevInformation.ExecuteReader();
            string TempStatus = string.Empty;

            while (reader.Read())
            {
                view.GenderEmployee = reader.GetInt32(1) - 1;
                view.FirstName = reader.GetString(2);
                view.LastName = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    view.Patronymic = reader.GetString(4);
                view.EmploymentDate = reader.GetDateTime(5);
                view.DateOfBirth = reader.GetDateTime(6);
                view.PositionNameEnum = reader.GetInt32(7) - 1;
                view.Salary = reader.GetDecimal(8);
                view.County = reader.GetString(9);
                view.Region = reader.GetString(10);
                view.City = reader.GetString(11);
                view.Street = reader.GetString(12);
                view.StreetNumber = reader.GetInt32(13);
                view.ApartmentNumber = reader.GetInt32(14);
                view.PhoneNumber = reader.GetString(15);
                TempStatus = reader.GetString(16);
            }
            reader.Close();

            //commandRevInformation.CommandText = $@"select Status.Id from Status where Status.Status = '{TempStatus}'";
            ////bool k = view.statusList.Contains(TempStatus);
            //reader = commandRevInformation.ExecuteReader();
            int x = 0;
            for (int i = 0; i < view.statusList.Count; i++)
            {
                if ((view.statusList as List<string>)[i] == TempStatus)
                    x = i;
            }
            //int item = view.statusList.Where(z => z.ID == 12).FirstOrDefault();
            view.StatusNow = x;

            //while (reader.Read())
            //{
            //    view.StatusNow = reader.GetInt32(0) - 1; // получить id с имени
            //}
           // reader.Close();
        }

        /// <summary>
        /// Метод заполняющий информацию о выбранном сотруднике
        /// </summary>
        public void fillEmployeeInfo()
        {
            if (view.SelectedEmployee >= 0 && view.SelectedDepartment >= 0)
            {
                view.Selected_Change_Employee_Department_Combo = view.SelectedDepartment;
                GetEmployee($"select  Employee.Id, Gender.id, FullName.First_Name, FullName.Last_Name, FullName.Patronymic, [Employment Date], [Date Of Birth],  PositionName.Id, Position.Salary, Address.Country, Address.Region, Address.City, Address.Street, Address.[Street Number], Address.[Apartment Number], [Phone Number], Status.Status from Employee inner join Address on Employee.Address = Address.Id inner join FullName on Employee.[Full Name] = FullName.Id inner join Gender on Employee.Gender = Gender.Id inner join Position on Employee.Position = Position.Id inner join PositionName on Position.PositionName = PositionName.Id inner join Status on Employee.Status = Status.Id where Employee.Id = {(view.employeesViewItems.GetItemAt(view.SelectedEmployee) as MyItem).Id}");
            }
        }

        /// <summary>
        /// Сохранение изменений информации
        /// </summary>
        public void ChangeInformation()
        {



            //Full_Name tempName;

            // UPDATE Department SET [Department Name] = 1 WHERE (Employee = 3)

            // Full Name
            var sqlFullName = $@"UPDATE FullName SET First_Name = N'{view.FirstName}', Last_Name = N'{view.LastName}', Patronymic =  N'{view.Patronymic}' where FullName.Id = (select Employee.[Full Name] from Employee where Employee.id = {(view.employeesViewItems.GetItemAt(view.SelectedEmployee) as MyItem).Id})";

            //// Address
            var sqlAddress = $@"UPDATE Address SET Country = N'{view.County}', Region = N'{view.Region}', City = N'{view.City}', Street = N'{view.Street}', [Street Number] = {view.StreetNumber}, [Apartment Number] = {view.ApartmentNumber} where Address.Id = (select Employee.Address from Employee where Employee.id = {(view.employeesViewItems.GetItemAt(view.SelectedEmployee) as MyItem).Id})";

            //// id Status from Text
            //var sqlRevId = $@"select Status.Id from Status where Status.Status = '{addNewEmployee.StatusNow}'";


            SqlCommand command = new SqlCommand(sqlFullName, test.connection);

            // update FullName
            command.ExecuteNonQuery();

            // update Address
            command.CommandText = sqlAddress;
            command.ExecuteNonQuery();

            //// Get id Status from selected Text
            //command.CommandText = sqlRevId;

            // Get id Status from selected Text
            //SqlDataReader reader = command.ExecuteReader();
            //int x = 0;
            //while (reader.Read())
            //{
            //    x = reader.GetInt32(0);
            //}
            //reader.Close();


            //var sqlReqAddEmpl = $@"INSERT INTO Employee(Gender,[Full Name], [Employment Date], [Date Of Birth], Position, Address, [Phone Number], Status) VALUES ({ addNewEmployee.GenderEmployee + 1}, (SELECT max(Id) FROM FullName), '{addNewEmployee.EmploymentDate.Year}-{addNewEmployee.EmploymentDate.Month}-{addNewEmployee.EmploymentDate.Day}', '{addNewEmployee.DateOfBirth.Year}-{addNewEmployee.DateOfBirth.Month}-{addNewEmployee.DateOfBirth.Day}', {addNewEmployee.PositionNameEnum + 1}, (SELECT max(Id) FROM Address), '{addNewEmployee.PhoneNumber}', {x})";

            //// add Employee
            //command.CommandText = sqlReqAddEmpl;
            //command.ExecuteNonQuery();

            //// into department
            //var sqlReqAddEmplInDepartment = $@"insert into Department([Department Name], Employee) values ({addNewEmployee.SelectedDepartment + 1}, (SELECT max(Id) FROM Employee))";
            //command.CommandText = sqlReqAddEmplInDepartment;
            //command.ExecuteNonQuery();
            fillEmployeesList();
        }

        /// <summary>
        /// Редактирование наименования отдела
        /// </summary>
        public void editDepartmentName()
        {
            if (!view.departments_ComboIsEditable)
            {
                rememberCurrentSelected();
                view.departments_ComboIsEditable = !view.departments_ComboIsEditable;
            }
            else
            {
                test.ChangeDepartmentName(departmentNumber, view.DepartmentComboText);
                view.departmentList = departmentsNames;
                recall();
                view.departments_ComboIsEditable = !view.departments_ComboIsEditable;
            }
        }

        /// <summary>
        /// Метод создающий окно добавления нового отдела
        /// </summary>
        public void AddDepartmentFormCall()
        {
            addNewDepartment = new AddNewDep();
            addNewDepartment.GetDepatments(this);
            addNewDepartment.Owner(view as MainWindow);
            addNewDepartment.Show();
        }

        /// <summary>
        /// Добавление нового департамента
        /// </summary>
        public void AddNewDepartment()
        {
            test.AddNewDepartment(addNewDepartment.NewDepName);
            System.Console.WriteLine(departmentsNames);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.departmentsNames)));
        }

        /// <summary>
        /// Метод создающий окно добавления нового сотрудника
        /// </summary>
        public void AddEmployeeFormCall()
        {
            addNewEmployee = new AddEmployee();
            addNewEmployee.GetDepatments(this);
            addNewEmployee.Owner(view as MainWindow);
            fillCommonInformationAddEmployee();
            fillDepartmentComboAddEmployee(/*addNewEmployee*/);
            addNewEmployee.Show();
        }

        /// <summary>
        /// Заполнение списка департаментов для окна добавления нового сотрудника
        /// </summary>
        public void fillAddEmployeeDepartmentCombo()
        {
            addNewEmployee.departmentList = departmentsNames;
        }

        /// <summary>
        /// Сохранение нового сотрудника
        /// </summary>
        public void SaveNewEmployee()
        {
            //Full_Name tempName;

            // Full Name
            var sqlFullName = $@"insert into FullName(First_Name, Last_Name, Patronymic) values (N'{addNewEmployee.FirstName}', N'{addNewEmployee.LastName}', N'{addNewEmployee.Patronymic}')";

            // Address
            var sqlAddress = $@"insert into Address(Country,Region, City, Street, [Street Number], [Apartment Number]) values (N'{addNewEmployee.County}', N'{addNewEmployee.Region}', N'{addNewEmployee.City}', N'{addNewEmployee.Street}', {addNewEmployee.StreetNumber}, {addNewEmployee.ApartmentNumber})";

            // id Status from Text
            var sqlRevId = $@"select Status.Id from Status where Status.Status = '{addNewEmployee.StatusNow}'";


            SqlCommand command = new SqlCommand(sqlFullName, test.connection);

            // fill FullName
            command.ExecuteNonQuery();

            // fill Address
            command.CommandText = sqlAddress;
            command.ExecuteNonQuery();

            // Get id Status from selected Text
            command.CommandText = sqlRevId;

            // Get id Status from selected Text
            SqlDataReader reader = command.ExecuteReader();
            int x = 0;
            while (reader.Read())
            {
                x = reader.GetInt32(0);
            }
            reader.Close();


            var sqlReqAddEmpl = $@"INSERT INTO Employee(Gender,[Full Name], [Employment Date], [Date Of Birth], Position, Address, [Phone Number], Status) VALUES ({ addNewEmployee.GenderEmployee + 1}, (SELECT max(Id) FROM FullName), '{addNewEmployee.EmploymentDate.Year}-{addNewEmployee.EmploymentDate.Month}-{addNewEmployee.EmploymentDate.Day}', '{addNewEmployee.DateOfBirth.Year}-{addNewEmployee.DateOfBirth.Month}-{addNewEmployee.DateOfBirth.Day}', {addNewEmployee.PositionNameEnum + 1}, (SELECT max(Id) FROM Address), '{addNewEmployee.PhoneNumber}', {x})";
                       
            // add Employee
            command.CommandText = sqlReqAddEmpl;
            command.ExecuteNonQuery();

            // into department
            var sqlReqAddEmplInDepartment = $@"insert into Department([Department Name], Employee) values ({addNewEmployee.SelectedDepartment + 1}, (SELECT max(Id) FROM Employee))";
            command.CommandText = sqlReqAddEmplInDepartment;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Метод удаляющий выбранного сотрудника
        /// </summary>
        public void DeleteEmployee()
        {
            int z = (view.employeesViewItems.GetItemAt(view.SelectedEmployee) as MyItem).Id;
            var sqlReqDelEmpl = $"delete from Employee where Id = {(view.employeesViewItems.GetItemAt(view.SelectedEmployee) as MyItem).Id}";

            SqlCommand command = new SqlCommand(sqlReqDelEmpl, test.connection);
            command.ExecuteNonQuery();
            fillEmployeesList();
            //test.DeleteEmployee(view.SelectedDepartment, view.SelectedEmployee);


            //fillEmployeesList();  /* в теории можно избежать привязкой*/
        }

        int
            departmentNumber,
            employeeNumber;

        public event PropertyChangedEventHandler PropertyChanged; // INotifyPropertyChanged

        //public void NotifyPropertyChanged(string propName)
        //{
        //    if (this.PropertyChanged == null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //}

        ///// <summary>
        ///// Метод запоминающий положение выбора пользователя
        ///// </summary>
        void rememberCurrentSelected()
        {
            if (view.SelectedDepartment >= 0 && view.SelectedEmployee >= 0)
            {
                departmentNumber = view.SelectedDepartment;
                employeeNumber = view.SelectedEmployee;
            }
        }

        /// <summary>
        /// Метод вспоминающий ранее выбранное положение выбора пользователя
        /// </summary>
        void recall()
        {
            view.SelectedDepartment = departmentNumber;
            view.SelectedEmployee = employeeNumber;
        }
    }
}