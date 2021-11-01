using FirebaseAdmin.Messaging;
using HRS.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Infrastructure.Services.Notifications
{
    public class NotificationService
    {
        public async Task<bool> SendByFCM(string token,NotificationDto dto)
        {
            try
            {
                var notification = new Message()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "Title", dto.Title },
                    { "Body", dto.Body },
                    { "Action", dto.Action.ToString() },
                    { "ActionId", dto.ActionId }
                },
                    Token = token
                };
                await FirebaseMessaging.DefaultInstance.SendAsync(notification);
                return true;

            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
