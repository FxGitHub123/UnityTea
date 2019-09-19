
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;
using System.Xml;
using System.IO;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif


public class TeaPostBuildProcess
{
    [PostProcessBuild(100)]
    public static void OnPreprocessBuild( BuildTarget buildTarget, string buildPath)
    {
#if UNITY_ANDROID        
        if (buildTarget == BuildTarget.Android)
        {
            OnPreprocessBuild_android();
        }
#elif UNITY_IOS

        if(buildTarget == BuildTarget.iOS)
        {
            OnBuildIOS(buildPath);
        }
#endif

    }




#if UNITY_ANDROID
    public static void OnPreprocessBuild_android()
    {

        Debug.Log("<color=green>===============================START BUILD=====================================</color>");
        // 读取xml
        string xmlPath = Application.dataPath + "/Plugins/Android/AndroidManifest.xml";

        if (!File.Exists(xmlPath))
        {
            Debug.LogError("Please Move Manifest File To Plugins/Android/");
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        XmlNode manNode = xmlDoc.SelectSingleNode("/manifest");
        string ns = manNode.GetNamespaceOfPrefix("android");

        XmlNode application = xmlDoc.SelectSingleNode("/manifest/application");

        XmlNode provider = xmlDoc.SelectSingleNode("/manifest/application/provider");
        if(provider != null)
        {
            string value = provider.Attributes["android:name"].Value;
            if (value != null && value == "com.yodo1.ttanalytics.TTProvider")
            {
                Debug.Log("TTProvider Already Exis Won`t Merge");
                Debug.Log("<color=green>===============================END BUILD=====================================</color>");
                return;
            }
        }
        
        // 自定义
        XmlElement node = xmlDoc.CreateElement("provider");

        application.AppendChild(node);
      
        node.SetAttribute("name",ns, "com.yodo1.ttanalytics.TTProvider");
        node.SetAttribute ("authorities", ns, "${applicationId}.ttprovider");
        node.SetAttribute("exported", ns, "false");
        node.SetAttribute("multiprocess", ns, "true");

        xmlDoc.Save(xmlPath);
        AssetDatabase.Refresh();

        Debug.Log("===============================END BUILD=====================================");

    }

    static XmlNode FindNode(XmlDocument xmlDoc, string xpath, string attributeName, string attributeValue)
    {
        XmlNodeList nodes = xmlDoc.SelectNodes(xpath);
        //Debug.Log(nodes.Count);
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes.Item(i);
            string _attributeValue = node.Attributes[attributeName].Value;
            if (_attributeValue == attributeValue)
            {
                return node;
            }
        }
        return null;
    }

#endif


#if UNITY_IOS


    public static void OnBuildIOS(string buildPath)
    {
        string projPath = Path.Combine(buildPath, "Unity-iPhone.xcodeproj/project.pbxproj");


        PBXProject pbxProj = new PBXProject();
        pbxProj.ReadFromFile(projPath);

        // 更新项目配置
        string targetGuid= pbxProj.TargetGuidByName("Unity-iPhone");

        pbxProj.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");
        pbxProj.AddBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

        pbxProj.AddFrameworkToProject(targetGuid, "libz.tbd",			false);
        pbxProj.AddFrameworkToProject(targetGuid, "libsqlite3.tbd",		false);


        pbxProj.WriteToFile(projPath);


        // infoplist fix
        string plistPath = Path.Combine(buildPath, "Info.plist");
        if(File.Exists(plistPath))
        {
            PlistDocument doc = new PlistDocument();
            doc.ReadFromFile(plistPath);
            PlistElementDict rootDict =  doc.root.AsDict();
            
            if(rootDict != null)
            {
                if(rootDict["NSAppTransportSecurity"].AsDict() == null)
                {
                    PlistElementDict data = new PlistElementDict();
                    data.SetBoolean("NSAllowsArbitraryLoads", true);
                    rootDict["NSAppTransportSecurity"] = data;
                }

                doc.WriteToFile(plistPath);
            }
        }
        else
        {
            Debug.LogError(">> Can't find Info.plist");
        }
    }


#endif







}
