using System;using System.Collections.Generic;  
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

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

                output.Close();
                WriteLog("Порт открыт");
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        public void TestMthod()
        {

        }
    }
}
