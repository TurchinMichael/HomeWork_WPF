using System;
using System.Collections.Generic;
using System.Windows;

namespace WPF_Company_Employees
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window, IViewForNewEmployee
    {
        Presenter p;

        /// <summary>
        /// Класс добавления сотрудника
        /// </summary>
        public AddEmployee()
        {
            // инициализация
            InitializeComponent();

            // заполнение заранее известной информацией (для открывающихся Combo_Box)
            gender_Combo.ItemsSource = Enum.GetValues(typeof(Gender));
            position_Combo.ItemsSource = Enum.GetValues(typeof(PositionName));
            status_Combo.ItemsSource = Enum.GetValues(typeof(Status));

            // events
            refreshButton.Click += delegate { Refresh(); };
            saveButton.Click += delegate { p.SaveNewEmployee(); MessageBox.Show("Сотрудник успешно добавлен");};
        }

        /// <summary>
        /// Метод получающий данные из презентера
        /// </summary>
        public void GetDepatments(Presenter presenter)
        {
            p = presenter;
            p.fillAddEmployeeDepartmentCombo();
        }

        /// <summary>
        /// Метод для назначения родительской формы
        /// </summary>
        /// <param name="owner">Родительская форма</param>
        public new void Owner(Window owner)
        {
            (this as Window).Owner = owner;
        }

        /// <summary>
        /// Отображение формы
        /// </summary>
        public new void Show()
        {
            (this as Window).Show();
        }
        
        #region IViewForNewEmployee

        public IEnumerable<string> departmentList
        {
            get => Departments_Combo.ItemsSource as IEnumerable<string>;
            set => Departments_Combo.ItemsSource = value;
        }

        public int SelectedDepartment
        {
            get => Departments_Combo.SelectedIndex;
            set => Departments_Combo.SelectedIndex = value;
        }

        public int GenderEmployee
        {
            get => gender_Combo.SelectedIndex;
            set => gender_Combo.SelectedIndex = value;
        }
        public string FirstName
        {
            get => name_Box.Text;
            set => name_Box.Text = value;
        }
        public string LastName
        {
            get => lastName_Box.Text;
            set => lastName_Box.Text = value;
        }
        public string Patronymic
        {
            get => patronymic_Box_Copy.Text;
            set => patronymic_Box_Copy.Text = value;
        }
        public DateTime EmploymentDate
        {
            get => employmentDate_Picker.SelectedDate.Value;
            set => employmentDate_Picker.SelectedDate = value;
        }
        public DateTime DateOfBirth
        {
            get => dateOfBirth_Picker.SelectedDate.Value;
            set => dateOfBirth_Picker.SelectedDate = value;
        }
        public int PositionNameEnum
        {
            get => position_Combo.SelectedIndex;
            set => position_Combo.SelectedIndex = value;
        }
        public decimal Salary
        {
            get => decimal.Parse(salary_Box.Text);
            set => salary_Box.Text = value.ToString();
        }
        public string County
        {
            get => county_Box.Text;
            set => county_Box.Text = value;
        }
        public string Region
        {
            get => region_Box.Text;
            set => region_Box.Text = value;
        }
        public string City
        {
            get => city_Box.Text;
            set => city_Box.Text = value;
        }
        public string Street
        {
            get => street_Box.Text;
            set => street_Box.Text = value;
        }
        public int StreetNumber
        {
            get => int.Parse(streetNumber_Box.Text);
            set => streetNumber_Box.Text = value.ToString();
        }
        public int ApartmentNumber
        {
            get => int.Parse(apartmentNumber_Box_Copy.Text);
            set => apartmentNumber_Box_Copy.Text = value.ToString();
        }
        public string PhoneNumber
        {
            get => phoneNumber_Box.Text;
            set => phoneNumber_Box.Text = value;
        }
        public string StatusNow
        {
            get => status_Combo.Text;
            set => status_Combo.Text = value;
        }

        #endregion

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