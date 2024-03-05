using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Examples.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public WorkingScheme WorkingScheme { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}


public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}


public partial class WorkingDay
{
    public int Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayOfWeek Day { get; set; }
    public int WorkingSchemeId { get; set; }
    public WorkingScheme WorkingScheme { get; set; }
}

public class WorkingScheme
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<WorkingDay> WorkingDays { get; set; }

    [NotMapped]
    public static WorkingScheme Default => new()
    {
        WorkingDays = new[]
        {
                new WorkingDay { Day = DayOfWeek.Monday, StartTime = new TimeOnly(9, 0, 0), EndTime = new TimeOnly(18, 0, 0) },
                new WorkingDay { Day = DayOfWeek.Tuesday, StartTime = new TimeOnly(9, 0, 0), EndTime = new TimeOnly(18, 0, 0) },
                new WorkingDay
                    { Day = DayOfWeek.Wednesday, StartTime = new TimeOnly(9, 0, 0), EndTime = new TimeOnly(18, 0, 0) },
                new WorkingDay
                    { Day = DayOfWeek.Thursday, StartTime = new TimeOnly(9, 0, 0), EndTime = new TimeOnly(18, 0, 0) },
                new WorkingDay { Day = DayOfWeek.Friday, StartTime = new TimeOnly(9, 0, 0), EndTime = new TimeOnly(18, 0, 0) }
            }
    };
}

public record OrderData
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}