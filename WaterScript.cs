using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
    float moveSpeed = 1;
	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent<Renderer> ();
        StartCoroutine(doanimation());
	}
	Renderer renderer;
    IEnumerator doanimation()
    {
        while (true)
        {
            StartCoroutine(FloatInDirection("Up"));
            yield return new WaitForSeconds(2f);
            StartCoroutine(FloatInDirection("Back"));
            yield return new WaitForSeconds(2f);
        }
    }

	Vector2 uvOffset = Vector2.zero;
	void LateUpdate(){
		uvOffset += new Vector2(0, 0.05f * Time.deltaTime);
		if (renderer.enabled) {
			renderer.materials[0].SetTextureOffset("_MainTex", uvOffset);		
		}
	}

    public IEnumerator FloatInDirection(string Direction, float modifier = 1f)
    {
        if (Direction == "Up")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.01f)
            {
                transform.position += transform.up * Time.deltaTime * (moveSpeed - c) * modifier * (0.2f);
                yield return new WaitForSeconds(0.02f);
            }
        }
        if (Direction == "Back")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.01f)
            {
                transform.position += -1 * transform.up * Time.deltaTime * (moveSpeed - c) * modifier * (0.2f);
                yield return new WaitForSeconds(0.02f);
            }
        }
        if (Direction == "Left")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += -1 * transform.right * Time.deltaTime * (moveSpeed - c) * modifier;
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (Direction == "Right")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += transform.right * Time.deltaTime * (moveSpeed - c) * modifier;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
