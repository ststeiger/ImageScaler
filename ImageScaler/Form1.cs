
using System.Windows.Forms;


namespace ImageScaler
{


    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        } // End Constructor


        private void button1_Click(object sender, System.EventArgs e)
        {
            ScaleImage();
		} // End Sub button1_Click


        public static void ScaleImage()
		{
			using (System.Drawing.Image image = System.Drawing.Image.FromFile(@"c:\logo.png")) {
				using (System.Drawing.Image newImage = ScaleImage (image, 300, 400)) {
					newImage.Save (@"c:\test.png", System.Drawing.Imaging.ImageFormat.Png);
				}
			}
		} // End Sub ScaleImage


        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth, int maxHeight)
		{
			double ratioX = (double)maxWidth / image.Width;
			double ratioY = (double)maxHeight / image.Height;
			double ratio = System.Math.Min (ratioX, ratioY);

			int newWidth = (int)(image.Width * ratio);
			int newHeight = (int)(image.Height * ratio);

			System.Drawing.Image newImage = new System.Drawing.Bitmap (newWidth, newHeight);

			using (System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(newImage)) {
				gfx.DrawImage (image, 0, 0, newWidth, newHeight);
			}

			return newImage;
		} // End Function ScaleImage


    }


}
