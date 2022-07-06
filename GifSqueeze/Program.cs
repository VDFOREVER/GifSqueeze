namespace GifSqueeze;
class programm
{
    static List<Image> images = new List<Image>();
    static void Main(string[] args)
    {
        try
        {
            if (args.Length <= 0) return;


            string url = args[0];

            if (!File.Exists(url)) {
                Console.WriteLine("Invalid url"); return;}

            if (Path.GetExtension(url) == ".gif")
            {
                Image bitmap = Image.FromFile(Path.GetFullPath(url));
                FrameDimension dimension = new FrameDimension(bitmap.FrameDimensionsList[0]);
                int frame_count = bitmap.GetFrameCount(dimension);

                for (int i = 0; i < frame_count; i++)
                {
                    bitmap.SelectActiveFrame(new FrameDimension(bitmap.FrameDimensionsList[0]), i);
                    images.Add(new Bitmap(bitmap));
                }
                /*
                for (int i = 0; i < images.Count; i++)
                {
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(Encoder.Quality, 50L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    images[i].Save(Path.GetFileNameWithoutExtension(url) + $"_{i}.jpeg", GetEncoder(ImageFormat.Jpeg), myEncoderParameters);
                }
                */
            }
            if (Path.GetExtension(url) == ".jpg" || Path.GetExtension(url) == ".png" || Path.GetExtension(url) == ".jpeg")
            {
                using (Bitmap bmp1 = new Bitmap(Path.GetFullPath(url)))
                {
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(Encoder.Quality, 50L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    bmp1.Save(Path.GetFileNameWithoutExtension(url) + "_compress" + Path.GetExtension(url), GetEncoder(ImageFormat.Jpeg), myEncoderParameters);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        Console.ReadKey();
    }
    static private ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
    }
}