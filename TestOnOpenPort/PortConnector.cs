using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TestOnOpenPort
{
    class PortConnector
    {
        public string mesege = "";
        Action<string> ConsoleWrite;

        public PortConnector(Action<string> write)
        {
            ConsoleWrite = write;
        }

        public void OpenConnection()
        {
            HttpListener lisen = new HttpListener(); // запуск слушателя 
            lisen.Prefixes.Add(@"http://192.168.1.17:8825/"); // адрес прослушки
            ConsoleWrite("Создаем подключение");

            try
            {
                lisen.Start();
                ConsoleWrite("Соеденение открыто");
            }
            catch (Exception ex)
            {
                ConsoleWrite(ex.Message);
            }
        }
    }
}
