select Position.Id, PositionName.Name, Position.Salary

from Position
    inner join PositionName on Position.PositionName = PositionName.Id