using UnityEngine;
using System.Collections;

public class soundscript : MonoBehaviour {
    AudioSource ths;
	// Use this for initialization
	void Start () {
        ths = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
	}
	
	// Update is called once per frame
	void Update () {
        if (!ths.isPlaying)
        {
            Destroy(gameObject);
        }
	}
}
