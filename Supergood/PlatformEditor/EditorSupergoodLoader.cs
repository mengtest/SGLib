using UnityEngine;
using System.Collections;
namespace Supergood.Unity.Editor
{
	internal class EditorSupergoodLoader : SGCross.CompiledSupergoodLoader
	{
		protected override SupergoodGameObject SGGameObject {
			get {
				EditorSupergoodGameObject editorSG = ComponentFactory.GetComponent<EditorSupergoodGameObject> ();
				if (editorSG.Supergood == null) {
					editorSG.Supergood = new EditorSupergood ();
				}
				return editorSG;
			}
		}
	}
}
