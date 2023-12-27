using System.ComponentModel.DataAnnotations;

namespace WebProje.ViewModels;

public class WorkingTimesViewModel
{
    [Required] [MinLength(1)] public List<string> DaysOfWeek { set; get; } = new List<string>();

    [Required] public int StartTime { set; get; }
    [Required] public int EndTime { set; get; }
}