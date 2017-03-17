<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableSessionState="ReadOnly" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<script language="c#" runat="server">

    void RadBtnListGender_SelectedIndexChanged(Object sender, EventArgs e) {

        LabelBMIResult.Text = "Please enter details of your daily excercise.";
        LabelIdealWeight.Text = "And click on [Advise Me] button for your results.";
        LabelFinalResult.Text = "";
        LabelKgToCals.Text = "";
        LabelResultOfWeek.Text = "";
    }

 </script>


<head runat="server">
    
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <link href="Content/myStyle.css" rel="stylesheet" />
   
    </head>
<body">
    <div class="container">
    <form id="form1" runat="server">
    <%--<div>--%>
        <div class="row">
            <h1 class="mainheader" > Healthy Life Style Advisor</h1>
        </div>
   <div class="row jumbotron" style="color:black;font-weight:bold;">
     <div class="col-md-3" style="padding:0 0 0 0;">
        <fieldset>
             <h4>Your Basic Details</h4>
        <div class="btn-group">
            <asp:RadioButtonList ID="RadBtnListGender" AutoPostBack="true" runat="server" RepeatDirection="Vertical" 
                OnSelectedIndexChanged="RadBtnListGender_SelectedIndexChanged">
                  <asp:ListItem Text="Male" Value="0" selected="true" /> 
                  <asp:ListItem Text="Female" Value="1" />
            </asp:RadioButtonList>
           <asp:RequiredFieldValidator runat="server" ID="GenderCheck" ControlToValidate="RadBtnListGender" />
        </div>
        <asp:Image ID="ManOrWoman" runat="server" ImageUrl="~/Content/man.png" Width="64px" />
        <br /><br />
                  <div style="text-align:left;">
                      
                      <div class="row"> 
                           <div class="col-md-3 nP"> <span>Your Age:</span> </div>
                           <div class="col-md-9">
                               <asp:TextBox ID="TextBoxAge" Width="30px" Height="30px" runat="server" ToolTip="Age in Years">46</asp:TextBox>
                           </div>
                      </div>
                      <asp:RangeValidator runat="server" Type="Integer" 
                      MinimumValue="16" MaximumValue="80" ControlToValidate="TextBoxAge" 
                      ErrorMessage="You must be between 16 and 80 years old." />
                      <asp:RequiredFieldValidator runat="server" ID="AgeCheck" ControlToValidate="TextBoxAge" />

                      <div class="row">
                          <div class="col-md-3 nP"> <span>Height:</span> </div>
                          <div class="col-md-9">
                              <asp:TextBox ID="TextBoxHeight" Width="30px" Height="30px" runat="server" ToolTip="in centimeters">175</asp:TextBox>
                          </div>
                      </div>
                      <asp:RangeValidator runat="server" Type="Integer" 
                      MinimumValue="50" MaximumValue="200" ControlToValidate="TextBoxHeight" 
                      ErrorMessage="Your height must be between 50cm and 2 Meters" />
                     <asp:RequiredFieldValidator runat="server" ID="HeightCheck" ControlToValidate="TextBoxHeight"/>
                    
                       <div class="row">
                          <div class="col-md-3 nP"> <span>Weight:</span> </div>
                          <div class="col-md-9">
                              <asp:TextBox ID="TextBoxWeight" Width="30px" Height="30px" runat="server" ToolTip="in kilograms">85</asp:TextBox>
                          </div>
                     </div>
                     <asp:RangeValidator runat="server" Type="Integer" 
                     MinimumValue="40" MaximumValue="150" ControlToValidate="TextBoxWeight" 
                     ErrorMessage="This advisor designed for those who is NOT heavier than 150kg and lighter than 40Kg" />
                     <asp:RequiredFieldValidator runat="server" ID="WeightCheck" ControlToValidate="TextBoxWeight"/>
                </div>
        </fieldset>
     </div>
        
       
     <div class="col-md-5" style="padding:0 0 0 0;">
        <fieldset>
            <h4>Your Weekly Exercise Pattern</h4>
                  <div style="text-align: left;">
                      <%--this is 1st --%>
                      <div class="row">
                          <div class="col-md-5 nP"> <span">Moderate House Work:</span> </div>
                          <div class="col-md-7">
                               <asp:TextBox ID="TextHouseCleanHrs" Width="30px" Height="30px" runat="server" ToolTip="in minutes">30</asp:TextBox>mins
                          </div>
                      </div> 
                      <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                      ControlToValidate="TextHouseCleanHrs" ErrorMessage="Value must be a whole number" />   
                      <asp:RequiredFieldValidator runat="server" ID="HouseCleanHrsCheck" ControlToValidate="TextHouseCleanHrs"/>
                       <%--this is 2nd --%>
                      <div class="row">
                          <div class="col-md-5 nP"> <span">Running-6 mph:</span> </div>
                          <div class="col-md-7">
                               <asp:TextBox ID="TextRunningHrs" Width="30px" Height="30px" runat="server" ToolTip="in minutes">30</asp:TextBox>mins
                          </div>
                      </div> 
                      <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                      ControlToValidate="TextRunningHrs" ErrorMessage="Value must be a whole number" />   
                      <asp:RequiredFieldValidator runat="server" ID="RunningHrsCheck" ControlToValidate="TextRunningHrs"/>
                       <%--this is 3rd --%>
                      <div class="row">
                          <div class="col-md-5 nP"> <span">Walking 3.0 mph:</span> </div>
                          <div class="col-md-7">
                               <asp:TextBox ID="TextWalkingHrs" Width="30px" Height="30px" runat="server" ToolTip="in minutes">30</asp:TextBox>mins
                          </div>
                      </div> 
                      <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                      ControlToValidate="TextWalkingHrs" ErrorMessage="Value must be a whole number" />   
                      <asp:RequiredFieldValidator runat="server" ID="WalkingHrsCheck" ControlToValidate="TextWalkingHrs"/>

                       <%--this is 4th --%>
                      <div class="row">
                          <div class="col-md-5 nP"> <span">Walk upstairs:</span> </div>
                          <div class="col-md-7">
                               <asp:TextBox ID="TextStairHrs" Width="30px" Height="30px" runat="server" ToolTip="in minutes">30</asp:TextBox>mins
                          </div>
                      </div> 
                      <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                      ControlToValidate="TextStairHrs" ErrorMessage="Value must be a whole number" />   
                      <asp:RequiredFieldValidator runat="server" ID="StairsHrsCheck" ControlToValidate="TextStairHrs"/>
                       <%--this is 5th --%>
                      <div class="row">
                          <div class="col-md-5 nP"> <span">Crew, sculling, rowing:</span> </div>
                          <div class="col-md-7">
                               <asp:TextBox ID="TextRowingHrs" Width="30px" Height="30px" runat="server" ToolTip="in minutes">30</asp:TextBox>mins
                          </div>
                      </div> 
                      <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                      ControlToValidate="TextRowingHrs" ErrorMessage="Value must be a whole number" />   
                      <asp:RequiredFieldValidator runat="server" ID="RowingHrsCheck" ControlToValidate="TextRowingHrs"/>

                       <%--this is 6th --%>
                      <div class="row">
                          <div class="col-md-5 nP"> <span">Swimming laps, freestyle, slow:</span> </div>
                          <div class="col-md-7">
                               <asp:TextBox ID="TextSwimmingHrs" Width="30px" Height="30px" runat="server" ToolTip="in minutes">30</asp:TextBox>mins
                          </div>
                      </div> 
                      <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                      ControlToValidate="TextSwimmingHrs" ErrorMessage="Value must be a whole number" />   
                      <asp:RequiredFieldValidator runat="server" ID="SwimminhHrsCheck" ControlToValidate="TextSwimmingHrs"/>

                  </div>
        </fieldset>
     </div>
     <div class="col-md-3" style="padding:0 0 0 0;">
         <a href="#getHealthResults"></a>
         <h4>Your Results</h4>
          <div>
            <asp:Label ID="Label4" runat="server" Text="BMI:" Font-Bold="True" Font-Names="Verdana"></asp:Label> <br />
            <asp:TextBox ID="TextBoxBMI" runat="server" Width="56px" Enabled="False" Font-Bold="True" Font-Italic="True" Font-Names="Lucida Sans" ToolTip="A&nbsp;body mass index (BMI)&nbsp;above the healthy weight range or&nbsp;too much fat around your waist can increase your risk of serious health problems, like heart disease, type 2 diabetes, stroke and certain cancers."></asp:TextBox> <br /> <br />
          
           <asp:Label ID="Label5" runat="server" Text="BMR" Font-Bold="True" Font-Names="Verdana"></asp:Label><br />
           <asp:TextBox ID="TextBoxBMR" runat="server" Width="56px" Enabled="False" ToolTip="Basal metabolic rate (BMR) is the amount of energy expended while at rest in a neutrally temperate environment, in the post-absorptive state (meaning that the digestive system is inactive, which requires about twelve hours of fasting)."></asp:TextBox> <br /> <br />

           <asp:Label ID="Label12" runat="server" Text=""></asp:Label> <br />
           <asp:TextBox ID="TextDesiredWeight" runat="server" Width="39px" Enabled="False"></asp:TextBox> <asp:Label ID="Label20" runat="server" Text=" kg"></asp:Label> <br />
           <br />
           <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="Advise Me!" /><br />
           
        </div>

     </div>
 </div>
  <div class="row jumbotron" id="resultPane">
      <div class="col-md-7" style="line-height:20px;padding-top:20px;">
          <ul>
            <li><asp:Label CssClass="testInfoLabel2" ID="LabelBMIResult" runat="server" ForeColor="black">Please enter details of your daily excercise.</asp:Label> </li> 
            <li><asp:Label CssClass="testInfoLabel2" ID="LabelIdealWeight" runat="server">And click on [Advise Me] button for your results.</asp:Label></li>
            <li><asp:Label CssClass="testInfoLabel2" ID="LabelFinalResult" runat="server"></asp:Label></li>
            <li><asp:Label CssClass="testInfoLabel2" ID="LabelKgToCals" runat="server" Visible="False"></asp:Label></li>
            <li><asp:Label CssClass="testInfoLabel2" ID="LabelResultOfWeek" runat="server"></asp:Label></li>
          </ul>
      </div>
      <div class="col-md-5" style="width:40%">     
          <asp:Chart ID="cTestChart" runat="server" Width="450px" Height="196px"/>
      </div>
   </div>
  </form>

</div>
</body>
</html>
