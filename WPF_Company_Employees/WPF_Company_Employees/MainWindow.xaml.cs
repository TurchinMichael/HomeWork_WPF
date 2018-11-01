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
    delegate void voidDelegate();

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> employeesListString = new ObservableCollection<string>();
        Test test = new Test();

        public MainWindow()
        {
            test.CreateTestData();
            InitializeComponent();
            fillDepartmentCombo();
            position_Combo.ItemsSource = Enum.GetValues(typeof(PositionName));
            status_Combo.ItemsSource = Enum.GetValues(typeof(Status));
            gender_Combo.ItemsSource = Enum.GetValues(typeof(Gender));
            edit();
        }

        /// <summary>
        /// Метод заполняющий список отделов
        /// </summary>
        void fillDepartmentCombo()
        {
            departments_Combo.Items.Clear();
            Change_Employee_Department_Combo.Items.Clear();

            for (int i = 0; i < test.departments.Count; i++)
            {
                departments_Combo.Items.Add(test.departments[i].DepartmentName);
                Change_Employee_Department_Combo.Items.Add(test.departments[i].DepartmentName);
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
        /// Метод заполняющий лист работников выбранного отдела
        /// </summary>
        void fillEmployeesList()
        {
            if (departments_Combo.SelectedIndex >= 0)
            {
                employeesListString.Clear();
                for (int i = 0; i < test.departments[departments_Combo.SelectedIndex].Employees.Count; i++)
                {
                    employeesListString.Add(test.departments[departments_Combo.SelectedIndex].Employees[i].Full_Name.ToString());
                }
                employeesList.ItemsSource = employeesListString;
            }
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
        /// Метод заполняющий информацию о выбранном сотруднике
        /// </summary>
        void fillEmployeeInfo()
        {
            if (employeesList.SelectedIndex >= 0 && departments_Combo.SelectedIndex >= 0)
            {
                gender_Combo.SelectedIndex = (int)test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].GenderEmployee;
                full_Name_Box.Text = test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Full_Name.ToString();
                employmentDate_Picker.SelectedDate = test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].EmploymentDate;
                dateOfBirth_Picker.SelectedDate= test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].DateOfBirth;
                position_Combo.SelectedIndex = (int)test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Position.PositionNameEnum;
                salary_Box.Text = test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Position.Salary.ToString();
                address_Box.Text = test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Address.ToString();
                phoneNumber_Box.Text = test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].PhoneNumber;
                status_Combo.SelectedIndex = (int)test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].StatusNow;
            }
        }






        #region GhangeInformation
        /// <summary>
        /// Метод выполняющий повторяющиеся действия
        /// </summary>
        /// <param name="act">Метод, который должен сменять информацию о сотруднике</param>
        void changeInformation(Action act)
        {
            if (departments_Combo.SelectedIndex >= 0 && employeesList.SelectedIndex >= 0)
            {
                rememberCurrentSelected();
                act.Invoke();
                fillEmployeesList();
                fillEmployeeInfo();
                recall();
            }
        }

        /// <summary>
        /// Метод сменяющий пол сотрудника
        /// </summary>
        private void gender_Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].GenderEmployee = (Gender)gender_Combo.SelectedIndex;
            fillEmployeeInfo();
        }

        /// <summary>
        ///  Метод вызывающийся при смене фокуса клавиатуры с текста ФИО
        /// </summary>
        private void full_Name_Box_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            changeInformation(Full_Name_Change);
        }

        /// <summary>
        /// Метод сменяющий ФИО
        /// </summary>
        void Full_Name_Change()
        {
            test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Full_Name = new Full_Name(full_Name_Box.Text);
        }

        /// <summary>
        /// Метод вызывающийся при смене фокуса клавиатуры с даты приема на работу
        /// </summary>
        private void employmentDate_Picker_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
                changeInformation(EmploymentDateChange);
        }

        /// <summary>
        /// Метод сменяющий дату приема на работу
        /// </summary>
        void EmploymentDateChange()
        {
            test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].EmploymentDate = employmentDate_Picker.SelectedDate.Value;
        }
                
        /// <summary>
        /// Метод вызывающийся при смене фокуса клавиатуры с даты рождения сотрудника
        /// </summary>
        private void dateOfBirth_Picker_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            changeInformation(DateOfBirthChange);
        }

        /// <summary>
        /// Метод сменяющий дату рождения сотрудника
        /// </summary>
        void DateOfBirthChange()
        {
            test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].DateOfBirth = dateOfBirth_Picker.SelectedDate.Value;
        }
        
        /// <summary>
        /// Метод сменяющий должность сотрудника
        /// </summary>
        private void position_Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Position.PositionNameEnum = (PositionName)position_Combo.SelectedIndex;
            fillEmployeeInfo();
        }

        /// <summary>
        /// Метод вызывающийся при смене фокуса клавиатуры с зарплаты
        /// </summary>
        private void salary_Box_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            changeInformation(Salary_Change);
        }

        /// <summary>
        /// Метод сменяющий зарплату
        /// </summary>
        void Salary_Change()
        {
            test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Position.Salary = decimal.Parse(salary_Box.Text);
        }

        /// <summary>
        /// Метод вызывающийся при смене фокуса клавиатуры с адреса
        /// </summary>
        private void address_Box_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            changeInformation(Address_Change);
        }

        /// <summary>
        /// Метод сменяющий адрес
        /// </summary>
        void Address_Change()
        {
            test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].Address = new Address(address_Box.Text);
        }
        
        /// <summary>
        /// Метод вызывающийся при смене фокуса клавиатуры с телефонного номера
        /// </summary>
        private void phoneNumber_Box_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            changeInformation(PhoneNumber_Change);
        }

        /// <summary>
        /// Метод сменяющий телефонный номер
        /// </summary>
        void PhoneNumber_Change()
        {
            test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].PhoneNumber = phoneNumber_Box.Text;
        }
        
        /// <summary>
        /// Метод сменяющий статус сотрудника
        /// </summary>
        private void status_Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex].StatusNow = (Status)status_Combo.SelectedIndex;
            fillEmployeeInfo();
        }

        #endregion











        bool first = true;
        /// <summary>
        /// Метод вызываемый при попытке изменить имя отдела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void departments_Combo_KeyDown(object sender, KeyEventArgs e)
        {
            Change_Employee_Department_Combo.Text = departments_Combo.Text;

            if (first && departments_Combo.SelectedIndex >= 0)
            {
                first = !first;
                rememberCurrentSelected();
            }

            if (e.Key == Key.Enter)
            {
                test.departments[departmentNumber].DepartmentName = departments_Combo.Text;
                fillDepartmentCombo();
                recall();
                first = !first;
            }
        }
        
        /// <summary>
        /// Метод вызываемый при смене отдела в окне сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Change_Employee_Department_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            transferEmployeeBetweenDepartments();
        }

        /// <summary>
        /// Метод переносящий сотрудника между отделами
        /// </summary>
        void transferEmployeeBetweenDepartments()
        {
            if (employeesList.SelectedIndex >= 0 && departments_Combo.SelectedIndex >= 0 && Change_Employee_Department_Combo.SelectedIndex >= 0)
            {
                test.departments[Change_Employee_Department_Combo.SelectedIndex].Employees.Add(test.departments[departments_Combo.SelectedIndex].Employees[employeesList.SelectedIndex]);
                test.departments[departments_Combo.SelectedIndex].Employees.RemoveAt(employeesList.SelectedIndex);

                departments_Combo.SelectedIndex = Change_Employee_Department_Combo.SelectedIndex;
                employeesList.SelectedIndex = test.departments[Change_Employee_Department_Combo.SelectedIndex].Employees.Count - 1;
            }
        }
        
        int
            departmentNumber,
            employeeNumber;

        /// <summary>
        /// Метод запоминающий положение выбора пользователя
        /// </summary>
        void rememberCurrentSelected()
        {
            departmentNumber = departments_Combo.SelectedIndex;
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


        bool canEdit = true;

        /// <summary>
        /// Метод вызывающийся при срабатывании события нажатия на кнопку "Edit"
        /// </summary>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            edit();
        }

        /// <summary>
        /// Метод разрешающий / запрещающий редактирование
        /// </summary>
        void edit()
        {
            canEdit = !canEdit;

            departments_Combo.IsEditable = canEdit;
            gender_Combo.IsEnabled = canEdit;
            full_Name_Box.IsEnabled = canEdit;
            employmentDate_Picker.IsEnabled = canEdit;
            dateOfBirth_Picker.IsEnabled = canEdit;
            position_Combo.IsEnabled = canEdit;
            salary_Box.IsEnabled = canEdit;
            address_Box.IsEnabled = canEdit;
            phoneNumber_Box.IsEnabled = canEdit;
            status_Combo.IsEnabled = canEdit;
            Change_Employee_Department_Combo.IsEnabled = canEdit;
            deleteButton.IsEnabled = canEdit;
        }
        
        /// <summary>
        /// Метод вызывающийся при срабатывании события нажатия на кнопку "Delete"
        /// </summary>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// Метод удаляющий выбранного сотрудника
        /// </summary>
        void Delete()
        {
            if (departments_Combo.SelectedIndex >= 0 && employeesList.SelectedIndex >= 0)
            {
                test.departments[departments_Combo.SelectedIndex].Employees.RemoveAt(employeesList.SelectedIndex);// departments_Combo.SelectedIndex
                fillEmployeesList();
            }
        }
    }
}