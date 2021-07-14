using System;
using System.Text;

using WebSocketSharp;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chat_prototype_client {
    class Program {
        private static string uri = "ws://127.0.0.1:9876/";

        static void Main(string[] args) {
            Console.Write("Bem vindo ao nosso chat! Por favor, digite o seu nome de usuário: ");
            string username = Console.ReadLine();

            Console.WriteLine($"Tudo certo { username }! Em qual sala você gostaria de entrar?: ");
            Console.WriteLine("1 - Quarto 1");
            Console.WriteLine("2 - Quarto 2");
            Console.WriteLine("3 - Quarto 3");
            Console.Write("Eu gostaria de entrar no quarto: ");
            int room = int.Parse(Console.ReadLine());

            StringBuilder uriBuilder = new StringBuilder(Program.uri);
            uriBuilder.Append("room"); uriBuilder.Append(room);
            using(WebSocket client = new WebSocket(uriBuilder.ToString())) {
                client.Connect();

                client.OnMessage += Client_OnMessage;

                Console.WriteLine("Bem vindo ao quarto " + room + "! Agora você poderá enviar mensagens para os outros usuários.");
                JObject message = new JObject();
                message["user"] = username;
                while(true) {
                    string textMessage = Console.ReadLine();
                    message["message"] = textMessage;

                    client.Send(message.ToString());
                }
            }
        }

        private static void Client_OnMessage(object sender, MessageEventArgs e) {
            Console.WriteLine(e.Data);
        }
    }
}
