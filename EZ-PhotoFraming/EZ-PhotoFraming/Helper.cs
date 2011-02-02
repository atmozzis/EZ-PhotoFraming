using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PhotoFraming
{
    class Helper
    {
        static public Boolean ParsePath(String FullPath, out String Directory, out String FileName, out String Extension)
        {
            String dir = Path.GetDirectoryName(FullPath);
            if (dir == String.Empty)
            {
                Directory = String.Empty;
                FileName = String.Empty;
                Extension = String.Empty;
                return false;
            }

            String ext = Path.GetExtension(FullPath);
            if (ext == String.Empty)
            {
                Directory = FullPath;
                FileName = String.Empty;
                Extension = String.Empty;
                return true;
            }
            else
            {
                Directory = dir;
                FileName = Path.GetFileName(FullPath);
                Extension = ext;
                return true;
            }
        }

        static public void SaveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
            img.Dispose();
        }

        static public ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}
