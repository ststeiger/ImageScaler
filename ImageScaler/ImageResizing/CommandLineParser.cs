

namespace ImageScaler.CommandLine.Utility
{


	/// <summary>
	/// Arguments class
	/// </summary>
	public class Arguments
	{


		// Variables
		private System.Collections.Specialized.StringDictionary Parameters;

		// Constructor
		public Arguments(string[] Args)
		{
			Parameters = new System.Collections.Specialized.StringDictionary();
			System.Text.RegularExpressions.Regex Spliter = 
				new System.Text.RegularExpressions.Regex(@"^-{1,2}|^/|=|:",
					 System.Text.RegularExpressions.RegexOptions.IgnoreCase
					|System.Text.RegularExpressions.RegexOptions.Compiled
			);

			System.Text.RegularExpressions.Regex Remover = 
				new System.Text.RegularExpressions.Regex(@"^['""]?(.*?)['""]?$",
					 System.Text.RegularExpressions.RegexOptions.IgnoreCase
					|System.Text.RegularExpressions.RegexOptions.Compiled
			);

			string Parameter = null;
			string[] Parts;

			// Valid parameters forms:
			// {-,/,--}param{ ,=,:}((",')value(",'))
			// Examples: 
			// -param1 value1 --param2 /param3:"Test-:-work" 
			//   /param4=happy -param5 '--=nice=--'
			foreach(string Txt in Args)
			{
				// Look for new parameters (-,/ or --) and a
				// possible enclosed value (=,:)
				Parts = Spliter.Split(Txt,3);

				switch(Parts.Length){
				// Found a value (for the last parameter 
				// found (space separator))
				case 1:
					if(Parameter != null)
					{
						if(!Parameters.ContainsKey(Parameter)) 
						{
							Parts[0] = 
								Remover.Replace(Parts[0], "$1");

							Parameters.Add(Parameter, Parts[0]);
						}
						Parameter=null;
					}
					// else Error: no parameter waiting for a value (skipped)
					break;

					// Found just a parameter
				case 2:
					// The last parameter is still waiting. 
					// With no value, set it to true.
					if(Parameter!=null)
					{
						if(!Parameters.ContainsKey(Parameter)) 
							Parameters.Add(Parameter, "true");
					}
					Parameter=Parts[1];
					break;

					// Parameter with enclosed value
				case 3:
					// The last parameter is still waiting. 
					// With no value, set it to true.
					if(Parameter != null)
					{
						if(!Parameters.ContainsKey(Parameter)) 
							Parameters.Add(Parameter, "true");
					}

					Parameter = Parts[1];

					// Remove possible enclosing characters (",')
					if(!Parameters.ContainsKey(Parameter))
					{
						Parts[2] = Remover.Replace(Parts[2], "$1");
						Parameters.Add(Parameter, Parts[2]);
					}

					Parameter=null;
					break;
				}
			}
			// In case a parameter is still waiting
			if(Parameter != null)
			{
				if(!Parameters.ContainsKey(Parameter)) 
					Parameters.Add(Parameter, "true");
			}
		}


		public int? GetInt(string param)
		{
			string str = this[param];
			int iValue;
			if(int.TryParse(str, out iValue))
				return iValue;

			return null;
		}

		public long? GetLong(string param)
		{
			string str = this[param];
			long lngValue;
			if(long.TryParse(str, out lngValue))
				return lngValue;

			return null;
		}

		public bool? GetBool(string param)
		{
			string str = this[param];
			bool bValue;
			if(bool.TryParse(str, out bValue))
				return bValue;

			return null;
		}


		// Retrieve a parameter value if it exists 
		// (overriding C# indexer property)
		public string this [string Param]
		{
			get
			{
				return(Parameters[Param]);
			}
		}
	}
}

