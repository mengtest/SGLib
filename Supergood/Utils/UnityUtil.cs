using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Supergood.Unity
{
	public class UnityUtil
	{
		public static Sprite CreateSprite (Texture2D texture2d)
		{
			if (texture2d == null)
				return null;
			return Sprite.Create (texture2d, new Rect (0, 0, texture2d.width, texture2d.height), new Vector2 (0.5f, 0.5f));
		}
	}
}
