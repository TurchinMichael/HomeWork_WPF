using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace WPF_Company_Employees
{
    #region enumVariables

    /// <summary>
    /// Наименование должности
    /// </summary>
    public enum PositionName
    {
        Programmer,
        Manager,
        Director,
        Accountant,
        Secretary
    }

    /// <summary>
    /// Пол сотруника
    /// </summary>
    public enum Gender
    {
        M, W
    }

    /// <summary>
    /// В каком состоянии на данный момент находится сотрудник
    /// </summary>
    public enum Status
    {
        Work,
        Vacation,
        Trial_Period,
        Sick_Leave
    }
    #endregion

    #region secondary classes
    /// <summary>
    /// Класс описывающий адрес
    /// </summary>
    class Address
    {
        string _country,
            _region,
            _city,
            _street;
        int _streetNumber,
            _apartmentNumber;

        /// <summary>
        /// Адрес сотрудника
        /// </summary>
        /// <param name="county">Страна</param>
        /// <param name="region">Регион</param>
        /// <param name="city">Город</param>
        /// <param name="street">Улица</param>
        /// <param name="streetNumber">Номер улицы</param>
        /// <param name="apartmentNumber">Номер квартиры</param>
        public Address(string county, string region, string city, string street, int streetNumber, int apartmentNumber)
        {
            _country = county;
            _region = region;
            _city = city;
            _street = street;
            _streetNumber = streetNumber;
            _apartmentNumber = apartmentNumber;
        }

        public override string ToString()
        {
            return ($"{_country} {_region} {_city} {_street} {_streetNumber.ToString()} {_apartmentNumber.ToString()}");
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public Address(string addressString)
        {
            _country = addressString; // временное решение
        }
    }

    /// <summary>
    /// Класс описывающий должность
    /// </summary>
    class Position
    {
        PositionName _position;
        decimal _salary;

        /// <summary>
        /// Оклад работника
        /// </summary>
        public decimal Salary
        {
            get => _salary;
            set => _salary = value;
        }

        public PositionName PositionNameEnum
        {
            get => _position;
            set => _position = value;
        }

        /// <summary>
        /// Должность занимаемая сотрудником
        /// </summary>
        /// <param name="position"></param>
        /// <param name="salary"></param>
        public Position(PositionName position, decimal salary)
        {
            _position = position;
            _salary = salary;
        }

        public override string ToString()
        {
            return _position.ToString();
        }
    }
    
    class Full_Name
    {
        string
            _firstName,
            _lastName,
            _patronymic = String.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic
        {
            get => _patronymic;
            set => _patronymic = value;
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="secondName">Фамилия</param>
        /// <param name="patronymic">Отчество</param>
        public Full_Name(string firstName, string lastName, string patronymic)
        {
            _firstName = firstName;
            _lastName = lastName;
            _patronymic = patronymic;
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="secondName">Фамилия</param>
        public Full_Name(string firstName, string secondName)
        {
            _firstName = firstName;
            _lastName = secondName;
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public Full_Name(string fullName)
        {
            _firstName = fullName; // временное решение
        }

        override public string ToString()
        {
            return ($"{_firstName} {_patronymic} {_lastName}");
        }
    }
    #endregion

    #region base classes

    /// <summary>
    /// Класс сущности работника компании
    /// </summary>
    class Employee
    {
        //public Image Photo
        //{
        //    get => _photo;
        //    set => _photo = value;
        //    //photo = Image.FromFile(fileName);
        //}
        //public string PhotoPath
        //{
        //    get => _photoPath;
        //    //photo = Image.FromFile(fileName);
        //}

        /// <summary>
        /// Дата заключения договора о приеме на работу
        /// </summary>
        DateTime _employmentDate;
        Gender _gender;
        DateTime _dateOfBirth;
        Position _position;
        Full_Name _fullName;
        Address _address;
        string _phoneNumber;
        Status _status;

        /// <summary>
        /// Пол сотрудника
        /// </summary>
        public Gender GenderEmployee
        {
            get => _gender;
            set => _gender = value;
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public Full_Name Full_Name
        {
            get => _fullName;
            set => _fullName = value;
        }

        /// <summary>
        /// Дата заключения договора о приеме на работу
        /// </summary>
        public DateTime EmploymentDate
        {
            get => _employmentDate;
            set => _employmentDate = value;
        }

        /// <summary>
        /// Дата рождения сотрудника
        /// </summary>
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
        }

        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public Position Position
        {
            get => _position;
            set => _position = value;
        }

        /// <summary>
        /// Адрес сотрудника
        /// </summary>
        public Address Address
        {
            get => _address;
            set => _address = value;
        }

        /// <summary>
        /// Телефонный номер сотрудника
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        /// <summary>
        /// Состояние в котором на данный момент находится работник
        /// </summary>
        public Status StatusNow
        {
            get => _status;
            set => _status = value;
        }

        /// <summary>
        /// Сотрудник
        /// </summary>
        /// <param name="fullName">ФИО сотрудника</param>
        /// <param name="employmentDate">Дата заключения договора о приеме на работу</param>
        /// <param name="dateOfBirth">Дата рождения сотрудника</param>
        /// <param name="position">Должность сотрудника</param>
        /// <param name="address">Адрес сотрудника</param>
        /// <param name="phoneNumber">Телефонный номер сотрудника</param>
        /// <param name="status">состояние в котором на данный момент находится работник</param>
        public Employee(Gender gender, Full_Name fullName, DateTime employmentDate, DateTime dateOfBirth, Position position, Address address, string phoneNumber, Status status)
        {
            _gender = gender;
            _fullName = fullName;
            _employmentDate = employmentDate;
            _dateOfBirth = dateOfBirth;
            _position = position;
            _address = address;
            _phoneNumber = phoneNumber;
            _status = status;
        }
        public Employee()
        {
        }
    }

    /// <summary>
    /// Класс описывающий сущность отдела, в котором числятся сотрудники
    /// </summary>
    class Department
    {
        string _departmentName;
        List<Employee> _employees = new List<Employee>();

        /// <summary>
        /// Работники входящие в данный отдел
        /// </summary>
        public List<Employee> Employees { get => _employees; set => _employees = value; }

        /// <summary>
        /// Наменование данного отдела
        /// </summary>
        public string DepartmentName { get => _departmentName; set => _departmentName = value; }

        /// <summary>
        /// Отдел
        /// </summary>
        /// <param name="departmentName">Наименование отдела</param>
        /// <param name="employees">Лист сотрудников, входящих в данный отдел</param>
        public Department(string departmentName, List<Employee> employees)
        {
            _departmentName = departmentName;
            _employees = employees;
        }
        public Department()
        {
        }
    }
    #endregion
}