using UnityEngine;
using System.Collections;

public class ZoneEntrance : MonoBehaviour {
    public string Name;
    public GUIStyle style;
	// Use this for initialization
	void Start () {
        StartCoroutine(doit());
	}
    float alpha = 0f;
    IEnumerator doit()
    {
        for (int c = 0; c < 20; ++c)
        {
            alpha += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3f);
        for (int c = 0; c < 20; ++c)
        {
            alpha -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
    //draw text of a specified color, with a specified outline color
    public static void DrawOutline(Rect position, string text, GUIStyle style, Color outColor, Color inColor)
    {
        var backupStyle = style;
        style.normal.textColor = outColor;
        position.x--;
        GUI.Label(position, text, style);
        position.x += 2;
        GUI.Label(position, text, style);
        position.x--;
        position.y--;
        GUI.Label(position, text, style);
        position.y += 2;
        GUI.Label(position, text, style);
        position.y--;
        style.normal.textColor = inColor;
        GUI.Label(position, text, style);
        style = backupStyle;
    }
    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUI.depth = -50;
        Color slam = GUI.color;
        slam.a = alpha;
        GUI.color = slam;
        DrawOutline(new Rect(1920 / 2 - 400, 150, 800, 100), Name, style, new Color(0,0,0), new Color(1,1,1));
        
    }

	// Update is called once per frame
	void Update () {
	
	}
}
