using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using Supergood.Unity;

public class DesTool : MonoBehaviour {
	public Button doneButton;
	public InputField inputPath;
	public InputField outputPath;

	public void Done(){
		Debug.Log ("bbbbbbbbb bbbb11111111");
		string path = inputPath.textComponent.text.Trim ();
		DirectoryInfo folder = new DirectoryInfo(path);
		Debug.Log ("bbbbbbbbb bbbb");

		string outPath = outputPath.text.Trim ();
		if (!Directory.Exists (outPath)) {
			Directory.CreateDirectory(outPath);
		}
		
		foreach (FileInfo file in folder.GetFiles())
		{
			if(!(file.FullName.EndsWith(".DS_Store") || file.FullName.EndsWith(".meta"))){
				Debug.Log(file.Name);
				byte[] bytes = FileUtil.Read(file.FullName);
				FileUtil.WriteEncrypt(outPath + "//" + file.Name+".bytes",bytes);
//
//				byte[] bytes2 = FileUtil.Read(outPath + "//" + file.Name+".txt");
//
//				DESUtil.Decrypt(bytes2,FileUtil.sKey);

//				FileUtil.Write(outPath + "//" + file.Name+".txt",bytes);
			}
				
		}
	}
  
}
