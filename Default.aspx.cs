using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Drawing;


public partial class _Default : System.Web.UI.Page
{

    public class WeightLossTrace
    {
        double weightInWeek;
        string weekName;
        double lostWeightInWeek;
        public WeightLossTrace(string weekName, double weightInWeek, double lostWeightInWeek)
        {
            this.WeekName = weekName;
            this.WeightInWeek = weightInWeek;
            this.LostWeightInWeek = lostWeightInWeek;
        }
        public string WeekName { get; set; }
        public double WeightInWeek { get; set;}
        public double LostWeightInWeek { get; set; }
    }

    List<WeightLossTrace> myWeightLossTrace = new List<WeightLossTrace>();

    public bool UnderWeight = false;
    public bool NormalWeight = false;
    public int[] WeightRanges = new int[4] { 60, 70, 80, 90 }; 
    public string[] excercises = new string[6] {
        "House Work Moderate","Running, 6 mph (10 min mile)","Walking 3.0 mph, moderate","Carrying moderate loads upstairs","Crew, sculling, rowing, competition","Swimming laps, freestyle, slow"
    };

    public int[,] calories = new int[6, 4] {
        {207,246,286,326} ,   
        {590,704,817,931} ,   
        {195,232,270,307},   
        {472,563,654,745},   
        {708,844,981,1117},  
        {413,493,572,651},   
    };

    protected void Page_Load(object sender, EventArgs e)
    {
        // Gets or sets a value that indicates whether unobtrusive JavaScript is used for client-side validation.
        this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        //RadBtnListGender.SelectedIndex = 0;
        //ManOrWoman.ImageUrl = "~/Content/manandwoman.jpg";
        cTestChart.Visible = false;
         
    }

   
    public void CalculateBMI()
    {
        string bmiResult;
        double bmi;
        double height = Convert.ToDouble(TextBoxHeight.Text);
        double weight = Convert.ToDouble(TextBoxWeight.Text);
        bmi = weight / ((height / 100) * (height / 100));
        TextBoxBMI.Text = Convert.ToString(Convert.ToInt16(bmi));
       
        if (bmi < 18.5) { bmiResult = "✓ You are underweight!"; UnderWeight = true; }
        else if (bmi >= 18.5 && bmi <= 25.00) { bmiResult = "✓ Your BMI is normal"; NormalWeight = true; }
        else if (bmi > 25.00 && bmi <= 30.00) { bmiResult = "✓ You are overweight!"; }
        else { bmiResult = "✓ You are obese!"; }
        LabelBMIResult.Text = bmiResult;
        CalculateIdealWeight();
    }

    public void CalculateBMR()
    {

        double bmr;
        int age;
        double height = Convert.ToDouble(TextBoxHeight.Text);
        double weight = Convert.ToDouble(TextBoxWeight.Text);
        age = Convert.ToInt16(TextBoxAge.Text);

        if (RadBtnListGender.SelectedIndex == 1)
        {
            bmr = (10 * weight + 6.25 * height) - (5 * age) + 5;
        }
        else
        {
            bmr = (10 * weight + 6.25 * height) - (5 * age) - 161;
        }

        TextBoxBMR.Text = Convert.ToString((int)bmr);

    }

