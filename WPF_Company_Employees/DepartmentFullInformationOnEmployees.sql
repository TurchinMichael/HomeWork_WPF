select Department.Id, [Department Name].Department_Name, FullName.First_Name, FullName.Last_Name, FullName.Patronymic,
[Employment Date],[Date Of Birth], Gender.Gender, PositionName.Name, Position.Salary, 
Address.Country, Address.Region, Address.City, Address.Street, Address.[Street Number], Address.[Apartment Number],
[Phone Number], Status.Status

from Department
    inner join Employee on Department.Employee = Employee.Id
    inner join [Department Name] on Department.[Department Name] = [Department Name].Id

    inner join Address on Employee.Address = Address.Id
    inner join FullName on Employee.[Full Name] = FullName.Id
    inner join Gender on Employee.Gender = Gender.Id
    inner join Position on Employee.Position = Position.Id
    inner join PositionName on Position.PositionName = PositionName.Id
    inner join Status on Employee.Status = Status.Id
	