
namespace ImageScaler 
{


    public class FileHelper
    {


        // ImageScaler.FileHelper.GetRasterImages();
        public static string[] GetRasterImages(string searchFolder)
        {
            return GetRasterImages(searchFolder, System.IO.SearchOption.TopDirectoryOnly);
        } // End Function GetRasterImages


        public static string[] GetRasterImages(string searchFolder, System.IO.SearchOption searchOption)
        {
            string[] filters = new string[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            return GetFilesFrom(searchFolder, filters, searchOption);
        } // End Function GetRasterImages


        public static string[] GetFilesFrom(string searchFolder, string[] filters, System.IO.SearchOption searchOption)
        {
            string[] arrRes = null;
            System.Collections.Generic.List<string> filesFound = new System.Collections.Generic.List<string>();

            string[] filez = System.IO.Directory.GetFiles(searchFolder, "*.*", searchOption);

            for (int i = 0; i < filters.Length; ++i)
            {
                string extension = "." + filters[i];

                for (int j = 0; j < filez.Length; ++j)
                {
                    if (filez[j].EndsWith(extension, System.StringComparison.InvariantCultureIgnoreCase))
                        filesFound.Add(filez[j]);
                } // Next j 

            } // Next i

            arrRes = filesFound.ToArray();
            filesFound.Clear();
            filesFound = null;

            return arrRes;
        } // End Function GetFilesFrom


    } // End Class FileHelper 


} // End Namespace ImageScaler 
