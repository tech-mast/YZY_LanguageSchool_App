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
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Converters;
using YZYLibraryAzure;

namespace YZYAdminGUI
{
    /// <summary>
    /// Interaction logic for UCWelcomePage.xaml
    /// </summary>
    public partial class UCWelcomePage : UserControl
    {
        public UCWelcomePage()
        {

            InitializeComponent();

            this.PieChart();
            this.ColumnChart();
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        YZYDbContextAzure ctx = new YZYDbContextAzure();
        public void PieChart()
        {

            int t = (from r in ctx.Courses where r.CategoryID == 1 || r.CategoryID == 2 || r.CategoryID == 3 select r).ToList().Count;
            P1.Values = new ChartValues<int> { t };     
            //int t = (from r in ctx.Courses where r.CategoryID == 1 select r).ToList().Count;
            P2.Values = new ChartValues<int> { (from r in ctx.Courses where r.CategoryID == 4 || r.CategoryID == 5 || r.CategoryID == 6  select r).ToList().Count
        };
            P3.Values = new ChartValues<int> { (from r in ctx.Courses where r.CategoryID == 7 || r.CategoryID == 8 || r.CategoryID == 9 select r).ToList().Count
        };
            P4.Values = new ChartValues<int> { (from r in ctx.Courses where r.CategoryID == 10 || r.CategoryID == 11 || r.CategoryID == 12 select r).ToList().Count
        };
            PointLabel = chartPoint => string.Format("{1:p}", chartPoint.Y, chartPoint.Participation);            
            DataContext = this;
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }


        public void ColumnChart()
        {
            int qc = (from u in ctx.Users where (string.Equals(u.Province, "QC")) select u).ToList().Count;
            int nb = (from u in ctx.Users where (string.Equals(u.Province, "NB")) select u).ToList().Count;
            int on = (from u in ctx.Users where (string.Equals(u.Province, "ON")) select u).ToList().Count;

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    //Title = "2020",
                    //Title = "",
                    Title = "residence",
                    Values = new ChartValues<double> { qc, nb,on }
                }
            };

            //also adding values updates and animates the chart automatically
            //SeriesCollection[0].Values.Add(48d);

            Labels = new[] {"QC", "NB", "ON"};
            //Formatter = value => value.ToString("N");
            Formatter = value => value.ToString();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}
