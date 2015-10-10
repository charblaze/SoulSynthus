using UnityEngine;
using System.Collections;

public class DeathOnEnter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // ISNTANT DEATH NOTHING I COULD DO
            Character w = other.gameObject.GetComponent(typeof(Character)) as Character;
            if (w != null)
            {
                if (w.candrown)
                {
                    w.Die();
                    w.currentHealth = -100;
                }
            }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
