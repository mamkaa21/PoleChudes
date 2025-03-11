using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

    internal class MyHub : Hub
    {
    static Dictionary<string, ISingleClientProxy> clientsByNick = new();
    public override Task OnConnectedAsync()
    {
        Clients.Caller.SendAsync("hello", "Придумайте ник");
        return base.OnConnectedAsync();
    }
    public MyHub() 
        { 
    
        }
    }

