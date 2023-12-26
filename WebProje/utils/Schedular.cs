using WebProje.Models;

namespace WebProje.utils;

public class Scheduler
{
    public List<DateTime> GenerateSchedule(List<WorkingTimes> workingTimesList, List<Appointment> appointments,
        int sessionTime, int month, int year, int day)
    {
        var schedule = new List<DateTime>();
        DateTime startOfTheDay = new DateTime(year, month, day);
        if (startOfTheDay < DateTime.Now)
        {
            return schedule;
        }
        DateTime endOfTheDay = startOfTheDay.AddDays(1).AddSeconds(-1);
        string today = startOfTheDay.DayOfWeek.ToString().Substring(0, 3);
        WorkingTimes wt = workingTimesList.FirstOrDefault(it => it.DaysOfWeek.Contains(today));
        if (wt == null)
        {
            return schedule;
        }

        int loopCount = (wt.EndTime - wt.StartTime) / sessionTime;
        for (int i = 0; i < loopCount; i++)
        {
            DateTime startTime = new DateTime(year, month, day, wt.StartTime + i * sessionTime, 0, 0);
            DateTime endTime = startTime.AddHours(sessionTime);
            if (appointments.Any(appointment =>
                    appointment.DateAndTime >= startTime && appointment.DateAndTime < endTime))
            {
                continue;
            }
            else
            {
                schedule.Add(startTime);
            }
        }

        return schedule;
    }
}