    public void CalculateIdealWeight()
    {
        double IdealWeightFrom, IdealWeightTo, WeightToGainFrom, WeightToLooseFrom, WeightToGainTo, WeightToLooseTo;
        double height = Convert.ToDouble(TextBoxHeight.Text);
        double weight = Convert.ToDouble(TextBoxWeight.Text);
        

        IdealWeightFrom = 18.50 * ((height / 100) * (height / 100));
        IdealWeightTo = 24.90 * ((height / 100) * (height / 100));
        LabelIdealWeight.Text = "✓ Your ideal weight is between " + Convert.ToString(Convert.ToInt16(IdealWeightFrom) + " kg and " +
                                                                 Convert.ToString(Convert.ToInt16(IdealWeightTo))) + " kg.";

        if (UnderWeight)
        {
            WeightToGainFrom = IdealWeightFrom - weight;
            WeightToGainTo = IdealWeightTo - weight;
            WeightToLooseFrom = weight - IdealWeightTo;
            WeightToLooseTo = weight - IdealWeightFrom;
            LabelFinalResult.Text = "✓ You need to gain at least " + Convert.ToString(Convert.ToInt16(WeightToGainFrom) + " kg but NO MORE than " +
                                                                                        Convert.ToString(Convert.ToInt16(WeightToGainTo))) + " kg of weight.";
            Label12.Text = "✓ You should try gaining weight";
            TextDesiredWeight.Text = Convert.ToString(Convert.ToInt16(WeightToGainFrom));
            LabelResultOfWeek.Text = "✓ Always ask a health professional to get advice on how to gain weight.";
        }
        else
        {
            WeightToLooseFrom = weight - IdealWeightTo;
            WeightToLooseTo = weight - IdealWeightFrom;
            if (Convert.ToInt16(WeightToLooseFrom) <= 0) { WeightToLooseFrom = 0; }
            if (Convert.ToInt16(WeightToLooseTo) <= 0) { WeightToLooseTo = 0; }
            if (WeightToLooseFrom != 0)
            {
                LabelFinalResult.Text = "✓ You need to lose at least " + Convert.ToString((int)(WeightToLooseFrom) + " kg BUT NO MORE than " +
                                                                                        Convert.ToString(Convert.ToInt16(WeightToLooseTo))) + " kg of weight.";
            } else
            {
                LabelFinalResult.Text = "✓ You don't need to lose weight however if you try losing weight DO NOT lose more than " + Convert.ToString(Convert.ToInt16(WeightToLooseTo)) + " kg.";
                                                                                            
            }
            Label12.Text = "You should try losing";
            TextDesiredWeight.Text = Convert.ToString((int)WeightToLooseFrom);
            LabelResultOfWeek.Text = "✓ You will burn " + Convert.ToString(Convert.ToInt16(CalcBurnCalWeekBasis(weight))) + " calories during the first week.";
            // this version of Advisor will only deal with minimum weight lost recommended
            //TextDesiredWeight.Enabled = true;
        }
        CalculateBMR();
           

        double TotalLostWeight = 0;
        double LostWeightInWeek = 0;
        double TargetToAchieve = Convert.ToDouble(TextDesiredWeight.Text);
        double[] WeeklyWTrack = new double[1000];
        int WeekNo = 0;
        double InitalWeight = weight;

        while (TotalLostWeight < TargetToAchieve)
        { 
                WeekNo = WeekNo + 1;
                LostWeightInWeek = CalcBurnCalWeekBasis(InitalWeight) / 3850;
                TotalLostWeight = TotalLostWeight + LostWeightInWeek;
                // here InitialWeigh is an initial for each week 
                InitalWeight = InitalWeight - LostWeightInWeek;
                WeeklyWTrack[WeekNo] = LostWeightInWeek;
                WeightLossTrace WeekRec = new WeightLossTrace("Wk-" + Convert.ToString(WeekNo), InitalWeight, LostWeightInWeek);
                myWeightLossTrace.Add(WeekRec);
        }
        
        if (!UnderWeight)
        {
            LabelKgToCals.Text = "✓ You need to burn " + Convert.ToInt16(TextDesiredWeight.Text) * 3850 + " calories to loose " +
                                            TextDesiredWeight.Text + " kg and this will take approx " + Convert.ToString(WeekNo)+"  weeks.";
        }
        else
        {
            LabelKgToCals.Text = "✓ You need to reserve " + Convert.ToInt16(TextDesiredWeight.Text) * 3850 + " calories to gain " + 
                                            TextDesiredWeight.Text + " kg.";
        }

    }
    
        
    protected void Button1_Click(object sender, EventArgs e)
    {
       CalculateBMI();
       if ((Convert.ToInt16(TextDesiredWeight.Text)>0) &&(!UnderWeight)) { ChartMaker(); }
    }

    
    protected void RadBtnListGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButton radioButton = sender as RadioButton;
        if (RadBtnListGender.SelectedIndex == 0) { ManOrWoman.ImageUrl = "~/Content/man.png"; }
        else { ManOrWoman.ImageUrl = "~/Content/woman.png";}
    }

    
    public double CalcBurnCalWeekBasis(double weight)
    {
        int WhichWeightGroup = 0;
        double TotalWeekCals = 0;

        if (weight <= 60) { WhichWeightGroup = 0; }
        else if (weight >= 60 && weight <= 70) { WhichWeightGroup = 1; }
        else if (weight >= 70 && weight <= 80) { WhichWeightGroup = 2; }
        else if (weight >= 80) { WhichWeightGroup = 3; }

        TotalWeekCals = TotalWeekCals + (Convert.ToDouble(TextHouseCleanHrs.Text) / 60) * calories[0, WhichWeightGroup];
        TotalWeekCals = TotalWeekCals + (Convert.ToDouble(TextRowingHrs.Text) / 60) * calories[4, WhichWeightGroup];
        TotalWeekCals = TotalWeekCals + (Convert.ToDouble(TextStairHrs.Text) / 60) * calories[3, WhichWeightGroup];
        TotalWeekCals = TotalWeekCals + (Convert.ToDouble(TextSwimmingHrs.Text) / 60) * calories[5, WhichWeightGroup];
        TotalWeekCals = TotalWeekCals + (Convert.ToDouble(TextWalkingHrs.Text) / 60) * calories[2, WhichWeightGroup];
        TotalWeekCals = TotalWeekCals + (Convert.ToDouble(TextRunningHrs.Text) / 60) * calories[1, WhichWeightGroup];

        return TotalWeekCals;
    }


    public void ChartMaker()
    {
        List<int> WeightTrace = new List<int>();

        //SET UP THE DATA TO PLOT  

        double[] yVal;
        string[] xName;
        

        xName = new string[myWeightLossTrace.Count];
        yVal = new double[myWeightLossTrace.Count];
       
        
            for (int i = 0; i < myWeightLossTrace.Count; i++)
            {
                xName[i] = myWeightLossTrace[i].WeekName;
                yVal[i] = myWeightLossTrace[i].WeightInWeek;
            }
        

        
        //BIND THE DATA TO THE CHART
        cTestChart.Series.Add(new Series());
        cTestChart.Series[0].Points.DataBindXY(xName, yVal);



        //SET THE CHART TYPE TO BE SPLINE
        cTestChart.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
       

        //cTestChart.Series[0].AxisLabel = "";
        //cTestChart.Series[0]["PieLabelStyle"] = "Outside";
        //cTestChart.Series[0]["PieStartAngle"] = "-90";

        //SET THE COLOR PALETTE FOR THE CHART TO BE A PRESET OF NONE 
        //DEFINE OUR OWN COLOR PALETTE FOR THE CHART 
        cTestChart.Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.None;
        cTestChart.PaletteCustomColors = new Color[] { Color.Blue, Color.Red };

        //SET THE IMAGE OUTPUT TYPE TO BE JPEG
        cTestChart.ImageType = System.Web.UI.DataVisualization.Charting.ChartImageType.Jpeg;

        //ADD A PLACE HOLDER CHART AREA TO THE CHART
        //SET THE CHART AREA TO BE 3D
        cTestChart.ChartAreas.Add(new ChartArea());
        cTestChart.ChartAreas[0].Area3DStyle.Enable3D = false;


        //ADD A PLACE HOLDER LEGEND TO THE CHART
        //DISABLE THE LEGEND
        cTestChart.ChartAreas[0].AxisY.Maximum = (int)myWeightLossTrace[0].WeightInWeek;
        cTestChart.ChartAreas[0].AxisY.Minimum = (int)myWeightLossTrace[myWeightLossTrace.Count-1].WeightInWeek;


        cTestChart.Series[0].LegendText = "weight";

        cTestChart.Legends.Add(new Legend());
        cTestChart.Legends[0].Title = "week by week";
        cTestChart.Legends[0].Enabled = true;


        cTestChart.Visible = true;

    }


    
}
