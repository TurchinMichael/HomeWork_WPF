using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApplicationCompany.Models
{
    public class GetEmployee
    {
        // presenter
        public Employee GetEmployeeId(int id)
        {
            string s = $"select  Employee.Id, Gender.id, FullName.First_Name, FullName.Last_Name, FullName.Patronymic, [Employment Date], [Date Of Birth],  PositionName.Id, Position.Salary, Address.Country, Address.Region, Address.City, Address.Street, Address.[Street Number], Address.[Apartment Number], [Phone Number], Status.Status from Employee inner join Address on Employee.Address = Address.Id inner join FullName on Employee.[Full Name] = FullName.Id inner join Gender on Employee.Gender = Gender.Id inner join Position on Employee.Position = Position.Id inner join PositionName on Position.PositionName = PositionName.Id inner join Status on Employee.Status = Status.Id where Employee.Id = {id}";

            Connection newConnection = new Connection();

            newConnection.connect();

            SqlCommand commandRevInformation = new SqlCommand(s, newConnection.sqlConnection);

            SqlDataReader reader = commandRevInformation.ExecuteReader();
            //string TempStatus = string.Empty;

            Employee tempEmployee = new Employee();

            while (reader.Read())
            {
                tempEmployee = new Employee(
                    (Gender)Enum.ToObject(typeof(Gender), reader.GetInt32(1) - 1),
                    new Full_Name(reader.GetString(2), reader.GetString(3), reader.GetString(4)),
                    reader.GetDateTime(5),
                    reader.GetDateTime(6),
                    new Position((PositionName)Enum.ToObject(typeof(PositionName), reader.GetInt32(7) - 1), reader.GetDecimal(8)),
                    new Address(reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetInt32(13), reader.GetInt32(14)),
                    reader.GetString(15),
                    (Status)Enum.ToObject(typeof(Status), reader.GetString(16)));
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
    }
}