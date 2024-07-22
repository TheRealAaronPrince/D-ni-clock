using System;
using System.Runtime;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace d_ni_clock
{
	public partial class MainForm : Form
	{
		Label label1 = new Label();
		Label label2 = new Label();
		int screenWidth = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width.ToString());
		double anchor = DateTimeOffset.Parse("april 21 1991 16:54:00 -0:00").ToUnixTimeMilliseconds();
		double time;
		double dniTime;
		double timeRatio = 22656250/31556925.216;
		string[] months = {"Leefo","Leebro","Leesahn","Leetar","Leevot","Leevofo","Leevobro","Leevosahn","Leevotar","Leenovoo"};
		Timer t = new Timer();
		//              .                          .
		// 1.392857388844137931034482758620689655172 of a second
		public MainForm()
		{
			this.BackColor = Color.BlanchedAlmond;
			this.FormBorderStyle = FormBorderStyle.None;
            this.Width = 500;
			this.Height = 100;
			InitializeComponent();
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.ForeColor = System.Drawing.Color.FromArgb(255,51,17,0);
			label1.Location = new Point(0,-10);
			label1.Width = this.Width;
			label1.Height = 62;
			label1.Font = new Font("Ink Free",35);
			label1.TextAlign = ContentAlignment.BottomCenter;
			Controls.Add(label1);
			label2.BackColor = System.Drawing.Color.FromArgb(255,32,32,32);
			label2.ForeColor = System.Drawing.Color.FromArgb(255,230,32,0);
			label2.Location = new Point(0,47);
			label2.Width = this.Width;
			label2.Height = 57;
			label2.Font = new Font("consolas",35);
			label2.TextAlign = ContentAlignment.BottomCenter;
			Controls.Add(label2);
			t.Interval = 139;
			t.Tick += new EventHandler(clockTick);
			t.Start();
		}
		private void clockTick(object sender, EventArgs e)
		{
			this.Location = new Point((screenWidth-500),0);
			this.SendToBack();
			time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			dniTime = ((time - anchor)/1000)*(timeRatio);
			double hahr = factorOut(dniTime,22656250.0) + 9647;
			double vailee = factorOut(dniTime,2265625.0,10.0);
			double yahr = factorOut(dniTime,78125.0,29.0)+1;
			double gahrtahvo = factorOut(dniTime,15625.0,5.0);
			double pahrtahvo = factorOut(dniTime,3125.0,5.0);
			double tahvo = factorOut(dniTime,625.0,5.0);
			double gorahn = factorOut(dniTime,25.0,25.0);
			double prorahn = factorOut(dniTime,1.0,25.0);
			string date = months[Convert.ToInt32(vailee)] + " " + Convert.ToString(yahr)  + ", " + Convert.ToString(hahr);
			string pahrConcat = Convert.ToString(gahrtahvo) + ":" + Convert.ToString((pahrtahvo*5)+tahvo) + ":" + Convert.ToString(gorahn) + ":" + Convert.ToString(prorahn);
			string gahrConcat = Convert.ToString((gahrtahvo*5)+pahrtahvo) + ":" + Convert.ToString(tahvo) + ":" + Convert.ToString(gorahn) + ":" + Convert.ToString(prorahn);
			label1.Text = date;
			label2.Text = gahrConcat;
			this.label1.Refresh();
		}
		private double factorOut(double a, double b, double c = 0)
		{
			if(c == 0)
			{
				return Math.Floor((a-(a%b))/b);
			}
			else
			{
				return Math.Floor((a-(a%b))/b)%c;
			}
		}
	}
}