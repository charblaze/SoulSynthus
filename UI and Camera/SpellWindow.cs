using UnityEngine;
using System.Collections;

public class SpellWindow : MonoBehaviour {

    public int ID;
    public int loc;
    public Texture2D closetext;
    private GameObject player;
    private PlayerController playerscript;
    private Spells spells;
    public GUIStyle thestylebro;
    public GUIStyle buttonstyle, closestyle, icostyle, descripstyle;
    private SpellCastScript scs;
    public AudioClip closesound, assignsound, forgetsound;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
        spells = player.GetComponent(typeof(Spells)) as Spells;
        scs = player.GetComponent(typeof(SpellCastScript)) as SpellCastScript;
	}
	
	// Update is called once per frame
	void OnGUI() {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUI.depth = 0;
        GUI.Box(new Rect(1920 / 2 + 250, 1080 / 2 - 150, 200, 300), "", thestylebro);
        if (GUI.Button(new Rect(1920 / 2 + 430, 1080 / 2 - 150, 20, 20), closetext, closestyle))
        {
            Time.timeScale = 1; AudioSource.PlayClipAtPoint(closesound, transform.position); 
            Destroy(gameObject);
        }
        GUI.Box(new Rect(1920 / 2 + 260, 1080 / 2 - 140, 50, 50), spells.getSpellUI_Icon(ID), icostyle);
        GUI.Label(new Rect(1920 / 2 + 320, 1080 / 2 - 140, 100, 35), spells.getSpellName(ID));
        GUI.Label(new Rect(1920 / 2 + 260, 1080 / 2 - 80, 180, 250), spells.getSpellDescription(ID), descripstyle);
        if (spells.getIsOrganic(ID))
        {
            GUI.Label(new Rect(1920 / 2 + 320, 1080 / 2 - 110, 100, 25), "Organic");
        }
        else
        {
            GUI.Label(new Rect(1920 / 2 + 320, 1080 / 2 - 110, 100, 25), "Inorganic");
        }
        GUI.Label(new Rect(1920 / 2 + 370, 1080 / 2 + 120, 100, 20), spells.getSpellCost(ID) + " Stamina");
        GUI.Label(new Rect(1920 / 2 + 260, 1080 / 2 + 120, 120, 20), "Cooldown: " + spells.getSpellCD(ID) + " s");
        if ((playerscript.getSpell1() == ID && scs.OneonCD) || (playerscript.getSpell2() == ID && scs.TwoonCD) ||
            (playerscript.getSpellL() == ID && scs.LonCD) || (playerscript.getSpellR() == ID && scs.RonCD))
        {
            GUI.Box(new Rect(1920 / 2 + 250, 1080 / 2 + 150, 200, 20), "Cannot Equip On Cooldown", buttonstyle);
        }
        else
        {
            if (!scs.LonCD)
            {
                if (GUI.Button(new Rect(1920 / 2 + 250, 1080 / 2 + 150, 100, 20), "Set to LMB", buttonstyle))
                {
                    Time.timeScale = 1; AudioSource.PlayClipAtPoint(assignsound, transform.position);
                    playerscript.equipSpelltoL(ID);
                    if (playerscript.getSpellR() == ID)
                    {
                        playerscript.unequipSpellR();
                    }
                    if (playerscript.getSpell1() == ID)
                    {
                        playerscript.unequipSpell1();
                    }
                    if (playerscript.getSpell2() == ID)
                    {
                        playerscript.unequipSpell2();
                    }
                    if (playerscript.getSpellQ() == ID)
                    {
                        playerscript.unequipSpellQ();
                    }
                    if (playerscript.getSpellQ2() == ID)
                    {
                        playerscript.unequipSpellQ2();
                    }
                }
            }
            if (!scs.QonCD)
            {
                if (GUI.Button(new Rect(1920 / 2 + 250, 1080 / 2 + 190, 100, 20), "Set to Q", buttonstyle))
                {
                    Time.timeScale = 1; AudioSource.PlayClipAtPoint(assignsound, transform.position); 
                    playerscript.equipSpelltoQ(ID);
                    if (playerscript.getSpellR() == ID)
                    {
                        playerscript.unequipSpellR();
                    }
                    if (playerscript.getSpell1() == ID)
                    {
                        playerscript.unequipSpell1();
                    }
                    if (playerscript.getSpell2() == ID)
                    {
                        playerscript.unequipSpell2();
                    }
                    if (playerscript.getSpellQ2() == ID)
                    {
                        playerscript.unequipSpellQ2();
                    }
                    if (playerscript.getSpellL() == ID)
                    {
                        playerscript.unequipSpellL();
                    }
                }
            }
            if (!scs.Q2onCD)
            {
                if (GUI.Button(new Rect(1920 / 2 + 350, 1080 / 2 + 190, 100, 20), "Set to R", buttonstyle))
                {
                    Time.timeScale = 1; AudioSource.PlayClipAtPoint(assignsound, transform.position);
                    playerscript.equipSpelltoQ2(ID);
                    if (playerscript.getSpellR() == ID)
                    {
                        playerscript.unequipSpellR();
                    }
                    if (playerscript.getSpell1() == ID)
                    {
                        playerscript.unequipSpell1();
                    }
                    if (playerscript.getSpell2() == ID)
                    {
                        playerscript.unequipSpell2();
                    }
                    if (playerscript.getSpellQ() == ID)
                    {
                        playerscript.unequipSpellQ();
                    }
                    if (playerscript.getSpellL() == ID)
                    {
                        playerscript.unequipSpellL();
                    }
                }
            }
            if (!scs.RonCD)
            {
                if (GUI.Button(new Rect(1920 / 2 + 250, 1080 / 2 + 170, 100, 20), "Set to RMB", buttonstyle))
                {
                    Time.timeScale = 1; AudioSource.PlayClipAtPoint(assignsound, transform.position); 
                    playerscript.equipSpelltoR(ID);
                    if (playerscript.getSpellL() == ID)
                    {
                        playerscript.unequipSpellL();
                    }
                    if (playerscript.getSpell1() == ID)
                    {
                        playerscript.unequipSpell1();
                    }
                    if (playerscript.getSpell2() == ID)
                    {
                        playerscript.unequipSpell2();
                    }
                    if (playerscript.getSpellQ() == ID)
                    {
                        playerscript.unequipSpellQ();
                    }
                    if (playerscript.getSpellQ2() == ID)
                    {
                        playerscript.unequipSpellQ2();
                    }
                }
            }
            if (!scs.OneonCD)
            {
                if (GUI.Button(new Rect(1920 / 2 + 350, 1080 / 2 + 150, 100, 20), "Set to 1", buttonstyle))
                {
                    Time.timeScale = 1; AudioSource.PlayClipAtPoint(assignsound, transform.position); 
                    playerscript.equipSpellto1(ID);
                    if (playerscript.getSpellR() == ID)
                    {
                        playerscript.unequipSpellR();
                    }
                    if (playerscript.getSpellL() == ID)
                    {
                        playerscript.unequipSpellL();
                    }
                    if (playerscript.getSpell2() == ID)
                    {
                        playerscript.unequipSpell2();
                    }
                    if (playerscript.getSpellQ() == ID)
                    {
                        playerscript.unequipSpellQ();
                    }
                    if (playerscript.getSpellQ2() == ID)
                    {
                        playerscript.unequipSpellQ2();
                    }
                }
            }
            if (!scs.TwoonCD)
            {
                if (GUI.Button(new Rect(1920 / 2 + 350, 1080 / 2 + 170, 100, 20), "Set to 2", buttonstyle))
                {
                    Time.timeScale = 1; AudioSource.PlayClipAtPoint(assignsound, transform.position); 
                    playerscript.equipSpellto2(ID);
                    if (playerscript.getSpellR() == ID)
                    {
                        playerscript.unequipSpellR();
                    }
                    if (playerscript.getSpell1() == ID)
                    {
                        playerscript.unequipSpell1();
                    }
                    if (playerscript.getSpellL() == ID)
                    {
                        playerscript.unequipSpellL();
                    }
                    if (playerscript.getSpellQ() == ID)
                    {
                        playerscript.unequipSpellQ();
                    }
                    if (playerscript.getSpellQ2() == ID)
                    {
                        playerscript.unequipSpellQ2();
                    }
                }
            }
            if (GUI.Button(new Rect(1920 / 2 + 250, 1080 / 2 - 170, 100, 20), "Forget Spell", buttonstyle))
            {
                Time.timeScale = 1; AudioSource.PlayClipAtPoint(forgetsound, transform.position); 
                spells.UnlearnSpell(loc);
                Destroy(gameObject);
            }
        }
	}


    void Update()
    {
    }
}
