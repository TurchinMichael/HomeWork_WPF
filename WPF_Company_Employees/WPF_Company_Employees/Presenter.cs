using System.Collections.ObjectModel;


namespace WPF_Company_Employees
{
    public class Presenter
    {
        public Test test;
        private IView view;
        private IViewForNewEmployee addNewEmployee;
        private IViewNewDepartment addNewDepartment;

        public Presenter(IView View)
        {
            this.view = View;
            test = new Test();
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
                return temp;
            }
        }

        /// <summary>
        /// Заполнение списка отделов
        /// </summary>
        public void fillDepartmentCombo()
        {
            view.departmentList = departmentsNames;
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
            fillDepartmentCombo(); /* в теории можно избежать привязкой*/
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

            if (addNewEmployee.Patronymic == "Отчество" || addNewEmployee.Patronymic == "")
            {
                tempName
                    = new Full_Name(
                        addNewEmployee.FirstName,
                        addNewEmployee.LastName);
            }
            else
            {
                tempName
                    = new Full_Name(
                        addNewEmployee.FirstName,
                        addNewEmployee.LastName,
                        addNewEmployee.Patronymic);
            }
            
            test.AddNewEmployee(addNewEmployee.SelectedDepartment,
                new Employee(
                    (Gender)addNewEmployee.GenderEmployee,
                    tempName,
                    addNewEmployee.EmploymentDate,
                    addNewEmployee.DateOfBirth,
                    new Position((PositionName)addNewEmployee.PositionNameEnum, addNewEmployee.Salary),
                    new Address(
                        addNewEmployee.County,
                        addNewEmployee.Region,
                        addNewEmployee.City,
                        addNewEmployee.Street,
                        addNewEmployee.StreetNumber,
                        addNewEmployee.ApartmentNumber),
                    addNewEmployee.PhoneNumber,
                    (Status)addNewEmployee.StatusNow));

            fillEmployeesList();  /* в теории можно избежать привязкой*/
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