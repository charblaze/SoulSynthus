using UnityEngine;
using System.Collections;

public class ItemWindowScript : MonoBehaviour {

    public int ID;
    public int loc;
    public Texture2D closetext;
    private GameObject player;
    private PlayerController playerscript;
    private Spells spells;
    public GUIStyle thestylebro, closestyle, icostyle;
    public GUIStyle buttonstyle;
    public GUIStyle descriptionstyle;
    public AudioClip useitem, othersound, closesound;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
        spells = player.GetComponent(typeof(Spells)) as Spells;
	}

    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUI.depth = 0;
        GUI.Box(new Rect(1920 / 2 + 250, 1080 / 2 - 150, 200, 300), "", thestylebro);
        if (GUI.Button(new Rect(1920 / 2 + 430, 1080 / 2 - 150, 20, 20), closetext, closestyle))
        {
            AudioSource.PlayClipAtPoint(closesound, transform.position);
            Destroy(gameObject);
        }
        GUI.Box(new Rect(1920 / 2 + 260, 1080 / 2 - 140, 50, 50), spells.getItemSprite(ID), icostyle);
        GUI.Label(new Rect(1920 / 2 + 320, 1080 / 2 - 140, 100, 35), spells.getItemName(ID));
        GUI.Label(new Rect(1920 / 2 + 260, 1080 / 2 - 80, 180, 250), spells.getItemDescrip(ID),descriptionstyle);
        GUI.Label(new Rect(1920 / 2 + 320, 1080 / 2 - 110, 100, 25), spells.getItemGroup(ID));
        if (spells.getIsReactable(ID))
        {
            GUI.Label(new Rect(1920 / 2 + 260, 1080 / 2 + 120, 100, 20), "Reactive");
        }
        if (!(spells.getIsReactable(ID) || spells.getItemIsKey(ID)))
        {
            if (GUI.Button(new Rect(1920 / 2 + 250, 1080 / 2 + 150, 100, 20), "Use", buttonstyle))
            {
                AudioSource.PlayClipAtPoint(useitem, transform.position);
                playerscript.GainHealth(spells.getItemHPRestore(ID));
                playerscript.stamina += Mathf.CeilToInt(spells.getItemStamRestore(ID));
                if (spells.getItemXPGain(ID) > 0)
                {
                    GameObject ja = Instantiate(Resources.Load("JoulesAnimation"), player.transform.position, player.transform.rotation) as GameObject;
                    JouleAnimation ji = ja.GetComponent(typeof(JouleAnimation)) as JouleAnimation;
                    ji.xp = (int)spells.getItemXPGain(ID);
                    playerscript.xp += spells.getItemXPGain(ID);
                }
				if(ID!= 11){
					spells.RemoveItem(loc);}
                Destroy(gameObject);
            }
        }
        if (spells.getItemIsKey(ID))
        {
            GUI.Label(new Rect(1920 / 2 + 400, 1080 / 2 + 120, 100, 20), "Key");
        }
        else
        {
			if(ID != 11){
            if (GUI.Button(new Rect(1920 / 2 + 350, 1080 / 2 + 150, 100, 20), "Drop", buttonstyle))
            {
                AudioSource.PlayClipAtPoint(othersound, transform.position);
                spells.RemoveItem(loc);
                Destroy(gameObject);
            }
			}
        }
    }


	// Update is called once per frame
	void Update () {

	}
}
