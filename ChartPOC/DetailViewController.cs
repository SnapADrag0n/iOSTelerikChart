using System.Collections;
using ChartPOC;
using TelerikUI;
using UIKit;

namespace TestOps
{
    public partial class DetailViewController : UIViewController, IUIScrollViewDelegate
    {
        public object DetailItem { get; set; }
        TKChart chart;
        ChartDelegate chartDelegate = new ChartDelegate(0, 3);
       
        public static ArrayList monthResults;
        public DetailViewController(IntPtr handle) : base(handle)
        {
        }


        public void InitData()
        {
            monthResults = new ArrayList();

            monthResults.Add(new MonthResult("00:00", 0.1, 0.05, 1.7, 0));
            monthResults.Add(new MonthResult("01:00", 0.2, 0.1, 2.2, 1));
            monthResults.Add(new MonthResult("02:00", 0.3, 0.15, 0.6, 2));
            monthResults.Add(new MonthResult("03:00", 0.4, 0.2, 1.3, 3));
            monthResults.Add(new MonthResult("04:00", 0.5, 0.25, 0.5, 4));
            monthResults.Add(new MonthResult("05:00", 0.6, 0.3, 2.8, 5));
            monthResults.Add(new MonthResult("06:00", 0.7, 0.35, 1.1, 6));
            monthResults.Add(new MonthResult("07:00", 0.8, 0.4, 1.4, 7));
            monthResults.Add(new MonthResult("08:00", 0.9, 0.45, 0.6, 8));
            monthResults.Add(new MonthResult("09:00", 1.0, 0.5, 3, 9));
            monthResults.Add(new MonthResult("10:00", 1.1, 0.55, 2.8, 10));
            monthResults.Add(new MonthResult("11:00", 1.2, 0.6, 2.1, 11));
            monthResults.Add(new MonthResult("12:00", 1.3, 0.65, 1.2, 12));
            monthResults.Add(new MonthResult("13:00", 1.4, 0.7, 0.8, 13));
            monthResults.Add(new MonthResult("14:00", 1.5, 0.75, 1.5, 14));
            monthResults.Add(new MonthResult("15:00", 1.6, 0.8, 0.8, 15));
            monthResults.Add(new MonthResult("16:00", 1.7, 0.85, 3, 16));
            monthResults.Add(new MonthResult("17:00", 1.8, 0.9, 2.1, 17));
            monthResults.Add(new MonthResult("18:00", 1.9, 0.95, 1.3, 18));
            monthResults.Add(new MonthResult("19:00", 2.0, 1, 0.8, 19));
            monthResults.Add(new MonthResult("20:00", 2.1, 1.05, 0.9, 20));
            monthResults.Add(new MonthResult("21:00", 2.2, 1.1, 1.2, 21));
            monthResults.Add(new MonthResult("22:00", 0, 0, 0, 22));
            monthResults.Add(new MonthResult("23:00", 0, 0, 0, 23));
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitData();
          
            try
            {
                TKChart chart = new TKChart(this.View2.Bounds);

                chart.Frame = this.View2.Bounds;
                chart.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
                chart.AllowPanDeceleration = true;
                chart.Delegate = chartDelegate;
                chart.ZoomMode = TKChartZoomMode.Symmetric;

                chart.DataPointSelectionMode = TKChartSelectionMode.Single;
                chart.AllowAnimations = true;
               
                this.View2.AddSubview(chart);
                chart.BackgroundColor =  UIColor.Clear;
               

                TKChartNumericAxis yAxis = new TKChartNumericAxis();
               
                yAxis.Position = TKChartAxisPosition.Left;
                yAxis.Style.LineHidden = false;
               
                yAxis.Style.LabelStyle.TextOffset = new UIOffset(10, 0);
                yAxis.Style.LabelStyle.FirstLabelTextOffset = new UIOffset(10, 0);
              

                chart.AddAxis(yAxis);

                TKChartCategoryAxis xAxis = new TKChartCategoryAxis();
             
                var formatter = new NSDateFormatter();
                formatter.DateFormat = "HH:mm";
               
                xAxis.PlotMode = TKChartAxisPlotMode.BetweenTicks;
                xAxis.Style.LabelStyle.TextAlignment = TKChartAxisLabelAlignment.Bottom;

                xAxis.Style.LabelStyle.FitMode = TKChartAxisLabelFitMode.None;
               
                xAxis.Style.LabelStyle.TextOffset = new UIOffset(0, 10);
              
                chart.AddAxis(xAxis);
                

               
                var values1 = new[] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 };
                var values2 = new[] { 0.05, 0.1, 0.15, 0.2, 0.25, 0.3, 0.35, 0.4, 0.45, 0.5 };
                var values3 = new[] { 1.7, 2.2, 0.6, 1.3, 0.5, 2.8, 1.1, 1.4, 0.6, 3 };
                string[] categories = new string[] { "00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00" };

                var offPeakData = new List<TKChartDataPoint>();
                var onPeakData = new List<TKChartDataPoint>();
                var superOffPeakData = new List<TKChartDataPoint>();
              
                foreach (var item in monthResults)
                {
                    var value = item as MonthResult;
                    offPeakData.Add(new TKChartDataPoint(new NSString(value.Interval), new NSNumber(value.OffPeak), new NSString("OffPeak")));
                    onPeakData.Add(new TKChartDataPoint(new NSString(value.Interval), new NSNumber(value.OnPeak), new NSString("OnPeak")));
                    superOffPeakData.Add(new TKChartDataPoint(new NSString(value.Interval), new NSNumber(value.SuperOffPeak), new NSString("SuperOffPeak")));

                }

                var stackInfo = new TKChartStackInfo(new NSNumber(1), TKChartStackMode.Stack);

                var offPeakseries = new TKChartColumnSeries(offPeakData.ToArray());
                offPeakseries.Title = "OffPeak";
                offPeakseries.StackInfo = stackInfo;
                offPeakseries.Selection = TKChartSeriesSelection.NotSet;

                //Set series color Start
                var paletteItemseries1 = new TKChartPaletteItem();
                paletteItemseries1.Fill = new TKSolidFill(UIColor.FromRGBA(73, 169, 66, 255));
                var paletteseries1 = new TKChartPalette();
                paletteseries1.AddPaletteItem(paletteItemseries1);
                offPeakseries.Style.Palette = paletteseries1;

                //End
                chart.AddSeries(offPeakseries);

                var onPeakSeries = new TKChartColumnSeries(onPeakData.ToArray());
                onPeakSeries.Title = "OnPeak";
                onPeakSeries.StackInfo = stackInfo;
                onPeakSeries.Selection = TKChartSeriesSelection.NotSet;
                //Set series color Start
                var paletteItemseries2 = new TKChartPaletteItem();
                paletteItemseries2.Fill = new TKSolidFill(UIColor.FromRGBA(0, 115, 177, 255));
                var paletteseries2 = new TKChartPalette();
                paletteseries2.AddPaletteItem(paletteItemseries2);
                onPeakSeries.Style.Palette = paletteseries2;
                //End
                chart.AddSeries(onPeakSeries);

                var superOffPeakSeries = new TKChartColumnSeries(superOffPeakData.ToArray());
                superOffPeakSeries.Title = "SuperOffPeak";
                superOffPeakSeries.StackInfo = stackInfo;
                superOffPeakSeries.Selection = TKChartSeriesSelection.NotSet;

                //Set series color start
                var paletteItem11 = new TKChartPaletteItem();
                paletteItem11.Fill = new TKSolidFill(UIColor.FromRGBA(244, 123, 32, 255));
                var palette11 = new TKChartPalette();
                palette11.AddPaletteItem(paletteItem11);
                superOffPeakSeries.Style.Palette = palette11;
                //End
                chart.AddSeries(superOffPeakSeries);

              
                chart.XAxis.AllowPan = true;
             
                chart.XAxis.Zoom = 4.5;

               
                var gridStyle = chart.GridStyle;

                gridStyle.VerticalLineStroke = new TKStroke(UIColor.FromWhiteAlpha(215.0f / 255.0f, 1.0f));
                gridStyle.VerticalLineAlternateStroke = new TKStroke(UIColor.FromWhiteAlpha(215.0f / 255.0f, 1.0f));
                gridStyle.VerticalLinesHidden = false;
               

                gridStyle.HorizontalFill = null;
                gridStyle.HorizontalAlternateFill = null;
                gridStyle.HorizontalLinesHidden = true;

                foreach (var item in monthResults)
                {
                    var value = item as MonthResult;
                    if (value.OffPeak == 0 && value.OnPeak == 0 && value.SuperOffPeak == 0)
                    {
                        UIImageView imageView = new UIImageView();
                        imageView.Image = UIImage.FromBundle("ic_service_outage.png");
                        imageView.ContentMode = UIViewContentMode.Center;
                        imageView.UserInteractionEnabled = true;
                        imageView.RemoveFromSuperview();
                        imageView.Tag = 101;
                        imageView.BackgroundColor = UIColor.Clear;
                        imageView.Alpha = 0.7f;
                        chart.AddAnnotation(new TKChartViewAnnotation(imageView, new NSString(value.Interval), new NSNumber(3.6), chart.Series[0]));


                    }
                }
                
                
            }
            catch (Exception ex)
            {

            }
         
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        class ChartDelegate : TKChartDelegate
        {
            int selectedSeriesIndex;
            int selectedDataPointIndex;
            public ChartDelegate(int selectedSeriesIndex, int selectedDataPointIndex)
            {
                this.selectedSeriesIndex = selectedSeriesIndex;
                this.selectedDataPointIndex = selectedDataPointIndex;
            }
            public override void SeriesSelected(TKChart chart, TKChartSeries series)
            {

                //chart.ReloadData();
                //chart.RemoveAllData();
                //chart.ClearAllSelection();

            }

            public override void PointSelected(TKChart chart, TKChartData point, TKChartSeries series, nint index)
            {
                chart.RemoveAllAnnotations();
                try
                {
                    this.selectedSeriesIndex = (int)series.Index;
                    this.selectedDataPointIndex = (int)index;
                    TKChartSelectionInfo[] selectedPoint = chart.SelectedPoints;
                    TKChartData values = selectedPoint[0].DataPoint;
                    string xAxisValue = values.GetDataXValue().ToString();
                    var yAxisValue = values.GetDataYValue().ToString();
                    float number = 0;
                    float.TryParse(yAxisValue, out number);
                    UIImageView imageView = new UIImageView();
                    imageView.Image = UIImage.FromBundle("ic_service_outage.png");
                  
                    imageView.ContentMode = UIViewContentMode.Center;
                    imageView.UserInteractionEnabled = true;
                    imageView.RemoveFromSuperview();
                    imageView.Tag = 101;
                    imageView.BackgroundColor = UIColor.Clear;
                    imageView.Alpha = 0.7f;
                    var offPeakValues = new[] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 };
                    var onPeakValues = new[] { 0.05, 0.1, 0.15, 0.2, 0.25, 0.3, 0.35, 0.4, 0.45, 0.5 };
                    var superPeakValues = new[] { 1.7, 2.2, 0.6, 1.3, 0.5, 2.8, 1.1, 1.4, 0.6, 3 };
                    var result = monthResults[this.selectedDataPointIndex];
                    var selectedDataPointValues = result as MonthResult;
                    int yAxisMaxVal = (int)Math.Ceiling(((NSNumber)chart.YAxis.Range.Maximum).FloatValue);

                    chart.AddAnnotation(new TKChartViewAnnotation(imageView, new NSString(xAxisValue), new NSNumber(yAxisMaxVal-0.50), chart.Series[0]));



                    foreach (var item in monthResults)
                    {
                        var value = item as MonthResult;
                        if (value.OffPeak == 0 && value.OnPeak == 0 && value.SuperOffPeak == 0)
                        {
                            UIImageView imageView2 = new UIImageView();
                            imageView2.Image = UIImage.FromBundle("ic_service_outage.png");
                            imageView2.ContentMode = UIViewContentMode.Center;
                            imageView2.UserInteractionEnabled = true;
                            imageView2.RemoveFromSuperview();
                            imageView2.Tag = 101;
                            imageView2.BackgroundColor = UIColor.Clear;
                            imageView2.Alpha = 0.7f;
                            chart.AddAnnotation(new TKChartViewAnnotation(imageView2, new NSString(value.Interval), new NSNumber(3.6), chart.Series[0]));


                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

          
            private nfloat GetCellCenter(nfloat x)
            {
                return (x - (x % 40) + 11);
            }
            public override void SeriesDeselected(TKChart chart, TKChartSeries series)
            {
                // Here you can perform the desired action when the selection is changed.
            }

            public override void PointDeselected(TKChart chart, TKChartData point, TKChartSeries series, nint index)
            {

                // Here you can perform the desired action when the selection is changed.
                ////int count = chart.Annotations.Count();
                ////chart.RemoveAnnotation(chart.Annotations[count - 1]);
                
            }


        }
    }

}



