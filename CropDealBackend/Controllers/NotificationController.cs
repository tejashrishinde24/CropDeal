using CropDealBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/notifications")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotification _notification;

    public NotificationController(INotification notification)
    {
        _notification = notification;
    }

    [HttpGet("{dealerId}")]
    public async Task<IActionResult> GetNotifications(int dealerId)
    {
        var notifications = await _notification.GetNotificationsAsync(dealerId);
        return Ok(notifications);
    }

    // 📌 Mark a specific notification as read
    [HttpPut("mark-read/{id}")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var success = await _notification.MarkAsReadAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }

    // 📌 Mark all notifications for a dealer as read
    [HttpPut("mark-all-read/{dealerId}")]
    public async Task<IActionResult> MarkAllAsRead(int dealerId)
    {
        var success = await _notification.MarkAllAsReadAsync(dealerId);
        if (!success) return NotFound();

        return NoContent();
    }

    // 📌 Create a notification (Example: When a new crop is added)
    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
    {
        var newNotification = await _notification.CreateNotificationAsync(notification.DealerId, notification.CropId, notification.Message);

        return CreatedAtAction(nameof(GetNotifications), new { dealerId = newNotification.DealerId }, newNotification);
    }

    // 📌 Delete a specific notification
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        var success = await _notification.DeleteNotificationAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }

    // 📌 Delete all notifications for a dealer
    [HttpDelete("delete-all/{dealerId}")]
    public async Task<IActionResult> DeleteAllNotifications(int dealerId)
    {
        var success = await _notification.DeleteAllNotificationsAsync(dealerId);
        if (!success) return NotFound();

        return NoContent();
    }
}
