using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;

namespace WPF_Company_Employees
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Presenter p;

        public MainWindow()
        {
            // инициализация
            InitializeComponent();
            p = new Presenter(this);

            #region ToDo
            mainGrid.DataContext = p;
            #endregion

            // заполнение заранее известной информацией (для открывающихся Combo_Box)
            //gender_Combo.ItemsSource = Enum.GetValues(typeof(Gender));  /* в теории можно избежать привязкой*/
            //position_Combo.ItemsSource = Enum.GetValues(typeof(PositionName));  /* в теории можно избежать привязкой*/
            //status_Combo.ItemsSource = Enum.GetValues(typeof(Status));  /* в теории можно избежать привязкой*/
            Change_Employee_Department_Combo.ItemsSource = departments_Combo.Items;

            // events
            addDepButton.Click += delegate { p.AddDepartmentFormCall(); };
            editDepNameButton.Click += delegate { p.editDepartmentName(); };
            addButton.Click += delegate { p.AddEmployeeFormCall(); };
            deleteButton.Click += delegate { p.DeleteEmployee(); };
            applyButton.Click += delegate { p.ChangeInformation(); };
            departments_Combo.SelectionChanged += delegate { p.fillEmployeesList(); };
            employeesView.SelectionChanged += delegate { p.fillEmployeeInfo(); };
            Change_Employee_Department_Combo.SelectionChanged += delegate { p.Change_Employee_Department(); };


            // TestZone AddEmployeeInDepartment


            //SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            //connectionString.DataSource = @"(LocalDB)\MSSQLLocalDB";
            //connectionString.InitialCatalog = @"C:\Users\Графическая Станция\HWWPF.mdf";
            //connectionString.IntegratedSecurity = true;
            //connectionString.Pooling = false;

            //// Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\Графическая Станция\HWWPF.mdf";Integrated Security=True;Connect Timeout=30
            ////string connectionString =  @"";


            //// (1, 50, '2018-08-11', '1993-8-21',  1, 1, '8-963-777-39-97', 1)

            //testButton_Copy.Click += delegate
            //{
            //    //status_Combo.Items.Clear();
            //    using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            //    {
            //        connection.Open();
            //        status_Combo.Items.Clear();
            //        var sqlRevId = $@"select * from Status";
            //        SqlCommand commandRevId = new SqlCommand(sqlRevId, connection);
            //        //MessageBox.Show(sqlRevId);
            //        SqlDataReader reader = commandRevId.ExecuteReader();



            //        while (reader.Read())
            //        {
            //            status_Combo.Items.Add(reader.GetString(1));
            //            //MessageBox.Show((reader.GetInt32(0)).ToString());
            //        }
            //    }
            //};

            //try
            //{
            //    testButton.Click += delegate
            //    {
            //        //(int)Enum.GetValues(typeof(Status))
            //        using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            //        {

            //            connection.Open();
            //            //SqlCommand command = new SqlCommand(sqlFullName, connection);
            //            //command.ExecuteNonQuery();
            //            //MessageBox.Show(sqlFullName);


            //            // Full Name
            //            var sqlFullName = $@"insert into FullName(First_Name, Last_Name, Patronymic) values (N'{name_Box.Text}', N'{lastName_Box.Text}', N'{patronymic_Box.Text}')";

            //            // Address
            //            var sqlAddress = $@"insert into Address(Country,Region, City, Street, [Street Number], [Apartment Number]) values (N'{county_Box.Text}', N'{region_Box.Text}', N'{city_Box.Text}', N'{street_Box.Text}', {streetNumber_Box.Text}, {apartmentNumber_Box.Text})";

            //            // id Status from Text
            //            var sqlRevId = $@"select Status.Id from Status where Status.Status = '{status_Combo.Text}'";


            //            SqlCommand command = new SqlCommand(sqlFullName, connection);

            //            // fill FullName
            //            command.ExecuteNonQuery();

            //            // fill Address
            //            command.CommandText = sqlAddress;
            //            command.ExecuteNonQuery();

            //            // Get id Status from selected Text
            //            command.CommandText = sqlRevId;


            //            // Get id Status from selected Text
            //            SqlDataReader reader = command.ExecuteReader();
            //            int x = 0;
            //            while (reader.Read())
            //            {
            //                x = reader.GetInt32(0);
            //            }
            //            reader.Close();


            //            var sqlReqAddEmpl = $@"INSERT INTO Employee(Gender,[Full Name], [Employment Date], [Date Of Birth], Position, Address, [Phone Number], Status) VALUES ({ gender_Combo.SelectedIndex + 1}, (SELECT max(Id) FROM FullName), '{employmentDate_Picker.SelectedDate.Value.Year}-{employmentDate_Picker.SelectedDate.Value.Month}-{employmentDate_Picker.SelectedDate.Value.Day}', '{dateOfBirth_Picker.SelectedDate.Value.Year}-{dateOfBirth_Picker.SelectedDate.Value.Month}-{dateOfBirth_Picker.SelectedDate.Value.Day}', {position_Combo.SelectedIndex + 1}, (SELECT max(Id) FROM Address), '{phoneNumber_Box.Text}', {x})";

            //            // add Employee
            //            command.CommandText = sqlReqAddEmpl;
            //            command.ExecuteNonQuery();
            //        }
            //    };
            //}
            //catch (Exception e)
            //{ MessageBox.Show(e.ToString()); }
        }

        #region IView

        public bool departments_ComboIsEditable
        {
            get => departments_Combo.IsEditable;
            set => departments_Combo.IsEditable = value;
        }
        public int Selected_Change_Employee_Department_Combo
        {
            get => Change_Employee_Department_Combo.SelectedIndex;
            set => Change_Employee_Department_Combo.SelectedIndex = value;
        }
        public int SelectedDepartment
        {
            get => departments_Combo.SelectedIndex;
            set => departments_Combo.SelectedIndex = value;
        }
        public int SelectedEmployee
        {
            get => employeesView.SelectedIndex;
            set => employeesView.SelectedIndex = value;
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
            get => patronymic_Box.Text;
            set => patronymic_Box.Text = value;
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
            get => int.Parse(apartmentNumber_Box.Text);
            set => apartmentNumber_Box.Text = value.ToString();
        }
        public string PhoneNumber
        {
            get => phoneNumber_Box.Text;
            set => phoneNumber_Box.Text = value;
        }
        public int StatusNow
        {
            get => status_Combo.SelectedIndex;
            set => status_Combo.SelectedIndex = value;
        }
        public ICollection<string> employeeList
        {
            get => employeesView.ItemsSource as ICollection<string>;
            set => employeesView.ItemsSource = value;
        }
        public ICollection<string> departmentList
        {
            get => departments_Combo.ItemsSource as ICollection<string>;
            set => departments_Combo.ItemsSource = value;
        }
        public string DepartmentComboText
        {
            get => departments_Combo.Text;
            set => departments_Combo.Text = value;
        }

        /// <summary>
        /// Список сотрудников входящих в отдел
        /// </summary>
        public ICollection<string> statusList {
            get => status_Combo.ItemsSource as ICollection<string>;
            set => status_Combo.ItemsSource = value;
        }

        /// <summary>
        /// Список сотрудников входящих в отдел
        /// </summary>
        public ICollection<string> genderList
        {
            get => gender_Combo.ItemsSource as List<string>;
            set => gender_Combo.ItemsSource = value;
        }

        /// <summary>
        /// Список сотрудников входящих в отдел
        /// </summary>
        public ICollection<string> positionList
        {
            get => position_Combo.ItemsSource as ICollection<string>;
            set => position_Combo.ItemsSource = value;
        }

        #endregion
    }
}