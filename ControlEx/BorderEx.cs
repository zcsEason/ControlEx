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
    public class BorderEx : Border
    {

        public bool IsClipToCorner
        {
            get { return (bool)GetValue(IsClipToCornerProperty); }
            set
            {
                SetValue(IsClipToCornerProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for IsClipToCorner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClipToCornerProperty =
            DependencyProperty.Register("IsClipToCorner", typeof(bool), typeof(BorderEx), new PropertyMetadata(false, new PropertyChangedCallback(IsClipToCornerChanged)));

        private static void IsClipToCornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bd = d as BorderEx;
            if (bd.IsLoaded)
                bd.UpdateClip();
        }

        void UpdateClip()
        {
            Clip = GetLayoutClip(RenderSize);
        }


        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            if (IsClipToCorner)
            {
                return GetClips(new Size(ActualWidth, ActualHeight));
            }
            return base.GetLayoutClip(layoutSlotSize);
        }




        PathGeometry GetClips(Size layoutSlotSize)
        {


            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure();
            pg.Figures.Add(pf);
            //左上角起始点
            Point start;
            if (CornerRadius.TopLeft != 0)
            {
                start = new Point(CornerRadius.TopRight, 0);
            }
            else
                start = new Point(0, 0);
            pf.StartPoint = start;

            //右上角
            Point pt2;
            if (CornerRadius.TopRight == 0)
            {
                pt2 = new Point(layoutSlotSize.Width, 0);
                LineSegment l = new LineSegment(pt2, true);
                pf.Segments.Add(l);


            }
            else
            {
                pt2 = new Point(layoutSlotSize.Width - CornerRadius.TopRight, 0);
                LineSegment l = new LineSegment(pt2, true);
                pf.Segments.Add(l);

                //右上角圆弧
                var pt3 = new Point(layoutSlotSize.Width, 0);
                var pt4 = new Point(layoutSlotSize.Width, CornerRadius.TopRight);
                BezierSegment l2 = new BezierSegment(pt2, pt3, pt4, true);
                pf.Segments.Add(l2);
            }

            //右下角
            Point pt5;
            if (CornerRadius.BottomRight == 0)
            {
                pt5 = new Point(layoutSlotSize.Width, layoutSlotSize.Height);
                LineSegment l = new LineSegment(pt5, true);
                pf.Segments.Add(l);
            }
            else
            {
                pt5 = new Point(layoutSlotSize.Width, layoutSlotSize.Height - CornerRadius.BottomRight);
                LineSegment l = new LineSegment(pt5, true);
                pf.Segments.Add(l);

                //圆弧
                var pt6 = new Point(layoutSlotSize.Width, layoutSlotSize.Height);
                var pt7 = new Point(layoutSlotSize.Width - CornerRadius.BottomRight, layoutSlotSize.Height);
                BezierSegment l2 = new BezierSegment(pt5, pt6, pt7, true);
                pf.Segments.Add(l2);

            }

            //左下角
            Point pt8;
            if (CornerRadius.BottomLeft == 0)
            {
                pt8 = new Point(0, layoutSlotSize.Height);
                LineSegment l = new LineSegment(pt8, true);
                pf.Segments.Add(l);
            }
            else
            {
                pt8 = new Point(CornerRadius.BottomLeft, layoutSlotSize.Height);
                LineSegment l = new LineSegment(pt8, true);
                pf.Segments.Add(l);

                //圆弧
                var pt9 = new Point(0, layoutSlotSize.Height);
                var pt10 = new Point(0, layoutSlotSize.Height - CornerRadius.BottomLeft);
                BezierSegment l2 = new BezierSegment(pt8, pt9, pt10, true);
                pf.Segments.Add(l2);
            }

            //左上角
            Point pt11;
            if (CornerRadius.TopLeft == 0)
            {

            }
            else
            {
                pt11 = new Point(0, CornerRadius.TopLeft);
                LineSegment l = new LineSegment(pt11, true);
                pf.Segments.Add(l);

                //圆弧
                var pt12 = new Point(0, 0);
                var pt13 = new Point(CornerRadius.TopLeft, 0);
                BezierSegment l2 = new BezierSegment(pt11, pt12, pt13, true);
                pf.Segments.Add(l2);

            }

            return pg;
        }



    }

}
