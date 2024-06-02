namespace OrderService.SharedKernel.HelperMethods;

public class Utility
{
    public static DateTime GetCurrentTime()
    {
        return DateTime.UtcNow.AddHours(1);
    }
}
