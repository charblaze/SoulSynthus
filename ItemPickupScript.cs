using UnityEngine;
using System.Collections;

public class ItemPickupScript : MonoBehaviour
{
    GameObject player;
    Spells spells;
    public int[] itemscontained;
    public int[] hatscontained;
    public int[] tunicscontained;
    public int[] amuletscontained;
    public int[] spellscontained;
    public int LootID;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spells = player.GetComponent(typeof(Spells)) as Spells;
    }

    IEnumerator doit()
    {
        for (int c = 0; c < itemscontained.Length; ++c)
        {
            spells.AddItem(itemscontained[c]);
            yield return new WaitForSeconds(0.1f);
        }
        for (int c = 0; c < hatscontained.Length; ++c)
        {
            spells.addHat(hatscontained[c]); yield return new WaitForSeconds(0.1f);
        }
        for (int c = 0; c < tunicscontained.Length; ++c)
        {
            spells.addTunic(tunicscontained[c]); yield return new WaitForSeconds(0.1f);
        }
        for (int c = 0; c < amuletscontained.Length; ++c)
        {
            spells.addAmulet(amuletscontained[c]); yield return new WaitForSeconds(0.1f);
        }
        for (int c = 0; c < spellscontained.Length; ++c)
        {
            spells.LearnSpell(spellscontained[c]); yield return new WaitForSeconds(0.1f);
        }
        Items tt = player.GetComponent(typeof(Items)) as Items;
        tt.VodExterior[LootID] = true;
        Destroy(gameObject.transform.parent.gameObject);
    }
    bool doinit = false;
    // Update is called once per frame
    void Update()
    {
        if (tag == "Active" && !doinit)
        {
            doinit = true;
            StartCoroutine(doit());
        }
    }
}
