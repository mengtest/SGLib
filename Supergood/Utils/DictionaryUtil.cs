using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Supergood.Unity
{
	public class DictionaryUtil
	{
		public static object  GetObject (Dictionary<string,object> dictionary, params string[] paths)
		{
			int lenth = paths.Length;
			Dictionary<string,object> lastLevel = dictionary;
			for (int i=0; i<lenth; i++) {

				if (lastLevel == null) {
					return null;
				}

				if (lenth - 1 == i) {
					return Utilities.GetValueOrDefault<object> (lastLevel, paths [i], false);
				} else {

					lastLevel = Utilities.GetValueOrDefault<Dictionary<string,object>> (lastLevel, paths [i], false);
				}

			}
			return null;
		}

		public static Dictionary<string,object> GetDictionaryValue (Dictionary<string,object> dictionary, params string[] paths)
		{
			if(dictionary == null) return null;
			object temp = GetObject (dictionary, paths);
			if (temp != null) {
				if (temp is Dictionary<string,object>) {
					return  temp as Dictionary<string,object>;
				}
			}
			return null;
		}

		public static List<object> GetListValue (Dictionary<string,object> dictionary, params string[] paths)
		{
			if(dictionary == null) return null;
			object temp = GetObject (dictionary, paths);
			if (temp != null) {
				if (temp is List<object>) {
					return  temp as List<object>;
				}
			}
			return null;
		}

		public static string GetStringValue (Dictionary<string,object> dictionary, string defaultValue, params string[] paths)
		{
			if(dictionary == null) return defaultValue;
			object temp = GetObject (dictionary, paths);
			if (temp != null) {
				if (temp is string) {
					return (string)temp;
				}
			}
			return defaultValue;
		}

		public static bool GetBoolValue (Dictionary<string,object> dictionary, bool defaultValue, params string[] paths)
		{
			if(dictionary == null) return defaultValue;
			object temp = GetObject (dictionary, paths);
			if (temp != null) {
				if (temp is bool) {
					return (bool)temp;
				}
			}
			return defaultValue;
		}

		public static int GetIntValue (Dictionary<string,object> dictionary, int defaultValue, params string[] paths)
		{
			if(dictionary == null) return defaultValue;
			object temp = GetObject (dictionary, paths);
			return Utilities.CastValueInt (temp, defaultValue);
		}

		public static float GetLongValue (Dictionary<string,object> dictionary, float defaultValue, params string[] paths)
		{
			if(dictionary == null) return defaultValue;
			object temp = GetObject (dictionary, paths);
			return Utilities.CastValueFloat (temp, defaultValue);
		}

		public static long GetLongValue (Dictionary<string,object> dictionary, long defaultValue, params string[] paths)
		{
			if(dictionary == null) return defaultValue;
			object temp = GetObject (dictionary, paths);
			return Utilities.CastValueLong (temp, defaultValue);
		}

		public static double GetLongValue (Dictionary<string,object> dictionary, double defaultValue, params string[] paths)
		{
			if(dictionary == null) return defaultValue;
			object temp = GetObject (dictionary, paths);
			return Utilities.CastValueDouble (temp, defaultValue);
		}

		public static  Dictionary<string, object> MergeDictionary (Dictionary<string, object> first, Dictionary<string, object> second)
		{
			if (first == null)
				first = new Dictionary<string, object> ();
			if (second == null)
				return first;
			
			foreach (var item in second) {
				if (!first.ContainsKey (item.Key)) {
					first.Add (item.Key, item.Value);
				} else {
					if ((item.Value) is Dictionary<string, object> && first [item.Key] is Dictionary<string, object>) {
						MergeDictionary (first [item.Key] as Dictionary<string, object>, item.Value as Dictionary<string, object>);
					} else {
						first [item.Key] = item.Value;
					}
				}
			}
			
			return first;
		}
	}
}
