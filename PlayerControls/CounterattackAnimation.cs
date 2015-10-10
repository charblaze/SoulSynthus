using UnityEngine;
using System.Collections;

public class CounterattackAnimation : MonoBehaviour {
    private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;
    public GameObject target, user;
    Character tar, us;
    public Texture2D femcatex, malecatex;
    public Texture2D touse, exclamationmarks;
    public AudioClip animationSound;
	// Use this for initialization
	void Start () {
        GameObject play = GameObject.FindGameObjectWithTag("Player");
        PlayerController pc = play.GetComponent<PlayerController>();
        if(user == play){
        if (pc.gender == "male")
        {
            touse = malecatex;
        }
        else
        {
            touse = femcatex;
        }
        }
        tar = target.GetComponent<Character>();
        us = user.GetComponent<Character>();
        StartCoroutine(doanimation());
	}

    IEnumerator doanimation()
    {
        AudioSource.PlayClipAtPoint(animationSound, user.transform.position);
        tar.STUNNED(1f);
        Time.timeScale = 0.2f;
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1f));
        for (int c = 0; c < 5; ++c)
        {
            Time.timeScale += 0.2f;
            alpha -= 0.2f;
            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.05f));
        }
        Time.timeScale = 1f;
        StartCoroutine(tar.FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(tar.FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(tar.FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(tar.FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
		tar.LoseHealth(Mathf.CeilToInt(us.max_stamina * 1.21f));
        StartCoroutine(tar.FloatInDirection("Back"));
        Destroy(gameObject);
    }
    public static class CoroutineUtil
    {
        public static IEnumerator WaitForRealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
        }
    }

    float alpha = 1f;
    // Draw a rectangle
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

   

    float excposition = 100f;
    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        Color slam = GUI.color;
        slam.a = alpha;
        GUI.color = slam;
        GUI.depth = -51;

        GUI.DrawTexture(new Rect(0, 1080 / 2 - 150, 1920, 300), touse);
        GUIDrawRect(new Rect(0, 0, 1920, 50), new Color(0, 0, 0));
        GUIDrawRect(new Rect(0, 1080 - 50, 1920, 50), new Color(0, 0, 0));
        GUI.DrawTexture(new Rect(excposition, 1080 / 2 - 125, 150, 150), exclamationmarks);
    }

	
	// Update is called once per frame
	void Update () {
        excposition += Time.deltaTime;
	}
}
