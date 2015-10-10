using UnityEngine;
using System.Collections;

public class TestItemScript : MonoBehaviour {
    GameObject player;
    Spells spells;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        spells = player.GetComponent(typeof(Spells)) as Spells;
	}
	
	// Update is called once per frame
	void Update () {
        if (tag == "Active")
        {
            spells.AddItem(0);
            spells.AddItem(1);
            /*spells.addAmulet(0);
            spells.addHat(3);
            spells.addHat(2);
            spells.addTunic(0);*/
            
            // ORG TIER 1
            //spells.LearnSpell(0); spells.LearnSpell(1); spells.LearnSpell(2); spells.LearnSpell(3); spells.LearnSpell(4); spells.LearnSpell(5);

            // INROG TIER 1
            //spells.LearnSpell(41); spells.LearnSpell(40); spells.LearnSpell(42); spells.LearnSpell(43); spells.LearnSpell(44);
            
            // BIOCHM TIER 1
            //spells.LearnSpell(45); spells.LearnSpell(46); spells.LearnSpell(47); spells.LearnSpell(48);
            //spells.LearnSpell(25);
            //spells.LearnSpell(24);
            Destroy(gameObject);
        }
	}
}
