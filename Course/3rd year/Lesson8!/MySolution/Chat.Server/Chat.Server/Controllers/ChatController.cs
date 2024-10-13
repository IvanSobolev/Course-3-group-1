using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController
{
    [HttpGet("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            var username = HttpContext.Request.Query["username"];
            WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            _connectedClients.TryAdd(username, webSocket);
            await BroadcastMessage($"{username} присоединился к чату.");

            await ReceiveMessages(username, webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = 400;
        }
    }
}