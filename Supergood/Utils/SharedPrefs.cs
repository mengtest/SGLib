using UnityEngine;
using System.Collections;
using LitJson;

namespace Supergood.Unity
{
	public class SharedPrefs
	{
		private static readonly string sKey = "%ry0d6#09*lqya,|/p3@1pl)1!qp;.4q";

		public static int GetInt (string key)
		{
			return GetInt (key, 0);
		}

		public static int GetInt (string key, int defaultValue)
		{
			return PlayerPrefs.GetInt (key, defaultValue);
		}

		public static float GetFloat (string key)
		{
			return GetFloat (key, 0f);
		}

		public static float GetFloat (string key, float defaultValue)
		{
			return PlayerPrefs.GetFloat (key, defaultValue);
		}

		public static string GetString (string key)
		{
			string empty = string.Empty;
			return GetString (key, string.Empty);
		}

		public static string GetString (string key, string defaultValue)
		{
			return PlayerPrefs.GetString (key, defaultValue);
		}

		public static string GetStringDecrypt (string key)
		{
			string empty = string.Empty;
			return GetStringDecrypt (key, string.Empty);
		}
		
		public static string GetStringDecrypt (string key, string defaultValue)
		{
			string value = null;

			if (defaultValue == null) {
				value = PlayerPrefs.GetString (DESUtil.Encrypt (key, sKey), defaultValue);
			} else {
				value = PlayerPrefs.GetString (DESUtil.Encrypt (key, sKey), DESUtil.Encrypt (defaultValue, sKey));
			}


			if (value == null)
				return value;

			return DESUtil.Decrypt (value, sKey);
		}

		public static long GetLong (string key)
		{
			return GetLong (key, 0L);
		}

		public static long GetLong (string key, long defaultValue)
		{
			string strValue = PlayerPrefs.GetString (key, null);
			if (strValue == null) {
				return defaultValue;
			}
			long result = defaultValue;
			if (long.TryParse (strValue, out result)) {
				return result;
			} else {
				return defaultValue;
			}
		}

		public static void SetFloat (string key, float value)
		{
			PlayerPrefs.SetFloat (key, value);
		}

		public static void SaveFloat (string key, float value)
		{
			PlayerPrefs.SetFloat (key, value);
			Save ();
		}

		public static void SetInt (string key, int value)
		{
			PlayerPrefs.SetInt (key, value);
		}

		public static void SaveInt (string key, int value)
		{
			PlayerPrefs.SetInt (key, value);
			Save ();
		}

		public static void SetString (string key, string value)
		{
			PlayerPrefs.SetString (key, value);
		}

		public static void SaveString (string key, string value)
		{
			PlayerPrefs.SetString (key, value);
			Save ();
		}

		public static void SetStringEncrypt (string key, string value)
		{
			if (value == null)
				PlayerPrefs.SetString (DESUtil.Encrypt (key, sKey), value);
			else
				PlayerPrefs.SetString (DESUtil.Encrypt (key, sKey), DESUtil.Encrypt (value, sKey));
		}
		
		public static void SaveStringEncrypt (string key, string value)
		{
			SetStringEncrypt (key, value);
			Save ();
		}

		public static void SetLong (string key, long value)
		{
			PlayerPrefs.SetString (key, value.ToString ());
		}

		public static void SaveLong (string key, long value)
		{
			PlayerPrefs.SetString (key, value.ToString ());
			Save ();
		}

		public static  void DeleteAll ()
		{
			PlayerPrefs.DeleteAll ();
		}

		public static void DeleteKey (string key)
		{
			PlayerPrefs.DeleteKey (key);
		}

		public static void DeleteKeyEncrypt (string key)
		{
			PlayerPrefs.DeleteKey (DESUtil.Encrypt (key, sKey));
		}

		public static void Save ()
		{
			PlayerPrefs.Save ();
		}

		public static  bool HasKey (string key)
		{
			return PlayerPrefs.HasKey (key);
		}

		public static  bool HasKeyEncrypt (string key)
		{
			return PlayerPrefs.HasKey (DESUtil.Encrypt (key, sKey));
		}

		public static void Save<T> (string prefsKey,T t)
		{
			SaveString(prefsKey,JsonMapper.ToJson (t));
		}

		public static void Set<T> (string prefsKey,T t)
		{
			SetString(prefsKey,JsonMapper.ToJson (t));
		}
		
		public static T Load<T> (string prefsKey) where T:new()
		{
			string sDStr = GetString (prefsKey);
			if (sDStr != null && sDStr != "")
				return JsonMapper.ToObject<T> (sDStr);
			return new T ();
		}

		public static void SaveEncrypt<T> (string prefsKey,T t)
		{
			SetStringEncrypt(prefsKey,JsonMapper.ToJson (t));
		}
		
		public static T LoadDecrypt<T> (string prefsKey) where T:new()
		{
			string sDStr = GetStringDecrypt(prefsKey);
			if (sDStr != null && sDStr != "")
				return JsonMapper.ToObject<T> (sDStr);
			return new T ();
		}
	}
}
