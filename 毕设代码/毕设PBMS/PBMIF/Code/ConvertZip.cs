using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.GZip;
using System.Data;
using System.Xml;

namespace MCSFramework.WSI
{
    /// <summary> 
    /// ѹ����ʽ�� 
    /// </summary> 
    public enum ConvertType
    {
        /// <summary> 
        /// GZip ѹ����ʽ 
        /// </summary> 
        GZip,

        /// <summary> 
        /// BZip2 ѹ����ʽ 
        /// </summary> 
        BZip2,

        /// <summary> 
        /// Zip ѹ����ʽ 
        /// </summary> 
        Zip
    }

    /// <summary> 
    /// ʹ�� SharpZipLib ����ѹ���ĸ����࣬�򻯶��ֽ�������ַ�������ѹ���Ĳ����� 
    /// </summary> 
    public class ConvertZip
    {
        /// <summary> 
        /// ѹ����Ӧ�ߣ�Ĭ��Ϊ GZip�� 
        /// </summary> 
        public static ConvertType CompressionProvider = ConvertType.GZip;

        #region Public methods

        /// <summary> 
        /// ��ԭʼ�ֽ�����������ѹ�����ֽ����顣 
        /// </summary> 
        /// <param name="bytesToCompress">ԭʼ�ֽ����顣</param> 
        /// <returns>������ѹ�����ֽ�����</returns> 
        public static byte[] Compress(byte[] bytesToCompress)
        {
            MemoryStream ms = new MemoryStream();
            Stream s = OutputStream(ms);
            s.Write(bytesToCompress, 0, bytesToCompress.Length);
            s.Close();
            return ms.ToArray();
        }

        /// <summary> 
        /// ��ԭʼ�ַ���������ѹ�����ַ����� 
        /// </summary> 
        /// <param name="stringToCompress">ԭʼ�ַ�����</param> 
        /// <returns>������ѹ�����ַ�����</returns> 
        public static string Compress(string stringToCompress)
        {
            byte[] compressedData = CompressToByte(stringToCompress);
            string strOut = Convert.ToBase64String(compressedData);
            return strOut;
        }

        /// <summary> 
        /// ��ԭʼ�ַ���������ѹ�����ֽ����顣 
        /// </summary> 
        /// <param name="stringToCompress">ԭʼ�ַ�����</param> 
        /// <returns>������ѹ�����ֽ����顣</returns> 
        public static byte[] CompressToByte(string stringToCompress)
        {
            byte[] bytData = Encoding.Unicode.GetBytes(stringToCompress);
            return Compress(bytData);
        }

        /// <summary> 
        /// ����ѹ�����ַ�������ԭʼ�ַ����� 
        /// </summary> 
        /// <param name="stringToDecompress">��ѹ�����ַ�����</param> 
        /// <returns>����ԭʼ�ַ�����</returns> 
        public static string DeCompress(string stringToDecompress)
        {
            string outString = string.Empty;
            if (stringToDecompress == null)
            {
                throw new ArgumentNullException("stringToDecompress", "You tried to use an empty string");
            }

            try
            {
                byte[] inArr = Convert.FromBase64String(stringToDecompress.Trim());
                outString = Encoding.Unicode.GetString(DeCompress(inArr), 0, (DeCompress(inArr)).Length);
            }
            catch (NullReferenceException nEx)
            {
                return nEx.Message;
            }

            return outString;
        }

        /// <summary> 
        /// ����ѹ�����ֽ���������ԭʼ�ֽ����顣 
        /// </summary> 
        /// <param name="bytesToDecompress">��ѹ�����ֽ����顣</param> 
        /// <returns>����ԭʼ�ֽ����顣</returns> 
        public static byte[] DeCompress(byte[] bytesToDecompress)
        {
            byte[] writeData = new byte[4096];
            Stream s2 = InputStream(new MemoryStream(bytesToDecompress));
            MemoryStream outStream = new MemoryStream();

            while (true)
            {
                int size = s2.Read(writeData, 0, writeData.Length);
                if (size > 0)
                {
                    outStream.Write(writeData, 0, size);
                }
                else
                {
                    break;
                }
            }
            s2.Close();
            byte[] outArr = outStream.ToArray();
            outStream.Close();
            return outArr;
        }

        #endregion

        #region Private methods

        /// <summary> 
        /// �Ӹ�����������ѹ��������� 
        /// </summary> 
        /// <param name="inputStream">ԭʼ����</param> 
        /// <returns>����ѹ���������</returns> 
        private static Stream OutputStream(Stream inputStream)
        {
            switch (CompressionProvider)
            {
                case ConvertType.BZip2:
                    return new BZip2OutputStream(inputStream);

                case ConvertType.GZip:
                    return new GZipOutputStream(inputStream);

                case ConvertType.Zip:
                    return new ZipOutputStream(inputStream);

                default:
                    return new GZipOutputStream(inputStream);
            }
        }

        /// <summary> 
        /// �Ӹ�����������ѹ���������� 
        /// </summary> 
        /// <param name="inputStream">ԭʼ����</param> 
        /// <returns>����ѹ����������</returns> 
        private static Stream InputStream(Stream inputStream)
        {
            switch (CompressionProvider)
            {
                case ConvertType.BZip2:
                    return new BZip2InputStream(inputStream);

                case ConvertType.GZip:
                    return new GZipInputStream(inputStream);

                case ConvertType.Zip:
                    return new ZipInputStream(inputStream);

                default:
                    return new GZipInputStream(inputStream);
            }
        }

        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                //��streamװ�ص�XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        #endregion
    }
 }
