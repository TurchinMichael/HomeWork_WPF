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
    /// Логика взаимодействия для AddNewDepartment.xaml
    /// </summary>
    public partial class AddNewDepartment : Window
    {
        Test test;
        MainWindow _owner;

        public AddNewDepartment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод получающий данные из родительского окна
        /// </summary>
        public void GetDepatments(Test _test, MainWindow owner) // не очень нравится способ передачи данных между окнами, но другого не придумал
        {
            _owner = owner;
            test = _test;
        }

        /// <summary>
        /// Метод вызывающийся при срабатывании события нажатия на кнопку "Save"
        /// </summary>
        private void addNewDepButton_Click(object sender, RoutedEventArgs e)
        {
            Add();
            MessageBox.Show("Отдел успешно добавлен");
            _owner.fillDepartmentCombo();
            Refresh();
        }

        /// <summary>
        /// Метод добавляющий новый отдел
        /// </summary>
        void Add()
        {
            test.AddNewDepartment(nameNewDepartment_Box.Text);
        }

        /// <summary>
        /// Метод вызывающийся при срабатывании события нажатия на кнопку "Refresh"
        /// </summary>
        private void refreshDepButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Метод очищающий текстовый бокс с именем отдела
        /// </summary>
        void Refresh()
        {
            nameNewDepartment_Box.Clear();
        }
    }
}