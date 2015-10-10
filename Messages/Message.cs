using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour {

    public delegate void Messagebox(bool response);

    public static event Messagebox onClick;

    public string message;

    public GUIStyle buttonstyle, textstyle;

    public static void yes()
    {
        if (onClick != null)
        {
            onClick(true);
        }
    }

    public static void no()
    {
        if (onClick != null)
        {
            onClick(false);
        }
    }

    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUI.depth = -5;
        GUI.Label(new Rect(1920 / 2 - 100, 1080 / 2 - 50, 200, 100), message, textstyle);
        if(GUI.Button(new Rect(1920 / 2 - 100, 1080 / 2 + 50, 100 ,30), "Yes", buttonstyle)){
            yes();
            Destroy(gameObject);
        }
        if (GUI.Button(new Rect(1920 / 2, 1080 / 2 + 50, 100, 30), "No", buttonstyle))
        {
            no();
            Destroy(gameObject);
        }
    }

}
