using UnityEngine;
using System.Collections;

public class VodInteriorSpawners : MonoBehaviour
{
    GameObject player;
    Items items;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        items = player.GetComponent(typeof(Items)) as Items;

        if (items.VodExterior[11] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodInterior/0"));
        }
        if (items.VodExterior[12] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodInterior/1"));
        }
        if (items.VodExterior[13] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodInterior/2"));
        }
        if (items.VodExterior[14] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodInterior/3"));
        }
        if (items.VodExterior[15] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodInterior/4"));
        }
        if (items.VodExterior[16] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodInterior/5"));
        }
        if (items.VodExterior[17] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodInterior/6"));
        }
        if (items.VodExterior[18] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodInterior/7"));
        }
        if (items.VodExterior[19] == false)
        {
            Instantiate(Resources.Load("ItemPickups/VodInterior/8"));
        }

        if (items.bosses[1] == false)
        {
            Instantiate(Resources.Load("Enemies/Blood"));
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
