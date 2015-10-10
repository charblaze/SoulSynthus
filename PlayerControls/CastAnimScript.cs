using UnityEngine;
using System.Collections;

public class CastAnimScript : MonoBehaviour {
    public float howtime = 0;
	// Use this for initialization
    GameObject player;
	void Start () {
         player = GameObject.FindGameObjectWithTag("Player");
         
        StartCoroutine(doAnim());
	}

    IEnumerator doAnim()
    {
        timeslam = howtime;
        yield return new WaitForSeconds(timeslam);
        Destroy(gameObject);
    }
    private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;
    public static void GUIDrawRect(Rect position, Color color)
    {
        if (_staticRectTexture == null)
        {
            _staticRectTexture = new Texture2D(1, 1);
        }

        if (_staticRectStyle == null)
        {
            _staticRectStyle = new GUIStyle();
        }

        _staticRectTexture.SetPixel(0, 0, color);
        _staticRectTexture.Apply();

        _staticRectStyle.normal.background = _staticRectTexture;

        GUI.Box(position, GUIContent.none, _staticRectStyle);


    }
    float timeslam;
    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUIDrawRect(new Rect(1920 / 2 - 150, 1080 / 2 + 120, 300, 2), new Color(1, 1, 1));
        GUIDrawRect(new Rect(1920 / 2 - 150, 1080 / 2 + 120, 300 * (howtime/timeslam), 2), new Color(0, 0, 0));
    }

	// Update is called once per frame
	void Update () {
        howtime -= Time.deltaTime;
        transform.position = player.transform.position + player.transform.up * 0.2f;
        transform.rotation = player.transform.rotation;
        transform.localScale = transform.localScale + transform.localScale* Time.deltaTime * 0.4f;
	}
}
