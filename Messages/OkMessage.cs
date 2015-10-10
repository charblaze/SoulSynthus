using UnityEngine;
using System.Collections;

public class OkMessage : MonoBehaviour {
    public string message;
    public delegate void OKMESSAGE();
    public AudioClip pop;
    public static event OKMESSAGE clicked;

    public static void didit()
    {
        if (clicked != null)
        {
            clicked();
        }
    }
    
    public GUIStyle buttonstyle, textstyle;
	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().cantcast = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().cantcast = false;
    }

   

    void OnGUI()
    {
		float rx = Screen.width / 1920f;
		float ry = Screen.height / 1080f;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUI.depth = -5;
        GUI.Label(new Rect(1920 / 2 - 150, 1080 -250, 300, 200), message, textstyle);
        if (GUI.Button(new Rect(1920 / 2 - 150, 1080 - 50, 300, 30), "Okay (Space)", buttonstyle))
        {
            AudioSource.PlayClipAtPoint(pop, transform.position);
            didit();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().cantcast = false;
            Destroy(gameObject);
        }
        if (Input.GetButtonDown("Backstep") || Input.GetButtonDown("Run"))
        {
            AudioSource.PlayClipAtPoint(pop, transform.position);
            didit();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().cantcast = false;
            Destroy(gameObject);
        }
    }
}
