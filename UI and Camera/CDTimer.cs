using UnityEngine;
using System.Collections;

public class CDTimer : MonoBehaviour {

    public float counter = 0;

	// Use this for initialization
	void Start () {
	     
	}

    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = 1080 - (screenPosition.y + 1);
        GUI.Label(new Rect(screenPosition.x, screenPosition.y, 100, 50), ""+counter);
    }
	
	// Update is called once per frame
	void Update () {
        if (counter == -1)
        {
            Destroy(gameObject);
        }
        counter = counter - Time.deltaTime;
	}
}
