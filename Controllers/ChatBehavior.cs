using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace socket_sharp.Controllers {
    public class ChatBehavior : WebSocketBehavior {

        private readonly string uri = "ws://127.0.0.1:9876";

        protected override void OnMessage(MessageEventArgs e) {
            Console.WriteLine("Nova mensagem recebida! Enviando para os usuários conectados...");

            JObject message = JObject.Parse(e.Data);

            string response = $"{ message["user"] }: { message["message"] }";
            Sessions.Broadcast(response);
        }

        protected override void OnOpen() {
            string chatName = Context.RequestUri.AbsolutePath;
            Console.WriteLine();
            Console.WriteLine($"Um usuário se conectou ao chat { chatName.Remove(0, 1) } com sucesso!");
            
            //Sessions.Broadcast("Novo usuário conectado, digam ao olá ao seu novo companheiro de conversa :)");
        }

    }
}
