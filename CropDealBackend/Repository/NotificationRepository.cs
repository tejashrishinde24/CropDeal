using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CropDealBackend.Models;
using CropDealBackend.Models;
using Microsoft.AspNetCore.SignalR;

public class NotificationRepository:INotification
{
    private readonly CropDealContext _context;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationRepository(CropDealContext context, IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    // Get unread notifications for a dealer
    public async Task<List<Notification>> GetNotificationsAsync(int dealerId)
    {
        return await _context.Notifications
            .Where(n => n.DealerId == dealerId && n.IsRead==false)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    // 📌 Mark a specific notification as read
    public async Task<bool> MarkAsReadAsync(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null)
            return false;

        notification.IsRead = true;
        await _context.SaveChangesAsync();
        return true;
    }

    // 📌 Mark all notifications for a dealer as read
    public async Task<bool> MarkAllAsReadAsync(int dealerId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.DealerId == dealerId && n.IsRead == false)
            .ToListAsync();

        if (!notifications.Any()) return false;

        foreach (var notification in notifications)
        {
            notification.IsRead = true;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    // 📌 Create a notification when a new crop is added
    public async Task<Notification> CreateNotificationAsync(int dealerId, int cropId, string message)
    {
        var notification = new Notification
        {
            DealerId = dealerId,
            CropId = cropId,
            Message = message,
            CreatedAt = DateTime.Now,
            IsRead = false
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        // 🔴 Send real-time notification using SignalR
        await _hubContext.Clients.User(dealerId.ToString()).SendAsync("ReceiveNotification", message);

        return notification;
    }

    // 📌 Delete a specific notification
    public async Task<bool> DeleteNotificationAsync(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null)
            return false;

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return true;
    }

    // 📌 Delete all notifications for a dealer
    public async Task<bool> DeleteAllNotificationsAsync(int dealerId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.DealerId == dealerId)
            .ToListAsync();

        if (!notifications.Any()) return false;

        _context.Notifications.RemoveRange(notifications);
        await _context.SaveChangesAsync();
        return true;
    }
}
