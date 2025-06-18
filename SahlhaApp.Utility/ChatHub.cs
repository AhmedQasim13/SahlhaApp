using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace SahlhaApp.Utility
{
    
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> _userConnections = new();
        private static readonly ConcurrentDictionary<string, string> _userPairs = new();

        public async Task RegisterUser(string userId)
        {
            _userConnections[userId] = Context.ConnectionId;
            await Clients.Caller.SendAsync("UpdateUserList", _userConnections.Keys.ToList());
            await Clients.AllExcept(Context.ConnectionId).SendAsync("UpdateUserList", _userConnections.Keys.ToList());
            await Clients.Caller.SendAsync("Registered", userId);
        }

        public async Task InitiateChat(string toUserId)
        {
            if (_userConnections.TryGetValue(toUserId, out var connectionId))
            {
                var fromUserId = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

                _userPairs[fromUserId] = toUserId;
                _userPairs[toUserId] = fromUserId;

                await Clients.Client(connectionId).SendAsync("ChatRequest", fromUserId);
            }
            else
            {
                await Clients.Caller.SendAsync("UserOffline", toUserId);
            }
        }

        public async Task SendMessage(string toUserId, string message)
        {
            var senderId = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (!_userPairs.TryGetValue(senderId, out var pairedId) || pairedId != toUserId)
                return; 

            if (_userConnections.TryGetValue(toUserId, out var receiverConnId))
            {
                await Clients.Client(receiverConnId)
                             .SendAsync("ReceiveMessage", senderId, message);
            }

            await Clients.Caller.SendAsync("ReceiveMessage", senderId, message);
        }



        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (userId != null)
            {
                _userConnections.TryRemove(userId, out _);
                if (_userPairs.TryRemove(userId, out var pairedUserId))
                {
                    _userPairs.TryRemove(pairedUserId, out _);
                    await Clients.Client(_userConnections[pairedUserId])
                        .SendAsync("PartnerDisconnected");
                }
                await Clients.All.SendAsync("UpdateUserList", _userConnections.Keys.ToList());
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
