using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.Charting;

namespace ANCFCC_Etats
{
    public partial class etat_globale : Telerik.WinControls.UI.RadForm
    {
        PieSeries pieSeries;

        public etat_globale()
        {
            InitializeComponent();
        }

        private void etat_globale_Load(object sender, EventArgs e)
        {
            //base.OnLoad(e); 

            this.pieSeries = new PieSeries();

            //AngleRange range = this.pieSeries.Range; 
            //range.StartAngle = Decimal.ToDouble(1);
            //this.pieSeries.Range = range;

            //this.pieSeries.ShowLabels = true;
            

            this.PopulatePieSeries(); 
            this.radChartView1.Series.Clear();
            this.pieSeries.View=
            this.radChartView1.Series.Add(this.pieSeries);

            Theme theme = Theme.ReadCSSText(@"                                         theme                                         {                                            name: ControlDefault;                                            elementType: Telerik.WinControls.UI.RadChartElement;                                             controlType: Telerik.WinControls.UI.RadChartView;                                          }                                          PieSegment                                         {                                                 RadiusAspectRatio                                             {                                                 Value: 0.5;                                                 EndValue: 1;                                                 MaxValue: 1;                                                 Frames: 20;                                                 Interval: 10;                                                 EasingType: OutCircular;                                                 RandomDelay: 100;                                                 RemoveAfterApply: true;                                              }                                         }                                         "); ThemeRepository.Add(theme, false);

        }

        private void PopulatePieSeries() 
        { 
            this.pieSeries.ShowLabels = true; 
            this.pieSeries.LabelFormat = "{0:P2}"; 
            this.pieSeries.RadiusFactor = 0.9f; 
            this.pieSeries.Range = new AngleRange(270, 360); 
            this.pieSeries.DataPoints.Add(new PieDataPoint(40, "Apple")); 
            this.pieSeries.DataPoints.Add(new PieDataPoint(45, "Microsoft")); 
            this.pieSeries.DataPoints.Add(new PieDataPoint(40, "Google")); 
            this.pieSeries.DataPoints.Add(new PieDataPoint(25, "Android")); 
            this.pieSeries.DataPoints.Add(new PieDataPoint(15, "HTC")); 
            this.pieSeries.DataPoints.Add(new PieDataPoint(15, "Samsung")); 
            this.pieSeries.DataPoints.Add(new PieDataPoint(15, "Bada")); 
            this.pieSeries.DataPoints.Add(new PieDataPoint(30, "Others")); }
    }
}
