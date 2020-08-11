using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SamplesApp.Controls
{
    public sealed partial class RadialProgressBarControl : UserControl
	{
		private const string OutlineFigurePartName = "OutlineFigurePart";
		private const string OutlineArcPartName = "OutlineArcPart";
		private const string BarFigurePartName = "BarFigurePart";
		private const string BarArcPartName = "BarArcPart";

		private PathFigure outlineFigure;
		private PathFigure barFigure;
		private ArcSegment outlineArc;
		private ArcSegment barArc;

		private bool allTemplatePartsDefined = false;



		public double Minimum
		{
			get { return (double)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MinimumProperty =
			DependencyProperty.Register("Minimum", typeof(double), typeof(RadialProgressBarControl), new PropertyMetadata(0.0, MaximumChanged));



		public double Maximum
		{
			get { return (double)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MaximumProperty =
			DependencyProperty.Register("Maximum", typeof(double), typeof(RadialProgressBarControl), new PropertyMetadata(100.0, MaximumChanged));

		private static void MaximumChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var control = (RadialProgressBarControl)dependencyObject;
			control.RenderSegment();
		}



		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(double), typeof(RadialProgressBarControl), new PropertyMetadata(50.0, MaximumChanged));

		/// <summary>
		/// Gets or sets the thickness of the circular ouline and segment
		/// </summary>
		public double Thickness
		{
			get { return (double)GetValue(ThicknessProperty); }
			set { SetValue(ThicknessProperty, value); }
		}

		/// <summary>
		/// Identifies the Thickness dependency property
		/// </summary>
		public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(nameof(Thickness), typeof(double), typeof(RadialProgressBarControl), new PropertyMetadata(0.0, ThicknessChangedHandler));

		/// <summary>
		/// Gets or sets the color of the circular ouline on which the segment is drawn
		/// </summary>
		public Brush Outline
		{
			get { return (Brush)GetValue(OutlineProperty); }
			set { SetValue(OutlineProperty, value); }
		}

		/// <summary>
		/// Identifies the Outline dependency property
		/// </summary>
		public static readonly DependencyProperty OutlineProperty = DependencyProperty.Register(nameof(Outline), typeof(Brush), typeof(RadialProgressBarControl), new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.Transparent)));

		/// <summary>
		/// Initializes a new instance of the <see cref="RadialProgressBar"/> class.
		/// Create a default circular progress bar
		/// </summary>
		public RadialProgressBarControl()
		{
			this.InitializeComponent();
			SizeChanged += SizeChangedHandler;
			outlineFigure = OutlineFigurePart;
			outlineArc = OutlineArcPart;
			barFigure = BarFigurePart;
			barArc = BarArcPart;

			allTemplatePartsDefined = outlineFigure != null && outlineArc != null && barFigure != null && barArc != null;

			RenderAll();
		}

		// Render outline and progress segment when thickness is changed
		private static void ThicknessChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var sender = d as RadialProgressBarControl;
			sender.RenderAll();
		}

		// Render outline and progress segment when control is resized.
		private void SizeChangedHandler(object sender, SizeChangedEventArgs e)
		{
			var self = sender as RadialProgressBarControl;
			self.RenderAll();
		}

		private double ComputeNormalizedRange()
		{
			var range = Maximum - Minimum;
			var delta = Value - Minimum;
			var output = range == 0.0 ? 0.0 : delta / range;
			output = Math.Min(Math.Max(0.0, output), 0.9999);
			return output;
		}

		// Compute size of ellipse so that the outer edge touches the bounding rectangle
		private Size ComputeEllipseSize()
		{
			var safeThickness = Math.Max(Thickness, 0.0);
			var width = Math.Max((ActualWidth - safeThickness) / 2.0, 0.0);
			var height = Math.Max((ActualHeight - safeThickness) / 2.0, 0.0);
			return new Size(width, height);
		}

		// Render the segment representing progress ratio.
		private void RenderSegment()
		{
			if (!allTemplatePartsDefined)
			{
				return;
			}

			var normalizedRange = ComputeNormalizedRange();

			var angle = 2 * Math.PI * normalizedRange;
			var size = ComputeEllipseSize();
			var translationFactor = Math.Max(Thickness / 2.0, 0.0);

			double x = (Math.Sin(angle) * size.Width) + size.Width + translationFactor;
			double y = (((Math.Cos(angle) * size.Height) - size.Height) * -1) + translationFactor;

			barArc.IsLargeArc = angle >= Math.PI;
			barArc.Point = new Point(x, y);
		}

		// Render the progress segment and the loop outline. Needs to run when control is resized or retemplated
		private void RenderAll()
		{
			if (!allTemplatePartsDefined)
			{
				return;
			}

			var size = ComputeEllipseSize();
			var segmentWidth = size.Width;
			var translationFactor = Math.Max(Thickness / 2.0, 0.0);

			outlineFigure.StartPoint = barFigure.StartPoint = new Point(segmentWidth + translationFactor, translationFactor);
			outlineArc.Size = barArc.Size = new Size(segmentWidth, size.Height);
			outlineArc.Point = new Point(segmentWidth + translationFactor - 0.05, translationFactor);

			RenderSegment();
		}
	}
}
