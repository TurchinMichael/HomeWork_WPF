using System;
using System.Collections.ObjectModel;

namespace WebApplicationCompany.Models
{
    #region base classes

    /// <summary>
    /// Класс сущности сотрудника компании
    /// </summary>
    public class Employee
    {
        #region Временно не реализовал фото
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
        #endregion
        
        Full_Name _fullName;
        DateTime _employmentDate;
        Gender _gender;
        DateTime _dateOfBirth;
        Position _position;
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
        /// Состояние в котором на данный момент находится сотрудник
        /// </summary>
        public Status StatusNow
        {
            get => _status;
            set => _status = value;
        }

        /// <summary>
        /// Сотрудник
        /// </summary>
        /// <param name="gender">Пол сотрудника</param>
        /// <param name="fullName">ФИО сотрудника</param>
        /// <param name="employmentDate">Дата заключения договора о приеме на работу</param>
        /// <param name="dateOfBirth">Дата рождения сотрудника</param>
        /// <param name="position">Должность сотрудника</param>
        /// <param name="address">Адрес сотрудника</param>
        /// <param name="phoneNumber">Телефонный номер сотрудника</param>
        /// <param name="status">состояние в котором на данный момент находится сотрдуник</param>
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
    public class Department
    {
        string _departmentName;
        ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();

        /// <summary>
        /// Наменование данного отдела
        /// </summary>
        public string DepartmentName { get => _departmentName; set => _departmentName = value; }

        /// <summary>
        /// Сотрудники входящие в данный отдел
        /// </summary>
        public ObservableCollection<Employee> Employees { get => _employees; set => _employees = value; }
        
        /// <summary>
        /// Отдел
        /// </summary>
        /// <param name="departmentName">Наименование отдела</param>
        /// <param name="employees">Лист сотрудников, входящих в данный отдел</param>
        public Department(string departmentName, ObservableCollection<Employee> employees)
        {
            _departmentName = departmentName;
            _employees = employees;
        }

        /// <summary>
        /// Отдел
        /// </summary>
        /// <param name="departmentName">Наименование отдела</param>
        public Department(string departmentName)
        {
            _departmentName = departmentName;
            _employees = new ObservableCollection<Employee>();
        }

        public Department()
        {
        }
    }
    #endregion

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
    public class Address
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

        /// <summary>
        /// Страна
        /// </summary>
        public string County
        {
            get => _country;
            set => _country = value;
        }

        /// <summary>
        /// Регион
        /// </summary>
        public string Region
        {
            get => _region;
            set => _region = value;
        }

        /// <summary>
        /// Город
        /// </summary>
        public string City
        {
            get => _city;
            set => _city = value;
        }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street
        {
            get => _street;
            set => _street = value;
        }

        /// <summary>
        /// Номер улицы
        /// </summary>
        public int StreetNumber
        {
            get => _streetNumber;
            set => _streetNumber = value;
        }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public int ApartmentNumber
        {
            get => _apartmentNumber;
            set => _apartmentNumber = value;
        }

        /// <summary>
        /// Возврат полного, склееного адреса
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ($"{_country} {_region} {_city} {_street} {_streetNumber.ToString()} {_apartmentNumber.ToString()}");
        }
    }

    /// <summary>
    /// Класс описывающий должность
    /// </summary>
    public class Position
    {
        PositionName _position;
        decimal _salary;

        /// <summary>
        /// Наименование должности
        /// </summary>
        public PositionName PositionNameEnum
        {
            get => _position;
            set => _position = value;
        }

        /// <summary>
        /// Оклад сотрудника
        /// </summary>
        public decimal Salary
        {
            get => _salary;
            set => _salary = value;
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

    /// <summary>
    /// Класс описывающий ФИО сотрудника
    /// </summary>
    public class Full_Name
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
        
        override public string ToString()
        {
            return ($"{_lastName} {_firstName} {_patronymic}");
        }
    }
    #endregion
}