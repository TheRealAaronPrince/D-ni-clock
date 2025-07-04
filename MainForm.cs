using System;
using System.Runtime;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace d_ni_clock
{
	public partial class MainForm : Form
	{
		int randX = 0;
		int randY = 0;
		int change = 0;
		int Lwidth = 350;
		int Lheight = 100;
		string options = "C://clock Screensaver/font.txt";
		Label label1 = new Label();
		Label label2 = new Label();
		string font;
		double anchor = DateTimeOffset.Parse("april 21 1991 16:54:00 -0:00").ToUnixTimeMilliseconds();
		double test = DateTimeOffset.Parse("april 21 1991 16:54:00 -0:00").ToUnixTimeMilliseconds();
		double time;
		double dniTime;
		double timeRatio = 22656250/31556925.216;
		string[] months = {"Leefo","Leebro","Leesahn","Leetar","Leevot","Leevofo","Leevobro","Leevosahn","Leevotar","Leenovoo"};
		Timer t = new Timer();
		//              .                          .
		// 1.392857388844137931034482758620689655172 of a second
		public MainForm()
		{
			if(File.Exists(options))
			{
				try
				{
					font = File.ReadAllText(options);
				}
				catch (Exception)
				{
					font = "Times new Roman";
				}
			}
			InitializeComponent();
			Cursor.Hide();
			TopMost = true;
			this.KeyPress  += new KeyPressEventHandler(ScreenSaverForm_KeyPress);
			this.MouseMove += new MouseEventHandler(ScreenSaverForm_MouseMove);
			this.MouseClick += new MouseEventHandler(ScreenSaverForm_MouseClick);
			this.SizeChanged += Form1_SizeChanged;
			this.BackColor = Color.Black;
			this.WindowState = FormWindowState.Maximized;
			this.FormBorderStyle = FormBorderStyle.None;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.ForeColor = System.Drawing.Color.DarkGray;
			label1.Width = Lwidth;
			label1.Height = Lheight;
			label1.Font = new Font(font,30);
			while(label1.Width < System.Windows.Forms.TextRenderer.MeasureText(label1.Text, new Font(label1.Font.FontFamily, label1.Font.Size, label1.Font.Style)).Width)
			{
				label1.Font = new Font(label1.Font.FontFamily, label1.Font.Size - 0.5f, label1.Font.Style);
			}
			label1.TextAlign = ContentAlignment.MiddleCenter;
			Controls.Add(label2);
			label2.BackColor = System.Drawing.Color.Transparent;
			label2.ForeColor = System.Drawing.Color.DarkGray;
			label2.Width = Lwidth;
			label2.Height = Lheight;
			label2.Font = new Font(font,30);
			while(label2.Width < System.Windows.Forms.TextRenderer.MeasureText(label2.Text, new Font(label2.Font.FontFamily, label2.Font.Size, label2.Font.Style)).Width)
			{
				label2.Font = new Font(label2.Font.FontFamily, label2.Font.Size - 0.5f, label2.Font.Style);
			}
			label2.TextAlign = ContentAlignment.MiddleCenter;
			label2.Location = new Point(0,Lheight);
			Controls.Add(label1);
			t.Interval = 1;
			t.Tick += new EventHandler(clockTick);
			t.Start();
		}
		private void randomize()
		{
			Random r = new Random();
			randX = r.Next(0, this.Width-Lwidth);
			randY = r.Next(0, this.Height-(Lheight*2));
		}
		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			randomize();
			Redraw();
		}
		private void Redraw()
		{
			if(change==300)
			{
				randomize();
				change = 0;
			}
			change ++;
			label1.Width = Lwidth;
			label1.Height = 100;
			label1.Font = new Font(font,30);
			while(label1.Width < System.Windows.Forms.TextRenderer.MeasureText(label1.Text, new Font(label1.Font.FontFamily, label1.Font.Size, label1.Font.Style)).Width)
			{
				label1.Font = new Font(label1.Font.FontFamily, label1.Font.Size - 0.5f, label1.Font.Style);
			}
			label1.Location = new Point(randX,randY);
			label2.Width = Lwidth;
			label2.Height = Lheight;
			label2.Font = new Font(font,30);
			while(label2.Width < System.Windows.Forms.TextRenderer.MeasureText(label2.Text, new Font(label2.Font.FontFamily, label2.Font.Size, label2.Font.Style)).Width)
			{
				label2.Font = new Font(label2.Font.FontFamily, label2.Font.Size - 0.5f, label2.Font.Style);
			}
			label2.Location = new Point(randX,randY+Lheight);
			this.label1.Refresh();
			this.label2.Refresh();
		}
		private void clockTick(object sender, EventArgs e)
		{
			string gDate = DateTime.Now.ToString("MMMM dd, yyyy");
			string gTime = DateTime.Now.ToString("hh:mm:ss");
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
			string clock = Convert.ToString(gahrtahvo) + ":" + Convert.ToString((pahrtahvo*5)+tahvo) + ":" + Convert.ToString(gorahn) + ":" + Convert.ToString(prorahn);
			label1.Text = date + "\n" + clock;
			label2.Text = gDate + "\n" + gTime;
			Redraw();
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
		private Point mouseLocation;
		private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
		{
			if (!mouseLocation.IsEmpty)
			{
				// Terminate if mouse is moved a significant distance
				if (Math.Abs(mouseLocation.X - e.X) > 10 ||
				    Math.Abs(mouseLocation.Y - e.Y) > 10)
					Application.Exit();
			}
			// Update current mouse location
			mouseLocation = e.Location;
		}
		
		private void ScreenSaverForm_MouseClick(object sender, MouseEventArgs e)
		{
			Application.Exit();
		}
		
		private void ScreenSaverForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			Application.Exit();
		}
	}
}
