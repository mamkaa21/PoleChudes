﻿using Microsoft.AspNetCore.SignalR;
using SignalIR;
using System.Collections.Generic;

internal class MyHub : Hub
{
    static Dictionary<string, ISingleClientProxy> clientsByNick = new();
    private readonly Rooms rooms;

    public MyHub(Rooms rooms)
    {
        this.rooms = rooms;
        rooms.SetStart(async (a, b, id) =>
        {
            await clientsByNick[a].SendAsync("opponent", b, id);
            await clientsByNick[b].SendAsync("opponent", a, id);
            await clientsByNick[a].SendAsync("maketurn", "Ваш ход");
        });
    }

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
            rooms.AddNewClient(nick);
        }
    }

    public async void MakeTurn(Turn turn)
    {
        string next = rooms.GetNextPlayer(turn);
        //string turnRes = rooms.M
    }
}

