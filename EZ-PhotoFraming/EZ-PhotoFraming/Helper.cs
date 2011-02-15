using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EZ_PhotoFraming
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

        static public void ResizeImage(Size sourceSize, double borderPercent, int lengthLimit,
                                        out int newWidth, out int newHeight, out int frameThickness)
        {
            double Width = sourceSize.Width * (borderPercent * 2 + 1);
            double Height = sourceSize.Height + (sourceSize.Width * borderPercent * 2);

            double nPercentW = 0;
            double nPercentH = 0;

            nPercentW = lengthLimit / Width;
            nPercentH = lengthLimit / Height;

            if ((nPercentW + nPercentH) >= 2)
            {
                newWidth = sourceSize.Width;
                newHeight = sourceSize.Height;
                frameThickness = (int)(newWidth * borderPercent);
            }
            else if (nPercentH < nPercentW)
            {
                Width = Width * nPercentH;
                Height = lengthLimit;

                Width = Width / (borderPercent * 2 + 1);
                Height = Height - (Width * borderPercent * 2);
                frameThickness = (int)((lengthLimit - Width) / 2);

                newWidth = (int)Width;
                newHeight = lengthLimit - 2 * frameThickness;
            }
            else
            {
                Width = lengthLimit;
                Height = Height * nPercentW;

                Width = Width / (borderPercent * 2 + 1);
                Height = Height - (Width * borderPercent * 2);
                frameThickness = (int)((lengthLimit - Width) / 2);

                newWidth = lengthLimit - 2 * frameThickness;
                newHeight = (int)Height;
            }
        }
    }
}
