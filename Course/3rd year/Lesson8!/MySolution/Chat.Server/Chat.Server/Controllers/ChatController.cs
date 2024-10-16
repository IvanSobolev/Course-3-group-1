using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private static ConcurrentDictionary<string, WebSocket> _connectedUsers =
        new ConcurrentDictionary<string, WebSocket>();
    
    [HttpGet("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            var username = HttpContext.Request.Query["username"];
            WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            _connectedUsers.TryAdd(username, webSocket);
            await BroadcastMessage($"{username} присоединился к чату.");

            await ReceiveMessages(username, webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = 400;
        }
    }
    
    private async Task ReceiveMessages(string username, WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult result =
            await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            await BroadcastMessage($"{username}: {message}");

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),
                System.Threading.CancellationToken.None);
        }

        _connectedUsers.TryRemove(username, out _);
        await BroadcastMessage($"{username} покинул чат.");

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription,
            System.Threading.CancellationToken.None);
    }

    private async Task BroadcastMessage(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        foreach (var client in _connectedUsers.Values)
        {
            if (client.State == WebSocketState.Open)
            {
                await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true,
                    System.Threading.CancellationToken.None);
            }
        }
    }
}