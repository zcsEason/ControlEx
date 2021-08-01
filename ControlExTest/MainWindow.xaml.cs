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

namespace ControlExTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //https://www.cnblogs.com/kybs0/p/7305440.html
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                while(true)
                {
                    Dispatcher.Invoke(() =>
                    {
                        tb.Text += "死快点回来卡";
                        if (tb.Text.Length > 100)
                            tb.Text = "";
                    });
                    System.Threading.Thread.Sleep(500);
                }
            });
        }
    }
}
