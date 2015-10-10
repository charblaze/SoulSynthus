using UnityEngine;
using System.Collections;

public class VodExteriorItems : MonoBehaviour {
    GameObject player;
    Items items;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        items = player.GetComponent(typeof(Items)) as Items;

        if (items.VodExterior[0] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate5StamPots"), new Vector3(0.31869f, -3.4698f, -0.3972f), Quaternion.Euler(new Vector3(-46.5f, -31.87f, 31.84f)));
        }
        if (items.VodExterior[1] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate3HealthPots"), new Vector3(4.715f, 10.425f, -0.4f), Quaternion.Euler(new Vector3(-39.3f, 62.544f, -40.96f)));
        }
        if (items.VodExterior[2] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/CrateSmallChime"), new Vector3(-3.3f, -6.61f, -0.72f), Quaternion.Euler(new Vector3(-39.32f, 62.5f, -40.96f)));
        }
        if (items.VodExterior[3] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate4"));
        }
        if (items.VodExterior[4] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate5"));
        }
        if (items.VodExterior[5] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate6"));
        }
        if (items.VodExterior[6] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate7"));
        }
        if (items.VodExterior[7] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate8"));
        }
        if (items.VodExterior[8] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate9"));
        }
        if (items.VodExterior[9] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate10"));
        }
        if (items.VodExterior[10] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodExterior/Crate11"));
        }


        if (!items.bosses[3])
        {
            // spawn ophelia normal
            Instantiate(Resources.Load("NPC/Ophelia1"));
        }
        if (items.bosses[4])
        {
            // spawn ophelia in gardens
            Instantiate(Resources.Load("NPC/Ophelia2"));
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
