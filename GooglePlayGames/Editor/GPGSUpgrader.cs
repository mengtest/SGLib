// <copyright file="GPGSUpgrader.cs" company="Google Inc.">
// Copyright (C) 2014 Google Inc.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>

namespace GooglePlayGames
{
    using UnityEngine;
    using UnityEditor;
    using System.IO;

    [InitializeOnLoad]
    public class GPGSUpgrader
    {
        static GPGSUpgrader()
        {
            string prevVer = GPGSProjectSettings.Instance.Get(GPGSUtil.LASTUPGRADEKEY, "00000");
            if (prevVer != PluginVersion.VersionKey)
            {
                // if this is a really old version, upgrade to 911 first, then 915
                if (prevVer != PluginVersion.VersionKeyCPP)
                {
                    prevVer = Upgrade911(prevVer);
                }

                prevVer = Upgrade915(prevVer);

                // there is no migration needed to 920+
                Debug.Log("Upgrading from format version " + prevVer + " to " + PluginVersion.VersionKey);
                prevVer = PluginVersion.VersionKey;
                string msg = GPGSStrings.PostInstall.Text.Replace("$VERSION",
                                 PluginVersion.VersionString);
                EditorUtility.DisplayDialog(GPGSStrings.PostInstall.Title, msg, "OK");

            }

            GPGSProjectSettings.Instance.Set(GPGSUtil.LASTUPGRADEKEY, prevVer);
            GPGSProjectSettings.Instance.Save();
            AssetDatabase.Refresh();
        }

        private static string Upgrade915(string prevVer)
        {
            Debug.Log("Upgrading from format version " + prevVer + " to " + PluginVersion.VersionKeyU5);

            // all that was done was moving the Editor files to be in GooglePlayGames/Editor
            string[] obsoleteFiles =
                {
				"Assets/SGLib/Editor/GPGSAndroidSetupUI.cs",
				"Assets/SGLib/Editor/GPGSAndroidSetupUI.cs.meta",
				"Assets/SGLib/Editor/GPGSDocsUI.cs",
				"Assets/SGLib/Editor/GPGSDocsUI.cs.meta",
				"Assets/SGLib/Editor/GPGSIOSSetupUI.cs",
				"Assets/SGLib/Editor/GPGSIOSSetupUI.cs.meta",
				"Assets/SGLib/Editor/GPGSInstructionWindow.cs",
				"Assets/SGLib/Editor/GPGSInstructionWindow.cs.meta",
				"Assets/SGLib/Editor/GPGSPostBuild.cs",
				"Assets/SGLib/Editor/GPGSPostBuild.cs.meta",
				"Assets/SGLib/Editor/GPGSProjectSettings.cs",
				"Assets/SGLib/Editor/GPGSProjectSettings.cs.meta",
				"Assets/SGLib/Editor/GPGSStrings.cs",
				"Assets/SGLib/Editor/GPGSStrings.cs.meta",
				"Assets/SGLib/Editor/GPGSUpgrader.cs",
				"Assets/SGLib/Editor/GPGSUpgrader.cs.meta",
				"Assets/SGLib/Editor/GPGSUtil.cs",
				"Assets/SGLib/Editor/GPGSUtil.cs.meta",
				"Assets/SGLib/Editor/GameInfo.template",
				"Assets/SGLib/Editor/GameInfo.template.meta",
				"Assets/SGLib/Editor/PlistBuddyHelper.cs",
				"Assets/SGLib/Editor/PlistBuddyHelper.cs.meta",
				"Assets/SGLib/Editor/PostprocessBuildPlayer",
				"Assets/SGLib/Editor/PostprocessBuildPlayer.meta",
				"Assets/SGLib/Editor/ios_instructions",
				"Assets/SGLib/Editor/ios_instructions.meta",
				"Assets/SGLib/Editor/projsettings.txt",
				"Assets/SGLib/Editor/projsettings.txt.meta",
				"Assets/SGLib/Editor/template-AndroidManifest.txt",
				"Assets/SGLib/Editor/template-AndroidManifest.txt.meta",
				"Assets/SGLib/Plugins/Android/libs/armeabi/libgpg.so",
				"Assets/SGLib/Plugins/Android/libs/armeabi/libgpg.so.meta",
				"Assets/SGLib/Plugins/iOS/GPGSAppController 1.h",
				"Assets/SGLib/Plugins/iOS/GPGSAppController 1.h.meta",
				"Assets/SGLib/Plugins/iOS/GPGSAppController 1.mm",
				"Assets/SGLib/Plugins/iOS/GPGSAppController 1.mm.meta"
                };

            foreach (string file in obsoleteFiles)
            {
                if (File.Exists(file))
                {
                    Debug.Log("Deleting obsolete file: " + file);
                    File.Delete(file);
                }
            }

            return PluginVersion.VersionKeyU5;
        }

        private static string Upgrade911(string prevVer)
        {
            Debug.Log("Upgrading from format version " + prevVer + " to " + PluginVersion.VersionKeyCPP);

            // delete obsolete files, if they are there
            string[] obsoleteFiles =
                {
				"Assets/SGLib/GooglePlayGames/OurUtils/Utils.cs",
				"Assets/SGLib/GooglePlayGames/OurUtils/Utils.cs.meta",
				"Assets/SGLib/GooglePlayGames/OurUtils/MyClass.cs",
				"Assets/SGLib/GooglePlayGames/OurUtils/MyClass.cs.meta",
				"Assets/SGLib/Plugins/GPGSUtils.dll",
				"Assets/SGLib/Plugins/GPGSUtils.dll.meta",
                };

            foreach (string file in obsoleteFiles)
            {
                if (File.Exists(file))
                {
                    Debug.Log("Deleting obsolete file: " + file);
                    File.Delete(file);
                }
            }

            // delete obsolete directories, if they are there
            string[] obsoleteDirectories =
                {
				"Assets/SGLib/Plugins/Android/BaseGameUtils"
            };

            foreach (string directory in obsoleteDirectories)
            {
                if (Directory.Exists(directory))
                {
                    Debug.Log("Deleting obsolete directory: " + directory);
                    Directory.Delete(directory, true);
                }
            }

            Debug.Log("Done upgrading from format version " + prevVer + " to " + PluginVersion.VersionKeyCPP);
            return PluginVersion.VersionKeyCPP;
        }
    }
}
