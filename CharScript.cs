using UnityEngine;
using System.Collections;

public class CharScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    public Texture2D charbase;
    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUI.DrawTexture(new Rect(0, 0, 400, 720), charbase);
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
