
using System.Windows.Forms;


namespace ImageScaler
{


    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
		[System.STAThread]
		static void Main(string[] args)
        {
#if (false)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
#endif


			// args = new string[]{"--width", "200", "--Height", "250"
			// 	, "--fitproportional", "fAlSe", "/quality", "25"};

			ResizeParameters_t ResizeParameters = new ResizeParameters_t(args);
			ImageSizer.ResizeImages (ResizeParameters);

			System.Console.WriteLine(System.Environment.NewLine);
			System.Console.WriteLine(" -- Press any key to continue --- ");
			System.Console.ReadKey();
		} // End Sub Main(string[] args)


	} // End Class Program


} // End Namespace ImageScaler 
