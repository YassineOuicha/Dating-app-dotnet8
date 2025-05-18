using System;

namespace API.SignalR;

public class PresenceTracker
{
    private static readonly Dictionary<string, List<string>> OnlineUsers = [];

    public Task<bool> UserConnected(string username, string connectionId)
    {
        var isOnline = false;
        lock (OnlineUsers)
        {
            if (OnlineUsers.ContainsKey(username))
            {
                OnlineUsers[username].Add(connectionId);
            }
            else
            {
                OnlineUsers.Add(username, [connectionId]);
                isOnline = true;
            }
        }

        return Task.FromResult(isOnline);
    }

    public Task<bool> UserDisconnected(string username, string connectionId)
    {
        var isOffline = false;
        lock (OnlineUsers)
        {
            if (!OnlineUsers.ContainsKey(username))
            {
                return Task.FromResult(isOffline);
            }
            else
            {
                OnlineUsers[username].Remove(connectionId); // just the connectionId of this device/session
            }

            if (OnlineUsers[username].Count == 0)
            {
                OnlineUsers.Remove(username); // we remove the key if no other sessions/devices
                isOffline = true;
            }
        }

        return Task.FromResult(isOffline);
    }

    public Task<String[]> GetOnlineUsers()
    {
        string[] onlineUsers;
        lock (OnlineUsers)
        {
            onlineUsers = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
        }

        return Task.FromResult(onlineUsers);
    }

    public static Task<List<string>> GetConnectionsForUser(string username)
    {
        List<string> connectionIds;

        if (OnlineUsers.TryGetValue(username, out var connections))
        {
            lock (connections)
            {
                connectionIds = connections.ToList();
            }
        }
        else
        {
            connectionIds = [];
        }

        return Task.FromResult(connectionIds);
    }
}
