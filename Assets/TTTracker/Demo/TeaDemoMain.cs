using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaDemoMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TTAnalytics.StartWithAppID("xxxxx","yodo1","testdemo",true,false,true);
    }

    private void OnGUI()
    {
        
        if(GUI.Button(new Rect(30, 50, 100, 50), "test button"))
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("Mainbutton", "click");
            keyValues.Add("vodie", "watched");
            TTAnalytics.Event("ButtonCLick", keyValues);
            Debug.Log("Send event");
        }
    }
}
