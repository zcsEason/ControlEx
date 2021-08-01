using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ControlEx
{
    public class TextBlockEx:TextBlock
    {

        /// <summary>
        /// 当前文字是否被截取
        /// </summary>

        public bool IsTextTrimming
        {
            get { return (bool)GetValue(IsTextTrimmingProperty); }
            private set { SetValue(IsTextTrimmingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTextTrimming.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTextTrimmingProperty =
            DependencyProperty.Register("IsTextTrimming", typeof(bool), typeof(TextBlock), new PropertyMetadata(false));

        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            IsTextTrimming = GetIsTrimming();
            return base.GetLayoutClip(layoutSlotSize);
        }

         bool GetIsTrimming()
        {
            if (TextTrimming == TextTrimming.None) return false;

            var measure = new MeasureTool();
            measure.Width = RenderSize.Width;
            measure.Height = RenderSize.Height;
            var needSize = measure.GetContentRenderSize(this);

            if (TextWrapping == TextWrapping.NoWrap)  //比较长度
            {
                if (needSize.Width > RenderSize.Width) return true;
            }
            else  //比较高度
            {
                if (needSize.Height > RenderSize.Height) return true;
            }
            return false;
        }
    }

    class MeasureTool : TextBlock
    {
        public Size GetContentRenderSize(TextBlock target)
        {
            Text = target.Text;
            TextWrapping = target.TextWrapping;
            TextTrimming = TextTrimming.None;

            FontFamily = target.FontFamily;
            FontSize = target.FontSize;
            FontStyle = target.FontStyle;
            FontStretch = target.FontStretch;

            return MeasureOverride(target.RenderSize);
        }
    }
}
