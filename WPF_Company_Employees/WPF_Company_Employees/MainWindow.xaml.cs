using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Collections.ObjectModel;

namespace WPF_Company_Employees
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Test test = new Test();

        public MainWindow()
        {
            InitializeComponent();
            test.CreateTestData();
            fillDepartmentCombo();
            position_Combo.ItemsSource = Enum.GetValues(typeof(PositionName));
            status_Combo.ItemsSource = Enum.GetValues(typeof(Status));
            gender_Combo.ItemsSource = Enum.GetValues(typeof(Gender));
            Edit();
        }

        // =================== нормально разделенный интерфейс работы с главным окном (только считывание, и вызов методов из TestData File) =>
        
        /// <summary>
        /// Метод заполняющий список отделов
        /// </summary>
        public void fillDepartmentCombo()
        {
            departments_Combo.Items.Clear();
            Change_Employee_Department_Combo.Items.Clear();

            for (int i = 0; i < test.Departments.Count; i++)
            {
                departments_Combo.Items.Add(test.Departments[i].DepartmentName);
                Change_Employee_Department_Combo.Items.Add(test.Departments[i].DepartmentName);
            }
        }

        /// <summary>
        /// Метод заполняющий лист сотрудников выбранного отдела
        /// </summary>
        public void fillEmployeesList()
        {
            if (departments_Combo.SelectedIndex >= 0)
            {
                employeesList.ItemsSource = test.EmployeesListString(departments_Combo.SelectedIndex);
            }
        }

        /// <summary>
        /// Метод вызываемый при смене отдела в списке отделов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void departments_Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillEmployeesList();
            Change_Employee_Department_Combo.SelectedIndex = departments_Combo.SelectedIndex;
            employeesList.SelectedIndex = 0;
        }

        /// <summary>
        /// Метод вызываемый событием смены сотрудника в списке сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void employeesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillEmployeeInfo();
        }

        /// <summary>
        /// Метод вызываемый при смене отдела в окне сотрудника
        /// Метод переносящий сотрудника между отделами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Change_Employee_Department_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (employeesList.SelectedIndex >= 0 && departments_Combo.SelectedIndex >= 0 && Change_Employee_Department_Combo.SelectedIndex >= 0)
            {
                test.transferEmployeeBetweenDepartments(Change_Employee_Department_Combo.SelectedIndex, departments_Combo.SelectedIndex, employeesList.SelectedIndex);
                departments_Combo.SelectedIndex = Change_Employee_Department_Combo.SelectedIndex;

                employeesList.SelectedIndex = test.Departments[Change_Employee_Department_Combo.SelectedIndex].Employees.Count - 1; // т.к. происходит лишь считывание информации, нормально
            }
        }

        /// <summary>
        /// Метод заполняющий информацию о выбранном сотруднике
        /// </summary>
        void fillEmployeeInfo()
        {
            if (employeesList.SelectedIndex >= 0 && departments_Combo.SelectedIndex >= 0)
            {
                gender_Combo.SelectedIndex = (int)test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].GenderEmployee;
                name_Box.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Full_Name.FirstName;
                lastName_Box.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Full_Name.LastName;
                patronymic_Box_Copy.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Full_Name.Patronymic;
                employmentDate_Picker.SelectedDate = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].EmploymentDate;
                dateOfBirth_Picker.SelectedDate = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].DateOfBirth;
                position_Combo.SelectedIndex = (int)test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Position.PositionNameEnum;
                salary_Box.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Position.Salary.ToString();
                county_Box.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Address.County;
                region_Box.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Address.Region;
                city_Box.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Address.City;
                street_Box.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Address.Street;
                streetNumber_Box.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Address.StreetNumber.ToString();
                apartmentNumber_Box_Copy.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Address.ApartmentNumber.ToString();
                phoneNumber_Box.Text = test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].PhoneNumber;
                status_Combo.SelectedIndex = (int)test.Departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].StatusNow;
            }
        }

        #region Buttons

        /// <summary>
        /// Метод вызывающийся при нажатии на клавишу "add" относящийся к отделам
        /// </summary>
        private void addDepButton_Click(object sender, RoutedEventArgs e)
        {
            AddDepartmentFormCall();
        }

        /// <summary>
        /// Метод создающий окно добавления нового отдела, и передающий туда необходимую информацию
        /// </summary>
        void AddDepartmentFormCall()
        {
            AddNewDepartment childWindow = new AddNewDepartment();
            childWindow.GetDepatments(test, this);
            childWindow.Owner = this;
            childWindow.Show();
        }

        /// <summary>
        /// Метод вызывающийся при срабатывании события нажатия на кнопку "Apply"
        /// </summary>
        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeInformation();
        }

        /// <summary>
        /// Сохранение изменений информации
        /// </summary>
        void ChangeInformation()
        {
            rememberCurrentSelected();
            Full_Name tempName;

            if (patronymic_Box_Copy.Text == "Отчество" || patronymic_Box_Copy.Text == "")
            {
                tempName
                    = new Full_Name(
                        name_Box.Text,
                        lastName_Box.Text);
            }
            else
            {
                tempName
                    = new Full_Name(
                        name_Box.Text,
                        lastName_Box.Text,
                        patronymic_Box_Copy.Text);
            }

            test.ChangeEmployeeInformation(
                departments_Combo.SelectedIndex,
                employeesList.SelectedIndex,
                new Employee(
                    (Gender)gender_Combo.SelectedIndex,
                    tempName,
                    employmentDate_Picker.SelectedDate.Value.Date,
                    dateOfBirth_Picker.SelectedDate.Value,
                    new Position((PositionName)position_Combo.SelectedIndex, decimal.Parse(salary_Box.Text)),
                    new Address(
                        county_Box.Text,
                        region_Box.Text,
                        city_Box.Text,
                        street_Box.Text,
                        int.Parse(streetNumber_Box.Text),
                        int.Parse(apartmentNumber_Box_Copy.Text)),
                    phoneNumber_Box.Text,
                    (Status)status_Combo.SelectedIndex));

            // смена наименования отдела
            test.ChangeDepartmentName(departmentNumber, departments_Combo.Text);

            fillDepartmentCombo();
            fillEmployeesList();
            fillEmployeeInfo();
            recall();
        }

        ///// <summary>
        ///// Метод вызывающийся при срабатывании события нажатия на кнопку "Add" относящийся к сотрудникам
        ///// </summary>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeFormCall();
        }

        /// <summary>
        /// Метод создающий окно добавления нового сотрудника, и передающий туда необходимую информацию
        /// </summary>
        void AddEmployeeFormCall()
        {
            AddEmployee childWindow = new AddEmployee();
            childWindow.GetDepatments(test, this);
            childWindow.Owner = this;
            childWindow.Show();
        }

        /// <summary>
        /// Метод вызывающийся при срабатывании события нажатия на кнопку "Delete"
        /// </summary>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            test.DeleteEmployee(departments_Combo.SelectedIndex, employeesList.SelectedIndex);
            fillEmployeesList();
        }
        
        /// <summary>
        /// Метод вызывающийся при срабатывании события нажатия на кнопку "Edit"
        /// </summary>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            rememberCurrentSelected();
            Edit();
        }
        
        /// <summary>
        /// Метод разрешающий / запрещающий редактирование окон
        /// </summary>
        void Edit()
        {
            bool canEdit = true;
            canEdit = !name_Box.IsEnabled; // чтобы не создавать лишних глобальных перменных

            departments_Combo.IsEditable = canEdit;
            gender_Combo.IsEnabled = canEdit;
            name_Box.IsEnabled = canEdit;
            lastName_Box.IsEnabled = canEdit;
            patronymic_Box_Copy.IsEnabled = canEdit;
            employmentDate_Picker.IsEnabled = canEdit;
            dateOfBirth_Picker.IsEnabled = canEdit;
            position_Combo.IsEnabled = canEdit;
            salary_Box.IsEnabled = canEdit;
            county_Box.IsEnabled = canEdit;
            region_Box.IsEnabled = canEdit;
            city_Box.IsEnabled = canEdit;
            street_Box.IsEnabled = canEdit;
            streetNumber_Box.IsEnabled = canEdit;
            apartmentNumber_Box_Copy.IsEnabled = canEdit;
            phoneNumber_Box.IsEnabled = canEdit;
            status_Combo.IsEnabled = canEdit;
            Change_Employee_Department_Combo.IsEnabled = canEdit;
            applyButton.IsEnabled = canEdit;
            deleteButton.IsEnabled = canEdit;
            addButton.IsEnabled = canEdit;
            addDepButton.IsEnabled = canEdit;
        }

        #endregion

        int
            departmentNumber,
            employeeNumber;

        /// <summary>
        /// Метод запоминающий положение выбора пользователя
        /// </summary>
        void rememberCurrentSelected()
        {
            if (departments_Combo.SelectedIndex >= 0 && employeesList.SelectedIndex >= 0)
            {
                departmentNumber = departments_Combo.SelectedIndex;
                employeeNumber = employeesList.SelectedIndex;
            }
            if (employeeNumber < employeesList.SelectedIndex)
                employeeNumber = employeesList.SelectedIndex;
        }
        
        /// <summary>
        /// Метод вспоминающий ранее выбранное положение выбора пользователя
        /// </summary>
        void recall()
        {
            departments_Combo.SelectedIndex = departmentNumber;
            employeesList.SelectedIndex = employeeNumber;
        }
    }
}