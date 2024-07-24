namespace BlogPTC.Application.WebSockets.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string message);
    }
}
