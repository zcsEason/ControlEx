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

namespace ControlEx
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ControlEx"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ControlEx;assembly=ControlEx"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:ListBoxEx/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_ScrollViewer", Type = typeof(ScrollViewer))]
    public class ListBoxEx : ListBox
    {

        #region 字段
        bool isToTop = false;
        bool isToBottom = false;
        ScrollViewer scroll;
        #endregion

        static ListBoxEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxEx), new FrameworkPropertyMetadata(typeof(ListBoxEx)));
        }


        #region 事件

        public event EventHandler TopRefreshEvent;
        public event EventHandler BottomRegreshEvent;
        #endregion

        #region 命令相关

        /// <summary>
        /// 顶部刷新命令
        /// </summary>

        public ICommand TopRefreshCommand
        {
            get { return (ICommand)GetValue(TopRefreshCommandProperty); }
            set { SetValue(TopRefreshCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopRefreshCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopRefreshCommandProperty =
            DependencyProperty.Register("TopRefreshCommand", typeof(ICommand), typeof(ListBoxEx), new PropertyMetadata(null));


        /// <summary>
        /// 顶部刷新参数
        /// </summary>
        public object TopRefreshParameter
        {
            get { return (object)GetValue(TopRefreshParameterProperty); }
            set { SetValue(TopRefreshParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopRefreshParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopRefreshParameterProperty =
            DependencyProperty.Register("TopRefreshParameter", typeof(object), typeof(ListBoxEx), new PropertyMetadata(null));


        /// <summary>
        /// 底部刷新命令
        /// </summary>
        public ICommand BottomRefreshCommand
        {
            get { return (ICommand)GetValue(BottomRefreshCommandProperty); }
            set { SetValue(BottomRefreshCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomRefreshCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomRefreshCommandProperty =
            DependencyProperty.Register("BottomRefreshCommand", typeof(ICommand), typeof(ListBoxEx), new PropertyMetadata(null));



        public object BottomRefreshParameter
        {
            get { return (object)GetValue(BottomRefreshParameterProperty); }
            set { SetValue(BottomRefreshParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomRefreshParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomRefreshParameterProperty =
            DependencyProperty.Register("BottomRefreshParameter", typeof(object), typeof(ListBoxEx), new PropertyMetadata(null));



        /// <summary>
        /// 顶部刷新图片资源
        /// </summary>
        public ImageSource TopRefreshImageSrouce
        {
            get { return (ImageSource)GetValue(TopRefreshImageSrouceProperty); }
            set { SetValue(TopRefreshImageSrouceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopRefreshImageSrouce.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopRefreshImageSrouceProperty =
            DependencyProperty.Register("TopRefreshImageSrouce", typeof(ImageSource), typeof(ListBoxEx), new PropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/ControlEx;component/Images/1.gif", UriKind.Absolute))));



        public ImageSource BottomRefreshImageSource
        {
            get { return (ImageSource)GetValue(BottomRefreshImageSourceProperty); }
            set { SetValue(BottomRefreshImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomRefreshImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomRefreshImageSourceProperty =
            DependencyProperty.Register("BottomRefreshImageSource", typeof(ImageSource), typeof(ListBoxEx), new PropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/ControlEx;component/Images/1.gif", UriKind.Absolute))));



        #endregion


        #region 属性

        /// <summary>
        /// 是否正在做顶部刷新
        /// </summary>

        public bool IsTopRefreshing
        {
            get { return (bool)GetValue(IsTopRefreshingProperty); }
            set { SetValue(IsTopRefreshingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTopRefreshing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTopRefreshingProperty =
            DependencyProperty.Register("IsTopRefreshing", typeof(bool), typeof(ListBoxEx), new PropertyMetadata(false));




        public bool IsBottomRefreshing
        {
            get { return (bool)GetValue(IsBottomRefreshingProperty); }
            set { SetValue(IsBottomRefreshingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBottomRefreshing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBottomRefreshingProperty =
            DependencyProperty.Register("IsBottomRefreshing", typeof(bool), typeof(ListBoxEx), new PropertyMetadata(false));

        /// <summary>
        /// 是否正在刷新
        /// </summary>
        bool IsRefreshing
        {
            get
            {
                if (!IsTopRefreshing && !IsBottomRefreshing) return false;
                return true;
            }
        }

        #endregion


        #region 重写

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            scroll = Template.FindName("PART_ScrollViewer", this) as ScrollViewer;
            if (scroll != null)
                scroll.ScrollChanged += Scroll_ScrollChanged;
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);
            if (isToTop && e.Delta > 0)
            {
                if (!IsRefreshing)
                {
                    TopRefreshEvent?.Invoke(this, new EventArgs());
                    TopRefreshCommand?.Execute(TopRefreshParameter);
                }
            }
            if (isToBottom && e.Delta < 0)
            {
                if (!IsRefreshing)
                {
                    BottomRefreshCommand?.Execute(BottomRefreshParameter);
                    BottomRegreshEvent?.Invoke(this, new EventArgs());
                }
            }

            UpdateIsTopAndBottom();
        }
        #endregion

        #region 控件事件

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            UpdateIsTopAndBottom();
        }
        #endregion


        #region 自定义
        void UpdateIsTopAndBottom()
        {
            if (scroll != null)
            {
                if (scroll.VerticalOffset == 0)
                    isToTop = true;
                else
                    isToTop = false;

                if (scroll.VerticalOffset + scroll.ViewportHeight >= scroll.ExtentHeight)
                    isToBottom = true;
                else
                    isToBottom = false;

            }
        }
        #endregion
    }
}
