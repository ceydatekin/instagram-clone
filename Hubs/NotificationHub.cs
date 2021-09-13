using Instagram.Helpers;
using Instagram.InstagramContext;
using Instagram.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Instagram.Hubs
{
    public class NotificationHub : Hub
    {
        private IHttpContextAccessor httpContextAccessor;
        public NotificationHub(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task NotifyForFollow(string user, string receiverUserName, string message)
        {
            if (Connections.list.SingleOrDefault(s => s.Username == receiverUserName) != null)
            {
                await Clients.Client(Connections.list.SingleOrDefault(s => s.Username == receiverUserName).ConnectionId).SendAsync("GetNotificationForFollow", user, message);
            }
            else
            {
               
            }

        }
        public async Task NotifyForShare(string user, string message)
        {
                await Clients.All.SendAsync("GetNotificationForShare", user, message);
         

        }
        public async Task NotifyForLike(string user, string receiverUserName, string message)
        {
           if( Connections.list.SingleOrDefault(s => s.Username == receiverUserName)!=null)
            {
                await Clients.Client(Connections.list.SingleOrDefault(s => s.Username == receiverUserName).ConnectionId).SendAsync("GetNotificationForLike", user, message);
            }
            else
            {

            }
           
        }
        public override Task OnConnectedAsync()
        {
            Connections.list.Add(new ConnectionInfos()
            {
                ConnectionId = Context.ConnectionId,
                Username = new SessionHelper(httpContextAccessor).Get("kullanici_adi")
            });
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        { 
            Connections.list.RemoveAt(Connections.list.FindIndex(s => s.Username == new SessionHelper(httpContextAccessor).Get("kullanici_adi")));
            return base.OnDisconnectedAsync(exception);
        }
    }
      
    public static class Connections


    {
        public static List<ConnectionInfos> list = new List<ConnectionInfos>();
    }
}
