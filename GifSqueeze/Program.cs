namespace GifSqueeze;
class programm
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length <= 0) return;


            string url = args[0];

            if (!File.Exists(url)) {
                Console.WriteLine("Invalid url"); return;}

            if (Path.GetExtension(url) == ".jpg" || Path.GetExtension(url) == ".png" || Path.GetExtension(url) == ".jpeg")
            {
                using (Bitmap bmp1 = new Bitmap(Path.GetFullPath(url)))
                {
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(Encoder.Quality, 50L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    bmp1.Save(Path.GetFileNameWithoutExtension(url) + "_compress.jpeg", GetEncoder(ImageFormat.Jpeg), myEncoderParameters);
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

    public static Image[] GetFramesFromAnimatedGIF(Image IMG)
    {
        List<Image> IMGs = new List<Image>();
        int Length = IMG.GetFrameCount(FrameDimension.Time);
        for (int i = 0; i < Length; i++)
        {
            IMG.SelectActiveFrame(FrameDimension.Time, i);
            IMGs.Add(new Bitmap(IMG));
        }
        return IMGs.ToArray();
    }

}