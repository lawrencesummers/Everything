namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class ImageFormatHandler
    {
        private EncoderValue encoderValue_0 = EncoderValue.RenderProgressive;
        private EncoderValue encoderValue_1 = EncoderValue.ScanMethodInterlaced;
        private EncoderValue encoderValue_2 = EncoderValue.CompressionLZW;
        private ImageCodecInfo[] imageCodecInfo_0 = ImageCodecInfo.GetImageEncoders();
        private ImageCodecInfo[] imageCodecInfo_1 = ImageCodecInfo.GetImageDecoders();
        private ImageFormatTypes imageFormatTypes_0;
        private long long_0 = 50;
        private long long_1 = 0x18;

        public virtual ImageCodecInfo GetCodecInfo(ImageFormatTypes type)
        {
            string mimeType = GetMimeType(type);
            if (!string.IsNullOrEmpty(mimeType))
            {
                for (int i = 0; i < 2; i++)
                {
                    ImageCodecInfo[] infoArray;
                    if (i == 0)
                    {
                        infoArray = this.imageCodecInfo_0;
                    }
                    else
                    {
                        infoArray = this.imageCodecInfo_1;
                    }
                    foreach (ImageCodecInfo info in infoArray)
                    {
                        if (info.MimeType == mimeType)
                        {
                            return info;
                        }
                    }
                }
            }
            return null;
        }

        public virtual string GetDefaultFilenameExtension(ImageFormatTypes type)
        {
            string str = "";
            ImageCodecInfo codecInfo = this.GetCodecInfo(type);
            if (codecInfo != null)
            {
                str = codecInfo.FilenameExtension.Split(new char[] { ';' })[0];
                if (str.StartsWith("*."))
                {
                    str = str.Substring(2);
                }
            }
            return str;
        }

        public virtual EncoderParameters GetEncoderParameters(ImageFormatTypes type, out ImageCodecInfo info)
        {
            EncoderParameters parameters = null;
            info = this.GetCodecInfo(type);
            if (info != null)
            {
                EncoderParameter parameter;
                switch (type)
                {
                    case ImageFormatTypes.imgGIF:
                        parameters = new EncoderParameters(2);
                        parameter = new EncoderParameter(Encoder.Version, 10);
                        parameters.Param[0] = parameter;
                        parameter = new EncoderParameter(Encoder.ScanMethod, (long) this.encoderValue_1);
                        parameters.Param[1] = parameter;
                        return parameters;

                    case ImageFormatTypes.imgICON:
                        return parameters;

                    case ImageFormatTypes.imgJPEG:
                        parameters = new EncoderParameters(2);
                        parameter = new EncoderParameter(Encoder.RenderMethod, (long) this.encoderValue_0);
                        parameters.Param[0] = parameter;
                        parameter = new EncoderParameter(Encoder.Quality, this.long_0);
                        parameters.Param[1] = parameter;
                        return parameters;

                    case ImageFormatTypes.imgPNG:
                        parameters = new EncoderParameters(2);
                        parameter = new EncoderParameter(Encoder.RenderMethod, (long) this.encoderValue_0);
                        parameters.Param[0] = parameter;
                        parameter = new EncoderParameter(Encoder.ScanMethod, (long) this.encoderValue_1);
                        parameters.Param[1] = parameter;
                        return parameters;

                    case ImageFormatTypes.imgTIFF:
                        parameters = new EncoderParameters(2);
                        parameter = new EncoderParameter(Encoder.ColorDepth, this.long_1);
                        parameters.Param[0] = parameter;
                        parameter = new EncoderParameter(Encoder.Compression, (long) this.encoderValue_2);
                        parameters.Param[1] = parameter;
                        return parameters;
                }
            }
            return parameters;
        }

        public static ImageFormatTypes GetImageFormat(ImageFormat type)
        {
            if (type == ImageFormat.Bmp)
            {
                return ImageFormatTypes.imgBMP;
            }
            if (type == ImageFormat.Emf)
            {
                return ImageFormatTypes.imgEMF;
            }
            if (type == ImageFormat.Exif)
            {
                return ImageFormatTypes.imgEXIF;
            }
            if (type == ImageFormat.Gif)
            {
                return ImageFormatTypes.imgGIF;
            }
            if (type == ImageFormat.Icon)
            {
                return ImageFormatTypes.imgICON;
            }
            if (type == ImageFormat.Jpeg)
            {
                return ImageFormatTypes.imgJPEG;
            }
            if (type == ImageFormat.Png)
            {
                return ImageFormatTypes.imgPNG;
            }
            if (type == ImageFormat.Tiff)
            {
                return ImageFormatTypes.imgTIFF;
            }
            if (type == ImageFormat.Wmf)
            {
                return ImageFormatTypes.imgWMF;
            }
            return ImageFormatTypes.imgNone;
        }

        public static ImageFormat GetImageFormat(ImageFormatTypes type)
        {
            switch (type)
            {
                case ImageFormatTypes.imgBMP:
                    return ImageFormat.Bmp;

                case ImageFormatTypes.imgEMF:
                    return ImageFormat.Emf;

                case ImageFormatTypes.imgEXIF:
                    return ImageFormat.Exif;

                case ImageFormatTypes.imgGIF:
                    return ImageFormat.Gif;

                case ImageFormatTypes.imgICON:
                    return ImageFormat.Icon;

                case ImageFormatTypes.imgJPEG:
                    return ImageFormat.Jpeg;

                case ImageFormatTypes.imgPNG:
                    return ImageFormat.Png;

                case ImageFormatTypes.imgTIFF:
                    return ImageFormat.Tiff;

                case ImageFormatTypes.imgWMF:
                    return ImageFormat.Wmf;
            }
            return null;
        }

        public static string GetMimeType(ImageFormatTypes type)
        {
            string str = null;
            switch (type)
            {
                case ImageFormatTypes.imgBMP:
                    str = "bmp";
                    break;

                case ImageFormatTypes.imgEMF:
                    str = "x-emf";
                    break;

                case ImageFormatTypes.imgGIF:
                    str = "gif";
                    break;

                case ImageFormatTypes.imgICON:
                    str = "x-icon";
                    break;

                case ImageFormatTypes.imgJPEG:
                    str = "jpeg";
                    break;

                case ImageFormatTypes.imgPNG:
                    str = "png";
                    break;

                case ImageFormatTypes.imgTIFF:
                    str = "tiff";
                    break;

                case ImageFormatTypes.imgWMF:
                    str = "x-wmf";
                    break;
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = string.Format("image/{0}", str);
            }
            return str;
        }

        public ImageFormatTypes DefaultFormat
        {
            get
            {
                return this.imageFormatTypes_0;
            }
            set
            {
                this.imageFormatTypes_0 = value;
            }
        }

        public enum ImageFormatTypes
        {
            imgNone,
            imgBMP,
            imgEMF,
            imgEXIF,
            imgGIF,
            imgICON,
            imgJPEG,
            imgPNG,
            imgTIFF,
            imgWMF
        }
    }
}

