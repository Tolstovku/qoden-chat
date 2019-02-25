using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Qoden.Util;

namespace qoden_chat.src.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, string> ConnectionInRoom = new ConcurrentDictionary<string, string>();

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connId = Context.ConnectionId;
            if (ConnectionInRoom.TryRemove(connId, out var roomId))
            {
                await Groups.RemoveFromGroupAsync(connId, roomId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinRoom(string newRoomId)
        {
            var hasJoinedMsg = $"{UserInfoProvider.GetUsername(Context)} has joined the room.";
            var hasLeftMsg = $"{UserInfoProvider.GetUsername(Context)} has joined the room.";
            var connId = Context.ConnectionId;
            if (ConnectionInRoom.TryGetValue(connId, out var oldRoomId))
            {
                await Groups.RemoveFromGroupAsync(connId, oldRoomId);
            }
            await Groups.AddToGroupAsync(connId, newRoomId);
            ConnectionInRoom.AddOrUpdate(connId, newRoomId, (k, v) => v);
            await Clients.Group(newRoomId).SendAsync("sendMessage", hasJoinedMsg);
            if (oldRoomId != null)
                await Clients.Group(oldRoomId).SendAsync("sendMessage", hasLeftMsg);
        }

        public async Task SendMessage(string msg)
        {
            var msgWithAuthor = $"{UserInfoProvider.GetUsername(Context)}: {msg}\n";
            var connId = Context.ConnectionId;
            var roomId = ConnectionInRoom.GetValue(connId);
            await Clients.Group(roomId).SendAsync("sendMessage", msgWithAuthor);
        }
    }
}