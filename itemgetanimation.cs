using UnityEngine;
using System.Collections;

public class itemgetanimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(begone());
	}

    private IEnumerator begone()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
