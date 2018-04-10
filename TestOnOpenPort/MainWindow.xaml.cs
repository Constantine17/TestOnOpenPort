using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestOnOpenPort;
using System.Net;

namespace TestOnOpenPort
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WriteConsole(string text)
        {
            TextConsole.AppendText("\n" + text);
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            Action<string> write = WriteConsole;
            PortConnector portConnector = new PortConnector(write, "8825");
            portConnector.OpenConnection();
        }

        private void Time_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
