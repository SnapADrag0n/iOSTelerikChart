using System;
namespace ChartPOC
{
    public class MonthResult : NSObject
    {

        public string Interval { get; set; }
        public double OnPeak { get; set; }
        public double OffPeak { get; set; }
        public double SuperOffPeak { get; set; }
        public int Index { get; set; }

        public MonthResult(string interval, double onPeak, double offPeak, double superOffPeak, int index)
        {
            this.Interval = interval;
            this.OnPeak = onPeak;
            this.OffPeak = offPeak;
            this.SuperOffPeak = superOffPeak;
            this.Index = index;
        }
    }

    class MonthResultDataBinding 
    {

        private string propertyName;

        public MonthResultDataBinding(string propertyName)
        {
            this.propertyName = propertyName;
        }

        //public override NSObject GetValue(NSObject p0)
        //{
        //    if (propertyName == "Interval")
        //    {
        //        return ((MonthResult)(p0)).Interval;
        //    }
        //    else if (propertyName == "OnPeak")
        //    {
        //        return ((MonthResult)(p0)).OnPeak;
        //    }
        //    else if (propertyName == "OffPeak")
        //    {
        //        return ((MonthResult)(p0)).OffPeak;
        //    }
        //    else
        //    {
        //        return ((MonthResult)(p0)).SuperOffPeak;
        //    }
        //}
    }
}

