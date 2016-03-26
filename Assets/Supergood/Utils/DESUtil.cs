using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Supergood.Unity
{
	public class DESUtil
	{
		#region ========加密========
		/// <summary> 
		/// 加密数据 
		/// </summary> 
		/// <param name="Text">要加密的内容</param> 
		/// <param name="sKey">key，必须为32位</param> 
		/// <returns></returns> 
		public static string Encrypt (string Text, string sKey)
		{
			byte[] resultArray = Encrypt (UTF8Encoding.UTF8.GetBytes (Text), sKey);
			return Convert.ToBase64String (resultArray, 0, resultArray.Length);
		}

		/// <summary> 
		/// 加密数据 
		/// </summary> 
		/// <param name="Text">要加密的内容</param> 
		/// <param name="sKey">key，必须为32位</param> 
		/// <returns></returns> 
		public static byte[]  Encrypt (byte[] _EncryptArray, string sKey)
		{
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes (sKey);
			
			RijndaelManaged encryption = new RijndaelManaged ();
			
			encryption.Key = keyArray;
			
			encryption.Mode = CipherMode.ECB;
			
			encryption.Padding = PaddingMode.PKCS7;
			
			ICryptoTransform cTransform = encryption.CreateEncryptor ();

			return cTransform.TransformFinalBlock (_EncryptArray, 0, _EncryptArray.Length);
			
		}
		
		#endregion
		
		#region ========解密========
		/// <summary> 
		/// 解密数据 
		/// </summary> 
		/// <param name="Text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public static string Decrypt (string Text, string sKey)
		{
			byte[] resultArray = Decrypt (Convert.FromBase64String (Text), sKey);
			return UTF8Encoding.UTF8.GetString (resultArray);
		}

		/// <summary> 
		/// 解密数据 
		/// </summary> 
		/// <param name="Text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public static byte[] Decrypt (byte[] _EncryptArray, string sKey)
		{
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes (sKey);
			
			RijndaelManaged decipher = new RijndaelManaged ();
			
			decipher.Key = keyArray;
			
			decipher.Mode = CipherMode.ECB;
			
			decipher.Padding = PaddingMode.PKCS7;
			
			ICryptoTransform cTransform = decipher.CreateDecryptor ();

			
			return cTransform.TransformFinalBlock (_EncryptArray, 0, _EncryptArray.Length);
			
		}
		
		#endregion


		public static string getFileHash (string filePath)
		{           
			try {
				FileStream fs = new FileStream (filePath, FileMode.Open);
				int len = (int)fs.Length;
				byte[] data = new byte[len];
				fs.Read (data, 0, len);
				fs.Close ();
				MD5 md5 = new MD5CryptoServiceProvider ();
				byte[] result = md5.ComputeHash (data);
				string fileMD5 = "";
				foreach (byte b in result) {
					fileMD5 += Convert.ToString (b, 16);
				}
				return fileMD5;   
			} catch (FileNotFoundException e) {
				Console.WriteLine (e.Message);
				return "";
			}                                 
		}

		/// <summary>
		/// MD5加密
		/// </summary>
		/// <param name="sDataIn"></param>
		/// <returns></returns>
		public static string GetMD5 (string sDataIn)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider ();
			byte[] bytValue, bytHash;
			bytValue = System.Text.Encoding.UTF8.GetBytes (sDataIn + "md");
			bytHash = md5.ComputeHash (bytValue);
			md5.Clear ();
			string sTemp = "";
			for (int i = 0; i < bytHash.Length; i++) {
				sTemp += bytHash [i].ToString ("X").PadLeft (2, '0');
			}
			return sTemp.ToLower ();
		}

	}
}
