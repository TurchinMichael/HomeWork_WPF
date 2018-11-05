using System;
using System.Collections.Generic;

namespace WPF_Company_Employees
{
    public interface IView // все поля из окна
    {
        /// <summary>
        /// Список отделов
        /// </summary>
        IEnumerable<string> departmentList { get; set; }

        /// <summary>
        /// Список сотрудников входящих в отдел
        /// </summary>
        IEnumerable<string> employeeList { get; set; }

        /// <summary>
        /// Индекс выбранного отдела, для перевода сотрудника
        /// </summary>
        int Selected_Change_Employee_Department_Combo { get; set; }

        /// <summary>
        /// Индекс выбранного отдела
        /// </summary>
        int SelectedDepartment { get; set; }

        /// <summary>
        /// Индекс выбранного сотрудника
        /// </summary>
        int SelectedEmployee { get; set; }

        /// <summary>
        /// Наименивание отдела
        /// </summary>
        string DepartmentComboText { get; set; }

        /// <summary>
        /// Пол сотрудника
        /// </summary>
        int GenderEmployee { get; set; }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        string Patronymic { get; set; }

        /// <summary>
        /// Дата приема на работу сотрудника
        /// </summary>
        DateTime EmploymentDate { get; set; }

        /// <summary>
        /// Дата рождения на работу сотрудника
        /// </summary>
        DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Индекс выбранной должности
        /// </summary>
        int PositionNameEnum { get; set; }

        /// <summary>
        /// Зарплата сотрудника
        /// </summary>
        decimal Salary { get; set; }

        /// <summary>
        /// Страна сотрудника
        /// </summary>
        string County { get; set; }

        /// <summary>
        /// Регион сотрудника
        /// </summary>
        string Region { get; set; }

        /// <summary>
        /// Город сотрудника
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// Улица проживания сотрудника
        /// </summary>
        string Street { get; set; }

        /// <summary>
        /// Номер улицы сотрудника
        /// </summary>
        int StreetNumber { get; set; }

        /// <summary>
        /// Номер квартиры сотрудника
        /// </summary>
        int ApartmentNumber { get; set; }

        /// <summary>
        /// Номер телефона сотрудника
        /// </summary>
        string PhoneNumber { get; set; }

        /// <summary>
        /// Статус сотрудника
        /// </summary>
        int StatusNow { get; set; }
    }
}