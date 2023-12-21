namespace WebProje.Models;

public class WorkingTimes
{
    public int Id { get; set; }

    public string DaysOfWeek { set; get; } = "";
    
    public int StartTime { set; get; }
    
    public int EndTime { set; get; }
    
    public string UserId { set; get; } = null!;
    
    public User? User { set; get; } = null!;
    
}