
namespace ImageScaler
{


    // http://www.codeproject.com/Tips/552141/Csharp-Image-resize-convert-and-save


    /// <summary>
    /// Class contaning method to resize an image and save in JPEG format
    /// </summary>
    public class ImageSizer
    {


        // ImageSizer.ResizeImages(ResizeParameters);
        public static void ResizeImages(ResizeParameters_t ResizeParameters)
        {

            foreach (string strFileName in FileHelper.GetRasterImages(ResizeParameters.WorkingDirectory))
            {
                string strNewFileName = System.IO.Path.GetFileNameWithoutExtension(strFileName);
                
                strNewFileName = ResizeParameters.Prefix + strNewFileName + ResizeParameters.Suffix;
                strNewFileName = System.IO.Path.Combine(ResizeParameters.WorkingDirectory, strNewFileName);

                ResizeImage(strFileName, strNewFileName, ResizeParameters, ResizeParameters.OutputFormat);
            } // Next strFileName 

        } // End Sub ResizeImages 


        public static void ResizeImage(string fileName, string saveAsFileName, ResizeParameters_t ResizeParameters, System.Drawing.Imaging.ImageFormat outputFormat)
        {
            ResizeImage(
                 fileName
                , saveAsFileName
                , ResizeParameters.Width.Value
                , ResizeParameters.Height.Value
                , ResizeParameters.Quality.Value
                , outputFormat
                );
        } // End Sub ResizeImage


        public static void ResizeImage(string fileName, string filePath
                        , int maxWidth, int maxHeight, int quality, System.Drawing.Imaging.ImageFormat outputFormat)
        {

            using (System.Drawing.Image img = System.Drawing.Image.FromFile(fileName))
            {
                ResizeImage(
                     img
                    , filePath
                    , maxWidth
                    , maxHeight
                    , quality
                    , outputFormat
                    );

            } // End Using img

        } // End Sub ResizeImage


        /// <summary>
        /// Method to resize, convert and save the image.
        /// </summary>
        /// <param name="image">Bitmap image.</param>
        /// <param name="filePath">Output filename (and path).</param> 
        /// <param name="maxWidth">resize width.</param>
        /// <param name="maxHeight">resize height.</param>
        /// <param name="quality">quality setting value [max compressed 0 ... 100 max-quality].</param>
        public static void ResizeImage(System.Drawing.Image image, string filePath
                         , int maxWidth, int maxHeight, int quality, System.Drawing.Imaging.ImageFormat outputFormat)
        {
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = System.Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            
            filePath += "." + outputFormat.ToString().ToLowerInvariant().Replace("jpeg", "jpg");


            // Get an ImageCodecInfo object that represents the JPEG codec.
            System.Drawing.Imaging.ImageCodecInfo imageCodecInfo = GetEncoderInfo(outputFormat);

            // Create an Encoder object for the Quality parameter.
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            // Convert other formats (including CMYK) to RGB.
            using (System.Drawing.Bitmap newImage = new System.Drawing.Bitmap(newWidth, newHeight,
                // System.Drawing.Imaging.PixelFormat.Format24bppRgb // OMG bug
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb 
                ))
            {

                // Draws the image in the specified size with quality mode set to HighQuality
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(newImage))
                {
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                } // End Using Graphics graphics


                // Create an EncoderParameters object. 
                using (System.Drawing.Imaging.EncoderParameters encoderParameters =
                new System.Drawing.Imaging.EncoderParameters(1))
                {

                    // Save the image as a JPEG file with quality level.
                    using (System.Drawing.Imaging.EncoderParameter encoderParameter =
                new System.Drawing.Imaging.EncoderParameter(encoder, quality))
                    {
                        encoderParameters.Param[0] = encoderParameter;
                        newImage.Save(filePath, imageCodecInfo, encoderParameters);
                    } // End Using encoderParameter
                } // End Using encoderParameters
            } // End Using newImage
        } // End Sub ResizeImage


        /// <summary>
        /// Method to get encoder infor for given image format.
        /// </summary>
        /// <param name="format">Image format</param>
        /// <returns>image codec info.</returns>
        private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(
            System.Drawing.Imaging.ImageFormat format)
        {
            foreach (System.Drawing.Imaging.ImageCodecInfo cdc
                in System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders())
            {
                if (cdc.FormatID == format.Guid)
                    return cdc;
            } // Next ImageCodecInfo cdc

            return null;
            //return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        } // End Function GetEncoderInfo


    } // End Class ImageSizer 


} // End Namespace ImageScaler 
