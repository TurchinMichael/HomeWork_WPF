using System.Windows;

namespace WPF_Company_Employees
{
    /// <summary>
    /// Логика взаимодействия для AddNewDepartment.xaml
    /// </summary>
    public partial class AddNewDep : Window
    {
        Presenter p;

        /// <summary>
        /// Класс добавления нового отдела
        /// </summary>
        public AddNewDep()
        {
            InitializeComponent();

            addNewDepButton.Click += delegate {
                p.AddNewDepartment();
                MessageBox.Show("Отдел успешно добавлен");
                p.fillDepartmentCombo();
                nameNewDepartment_Box.Clear();
            };

            refreshDepButton.Click
                 += delegate { nameNewDepartment_Box.Clear(); };
        }

        /// <summary>
        /// Имя мепартамента
        /// </summary>
        public string NewDepName
        {
            get => nameNewDepartment_Box.Text;
            set => nameNewDepartment_Box.Text = value;
        }

        /// <summary>
        /// Метод получающий объект созданного презентера
        /// </summary>
        public void GetDepatments(Presenter presenter)
        {
            p = presenter;
        }
    }
}