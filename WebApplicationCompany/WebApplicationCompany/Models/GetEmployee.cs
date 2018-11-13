using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApplicationCompany.Models
{
    public class GetEmployee
    {
        Connection newConnection = new Connection();

        // presenter
        public Employee GetEmployeeId(int id)
        {
            string s = $"select  Employee.Id, Gender.id, FullName.First_Name, FullName.Last_Name, FullName.Patronymic, [Employment Date], [Date Of Birth],  PositionName.Id, Position.Salary, Address.Country, Address.Region, Address.City, Address.Street, Address.[Street Number], Address.[Apartment Number], [Phone Number], Status.Id from Employee inner join Address on Employee.Address = Address.Id inner join FullName on Employee.[Full Name] = FullName.Id inner join Gender on Employee.Gender = Gender.Id inner join Position on Employee.Position = Position.Id inner join PositionName on Position.PositionName = PositionName.Id inner join Status on Employee.Status = Status.Id where Employee.Id = {id}";


            newConnection.connect();

            SqlCommand commandRevInformation = new SqlCommand(s, newConnection.sqlConnection);

            SqlDataReader reader = commandRevInformation.ExecuteReader();
            //string TempStatus = string.Empty;

            Employee tempEmployee = new Employee();

            while (reader.Read())
            {
                //Gender gender = (Gender)Enum.ToObject(typeof(Gender), reader.GetInt32(1) - 1);
                //Full_Name full_Name = new Full_Name(reader.GetString(2), reader.GetString(3), reader.GetString(4));
                //DateTime dateTimeEmpl = reader.GetDateTime(5);
                //DateTime dateTimeBir = reader.GetDateTime(6);
                //Position position = new Position((PositionName)Enum.ToObject(typeof(PositionName), reader.GetInt32(7) - 1), reader.GetDecimal(8));
                //Address address = new Address(reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetInt32(13), reader.GetInt32(14));
                //Status status = (Status)Enum.ToObject(typeof(Status), reader.GetInt32(16));


                tempEmployee = new Employee(
                    (Gender)Enum.ToObject(typeof(Gender), reader.GetInt32(1) - 1),
                    new Full_Name(reader.GetString(2), reader.GetString(3), reader.GetString(4)),
                    reader.GetDateTime(5),
                    reader.GetDateTime(6),
                    new Position((PositionName)Enum.ToObject(typeof(PositionName), reader.GetInt32(7) - 1), reader.GetDecimal(8)),
                    new Address(reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetInt32(13), reader.GetInt32(14)),
                    reader.GetString(15),
                    (Status)Enum.ToObject(typeof(Status), reader.GetInt32(16)));
                //view.GenderEmployee = reader.GetInt32(1) - 1;
                //view.FirstName = reader.GetString(2);
                //view.LastName = reader.GetString(3);
                //if (!reader.IsDBNull(4))
                //    view.Patronymic = reader.GetString(4);
                //view.EmploymentDate = reader.GetDateTime(5);
                //view.DateOfBirth = reader.GetDateTime(6);
                //view.PositionNameEnum = reader.GetInt32(7) - 1;
                //view.Salary = reader.GetDecimal(8);
                //view.County = reader.GetString(9);
                //view.Region = reader.GetString(10);
                //view.City = reader.GetString(11);
                //view.Street = reader.GetString(12);
                //view.StreetNumber = reader.GetInt32(13);
                //view.ApartmentNumber = reader.GetInt32(14);
                //view.PhoneNumber = reader.GetString(15);
                //TempStatus = reader.GetString(16);
            }
            reader.Close();

            //int x = 0;
            //for (int i = 0; i < view.statusList.Count; i++)
            //{
            //    if ((view.statusList as List<string>)[i] == TempStatus)
            //        x = i;
            //}

            //view.StatusNow = x;
            return tempEmployee;
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {

                //Full_Name tempName;

                // Full Name
                var sqlFullName = $@"insert into FullName(First_Name, Last_Name, Patronymic) values (N'{employee.Full_Name.FirstName}', N'{employee.Full_Name.LastName}', N'{employee.Full_Name.Patronymic}')";

                // Address
                var sqlAddress = $@"insert into Address(Country,Region, City, Street, [Street Number], [Apartment Number]) values (N'{employee.Address.County}', N'{employee.Address.Region}', N'{employee.Address.City}', N'{employee.Address.Street}', {employee.Address.StreetNumber}, {employee.Address.ApartmentNumber})";

                // id Status from Text
                var sqlRevId = $@"select Status.Id from Status where Status.Status = '{(int)employee.StatusNow + 1}'";


                newConnection.connect();

                //SqlCommand commandRevInformation = new SqlCommand(s, newConnection.sqlConnection);

                SqlCommand command = new SqlCommand(sqlFullName, newConnection.sqlConnection);

                // fill FullName
                command.ExecuteNonQuery();

                // fill Address
                command.CommandText = sqlAddress;
                command.ExecuteNonQuery();

                // Get id Status from selected Text
                command.CommandText = sqlRevId;

                // Get id Status from selected Text
                SqlDataReader reader = command.ExecuteReader();
                int x = 0;
                while (reader.Read())
                {
                    x = reader.GetInt32(0);
                }
                reader.Close();


                var sqlReqAddEmpl = $@"INSERT INTO Employee(Gender,[Full Name], [Employment Date], [Date Of Birth], Position, Address, [Phone Number], Status) VALUES ({ (int)employee.GenderEmployee + 1}, (SELECT max(Id) FROM FullName), '{employee.EmploymentDate.Year}-{employee.EmploymentDate.Month}-{employee.EmploymentDate.Day}', '{employee.DateOfBirth.Year}-{employee.DateOfBirth.Month}-{employee.DateOfBirth.Day}', {(int)employee.Position.PositionNameEnum + 1}, (SELECT max(Id) FROM Address), '{employee.PhoneNumber}', {x})";

                // add Employee
                command.CommandText = sqlReqAddEmpl;
                command.ExecuteNonQuery();

                // into department
                //var sqlReqAddEmplInDepartment = $@"insert into Department([Department Name], Employee) values ({addNewEmployee.SelectedDepartment + 1}, (SELECT max(Id) FROM Employee))";
                //command.CommandText = sqlReqAddEmplInDepartment;
                //command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

    //public class PostEmployee
    //{
    //    public bool AddEmployee(Employee employee)
    //    {
    //        try
    //        {

    //            //Full_Name tempName;

    //            // Full Name
    //            var sqlFullName = $@"insert into FullName(First_Name, Last_Name, Patronymic) values (N'{employee.Full_Name.FirstName}', N'{employee.Full_Name.LastName}', N'{employee.Full_Name.Patronymic}')";

    //            // Address
    //            var sqlAddress = $@"insert into Address(Country,Region, City, Street, [Street Number], [Apartment Number]) values (N'{employee.Address.County}', N'{employee.Address.Region}', N'{employee.Address.City}', N'{employee.Address.Street}', {employee.Address.StreetNumber}, {employee.Address.ApartmentNumber})";

    //            // id Status from Text
    //            var sqlRevId = $@"select Status.Id from Status where Status.Status = '{(int)employee.StatusNow + 1}'";


    //            newConnection.connect();

    //            //SqlCommand commandRevInformation = new SqlCommand(s, newConnection.sqlConnection);

    //            SqlCommand command = new SqlCommand(sqlFullName, newConnection.sqlConnection);

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


    //            var sqlReqAddEmpl = $@"INSERT INTO Employee(Gender,[Full Name], [Employment Date], [Date Of Birth], Position, Address, [Phone Number], Status) VALUES ({ (int)employee.GenderEmployee + 1}, (SELECT max(Id) FROM FullName), '{employee.EmploymentDate.Year}-{employee.EmploymentDate.Month}-{employee.EmploymentDate.Day}', '{employee.DateOfBirth.Year}-{employee.DateOfBirth.Month}-{employee.DateOfBirth.Day}', {(int)employee.Position.PositionNameEnum + 1}, (SELECT max(Id) FROM Address), '{employee.PhoneNumber}', {x})";

    //            // add Employee
    //            command.CommandText = sqlReqAddEmpl;
    //            command.ExecuteNonQuery();

    //            // into department
    //            //var sqlReqAddEmplInDepartment = $@"insert into Department([Department Name], Employee) values ({addNewEmployee.SelectedDepartment + 1}, (SELECT max(Id) FROM Employee))";
    //            //command.CommandText = sqlReqAddEmplInDepartment;
    //            //command.ExecuteNonQuery();
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //        return true;
    //    }
    //}
}