using UnityEngine;
using System.Collections;

public class OpheliaSpawnerVOD : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Spells pc = GameObject.FindGameObjectWithTag("Player").GetComponent<Spells>();
        if (pc.LookForKey(12))
        {
            // spawn ophelia in front of the door
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
