using UnityEngine;
using System.Collections;
using System;
using System.IO;

namespace Supergood.Unity
{  
	public class FileUtil
	{


		public static string sKey = "wrysd6#09*lqya,|/p3@1W1)1!qp;.4q";
		
		public static bool Write (string path, string name, byte[] data)
		{
		
			return Write (path + "//" + name, data);
		}

		public static bool WriteEncrypt (string filePath, byte[] data)
		{
			return Write (filePath, DESUtil.Encrypt (data, sKey));
		}

		public static bool Write (string filePath, byte[] data)
		{
			try {
				FileStream fs = new FileStream (filePath, FileMode.Create);
				//获得字节数组
				//开始写入
				fs.Write (data, 0, data.Length);
				//清空缓冲区、关闭流
				fs.Flush ();
				fs.Close ();
				return true;
			} catch (Exception e) {
			}
			return false;
		}
		
		/**
   * path：删除文件的路径
   * name：删除文件的名称
   */
		
		public static void DeleteFile (string path, string name)
		{
			DeleteFile (path + "//" + name);
			
		}

		public static void DeleteFile (string filePath)
		{
			File.Delete (filePath);
			
		}

		public static bool Exists (string path, string name)
		{
		  
			return Exists (path + "//" + name);

		}

		public static bool Exists (string filePath)
		{
			try {
				FileInfo t = new FileInfo (filePath);
				return t.Exists;
			} catch (Exception e) {
			}
			return false;
			
		}

		public static byte[] Read (string filePath)
		{
			if (File.Exists (filePath)) {
				return File.ReadAllBytes (filePath);
			}
			return null;
		}

		public static Texture2D LoadPNG (string filePath)
		{
			Texture2D tex = null;
			byte[] fileData;
			
			if (File.Exists (filePath)) {
				fileData = File.ReadAllBytes (filePath);
				tex = new Texture2D (2, 2);
				
				// automatically resize the texture by its dimensions.
				tex.LoadImage (fileData); 
			}
			return tex;
		}

	}
}
