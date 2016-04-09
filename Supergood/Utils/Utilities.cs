namespace Supergood.Unity
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	
	internal static class Utilities
	{
		private const string WarningMissingParameter = "Did not find expected value '{0}' in dictionary";
		
		public static bool TryGetValue<T>(
			this IDictionary<string, object> dictionary,
			string key,
			out T value)
		{
			object resultObj;
			if (dictionary.TryGetValue(key, out resultObj) && resultObj is T)
			{
				value = (T)resultObj;
				return true;
			}
			
			value = default(T);
			return false;
		}
		
		public static long TotalSeconds(this DateTime dateTime)
		{
			TimeSpan t = dateTime - new DateTime(1970, 1, 1);
			long secondsSinceEpoch = (long)t.TotalSeconds;
			return secondsSinceEpoch;
		}
		
		public static T GetValueOrDefault<T>(
			this IDictionary<string, object> dictionary,
			string key,
			bool logWarning = true)
		{
			T result;
			if (!dictionary.TryGetValue<T>(key, out result))
			{
//				FacebookLogger.Warn(WarningMissingParameter, key);
			}
			
			return result;
		}
		
		public static string ToCommaSeparateList(this IEnumerable<string> list)
		{
			if (list == null)
			{
				return string.Empty;
			}
			
			return string.Join(",", list.ToArray());
		}
		
		public static string AbsoluteUrlOrEmptyString(this Uri uri)
		{
			if (uri == null)
			{
				return string.Empty;
			}
			
			return uri.AbsoluteUri;
		}


		public static  int CastValueInt(object o,int defalut = 0){
			if (o is int) {
				return ((int) o);
			} else if(o is long) {
				return  (int)((long) o);
			}else if(o is double){
				return  (int)((double) o);
			}else if (o is float) {
				return  (int)((float)o);
			}
			return defalut;

		}

		public static  float CastValueFloat(object o,float defalut = 0){
			if (o is int) {
				return (float)((int)o);
			} else if (o is long) {
				return (float) ((long)o);
			} else if (o is double) {
				return  (float)((double)o);
			} else if (o is float) {
				return  ((float)o);
			}
			return defalut;
			
		}

		public static  long CastValueLong(object o,long defalut = 0){
			if (o is int) {
				return (long)((int)o);
			} else if (o is long) {
				return  ((long)o);
			} else if (o is double) {
				return  (long)((double)o);
			} else if (o is float) {
				return  (long)((float)o);
			}
			return defalut;
			
		}

		public static  double CastValueDouble(object o,double defalut = 0){
			if (o is int) {
				return (double)((int) o);
			} else if(o is long) {
				return  (double)((long) o);
			}else if(o is double){
				return  ((double) o);
			}else if (o is float) {
				return  (double)((float)o);
			}
			return defalut;
			
		}
	}
}

