using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace ControlExTest
{
    /// <summary>
    /// ListBoxExTest.xaml 的交互逻辑
    /// </summary>
    public partial class ListBoxExTest : Window, INotifyPropertyChanged
    {
        ObservableCollection<string> source = new ObservableCollection<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public ListBoxExTest()
        {
            InitializeComponent();
            list.ItemsSource = source;
            InitSource();
            TopRefreshCommand = new RefreshCommand(ExeTopRefreshCommand);
            BottomRefreshCommand = new RefreshCommand(ExeBottomRefreshCommand);
        }
       




        #region 属性


        private RefreshCommand topRefreshCommand;

        public RefreshCommand TopRefreshCommand
        {
            get { return topRefreshCommand; }
            set
            {
                topRefreshCommand = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TopRefreshCommand"));
            }
        }

        private RefreshCommand bottomRefreshCommand;

        public RefreshCommand BottomRefreshCommand
        {
            get { return bottomRefreshCommand; }
            set
            {
                bottomRefreshCommand = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BottomRefreshCommand"));
            }
        }


        private bool isTopRefreshing;
        /// <summary>
        /// 顶部是否在刷新
        /// </summary>
        public bool IsTopRefreshing
        {
            get { return isTopRefreshing; }
            set
            {
                isTopRefreshing = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsTopRefreshing"));
            }
        }


        private bool isBottomRefreshing;
        /// <summary>
        /// 底部是否正在刷新
        /// </summary>
        public bool IsBottomRefreshing
        {
            get { return isBottomRefreshing; }
            set
            {
                isBottomRefreshing = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBottomRefreshing"));
            }
        }



        #endregion






        void InitSource()
        {
            for (int i = 0; i < 10; i++)
            {
                string str = $"这是第{i}个Item";
                source.Add(str);
            }
        }

        private void list_TopRefreshEvent(object sender, EventArgs e)
        {
            IsTopRefreshing = true;
            Task.Run(() =>
            {

                Random r = new Random();
                int sleep = r.Next(1000, 3000);
                System.Threading.Thread.Sleep(sleep);
                Dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var s = $"这是顶部刷新的数据{i}";
                        source.Insert(0, s);
                    }
                    IsTopRefreshing = false;
                });
            });
        }

        private void list_BottomRegreshEvent(object sender, EventArgs e)
        {
            IsBottomRefreshing = true;
            Task.Run(() =>
            {

                Random r = new Random();
                int sleep = r.Next(1000, 3000);
                System.Threading.Thread.Sleep(sleep);
                Dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var s = $"这是底部刷新的数据{i}";
                        source.Add(s);
                    }
                    IsBottomRefreshing = false;
                });
            });
        }


        void ExeTopRefreshCommand(object para)
        {
            IsTopRefreshing = true;
            Task.Run(() =>
            {

                Random r = new Random();
                int sleep = r.Next(1000, 3000);
                System.Threading.Thread.Sleep(sleep);
                Dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var s = $"这是顶部刷新的数据{i}";
                        source.Insert(0, s);
                    }
                    IsTopRefreshing = false;
                });
            });
        }

        void ExeBottomRefreshCommand(object para)
        {
            IsBottomRefreshing = true;
            Task.Run(() =>
            {

                Random r = new Random();
                int sleep = r.Next(1000, 3000);
                System.Threading.Thread.Sleep(sleep);
                Dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var s = $"这是底部刷新的数据{i}";
                        source.Add(s);
                    }
                    IsBottomRefreshing = false;
                });
            });
        }
    }
}
