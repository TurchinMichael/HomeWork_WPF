using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections.Generic;

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
                NotifyPropertyChanged(nameof(this.departmentsNames));

                return temp;
            }
        }

        /// <summary>
        /// Заполнение списка отделов
        /// </summary>
        public void fillDepartmentCombo()
        {
            //view.departmentList = departmentsNames;

        }

        /// <summary>
        /// Возвращает коллекцию создавая её из строк в БД
        /// </summary>
        /// <param name="s">Запрос возвращающий необходимые строки</param>
        ICollection<string> usualMoves(string s)
        {
            ICollection<string> t = new List<string>();
            SqlCommand commandRevInformation = new SqlCommand(s, test.connection);
            SqlDataReader reader = commandRevInformation.ExecuteReader();

            while (reader.Read())
                t.Add(reader.GetString(1));
            reader.Close();
            return t;
        }

        public void fillCommonInformation()
        {
            view.statusList = usualMoves($@"select * from Status");
            view.genderList = usualMoves($@"select * from Gender");
            view.positionList = usualMoves($@"select * from PositionName");
        }

        /// <summary>
        /// Метод заполняющий лист сотрудников выбранного отдела
        /// </summary>
        public void fillEmployeesList()
        {
            if (view.SelectedDepartment >= 0)
            {
                ObservableCollection<string> employeesList = new ObservableCollection<string>();

                foreach (var obj in test.Departments[view.SelectedDepartment].Employees)
                    employeesList.Add(obj.Full_Name.ToString());

                view.employeeList = employeesList;
                view.Selected_Change_Employee_Department_Combo = view.SelectedDepartment;
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
                test.transferEmployeeBetweenDepartments(view.Selected_Change_Employee_Department_Combo, view.SelectedDepartment, view.SelectedEmployee);

                view.SelectedDepartment = view.Selected_Change_Employee_Department_Combo;

                view.SelectedEmployee = test.Departments[view.Selected_Change_Employee_Department_Combo].Employees.Count - 1;
            }
        }

        /// <summary>
        /// Метод заполняющий информацию о выбранном сотруднике
        /// </summary>
        public void fillEmployeeInfo()
        {
            if (view.SelectedEmployee >= 0 && view.SelectedDepartment >= 0)
            {
                Employee selectedEmployee = test.Departments[view.SelectedDepartment].Employees[view.SelectedEmployee];
                view.Selected_Change_Employee_Department_Combo = view.SelectedDepartment;
                view.GenderEmployee = (int)selectedEmployee.GenderEmployee;
                view.FirstName = selectedEmployee.Full_Name.FirstName;
                view.LastName = selectedEmployee.Full_Name.LastName;
                view.Patronymic = selectedEmployee.Full_Name.Patronymic;
                view.EmploymentDate = selectedEmployee.EmploymentDate;
                view.DateOfBirth = selectedEmployee.DateOfBirth;
                view.PositionNameEnum = (int)selectedEmployee.Position.PositionNameEnum;
                view.Salary = selectedEmployee.Position.Salary;
                view.County = selectedEmployee.Address.County;
                view.Region = selectedEmployee.Address.Region;
                view.City = selectedEmployee.Address.City;
                view.Street = selectedEmployee.Address.Street;
                view.StreetNumber = selectedEmployee.Address.StreetNumber;
                view.ApartmentNumber = selectedEmployee.Address.ApartmentNumber;
                view.PhoneNumber = selectedEmployee.PhoneNumber;
                view.StatusNow = (int)selectedEmployee.StatusNow;
            }
        }

        /// <summary>
        /// Сохранение изменений информации
        /// </summary>
        public void ChangeInformation()
        {
            Full_Name tempName;

            if (view.Patronymic == "Отчество" || view.Patronymic == "")
            {
                tempName = new Full_Name(
                        view.FirstName,
                        view.LastName);
            }
            else
            {
                tempName = new Full_Name(
                        view.FirstName,
                        view.LastName, 
                        view.Patronymic);
            }

            test.ChangeEmployeeInformation(
                view.SelectedDepartment,
                view.SelectedEmployee,
                new Employee(
                    (Gender)view.GenderEmployee,
                    tempName,
                    view.EmploymentDate,
                    view.DateOfBirth,
                    new Position((PositionName)view.PositionNameEnum, view.Salary),
                    new Address(
                        view.County,
                        view.Region,
                        view.City,
                        view.Street,
                        view.StreetNumber,
                        view.ApartmentNumber),
                        view.PhoneNumber,
                        (Status)view.StatusNow));
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
            Full_Name tempName;

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


            // ==============

            //if (addNewEmployee.Patronymic == "Отчество" || addNewEmployee.Patronymic == "")
            //{
            //    tempName
            //        = new Full_Name(
            //            addNewEmployee.FirstName,
            //            addNewEmployee.LastName);
            //}
            //else
            //{
            //    tempName
            //        = new Full_Name(
            //            addNewEmployee.FirstName,
            //            addNewEmployee.LastName,
            //            addNewEmployee.Patronymic);
            //}
            
            //test.AddNewEmployee(addNewEmployee.SelectedDepartment,
            //    new Employee(
            //        (Gender)addNewEmployee.GenderEmployee,
            //        tempName,
            //        addNewEmployee.EmploymentDate,
            //        addNewEmployee.DateOfBirth,
            //        new Position((PositionName)addNewEmployee.PositionNameEnum, addNewEmployee.Salary),
            //        new Address(
            //            addNewEmployee.County,
            //            addNewEmployee.Region,
            //            addNewEmployee.City,
            //            addNewEmployee.Street,
            //            addNewEmployee.StreetNumber,
            //            addNewEmployee.ApartmentNumber),
            //        addNewEmployee.PhoneNumber,
            //        (Status)addNewEmployee.StatusNow));

            //fillEmployeesList();  /* в теории можно избежать привязкой*/
        }

        /// <summary>
        /// Метод удаляющий выбранного сотрудника
        /// </summary>
        public void DeleteEmployee()
        {
            test.DeleteEmployee(view.SelectedDepartment, view.SelectedEmployee);


            fillEmployeesList();  /* в теории можно избежать привязкой*/
        }
             
        int
            departmentNumber,
            employeeNumber;

        public event PropertyChangedEventHandler PropertyChanged; // INotifyPropertyChanged

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged == null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        
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