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
using System.Windows.Shapes;

namespace WPF_Company_Employees
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        Test _test;
        MainWindow _owner;
        public AddEmployee()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод получающий данные из родительского окна
        /// </summary>
        /// <param name="test">Тестовые данные</param>
        /// <param name="owner">Родительское окно</param>
        public void GetDepatments(Test test, MainWindow owner) // не очень нравится способ передачи данных между окнами, но другого не придумал
        {
            _owner = owner;
            _test = test;
            fillInformation();
        }

        /// <summary>
        /// Метод заполняющий известные окна
        /// </summary>
        void fillInformation()
        {
            Departments_Combo.Items.Clear();
            position_Combo.Items.Clear();
            status_Combo.Items.Clear();
            gender_Combo.Items.Clear();

            for (int i = 0; i < _test.Departments.Count; i++)
            {
                Departments_Combo.Items.Add(_test.Departments[i].DepartmentName);
            }
            position_Combo.ItemsSource = Enum.GetValues(typeof(PositionName));
            status_Combo.ItemsSource = Enum.GetValues(typeof(Status));
            gender_Combo.ItemsSource = Enum.GetValues(typeof(Gender));
        }
        
        /// <summary>
        /// Метод вызывающийся при срабатывании события нажатия на кнопку "Save"
        /// </summary>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveEmployee();
            MessageBox.Show("Сотрудник успешно добавлен");
            Refresh();
            _owner.fillEmployeesList();
        }

        /// <summary>
        /// Метод сохраняющий сотрудника
        /// </summary>
        void SaveEmployee()
        {
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

            _test.AddNewEmployee(
                Departments_Combo.SelectedIndex,
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
        }

        /// <summary>
        /// Метод вызывающийся при срабатывании события нажатия на кнопку "Refresh"
        /// </summary>
        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Метод очищающий окна
        /// </summary>
        void Refresh()
        {
            Departments_Combo.SelectedIndex = -1;
            gender_Combo.SelectedIndex = -1;
            name_Box.Clear();
            lastName_Box.Clear();
            patronymic_Box_Copy.Clear();
            employmentDate_Picker.SelectedDate = DateTime.Now;
            dateOfBirth_Picker.SelectedDate = DateTime.Now;
            position_Combo.SelectedIndex = -1;
            salary_Box.Clear();
            county_Box.Clear();
            region_Box.Clear();
            city_Box.Clear();
            street_Box.Clear();
            streetNumber_Box.Clear();
            apartmentNumber_Box_Copy.Clear();
            phoneNumber_Box.Clear();
            status_Combo.SelectedIndex = -1;
        }
    }
}