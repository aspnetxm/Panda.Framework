/*******************************************************************************
 * 作者：星星    
 * 描述：验证码图片  
 * 修改记录： 
*********************************************************************************/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Panda.Code
{
    public class Captcha
    {
        private int length;
        private Bitmap image;
        private Random random = new Random();

        public float FontSize { get; set; } = 16f;
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Width { get; set; } = 80;
        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; } = 30;
        /// <summary>
        /// 
        /// </summary>
        public bool QJGL { get; set; } = false;

        public byte[] Image()
        {
            GenerateImage();
            byte[] result = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Jpeg);
                result = memoryStream.GetBuffer();
            }
            return result;

        }

        public Captcha(int len = 4)
        {
            length = len;
            Code = GenerateRandomCode();
        }
         

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                image.Dispose();
            }
        }

        private void GenerateImage()
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectangle = new Rectangle(0, 0, Width, Height);
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.NarrowVertical, Color.LightGray, Color.White);
            graphics.FillRectangle(hatchBrush, rectangle);
            float num = (float)(rectangle.Height + 1);
            Font font;
            do
            {
                num -= 1f;
                font = new Font(new FontFamily("Times New Roman"), num, FontStyle.Bold);
            }
            while (graphics.MeasureString(Code, font).Width > (float)rectangle.Width);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddString(Code, font.FontFamily, (int)font.Style, FontSize, rectangle, stringFormat);
            float num2 = 4f;
            PointF[] destPoints = new PointF[]
            {
                new PointF((float)random.Next(rectangle.Width) / num2, (float)random.Next(rectangle.Height) / num2),
                new PointF((float)rectangle.Width - (float)random.Next(rectangle.Width) / num2, (float)random.Next(rectangle.Height) / num2),
                new PointF((float)random.Next(rectangle.Width) / num2, (float)rectangle.Height - (float)random.Next(rectangle.Height) / num2),
                new PointF((float)rectangle.Width - (float)random.Next(rectangle.Width) / num2, (float)rectangle.Height - (float)random.Next(rectangle.Height) / num2)
            };
            Matrix matrix = new Matrix();
            matrix.Translate(0f, 0f);
            graphicsPath.Warp(destPoints, rectangle, matrix, WarpMode.Perspective, 0f);
            hatchBrush = new HatchBrush(HatchStyle.Horizontal, Color.Blue, Color.Green);
            graphics.FillPath(hatchBrush, graphicsPath);

            if (QJGL)
            {
                int num3 = Math.Max(rectangle.Width, rectangle.Height);
                for (int i = 0; i < (int)((float)(rectangle.Width * rectangle.Height) / 30f); i++)
                {
                    int x = random.Next(rectangle.Width);
                    int y = random.Next(rectangle.Height);
                    int num4 = random.Next(num3 / 50);
                    int num5 = random.Next(num3 / 50);
                    graphics.FillEllipse(hatchBrush, x, y, num4, num5);
                }
            }

            font.Dispose();
            hatchBrush.Dispose();
            graphics.Dispose();

            image = TwistImage(bitmap, true, 1, 1);
        }
        private string GenerateRandomCode()
        {
            Random random = new Random();
            char[] array = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            int maxValue = array.Length - 1;
            string text = "";
            for (int i = 0; i < length; i++)
            {
                text += array[random.Next(0, maxValue)].ToString();
                random.NextDouble();
                random.Next(100, 1999);
            }
            System.Diagnostics.Debug.Print(text);
           
            return text;
        }

        /// <summary>
        /// 正弦曲线Wave扭曲图片（Edit By 51aspx.com）
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            double PI2 = 6.283185307179586476925286766559;

            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }
    }
}
