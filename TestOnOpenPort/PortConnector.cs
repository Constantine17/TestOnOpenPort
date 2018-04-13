using System;
using System.Collections.Generic;  
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TestOnOpenPort
{
    class PortConnector
    {
        public string messege = "";
        Action<string> ConsoleWrite;
        string port;

        public PortConnector(Action<string> funWrite, string port)
        {
            ConsoleWrite = funWrite;
            this.port = port;
        }

        private void WriteLog(string messege)
        {
            ConsoleWrite(messege);
            this.messege = messege;
        }

        public async void OpenConnection()
        {
            string host = Dns.GetHostName();
            var addressesList = Dns.GetHostEntry(host).AddressList;

            HttpListener lisen = new HttpListener(); // запуск слушателя
            
            foreach(var adress in addressesList)
            {
                if (adress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                    continue;

                string url = @"http://" + adress + ":" + port + "/";
                lisen.Prefixes.Add(url); // адрес прослушки
                WriteLog("Прослушивание по адрессу:  " + url);
            }

            try
            {
                lisen.Start();
                WriteLog("Соеденение открыто");

                var context = await lisen.GetContextAsync();

                var request = context.Request;

                var response = context.Response;
                var output = response.OutputStream;

                string text = "OK, port is opened. Your user host response adress: " + request.UserHostAddress;
                byte[] byteArray = Encoding.UTF8.GetBytes(text); // string to stream
                output.Write(byteArray, 0, byteArray.Length); // output date

                output.Close(); // response

                WriteLog("Порт открыт");
                WriteLog("Адрес запроса: " + request.RemoteEndPoint);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }
    }
}
