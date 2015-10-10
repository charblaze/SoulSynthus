using UnityEngine;
using System.Collections;

public class JouleAnimation : MonoBehaviour {
    GameObject player;
    public int xp;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(dying());
	}
    public GUIStyle myStyle;
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
        DrawOutline(new Rect(1920 - 200, 1080 - 200, 180, 25), "+" + xp + " J", myStyle, new Color(0, 0, 0), new Color(1, 1, 1));
    }

    IEnumerator dying()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    float speed = 1f;
	// Update is called once per frame
	void Update () {
        speed *= 1.1f;
        transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
        //transform.position += (player.transform.position - transform.position) * 1f * Time.deltaTime;
    }
}
