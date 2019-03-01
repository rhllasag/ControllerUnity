using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using Mapbox.Unity.Location;

public class HandleTextFile
{

    EditorLocationProviderLocationLog log = new EditorLocationProviderLocationLog();
    [MenuItem("Tools/Write file")]
    public void WriteString(string text)
    {
        string path = "Assets/Resources/test.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path, ImportAssetOptions.Default);
    }

    [MenuItem("Tools/Read file")]
    public  void ReadString()
    {
        string path = "Assets/Resources/test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

}
#endif

