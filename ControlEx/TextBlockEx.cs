using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace ControlEx
{
    public class TextBlockEx:TextBlock
    {

        /// <summary>
        /// 当前文字是否被截取
        /// </summary>

        public bool IsTextTrimmed
        {
            get { return (bool)GetValue(IsTextTrimmedProperty); }
            private set { SetValue(IsTextTrimmedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTextTrimming.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTextTrimmedProperty =
            DependencyProperty.Register("IsTextTrimmed", typeof(bool), typeof(TextBlock), new PropertyMetadata(false));



        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            IsTextTrimmed = GetIsTrimming();
            return base.GetLayoutClip(layoutSlotSize);
        }







        bool GetIsTrimming()
        {

            if (TextTrimming == TextTrimming.None)
            {
                return false;
            }

           
            if (TextWrapping == TextWrapping.NoWrap)  //比较长度
            {
                Size size = new Size(double.MaxValue, RenderSize.Height);
               var needsize=  MeasureOverride(size);

                if (needsize.Width > RenderSize.Width)
                {
                    MeasureOverride(RenderSize);
                    return true;
                }

            }
            else  //比较高度
            {
                Size size = new Size(RenderSize.Width,double.MaxValue);
                var needsize = MeasureOverride(size);
                if (needsize.Height > RenderSize.Height)
                {
                    MeasureOverride(RenderSize);
                    return true;
                }
            }
            return false;
        }
    }

 
}
