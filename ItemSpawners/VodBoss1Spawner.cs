using UnityEngine;
using System.Collections;

public class VodBoss1Spawner : MonoBehaviour {
    GameObject player;
    Items items;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        items = player.GetComponent(typeof(Items)) as Items;

        if (items.bosses[0] == false)
        {
            Instantiate(Resources.Load("Enemies/Boss1"));
        }
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
