
namespace ImageScaler
{


    public class ResizeParameters_t
    {
        public CommandLine.Utility.Arguments CommandLine;

        public int? Width;
        public int? Height;
        public int? Quality;
        public bool? FitProportional;

        public string Prefix;
        public string Suffix;
        public string WorkingDirectory;

        public System.Drawing.Imaging.ImageFormat OutputFormat;


        protected static System.Drawing.Imaging.ImageFormat GetImageFormat(string format)
        {
            System.Drawing.Imaging.ImageFormat imageFormat = null;

            try
            {
                System.Drawing.ImageFormatConverter imageFormatConverter = new System.Drawing.ImageFormatConverter();
                imageFormat = (System.Drawing.Imaging.ImageFormat)imageFormatConverter.ConvertFromString(format);
            }
            catch (System.Exception)
            {
                // throw;
            }

            return imageFormat;
        }


        public void SetDefaults()
        {
            if (!this.Width.HasValue)
                this.Width = 600;

            if (!this.Height.HasValue)
                this.Height = 600;

            if (!this.Quality.HasValue)
                this.Quality = 100; // 50 

            if (!this.FitProportional.HasValue)
                this.FitProportional = true;

            if (this.Prefix == null)
                this.Prefix = "resized_";

            if (this.Suffix == null)
                this.Suffix = "";

            if (this.WorkingDirectory == null)
            {
                this.WorkingDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                this.WorkingDirectory = System.IO.Path.GetDirectoryName(this.WorkingDirectory);
            }

            if (this.OutputFormat == null)
                this.OutputFormat = System.Drawing.Imaging.ImageFormat.Png;
            
        } // End Sub SetDefaults


        public ResizeParameters_t()
            : this(null)
        { } // End Constructor 


        public ResizeParameters_t(string[] pargs)
        {
            if (pargs != null)
            {
                this.CommandLine = new CommandLine.Utility.Arguments(pargs);

                this.Width = this.CommandLine.GetInt("width");
                this.Height = this.CommandLine.GetInt("height");
                this.FitProportional = this.CommandLine.GetBool("FitProportional");
                this.Quality = this.CommandLine.GetInt("quality");

                this.Prefix = this.CommandLine["prefix"];
                this.Suffix = this.CommandLine["sufffix"];

                this.WorkingDirectory = this.CommandLine["cwd"];

                string strOutputFormat = this.CommandLine["OutputFormat"];

                if (!string.IsNullOrEmpty(strOutputFormat))
                {
                    strOutputFormat = strOutputFormat.ToLowerInvariant().Replace("jpg", "jpeg");
                    this.OutputFormat = GetImageFormat(strOutputFormat);
                }
                
            } // End if(pargs != null)

            SetDefaults();
        } // End Constructor 


    } // End Class ResizeParameters_t


} // End Namespace ImageScaler 
