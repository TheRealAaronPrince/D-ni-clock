/*
 * Created by SharpDevelop.
 * User: princ
 * Date: 06/11/2023
 * Time: 21:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace d_ni_clock
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class Form1 : Form
	{
		string options = "C://clock Screensaver/font.txt";
		string fontName = "";
		public Form1()
		{
			InitializeComponent();
			this.Text = "screensaver settings";
			this.Size = new Size(300,100);
			this.MinimumSize = this.Size;
			this.MaximumSize = this.Size;
			ComboBox AllFonts = new ComboBox();
			AllFonts.DrawMode = DrawMode.OwnerDrawFixed;
			AllFonts.Location = new Point(0,0);
			AllFonts.Width = this.Width-20;
			AllFonts.DataSource = System.Drawing.FontFamily.Families.ToList();
			AllFonts.DrawItem += ComboBoxFonts_DrawItem;
			this.Controls.Add(AllFonts);
			Button save = new Button();
			save.Click += new EventHandler(saveFont);
			save.Size = new Size(75,25);
			save.Text = "save";
			save.Location = new Point(0,AllFonts.Height);
			this.Controls.Add(save);
			if(!File.Exists(options))
			{
				Directory.CreateDirectory("C://clock Screensaver/");
				File.Create(options).Dispose();
			}
			if(new FileInfo(options).Length == 0)
			{
				using (var sw = new StreamWriter(options))
				{
					sw.Write("Times new roman");
					sw.Close();
				}
			}
		}
		private void ComboBoxFonts_DrawItem(object sender, DrawItemEventArgs e)
		{
			var comboBox = (ComboBox)sender;
			var fontFamily = (FontFamily)comboBox.Items[e.Index];
			var font = new Font(fontFamily, comboBox.Font.SizeInPoints);
			e.Graphics.DrawString(font.Name, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
			fontName = font.Name.ToString();
		}
		private void saveFont(object sender, EventArgs e)
		{
			if(fontName.Length > 0)
			{
				using (var sw = new StreamWriter(options))
				{
					sw.Write(fontName);
					sw.Close();
				}
			}
			Application.Exit();
		}
	}
}
