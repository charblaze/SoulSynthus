using UnityEngine;
using System.Collections;

public class EquipWindow : MonoBehaviour {

    public int ID;
    public int loc;
    public int type;
    public Texture2D closetext;
    private GameObject player;
    private PlayerController playerscript;
    private Spells spells;
    public GUIStyle thestylebro, closestyle, icostyle, descripstyle;
    public GUIStyle buttonstyle;
    public AudioClip cloth1a, cloth2a, amuleta, dropplay, closeplay;
    // Use this for initialization
    void Start()
    {
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
            AudioSource.PlayClipAtPoint(closeplay, transform.position);
            Destroy(gameObject);
        }

        Texture2D theSprite;
        string name;
        string descrip;
        string group;
        if (type == 0)
        {
            theSprite = spells.getHatIcon(ID);
            name = spells.getHatName(ID);
            descrip = spells.getHatDescrip(ID);
            group = "Hat";
        }
        else if (type == 1)
        {
            theSprite = spells.getTunicIcon(ID);
            name = spells.getTunicName(ID);
            descrip = spells.getTunicDescrip(ID);
            group = "Tunic";
        }
        else if (type == 2)
        {
            theSprite = spells.getAmuletIcon(ID);
            name = spells.getAmuletName(ID);
            descrip = spells.getAmuletDescrip(ID);
            group = "Amulet";
        }
        else
        {
            theSprite = new Texture2D(50, 50);
            name = "Error";
            descrip = "Error";
            group = "Error";
        }

        GUI.Box(new Rect(1920 / 2 + 260, 1080 / 2 - 140, 50, 50), theSprite, icostyle);
        GUI.Label(new Rect(1920 / 2 + 320, 1080 / 2 - 140, 100, 35), name);
        GUI.Label(new Rect(1920 / 2 + 260, 1080 / 2 - 80, 180, 250), descrip, descripstyle);
        GUI.Label(new Rect(1920 / 2 + 320, 1080 / 2 - 110, 100, 25), group);

        if (GUI.Button(new Rect(1920 / 2 + 250, 1080 / 2 + 150, 100, 20), "Equip", buttonstyle))
        {
            if (type == 0)
            {
                playerscript.equipHat(ID);
                Instantiate(Resources.Load("Sounds/Clothsound1"));
            }
            else if (type == 1)
            {
                playerscript.UnequipTunic();
                playerscript.equipTunic(ID);
                Instantiate(Resources.Load("Sounds/Clothsound2"));
            }
            else if (type == 2)
            {
                playerscript.UnequipAmulet();
                playerscript.equipAmulet(ID);
                Instantiate(Resources.Load("Sounds/Chimesound"));
            }

        }

        if(ID != 1){
        if (GUI.Button(new Rect(1920 / 2 + 250, 1080 / 2 - 170, 100, 20), "Unequip", buttonstyle))
        {

            if (type == 0)
            {
                AudioSource.PlayClipAtPoint(cloth1a, transform.position);
                playerscript.equipHat(1);
            }
            else if (type == 1)
            {
                AudioSource.PlayClipAtPoint(cloth2a, transform.position);
                playerscript.UnequipTunic();
            }
            else if (type == 2)
            {
                AudioSource.PlayClipAtPoint(amuleta, transform.position);
                playerscript.UnequipAmulet();
            }
        }
        if (GUI.Button(new Rect(1920 / 2 + 350, 1080 / 2 + 150, 100, 20), "Drop", buttonstyle))
        {
            AudioSource.PlayClipAtPoint(dropplay, transform.position);
            if (type == 0)
            {
                if (spells.EquippedHat == ID)
                {
                    playerscript.equipHat(1);
                    spells.RemoveHat(loc);
                    Destroy(gameObject);
                }
                else
                {
                    spells.RemoveHat(loc);
                    Destroy(gameObject);
                }
            }
            else if (type == 1)
            {
                if (spells.EquippedTunic == ID)
                {
                    playerscript.UnequipTunic();
                    playerscript.equipTunic(1);
                    spells.RemoveTunic(loc);
                    Destroy(gameObject);
                }
                else
                {
                    spells.RemoveTunic(loc);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (spells.EquippedAmulet == ID)
                {
                    playerscript.UnequipAmulet();
                    playerscript.equipAmulet(1);
                    spells.removeAmulet(loc);
                    Destroy(gameObject);
                }
                else
                {
                    spells.removeAmulet(loc);
                    Destroy(gameObject);
                }
            }
        }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
