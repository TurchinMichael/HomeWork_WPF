using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Company_Employees
{
    interface IViewNewDepartment
    {
        /// <summary>
        /// Наименование нового департамента
        /// </summary>
        string NewDepName { get; set; }

        /// <summary>
        /// Получение презентера
        /// </summary>
        /// <param name="presenter">Презентер</param>
        void GetDepatments(Presenter presenter);
        
        /// <summary>
        /// Установка родитеской формы
        /// </summary>
        /// <param name="owner"></param>
        void Owner(Window owner);

        /// <summary>
        /// Отображение формы
        /// </summary>
        void Show();
    }
}
