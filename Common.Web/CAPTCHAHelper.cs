﻿using System.Drawing;
using System.Security.Cryptography;
using System.Threading;

namespace System.Web
{
    /// <summary>
    /// 验证码类
    /// </summary>
    public class Rand
    {
        #region 生成随机数字

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        public static string Number(int Length)
        {
            return Number(Length, false);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
            {
                Thread.Sleep(3);
            }

            var result = "";
            var random = new Random();
            for (var i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        #endregion 生成随机数字

        #region 生成随机字母与数字

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        public static string Str(int Length)
        {
            return Str(Length, false);
        }

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string Str(int Length, bool Sleep)
        {
            if (Sleep)
            {
                Thread.Sleep(3);
            }

            var Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var result = "";
            var n = Pattern.Length;
            var random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (var i = 0; i < Length; i++)
            {
                var rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }

        #endregion 生成随机字母与数字

        #region 生成随机纯字母随机数

        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        public static string Str_char(int Length)
        {
            return Str_char(Length, false);
        }

        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string Str_char(int Length, bool Sleep)
        {
            if (Sleep)
            {
                Thread.Sleep(3);
            }

            var Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var result = "";
            var n = Pattern.Length;
            var random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (var i = 0; i < Length; i++)
            {
                var rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }

        #endregion 生成随机纯字母随机数
    }

    /// <summary>
    /// 验证图片类
    /// </summary>
    public class CAPTCHAHelper
    {
        #region 私有字段

        private int letterCount = 4;   //验证码位数

        private int letterWidth = 16;  //单个字体的宽度范围

        private int letterHeight = 20; //单个字体的高度范围

        private static byte[] randb = new byte[4];

        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        private Font[] fonts =
            {
                new Font(new FontFamily("Times New Roman"),10 +Next(1),FontStyle.Regular),
                new Font(new FontFamily("Georgia"), 10 + Next(1),FontStyle.Regular),
                new Font(new FontFamily("Arial"), 10 + Next(1),FontStyle.Regular),
                new Font(new FontFamily("Comic Sans MS"), 10 + Next(1),FontStyle.Regular)
            };

        #endregion 私有字段

        #region 公有属性

        /// <summary>
        /// 验证码
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 验证码图片
        /// </summary>
        public Bitmap Image { get; private set; }

        #endregion 公有属性

        #region 构造函数

        public CAPTCHAHelper()
        {
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.CacheControl = "no-cache";
            this.Text = Rand.Number(4);
            this.CreateImage();
        }

        #endregion 构造函数

        #region 私有方法

        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="max">最大值</param>
        private static int Next(int max)
        {
            rand.GetBytes(randb);
            var value = BitConverter.ToInt32(randb, 0);
            value = value % (max + 1);
            if (value < 0)
            {
                value = -value;
            }

            return value;
        }

        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        private static int Next(int min, int max)
        {
            var value = Next(max - min) + min;
            return value;
        }

        #endregion 私有方法

        #region 公共方法

        /// <summary>
        /// 绘制验证码
        /// </summary>
        public void CreateImage()
        {
            var int_ImageWidth = this.Text.Length * this.letterWidth;
            var image = new Bitmap(int_ImageWidth, this.letterHeight);
            var g = Graphics.FromImage(image);
            g.Clear(Color.White);
            for (var i = 0; i < 2; i++)
            {
                var x1 = Next(image.Width - 1);
                var x2 = Next(image.Width - 1);
                var y1 = Next(image.Height - 1);
                var y2 = Next(image.Height - 1);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            int _x = -12, _y = 0;
            for (var int_index = 0; int_index < this.Text.Length; int_index++)
            {
                _x += Next(12, 16);
                _y = Next(-2, 2);
                var str_char = this.Text.Substring(int_index, 1);
                str_char = Next(1) == 1 ? str_char.ToLower() : str_char.ToUpper();
                var newBrush = new SolidBrush(this.GetRandomColor());
                var thePos = new Point(_x, _y);
                g.DrawString(str_char, this.fonts[Next(this.fonts.Length - 1)], newBrush, thePos);
            }
            for (var i = 0; i < 10; i++)
            {
                var x = Next(image.Width - 1);
                var y = Next(image.Height - 1);
                image.SetPixel(x, y, Color.FromArgb(Next(0, 255), Next(0, 255), Next(0, 255)));
            }
            image = this.TwistImage(image, true, Next(1, 3), Next(4, 6));
            g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, int_ImageWidth - 1, this.letterHeight - 1);
            this.Image = image;
        }

        /// <summary>
        /// 字体随机颜色
        /// </summary>
        public Color GetRandomColor()
        {
            var RandomNum_First = new Random((int)DateTime.Now.Ticks);
            Thread.Sleep(RandomNum_First.Next(50));
            var RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            var int_Red = RandomNum_First.Next(180);
            var int_Green = RandomNum_Sencond.Next(180);
            var int_Blue = (int_Red + int_Green > 300) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高,一般为3</param>
        /// <param name="dPhase">波形的起始相位,取值区间[0-2*PI)</param>
        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var PI = 6.283185307179586476925286766559;
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            var graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            var dBaseAxisLen = bXDir ? destBmp.Height : (double)destBmp.Width;
            for (var i = 0; i < destBmp.Width; i++)
            {
                for (var j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? PI * j / dBaseAxisLen : PI * i / dBaseAxisLen;
                    dx += dPhase;
                    var dy = Math.Sin(dx);
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    var color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            srcBmp.Dispose();
            return destBmp;
        }

        #endregion 公共方法
    }
}