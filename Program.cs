
using System;
using System.Windows.Forms;

namespace d_ni_clock
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (args.Length > 0 && (args[0] == "/s" || args[0] == "/p"))
			{
				if(!(args[0] == "/p"))
				{
				    Application.Run(new MainForm());
				}
			}
			else
			{
					Application.Run(new Form1());
			}
		}
		
	}
}
