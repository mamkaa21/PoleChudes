using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

internal class MyHub : Hub
{
    static Dictionary<string, ISingleClientProxy> clientsByNick = new();
    private readonly Rooms rooms;
    public override Task OnConnectedAsync()
    {
        Clients.Caller.SendAsync("hello", "Придумайте ник");
        return base.OnConnectedAsync();
    }
    
    public void Nickname(string nick)
    {
        var check = clientsByNick.Keys.FirstOrDefault(s => s == nick);
        if (check != null)
        {
            Clients.Caller.SendAsync("hello", "Этот ник занят. Придумайте другой");
            return;
        }
        else
        {
            clientsByNick.Add(nick, Clients.Caller);
            
        }
    }
}

