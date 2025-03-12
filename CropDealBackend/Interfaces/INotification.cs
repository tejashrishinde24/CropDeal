using System.Collections.Generic;
using System.Threading.Tasks;
using CropDealBackend.Models;
using CropDealBackend.Models;

public interface INotification
{
    Task<List<Notification>> GetNotificationsAsync(int dealerId);
    Task<bool> MarkAsReadAsync(int id);
    Task<bool> MarkAllAsReadAsync(int dealerId);
    Task<Notification> CreateNotificationAsync(int dealerId, int cropId, string message);
    Task<bool> DeleteNotificationAsync(int id);
    Task<bool> DeleteAllNotificationsAsync(int dealerId);
}