
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


			public void SetDefaults()
			{
				if(!this.Width.HasValue)
					this.Width = 600;

				if(!this.Height.HasValue)
					this.Height = 600;

				if(!this.Quality.HasValue)
					this.Quality = 50;

				if(!this.FitProportional.HasValue)
					this.FitProportional = true;

				if(this.Prefix == null)
					this.Prefix = "resized_";

				if(this.Suffix == null)
					this.Suffix = "";
			} // End Sub SetDefaults


			public ResizeParameters_t() : this(null)
			{ } // End Constructor 


			public ResizeParameters_t(string[] pargs)
			{
				if(pargs != null)
				{
					this.CommandLine = new CommandLine.Utility.Arguments(pargs);

					this.Width = this.CommandLine.GetInt("width");
					this.Height = this.CommandLine.GetInt("height");
					this.FitProportional = this.CommandLine.GetBool("FitProportional");
					this.Quality = this.CommandLine.GetInt("quality");
				
					this.Prefix = this.CommandLine["prefix"];
					this.Suffix = this.CommandLine["sufffix"];
				} // End if(pargs != null)

				SetDefaults();
			} // End Constructor 


		} // End Class ResizeParameters_t


} // End Namespace ImageScaler 
