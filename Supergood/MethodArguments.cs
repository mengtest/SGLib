namespace Supergood.Unity
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	
	internal class MethodArguments
	{
		private IDictionary<string, object> arguments = new Dictionary<string, object>();
		
		public MethodArguments() : this(new Dictionary<string, object>())
		{
		}
		
		public MethodArguments(MethodArguments methodArgs) : this(methodArgs.arguments)
		{
		}
		
		private MethodArguments(IDictionary<string, object> arguments)
		{
			this.arguments = arguments;
		}
		
		public void AddPrimative<T>(string argumentName, T value) where T : struct
		{
			this.arguments[argumentName] = value;
		}
		
		public void AddNullablePrimitive<T>(string argumentName, T? nullable) where T : struct
		{
			if (nullable != null && nullable.HasValue)
			{
				this.arguments[argumentName] = nullable.Value;
			}
		}
		
		public void AddString(string argumentName, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.arguments[argumentName] = value;
			}
		}
		
		public void AddCommaSeparatedList(string argumentName, IEnumerable<string> value)
		{
			if (value != null)
			{
				this.arguments[argumentName] = value.ToCommaSeparateList();
			}
		}
		
		public void AddDictionary(string argumentName, IDictionary<string, object> dict)
		{
			if (dict != null)
			{
				this.arguments[argumentName] = MethodArguments.ToStringDict(dict);
			}
		}
		
		public void AddList<T>(string argumentName, IEnumerable<T> list)
		{
			if (list != null)
			{
				this.arguments[argumentName] = list;
			}
		}
		
		public void AddUri(string argumentName, Uri uri)
		{
			if (uri != null && !string.IsNullOrEmpty(uri.AbsoluteUri))
			{
				this.arguments[argumentName] = uri.ToString();
			}
		}
		
		public string ToJsonString()
		{
			return MiniJSON.Json.Serialize(this.arguments);
		}
		
		private static Dictionary<string, string> ToStringDict(IDictionary<string, object> dict)
		{
			if (dict == null)
			{
				return null;
			}
			
			var newDict = new Dictionary<string, string>();
			foreach (KeyValuePair<string, object> kvp in dict)
			{
				newDict[kvp.Key] = kvp.Value.ToString();
			}
			
			return newDict;
		}
	}
}

