using socket_sharp.Controllers;
using System;

using WebSocketSharp;
using WebSocketSharp.Server;

namespace chat_prototype_server {
    class Program {
        static void Main(string[] args) {
            WebSocketServer server = new WebSocketServer("ws://127.0.0.1:9876");
            server.AddWebSocketService<ChatBehavior>("/room1");
            server.AddWebSocketService<ChatBehavior>("/room2");
            server.AddWebSocketService<ChatBehavior>("/room3");

            server.Start();

            Console.WriteLine("WebSocket server iniciado!\nAguardando requisições...");
            Console.ReadKey();

            server.Stop();
        }
    }
}
