using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {
    private PlayerController playerscript;
	private Spells spells;
	private PlayerTalents talents;
    string tagx;
	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
		spells = player.GetComponent(typeof(Spells)) as Spells;
		talents = player.GetComponent (typeof(PlayerTalents)) as PlayerTalents;
        tagx = gameObject.transform.tag;
	}
	
	// Update is called once per frame
	void Update () {
        if (tag == "Active")
        {
            GameObject spawn = Instantiate(Resources.Load("UI/CheckpointMenu")) as GameObject;
            if (spawn != null)
            {
                playerscript.menuIsUp = true;
                playerscript.isStunned = true;

                playerscript.CheckpointName = gameObject.name;
                playerscript.CheckpointLevel = Application.loadedLevelName;
            }
            tag = tagx;
        }
	}
}