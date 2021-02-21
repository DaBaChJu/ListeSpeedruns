using System;
using System.Windows.Forms;

namespace ListeSpeedruns
{
	static class Program
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if( System.IO.File.Exists(Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "MediaInfo.dll") )
				Application.Run(new Form1(args));
			else
				MessageBox.Show("MediaInfo.dll non trouvée.");

		}
	}
}
