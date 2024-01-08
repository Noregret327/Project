
using System;
using System.Text;

namespace ScaleClient1.Util
{
    /// <summary>
    /// 处理数据类型转换，数制转换、编码转换相关的类
    /// </summary>    
    public sealed class ConvertHelper
    {
        #region 补足位数
        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //连接text
            temp += text;

            //返回补足0的字符串
            return temp;
        }
        #endregion

        #region 各进制数间转换
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase32("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase32(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }

        public static string ConvertBase16(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }

        /// <summary>
        /// 64位补全版
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string ConvertBase64(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    string fix = "";
                    if (resultLength < 64)
                    {
                        for(int i = 0; i < 64 - resultLength; i++)
                        {
                            fix = fix + "0";
                        }
                    }
                    result = fix + result;
                    //switch (resultLength)
                    //{
                    //    case 7:
                    //        result = "0" + result;
                    //        break;
                    //    case 6:
                    //        result = "00" + result;
                    //        break;
                    //    case 5:
                    //        result = "000" + result;
                    //        break;
                    //    case 4:
                    //        result = "0000" + result;
                    //        break;
                    //    case 3:
                    //        result = "00000" + result;
                    //        break;
                    //}
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }

        public static float HexToFloat(string hexString)
        {
            uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);
            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);
            Console.WriteLine(" float convert = {0} ", f);
            return f;
        }
        public static double HexToDouble(string hexString)
        {
            Console.WriteLine("hexString = " + hexString);
            //string hexString = "43480170";
            Int64 num = Int64.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);
            byte[] doubleVals = BitConverter.GetBytes(num);
            double f = BitConverter.ToDouble(doubleVals, 0);
            return f;
        }
        

        public static int FromHex(String value)
        {
            int v = Convert.ToInt32(value, 16);

            unchecked
            {
                return (v <= 0x7FFFFF) ? v : v | (int)0xFF000000;
            }
        }

        public static string HexToASCII(string s)
        {
            Console.WriteLine("hex_s = " + s);
            byte[] buff = new byte[s.Length / 2];
            int index = 0;
            for (int i = 0; i < s.Length; i += 2)
            {
                buff[index] = Convert.ToByte(s.Substring(i, 2), 16);
                ++index;
            }
            string result = Encoding.Default.GetString(buff);
            Console.WriteLine("result_s = " + result);
            return result;
        }
        #endregion

        #region 使用指定字符集将string转换成byte[]
        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        #endregion

        #region 使用指定字符集将byte[]转换成string
        /// <summary>
        /// 使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion

        #region string类型转化为ushort数组
        public static ushort[] stringToUshort(String inString)

        {

            if (inString.Length % 2 == 1) { inString += " "; }

            char[] bufChar = inString.ToCharArray();

            byte[] outByte = new byte[bufChar.Length];

            byte[] bufByte = new byte[2];

            ushort[] outShort = new ushort[bufChar.Length / 2];

            for (int i = 0, j = 0; i < bufChar.Length; i += 2, j++)

            {

                bufByte[0] = BitConverter.GetBytes(bufChar[i])[0];

                bufByte[1] = BitConverter.GetBytes(bufChar[i + 1])[0];

                outShort[j] = BitConverter.ToUInt16(bufByte, 0);

            }

            return outShort;

        }
        #endregion
        #region ushort数组转化为string
        public static string ushortToString(ushort[] inUshort)

        {

            byte[] outByte = new byte[inUshort.Length * 2];



            for (int i = 0; i < inUshort.Length; i++)

            {
                byte[] bufByte = BitConverter.GetBytes(inUshort[i]);
                outByte[i * 2] = bufByte[0];

                outByte[i * 2 + 1] = bufByte[1];

            }
            string str = ASCIIEncoding.ASCII.GetString(outByte).Trim();
            return str;

        }
        #endregion



        #region 将byte[]转换成int
        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }
        #endregion

        #region ByteToHex 字节转十六进制
        /// <summary>
        /// 将字节数组转换成16进制字符串
        /// </summary>
        /// <param name="comByte">需要转换的字节数组</param>
        /// <returns>a hex string</returns>
        public static string ByteToHex(byte[] comByte)
        {
            //创建一个 StringBuilder 对象
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            //遍历字节数组中的字节
            foreach (byte data in comByte)
                //将字节数组中的字节转换从16进制并加入 stringbuilder
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            //返回字符串
            return builder.ToString().ToUpper();
        }
        #endregion

        #region toHex转换成Hex
        public static string toHex(string inp)
        {
            string outp = string.Empty;
            char[] value = inp.ToCharArray();

            foreach (char L in value)
            {
                int v = Convert.ToInt32(L);

                outp += string.Format("{0:X2}" + " ", v);

            }

            return outp;

        }
        #endregion

        #region ConvertHex Hex to Ascii
        public static string ConvertHex(string hexString) //>>>>>>>>>>>>>>>>>>new the textbox can only accept 2 characters
        {
            try
            {
                string ascii = string.Empty;

                // hexString = hexString.Replace("  ", ""); //this make sure no white space is trying to get converted//-------------------------------------18/5/17

                StringBuilder sb = new StringBuilder(hexString); //-------------------------------------18/5/17
                sb.Replace(" ", "");
                sb.Replace("  ", "");
                hexString = sb.ToString();

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    //uint decval = Convert.ToUInt32(hs, 16);
                    // char character = Convert.ToChar(decval); //-------------------------------------18/5/17

                    char character = Convert.ToChar(Convert.ToUInt32(hs, 16)); //-------------------------------------18/5/17
                    // string character = decval.ToString("x2");
                    ascii += character;
                    
                    // ascii += decval;

                }
                Console.WriteLine("ascii(WriteToPC) = " + ascii);
                return ascii;

            }
            catch (Exception ex)
            {

                //sendDataNoError = false;
                //Form1 f1 = new Form1();
                //f1.txRepeaterDelay.Stop();
                //f1.send_repeat_counter = 100;
                //f1.richTextBoxSend.Text = "";

                //MessageBox.Show("Enter a Valid Hexadecimal Value. Original error :  " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

            return string.Empty;
        }

        #endregion
    }
}
