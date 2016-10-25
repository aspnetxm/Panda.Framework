/*******************************************************************************
 * 作者：星星    
 * 描述：验证码   
 * 修改记录： 
*********************************************************************************/

using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Panda.Code
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class AuthCode
    {
        #region 变量声明
        Color m_BackgroundColor, m_BorderColor, m_PointColor, m_LineColor, m_BrushColor1, m_BrushColor2;
        int m_iCodeLen, m_iPoint, m_iLine, m_iBorderWidth, m_Width, m_Height, m_Angle, m_OffsetH;
        string m_strValidate;
      
        #endregion

        public AuthCode()
        {
            m_BackgroundColor = Color.Transparent;
            m_BorderColor = Color.Silver;

            m_BrushColor1 = Color.DarkBlue;
            m_BrushColor2 = Color.Red;

            m_PointColor = Color.Silver;
            m_LineColor = Color.Silver;

            m_iBorderWidth = 0;
            m_iPoint = 120;                 //干扰点
            m_iLine = 5;                    //干扰线
            m_iCodeLen = 4;                 //验证码位数

            m_Width = 80;
            m_Height = 30;

            m_Angle = 30;

            m_OffsetH = 0;
        }

        #region  属性

        public int Width
        { set { m_Width = value; } }

        public int Height
        { set { m_Height = value; } }

        public int CodeLen
        { set { this.m_iCodeLen = value; } }

        public string Code
        { get { return this.m_strValidate; } }

        public int FontSize { get; set; } = 14;

        public int Point
        { set { m_iPoint = value; } }

        public int Line
        { set { m_iLine = value; } }

        public int Angle
        { set { m_Angle = value; } }

        public int OffsetH
        { set { m_OffsetH = value; } }


        public int BorderWidth
        { set { m_iBorderWidth = value; } }

        public string BackgroundColor
        {
            set
            {
                if (value.Substring(0, 1) == "#")
                    m_BackgroundColor = Color.FromArgb(Convert.ToInt32(value.Substring(1, 2), 16), Convert.ToInt32(value.Substring(3, 2), 16), Convert.ToInt32(value.Substring(5, 2), 16));
                else
                    m_BackgroundColor = Color.FromName(value);
            }
        }

        public string BorderColor
        {
            set
            {
                if (value.Substring(0, 1) == "#")
                    m_BorderColor = Color.FromArgb(Convert.ToInt32(value.Substring(1, 2), 16), Convert.ToInt32(value.Substring(3, 2), 16), Convert.ToInt32(value.Substring(5, 2), 16));
                else
                    m_BorderColor = Color.FromName(value);
            }
        }

        public string BrushColor1
        {
            set
            {
                if (value.Substring(0, 1) == "#")
                    m_BrushColor1 = Color.FromArgb(Convert.ToInt32(value.Substring(1, 2), 16), Convert.ToInt32(value.Substring(3, 2), 16), Convert.ToInt32(value.Substring(5, 2), 16));
                else
                    m_BrushColor1 = Color.FromName(value);
            }
        }

        public string BrushColor2
        {
            set
            {
                if (value.Substring(0, 1) == "#")
                    m_BrushColor2 = Color.FromArgb(Convert.ToInt32(value.Substring(1, 2), 16), Convert.ToInt32(value.Substring(3, 2), 16), Convert.ToInt32(value.Substring(5, 2), 16));
                else
                    m_BrushColor2 = Color.FromName(value);
            }
        }

        public string PointColor
        {
            set
            {
                if (value.Substring(0, 1) == "#")
                    m_PointColor = Color.FromArgb(Convert.ToInt32(value.Substring(1, 2), 16), Convert.ToInt32(value.Substring(3, 2), 16), Convert.ToInt32(value.Substring(5, 2), 16));
                else
                    m_PointColor = Color.FromName(value);
            }
        }

        public string LineColor
        {
            set
            {
                if (value.Substring(0, 1) == "#")
                    m_LineColor = Color.FromArgb(Convert.ToInt32(value.Substring(1, 2), 16), Convert.ToInt32(value.Substring(3, 2), 16), Convert.ToInt32(value.Substring(5, 2), 16));
                else
                    m_LineColor = Color.FromName(value);
            }
        }

        #endregion

        /// <summary>
        /// 获取字母数字验证码字符串
        /// </summary>
        /// <param name="nLength">验证码字符个数</param>
        /// <returns>验证码字符串</returns>
        private string GetStrAscii(int nLength)
        {
            int nStrLength = nLength;
            string strString = "123456789QAZWSXEDCRFVTGBYHNUJMIKOLPqazwsxedcrfvtgbyhnujmikolp";
            int rl = strString.Length;
            StringBuilder strtemp = new StringBuilder();
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < nStrLength; i++)
            {
                random = new Random(unchecked(random.Next() * 1000));
                strtemp.Append(strString[random.Next(rl)]);
            }
            m_strValidate = strtemp.ToString();
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取中文验证码字符串
        /// </summary>
        /// <param name="nLength">验证码字符中文的个数</param>
        /// <returns>验证码字符串(中文)</returns>
        private string GetStrChinese(int nLength)
        {
            int nStrLength = nLength;
            object[] btStrings = new object[nStrLength];
            string[] strString = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            Random random = new Random();
            for (int i = 0; i < nStrLength; i++)
            {
                //区位码第1位   
                int nNext1 = random.Next(11, 14);
                string strChar1 = strString[nNext1];

                //区位码第2位   
                random = new Random(nNext1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的   

                int nNext2;
                if (nNext1 == 13)
                {
                    nNext2 = random.Next(0, 7);
                }
                else
                {
                    nNext2 = random.Next(0, 16);
                }
                string strChar2 = strString[nNext2];

                //区位码第3位   
                random = new Random(nNext2 * unchecked((int)DateTime.Now.Ticks) + i);
                int nNext3 = random.Next(10, 16);
                string strChar3 = strString[nNext3];

                //区位码第4位   
                random = new Random(nNext3 * unchecked((int)DateTime.Now.Ticks) + i);
                int nNext4;
                if (nNext3 == 10)
                {
                    nNext4 = random.Next(1, 16);
                }
                else if (nNext3 == 15)
                {
                    nNext4 = random.Next(0, 15);
                }
                else
                {
                    nNext4 = random.Next(0, 16);
                }
                string strChar4 = strString[nNext4];

                byte bt1 = Convert.ToByte(strChar1 + strChar2, 16);
                byte bt2 = Convert.ToByte(strChar3 + strChar4, 16);
                byte[] btString = new byte[2] { bt1, bt2 };

                btStrings.SetValue(btString, i);
            }

            //转化成汉字
            StringBuilder strReturn = new StringBuilder();
            Encoding edGb2312 = Encoding.GetEncoding("gb2312");
            for (int i = 0; i < nStrLength; i++)
            {
                strReturn.Append(edGb2312.GetString((byte[])Convert.ChangeType(btStrings[i], typeof(byte[]))));
            }
            m_strValidate = strReturn.ToString();
            return strReturn.ToString();
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="p_iWidth"></param>
        /// <param name="p_iHeight"></param>
        /// <returns></returns>
        public MemoryStream DrawCode()
        {
            using (Bitmap bitmap = new Bitmap(m_Width, m_Height))
            {
                MemoryStream stream = null;
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                graphics.Clear(m_BackgroundColor);

                #region 干扰
                Random random = new Random();
                for (int i = 0; i < m_iPoint; i++)
                {
                    int x = random.Next(m_Width), y = random.Next(m_Height);
                    bitmap.SetPixel(x, y, m_PointColor);
                }
                for (int i = 0; i < m_iLine; i++)
                {
                    int x1 = random.Next(2, m_Width), y1 = random.Next(2, m_Height);
                    int x2 = random.Next(2, m_Width), y2 = random.Next(2, m_Height);
                    graphics.DrawLine(new Pen(m_LineColor, 1.5f), x1, y1, x2, y2);
                }
                #endregion

                string strAscii = GetStrAscii(m_iCodeLen);

                RectangleF rfRect = new RectangleF(0f, 0f, m_Width, m_Height);
                LinearGradientBrush lgBrush = new LinearGradientBrush(rfRect, m_BrushColor1, m_BrushColor2, 45f);

                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;
                graphics.DrawString(strAscii, new Font("Times New Roman", FontSize, FontStyle.Regular), lgBrush, rfRect, drawFormat);

                if (m_iBorderWidth > 0)
                    graphics.DrawRectangle(new Pen(m_BorderColor, m_iBorderWidth), 0, 0, m_Width - 1, m_Height - 1);

                //保存图片数据
                stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);

                return stream;
            }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="p_iLen">位数</param>
        /// <param name="p_iStartX">X轴起始位置</param>
        /// <param name="p_iStratY">Y轴起始位置</param>
        /// <param name="p_iSpace">间距宽度</param>
        /// <param name="p_iBorderWidth"></param>
        /// <returns></returns>
        public MemoryStream DrawCode(int p_iStartX, int p_iFontWidth)
        {
            using (Bitmap bitmap = new Bitmap(m_Width, m_Height))
            {
                MemoryStream stream = null;
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                graphics.Clear(m_BackgroundColor);

                #region 干扰
                Random random = new Random();
                for (int i = 0; i < m_iPoint; i++)
                {
                    int x = random.Next(m_Width - 2), y = random.Next(m_Height);
                    bitmap.SetPixel(x, y, m_PointColor);
                }

                for (int i = 0; i < m_iLine; i++)
                {
                    int x1 = random.Next(2, m_Width - 2), y1 = random.Next(2, m_Height - 2);
                    int x2 = random.Next(2, m_Width - 2), y2 = random.Next(2, m_Height - 2);
                    int x3 = random.Next(2, m_Width - 2), y3 = random.Next(2, m_Height - 2);
                    //graphics.DrawLine(new Pen(m_LineColor, 1.5f), x1, y1, x2, y2);

                    graphics.DrawCurve(new Pen(m_LineColor, 1.5f), new Point[] { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) }, 0.9f);

                }
                #endregion

                string[] strColor = new String[8] { "Black", "DarkRed", "SeaGreen", "SteelBlue", "DarkOrchid", "Crimson", "Gray", "SkyBlue" };
                string strAscii = GetStrAscii(m_iCodeLen);
                int iStartX, iStartY;

                StringFormat sFormat = new StringFormat();
                sFormat.Alignment = StringAlignment.Center;
                sFormat.LineAlignment = StringAlignment.Center;

                var font = new Font("Times New Roman", FontSize, FontStyle.Regular);
                for (int i = 0; i < m_iCodeLen; i++)
                {
                    using (Bitmap bmp = new Bitmap(p_iFontWidth, m_Height))
                    {
                        Graphics grh = Graphics.FromImage(bmp);
                        grh.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                        grh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        grh.Clear(Color.Empty);
                        SolidBrush solidBrush = new SolidBrush(Color.FromName(strColor[random.Next(0, 7)]));

                        RectangleF rfRect = new RectangleF(0f, 0f, p_iFontWidth, m_Height);
                        grh.DrawString(strAscii[i].ToString(), font, solidBrush, rfRect, sFormat);

                        iStartX = p_iStartX + p_iFontWidth * i;
                        iStartY = random.Next(-m_OffsetH, m_OffsetH);
                        graphics.DrawImage(Rotate(bmp, random.Next(-m_Angle, m_Angle)), iStartX, iStartY);
                    }
                }
                if (m_iBorderWidth > 0)
                    graphics.DrawRectangle(new Pen(m_BorderColor, m_iBorderWidth), 0, 0, m_Width - 1, m_Height - 1);

                //保存图片数据
                stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);

                return stream;
            }
        }

        #region 图片旋转函数

        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        private Bitmap Rotate(Bitmap b, int angle)
        {
            //angle = angle % 360;

            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);

            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(angle);

            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);

            //重至绘图的所有变换
            g.ResetTransform();

            g.Save();
            g.Dispose();
            return dsImage;
        }

        #endregion 图片旋转函数
    }
}