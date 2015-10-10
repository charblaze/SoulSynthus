using UnityEngine;
using System.Collections;

public class CheckpointMenu : MonoBehaviour {

    public int ID;
    public int loc;
    public Texture2D closetext;
    private GameObject player;
    private PlayerController playerscript;
    private Spells spells;
    public GUIStyle thestylebro;
    public GUIStyle buttonstyle;
    private GUIStyle slamstyle;
    public GUIStyle hugetext;
    public GUIStyle h, closestyle, talentstyle, talentstylegotten, tooltipstyle;
    private PlayerTalents talents;
    public Texture2D inorg, org, biochm;
    public Texture2D inorg1bg, inorg2bg, inorg3bg, inorg4bg, inorg5bg, org1bg, org2bg, org3bg, org4bg, org5bg, bc1bg, bc2bg, bc3bg, bc4bg, bc5bg;
    public Texture2D mysteryicon;
    public AudioClip tap, orgosound, inorgosound, biochemsound, closesound;
    private int d;
    // subscribe to events
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Message.onClick += this.confirmedMessage;
        OkMessage.clicked += this.okMessaged;
    }

    public void okMessaged()
    {
        messaging = false;
    }

    private bool confirmed = false;
    private bool messaging = false;
    public void confirmedMessage(bool c)
    {
        if (c == true)
        {
            confirmed = true;
            messaging = false;
        }
        else
        {
            confirmed = false;
            messaging = false;
        }
    }


    // Use this for initialization
    void Start()
    {
        AudioSource.PlayClipAtPoint(closesound, transform.position);
        slamstyle = buttonstyle;
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
        spells = player.GetComponent(typeof(Spells)) as Spells;
        talents = player.GetComponent(typeof(PlayerTalents)) as PlayerTalents;
        if (talents.Declared)
        {
            t1o = true;
        }
        if (talents.declaration == "Biochemistry")
        {
            title = "Biochemistry";
        }
        else if (talents.declaration == "Organic")
        {
            title = "Organic Chemistry";
        }
        else if (talents.declaration == "Inorganic")
        {
            title = "Inorganic Chemistry";
        }
        Application.LoadLevel(playerscript.CheckpointLevel);
        playerscript.currentHealth = playerscript.maxHealth;
        playerscript.numheals = 5;
    }
    private char speci;

    private void confirming(int c, char spec)
    {
        messaging = true;
        speci = spec;
    }

    public AudioClip leveleduphealth;
    public AudioClip leveledupstam;
    private bool t1o = false, t2o = false, t3o = false, t4o = false, t5o = false;
    private string title;
    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUI.depth = 0;
        GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 240, 640, 480), "", thestylebro);
        // level up bttons
        int req = (int)(Mathf.Pow(1.056f, playerscript.level-1) * 850);
        int healthbonus = Mathf.CeilToInt(Mathf.Pow(0.983f, playerscript.level - 1) * 19);
        int stambonus = Mathf.CeilToInt(Mathf.Pow(0.983f, playerscript.level - 1) * 11);
        int spellpowerbonus = Mathf.CeilToInt(Mathf.Pow(0.63f, playerscript.level - 1) * 6);
        GUI.Box(new Rect(1920 / 2 - 520, 1080 / 2 - 340 + 100, 200, 50), "Level: " + playerscript.level + "\nRequires: " + req + "J", thestylebro);
        GUI.Box(new Rect(1920 / 2 - 520, 1080 / 2 - 340, 200, 100), "Health: " + playerscript.maxHealth + "\nStamina: " + playerscript.max_stamina + "\nSpell Power: " + playerscript.SPELLPOWERBONUS, thestylebro);
        if (GUI.Button(new Rect(1920 / 2 - 520, 1080 / 2 - 340 + 200, 200, 50), "Brew Stamina Concoction\n(+" + stambonus + " Maximum Stamina)", buttonstyle))
        {
            if (playerscript.xp >= req)
            {
                AudioSource.PlayClipAtPoint(leveledupstam, transform.position);
                playerscript.xp -= req;
                playerscript.level += 1;
                playerscript.max_stamina += stambonus;
                playerscript.stamina += stambonus;
            }
        }
        if (GUI.Button(new Rect(1920 / 2 - 520, 1080 / 2 - 340 + 150, 200, 50), "Brew Health Concoction\n(+" + healthbonus + " Maximum Health)", buttonstyle))
        {
            if (playerscript.xp >= req)
            {
                AudioSource.PlayClipAtPoint(leveleduphealth, transform.position);
                playerscript.xp -= req;
                playerscript.level += 1;
                playerscript.maxHealth += healthbonus;
                playerscript.currentHealth += healthbonus;
            }
        }
        if (GUI.Button(new Rect(1920 / 2 - 520, 1080 / 2 - 340 + 250, 200, 50), "Brew Spell Power Concoction\n(+" + spellpowerbonus + " Spell Power)", buttonstyle))
        {
            if (playerscript.xp >= req)
            {
                AudioSource.PlayClipAtPoint(leveledupstam, transform.position);
                playerscript.xp -= req;
                playerscript.level += 1;
                playerscript.SPELLPOWERBONUS += spellpowerbonus;
            }
        }
        if (!t1o && !t2o && !t3o && !t4o && !t5o && !talents.Declared)
        {
            GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 240, 620, 20), "Lab Desk", h);
        }
        if (!t1o && !t2o && !t3o && !t4o && !t5o && talents.Declared)
        {
            if (talents.declaration == "Biochemistry")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 240, 620, 20), "Lab Desk - " + talents.declaration, h);
            }
            else
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 240, 620, 20), "Lab Desk - " + talents.declaration + " Chemistry", h);
            }
        }
        if (t1o)
        {
            GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 240, 620, 20), "Lab Desk - " + title +" - Tier 1", h);

            // TIER 1 INORGANIC CHEMISTRY
            if (talents.declaration == "Inorganic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), inorg1bg, thestylebro);

                for (int c = 0; c < talents.Inorganic_Talents_t1.Length; c++)
                {
                    if (talents.Inorganic_Talents_t1[c].tier == 1)
                    {
                        if (talents.Inorganic_Talents_t1[c].visible)
                        {
                            if (!talents.Inorganic_Talents_t1[c].gotten)
                            {
                                if (talents.Inorganic_Talents_t1[c].mystery)
                                {
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c,'i');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c,'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c,'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c,'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Inorganic_Talents_t1[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Inorganic_Talents_t1[c].itemReq);
                                    }
                                    if (talents.Inorganic_Talents_t1[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Inorganic_Talents_t1[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(talents.Inorganic_Talents_t1[c].icon, talents.Inorganic_Talents_t1[c].name +
                                        "\n\n" + talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Inorganic_Talents_t1[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].reachable)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c,'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c,'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c,'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c,'i'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Inorganic_Talents_t1[c].pos, talents.Inorganic_Talents_t1[c].icon, talentstylegotten);
                            }
                        }
                    }
                }


            }
            // TIER 1 ORGANIC CHEMSITRY
            else if (talents.declaration == "Organic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), org1bg, thestylebro);
                for (int c = 0; c < talents.Organic_Talents.Length; c++)
                {
                    if (talents.Organic_Talents[c].tier == 1)
                    {
                        if (talents.Organic_Talents[c].visible)
                        {
                            if (!talents.Organic_Talents[c].gotten)
                            {
                                if (talents.Organic_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Organic_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Organic_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c,'o');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c,'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c,'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c,'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Organic_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Organic_Talents[c].itemReq);
                                    }
                                    if (talents.Organic_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Organic_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(talents.Organic_Talents[c].icon, talents.Organic_Talents[c].name +
                                        "\n\n" + talents.Organic_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Organic_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].reachable)
                                        {
                                            if (talents.Organic_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c,'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c,'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c,'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c,'o'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Organic_Talents[c].pos, talents.Organic_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
            else if (talents.declaration == "Biochemistry")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), bc1bg, thestylebro);
                for (int c = 0; c < talents.Biolchem_Talents.Length; c++)
                {
                    if (talents.Biolchem_Talents[c].tier == 1)
                    {
                        if (talents.Biolchem_Talents[c].visible)
                        {
                            if (!talents.Biolchem_Talents[c].gotten)
                            {
                                if (talents.Biolchem_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Biolchem_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Biolchem_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c,'b');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c,'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c,'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c,'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Biolchem_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Biolchem_Talents[c].itemReq);
                                    }
                                    if (talents.Biolchem_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Biolchem_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(talents.Biolchem_Talents[c].icon, talents.Biolchem_Talents[c].name +
                                        "\n\n" + talents.Biolchem_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Biolchem_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].reachable)
                                        {
                                            if (talents.Biolchem_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c,'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c,'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c,'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c,'b'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Biolchem_Talents[c].pos, talents.Biolchem_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
        }
        if (t2o)
        {
            GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 240, 620, 20), "Lab Desk - " + title + " - Tier 2", h);
            if (talents.declaration == "Inorganic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), inorg2bg, thestylebro);
                for (int c = 0; c < talents.Inorganic_Talents_t1.Length; c++)
                {
                    if (talents.Inorganic_Talents_t1[c].tier == 2)
                    {
                        if (talents.Inorganic_Talents_t1[c].visible)
                        {
                            if (!talents.Inorganic_Talents_t1[c].gotten)
                            {
                                if (talents.Inorganic_Talents_t1[c].mystery)
                                {
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c,'i');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c,'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c,'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c,'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Inorganic_Talents_t1[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Inorganic_Talents_t1[c].itemReq);
                                    }
                                    if (talents.Inorganic_Talents_t1[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Inorganic_Talents_t1[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(talents.Inorganic_Talents_t1[c].icon, talents.Inorganic_Talents_t1[c].name +
                                        "\n\n" + talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Inorganic_Talents_t1[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].reachable)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c,'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c,'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c,'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c,'i'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Inorganic_Talents_t1[c].pos, talents.Inorganic_Talents_t1[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
            
            else if (talents.declaration == "Organic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), org2bg, thestylebro);
                for (int c = 0; c < talents.Organic_Talents.Length; c++)
                {
                    if (talents.Organic_Talents[c].tier == 2)
                    {
                        if (talents.Organic_Talents[c].visible)
                        {
                            if (!talents.Organic_Talents[c].gotten)
                            {
                                if (talents.Organic_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Organic_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Organic_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'o');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Organic_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Organic_Talents[c].itemReq);
                                    }
                                    if (talents.Organic_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Organic_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(talents.Organic_Talents[c].icon, talents.Organic_Talents[c].name +
                                        "\n\n" + talents.Organic_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Organic_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].reachable)
                                        {
                                            if (talents.Organic_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'o'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Organic_Talents[c].pos, talents.Organic_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
            else if (talents.declaration == "Biochemistry")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), bc2bg, thestylebro);
                for (int c = 0; c < talents.Biolchem_Talents.Length; c++)
                {
                    if (talents.Biolchem_Talents[c].tier == 2)
                    {
                        if (talents.Biolchem_Talents[c].visible)
                        {
                            if (!talents.Biolchem_Talents[c].gotten)
                            {
                                if (talents.Biolchem_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Biolchem_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Biolchem_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'b');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Biolchem_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Biolchem_Talents[c].itemReq);
                                    }
                                    if (talents.Biolchem_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Biolchem_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(talents.Biolchem_Talents[c].icon, talents.Biolchem_Talents[c].name +
                                        "\n\n" + talents.Biolchem_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Biolchem_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].reachable)
                                        {
                                            if (talents.Biolchem_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'b'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Biolchem_Talents[c].pos, talents.Biolchem_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
        }
        if (t3o)
        {
            GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 240, 620, 20), "Lab Desk - " + title + " - Tier 3", h);
            if (talents.declaration == "Inorganic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), inorg3bg, thestylebro);
                for (int c = 0; c < talents.Inorganic_Talents_t1.Length; c++)
                {
                    if (talents.Inorganic_Talents_t1[c].tier == 3)
                    {
                        if (talents.Inorganic_Talents_t1[c].visible)
                        {
                            if (!talents.Inorganic_Talents_t1[c].gotten)
                            {
                                if (talents.Inorganic_Talents_t1[c].mystery)
                                {
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'i');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Inorganic_Talents_t1[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Inorganic_Talents_t1[c].itemReq);
                                    }
                                    if (talents.Inorganic_Talents_t1[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Inorganic_Talents_t1[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(talents.Inorganic_Talents_t1[c].icon, talents.Inorganic_Talents_t1[c].name +
                                        "\n\n" + talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Inorganic_Talents_t1[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].reachable)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'i'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Inorganic_Talents_t1[c].pos, talents.Inorganic_Talents_t1[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
            else if (talents.declaration == "Organic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), org3bg, thestylebro);
                for (int c = 0; c < talents.Organic_Talents.Length; c++)
                {
                    if (talents.Organic_Talents[c].tier == 3)
                    {
                        if (talents.Organic_Talents[c].visible)
                        {
                            if (!talents.Organic_Talents[c].gotten)
                            {
                                if (talents.Organic_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Organic_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Organic_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'o');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Organic_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Organic_Talents[c].itemReq);
                                    }
                                    if (talents.Organic_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Organic_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(talents.Organic_Talents[c].icon, talents.Organic_Talents[c].name +
                                        "\n\n" + talents.Organic_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Organic_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].reachable)
                                        {
                                            if (talents.Organic_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'o'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Organic_Talents[c].pos, talents.Organic_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
            else if (talents.declaration == "Biochemistry")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), bc3bg, thestylebro);
                for (int c = 0; c < talents.Biolchem_Talents.Length; c++)
                {
                    if (talents.Biolchem_Talents[c].tier == 3)
                    {
                        if (talents.Biolchem_Talents[c].visible)
                        {
                            if (!talents.Biolchem_Talents[c].gotten)
                            {
                                if (talents.Biolchem_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Biolchem_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Biolchem_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'b');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Biolchem_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Biolchem_Talents[c].itemReq);
                                    }
                                    if (talents.Biolchem_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Biolchem_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(talents.Biolchem_Talents[c].icon, talents.Biolchem_Talents[c].name +
                                        "\n\n" + talents.Biolchem_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Biolchem_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].reachable)
                                        {
                                            if (talents.Biolchem_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'b'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Biolchem_Talents[c].pos, talents.Biolchem_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
        }
        if (t4o)
        {
            GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 240, 620, 20), "Lab Desk - " + title + " - Tier 4", h);
            if (talents.declaration == "Inorganic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), inorg4bg, thestylebro);
                for (int c = 0; c < talents.Inorganic_Talents_t1.Length; c++)
                {
                    if (talents.Inorganic_Talents_t1[c].tier == 4)
                    {
                        if (talents.Inorganic_Talents_t1[c].visible)
                        {
                            if (!talents.Inorganic_Talents_t1[c].gotten)
                            {
                                if (talents.Inorganic_Talents_t1[c].mystery)
                                {
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'i');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Inorganic_Talents_t1[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Inorganic_Talents_t1[c].itemReq);
                                    }
                                    if (talents.Inorganic_Talents_t1[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Inorganic_Talents_t1[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(talents.Inorganic_Talents_t1[c].icon, talents.Inorganic_Talents_t1[c].name +
                                        "\n\n" + talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Inorganic_Talents_t1[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].reachable)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'i'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Inorganic_Talents_t1[c].pos, talents.Inorganic_Talents_t1[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
            else if (talents.declaration == "Organic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), org4bg, thestylebro);
                for (int c = 0; c < talents.Organic_Talents.Length; c++)
                {
                    if (talents.Organic_Talents[c].tier == 4)
                    {
                        if (talents.Organic_Talents[c].visible)
                        {
                            if (!talents.Organic_Talents[c].gotten)
                            {
                                if (talents.Organic_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Organic_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Organic_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'o');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Organic_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Organic_Talents[c].itemReq);
                                    }
                                    if (talents.Organic_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Organic_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(talents.Organic_Talents[c].icon, talents.Organic_Talents[c].name +
                                        "\n\n" + talents.Organic_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Organic_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].reachable)
                                        {
                                            if (talents.Organic_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'o'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Organic_Talents[c].pos, talents.Organic_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
            else if (talents.declaration == "Biochemistry")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), bc4bg, thestylebro);
                for (int c = 0; c < talents.Biolchem_Talents.Length; c++)
                {
                    if (talents.Biolchem_Talents[c].tier == 4)
                    {
                        if (talents.Biolchem_Talents[c].visible)
                        {
                            if (!talents.Biolchem_Talents[c].gotten)
                            {
                                if (talents.Biolchem_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Biolchem_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Biolchem_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'b');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Biolchem_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Biolchem_Talents[c].itemReq);
                                    }
                                    if (talents.Biolchem_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Biolchem_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(talents.Biolchem_Talents[c].icon, talents.Biolchem_Talents[c].name +
                                        "\n\n" + talents.Biolchem_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Biolchem_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].reachable)
                                        {
                                            if (talents.Biolchem_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'b'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Biolchem_Talents[c].pos, talents.Biolchem_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
        }
        if (t5o)
        {
            GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 240, 620, 20), "Lab Desk - " + title + " - Tier 5", h);
            if (talents.declaration == "Inorganic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), inorg5bg,thestylebro);
                for (int c = 0; c < talents.Inorganic_Talents_t1.Length; c++)
                {
                    if (talents.Inorganic_Talents_t1[c].tier == 5)
                    {
                        if (talents.Inorganic_Talents_t1[c].visible)
                        {
                            if (!talents.Inorganic_Talents_t1[c].gotten)
                            {
                                if (talents.Inorganic_Talents_t1[c].mystery)
                                {
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'i');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Inorganic_Talents_t1[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Inorganic_Talents_t1[c].itemReq);
                                    }
                                    if (talents.Inorganic_Talents_t1[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Inorganic_Talents_t1[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Inorganic_Talents_t1[c].pos, new GUIContent(talents.Inorganic_Talents_t1[c].icon, talents.Inorganic_Talents_t1[c].name +
                                        "\n\n" + talents.Inorganic_Talents_t1[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Inorganic_Talents_t1[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Inorganic_Talents_t1[c].reachable)
                                        {
                                            if (talents.Inorganic_Talents_t1[c].itemReq == -1)
                                            {
                                                if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'i'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Inorganic_Talents_t1[c].spellReq == -1)
                                                    {
                                                        if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'i'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Inorganic_Talents_t1[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'i'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Inorganic_Talents_t1[c].pos, talents.Inorganic_Talents_t1[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
            else if (talents.declaration == "Organic")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), org5bg, thestylebro);
                for (int c = 0; c < talents.Organic_Talents.Length; c++)
                {
                    if (talents.Organic_Talents[c].tier == 5)
                    {
                        if (talents.Organic_Talents[c].visible)
                        {
                            if (!talents.Organic_Talents[c].gotten)
                            {
                                if (talents.Organic_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Organic_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Organic_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'o');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Organic_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Organic_Talents[c].itemReq);
                                    }
                                    if (talents.Organic_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Organic_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Organic_Talents[c].pos, new GUIContent(talents.Organic_Talents[c].icon, talents.Organic_Talents[c].name +
                                        "\n\n" + talents.Organic_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Organic_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Organic_Talents[c].reachable)
                                        {
                                            if (talents.Organic_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Organic_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'o'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Organic_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Organic_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'o'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Organic_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Organic_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'o'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Organic_Talents[c].pos, talents.Organic_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
            else if (talents.declaration == "Biochemistry")
            {
                GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 440), bc5bg, thestylebro);
                for (int c = 0; c < talents.Biolchem_Talents.Length; c++)
                {
                    if (talents.Biolchem_Talents[c].tier == 5)
                    {
                        if (talents.Biolchem_Talents[c].visible)
                        {
                            if (!talents.Biolchem_Talents[c].gotten)
                            {
                                if (talents.Biolchem_Talents[c].mystery)
                                {
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(mysteryicon, "Possible Unknown Product\n\n" +
                                        talents.Biolchem_Talents[c].cost + " Joules required.\n\nUnknown Reactants Needed"), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].itemReq == -1)
                                        {
                                            if (talents.Biolchem_Talents[c].spellReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                        Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                        msgc.message = "Perform Experiment?";
                                                        confirming(c, 'b');
                                                        messaging = true; d = c;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.SpellCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Begin Research?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!messaging)
                                                    {
                                                        // need another spell
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require some spell reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasit = false;
                                            for (int d = 0; d < spells.ItemCount; d++)
                                            {
                                                if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                {
                                                    hasit = true;
                                                }
                                            }
                                            if (hasit)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Confirm Unlock?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit2 = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit2 = true;
                                                        }
                                                    }
                                                    if (hasit2)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin chemical analysis?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need a particular spell AND an item
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require some chemical reagent and some spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // need a particular item
                                                if (!messaging)
                                                {
                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                    ohyes.message = "You still require some chemical reagent to make this.";
                                                    messaging = true;
                                                }
                                            }
                                        }
                                    }
                                    GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                                }
                                else
                                {
                                    string itreq = "No First Reagent";
                                    string spreq = "No Second Reagent";
                                    if (talents.Biolchem_Talents[c].itemReq != -1)
                                    {
                                        itreq = spells.getItemName(talents.Biolchem_Talents[c].itemReq);
                                    }
                                    if (talents.Biolchem_Talents[c].spellReq != -1)
                                    {
                                        spreq = spells.getSpellName(talents.Biolchem_Talents[c].spellReq);
                                    }
                                    if (GUI.Button(talents.Biolchem_Talents[c].pos, new GUIContent(talents.Biolchem_Talents[c].icon, talents.Biolchem_Talents[c].name +
                                        "\n\n" + talents.Biolchem_Talents[c].cost + " Joules required.\n\n" + itreq + " required.\n\n" + spreq + " required.\n\n" + talents.Biolchem_Talents[c].description), talentstyle))
                                    {
                                        AudioSource.PlayClipAtPoint(tap, transform.position);
                                        if (talents.Biolchem_Talents[c].reachable)
                                        {
                                            if (talents.Biolchem_Talents[c].itemReq == -1)
                                            {
                                                if (talents.Biolchem_Talents[c].spellReq == -1)
                                                {
                                                    if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                            Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                            msgc.message = "Start assays?";
                                                            confirming(c, 'b'); messaging = true; d = c;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bool hasit = false;
                                                    for (int d = 0; d < spells.SpellCount; d++)
                                                    {
                                                        if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                        {
                                                            hasit = true;
                                                        }
                                                    }
                                                    if (hasit)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Begin the procedure?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // need another spell
                                                        if (!messaging)
                                                        {
                                                            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                            ohyes.message = "You still require the spell reagent to make this.";
                                                            messaging = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool hasit = false;
                                                for (int d = 0; d < spells.ItemCount; d++)
                                                {
                                                    if (talents.Biolchem_Talents[c].itemReq == spells.getItemIDAt(d))
                                                    {
                                                        hasit = true;
                                                    }
                                                }
                                                if (hasit)
                                                {
                                                    if (talents.Biolchem_Talents[c].spellReq == -1)
                                                    {
                                                        if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                msgc.message = "Confirm chemical trial?";
                                                                confirming(c, 'b'); messaging = true; d = c;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool hasit2 = false;
                                                        for (int d = 0; d < spells.SpellCount; d++)
                                                        {
                                                            if (talents.Biolchem_Talents[c].spellReq == spells.getSpellAt(d))
                                                            {
                                                                hasit2 = true;
                                                            }
                                                        }
                                                        if (hasit2)
                                                        {
                                                            if (talents.Biolchem_Talents[c].cost <= playerscript.xp)
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject themsg = Instantiate(Resources.Load("Messages/YesNoMessage")) as GameObject;
                                                                    Message msgc = themsg.GetComponent(typeof(Message)) as Message;
                                                                    msgc.message = "Mix the reagents?";
                                                                    confirming(c, 'b'); messaging = true; d = c;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!messaging)
                                                                {
                                                                    GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                    OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                    ohyes.message = "You need more Joules to reach the Activation Energy of this reaction.";
                                                                    messaging = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // need a particular spell AND an item
                                                            if (!messaging)
                                                            {
                                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                                ohyes.message = "You lack the spell reagent and the chemical reagent to make this.";
                                                                messaging = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // need a particular item
                                                    if (!messaging)
                                                    {
                                                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                        ohyes.message = "You still require the chemical reagent to make this.";
                                                        messaging = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // not reachable!!!
                                            if (!messaging)
                                            {
                                                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                                                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                                                ohyes.message = "You cannot perform this experiment without performing the previous one.";
                                                messaging = true;
                                            }
                                        }
                                    }
                                }
                                GUI.Label(new Rect(1920 / 2 - 320 + 640, 1080 / 2 - 240, 180, 480), GUI.tooltip, tooltipstyle);
                            }
                            else
                            {
                                GUI.Box(talents.Biolchem_Talents[c].pos, talents.Biolchem_Talents[c].icon, talentstylegotten);
                            }
                        }
                    }
                }
            }
        }
        float topleftx = 1920 / 2 - 320;
        float toplefty = 1080 / 2 - 240;
        if (!talents.Declared)
        {
            GUI.Box(new Rect(1920 / 2 - 320, 1080 / 2 - 220, 640, 100), "Choose a particular study of chemistry to pursue. \nThe path you devote your studies to determines \nwhat spells you can unlock.", hugetext);
            if (GUI.Button(new Rect(topleftx + 120 - 40, toplefty + 130, 80, 80), org, buttonstyle))
            {
                AudioSource.PlayClipAtPoint(orgosound, transform.position);
                talents.Declared = true;
                talents.declaration = "Organic";
                title = "Organic Chemistry";
                t1o = true;
            }
            if (GUI.Button(new Rect(topleftx + 280, toplefty + 130, 80, 80), inorg, buttonstyle))
            {
                AudioSource.PlayClipAtPoint(inorgosound, transform.position);
                talents.Declared = true;
                talents.declaration = "Inorganic";
                title = "Inorganic Chemistry";
                t1o = true;
            }
            if (GUI.Button(new Rect(topleftx + 640 - 160, toplefty + 130, 80, 80), biochm, buttonstyle))
            {
                AudioSource.PlayClipAtPoint(biochemsound, transform.position);
                talents.Declared = true;
                talents.declaration = "Biochemistry";
                title = "Biochemistry";
                t1o = true;
            }
            GUIStyle i = h;
            i.fontSize = 11;
            i.alignment = TextAnchor.UpperLeft;
            GUI.Box(new Rect(topleftx + 20, toplefty + 220, 199, 260), "Organic chemists concern themselves with the properties of mainly carbon based molecules. Carbon, the giver of life and nature, is the most versatile element in existance. Chemists devote their entire lives to organic chemistry but still have yet to uncover all of its secrets. Organic chemists are known to utilize most organic compounds' explosive properties, creating massive explosions. Some have even utilized refridgerants to freeze their foes as well. It is the most popular field of chemistry due to its versatility.", i);
            GUI.Box(new Rect(topleftx + 221, toplefty + 220, 198, 260), "Most often forget that everything in the universe is chemistry. While most people choose to study carbon based chemistry, there are a select few who study everything else chemistry has to offer. Inorganic chemists are known for mixing metals and other elements scattered all over the periodic table to produce the finest weapons known to mankind. Unlike simple blacksmiths, however, inorganic chemists use their weapons as extensions of themselves. Many are also versed in manipulation of chemical batteries, which they use to shock and stun foes.", i);
            GUI.Box(new Rect(topleftx + 421, toplefty + 220, 199, 260), "Biochemistry is considered one of the most complex forms of chemistry there is, and because of this most biochemists are treated as extremely high members of society and as healers / kings / scholars. Most biochemists specialize in conjuring spells that have healing effects. Some also choose to design microorganisms and spells that can poison and infect their foes. Many biochemists throughout history turned psychotic as they strived to understand the chemistry that composes their very own bodies, and have commited many atrocities from cutting up living bodies to drinking the blood of others.", i);

        }
        else
        {
            if (GUI.Button(new Rect(1920 / 2 - 320, 1080 / 2 + 220, 128, 20), "Tier 1", buttonstyle) && !messaging)
            {
                AudioSource.PlayClipAtPoint(tap, transform.position);
                t1o = true; t2o = false; t3o = false; t4o = false; t5o = false;
            }
            if (GUI.Button(new Rect(1920 / 2 - 320 + 128, 1080 / 2 + 220, 128, 20), "Tier 2", buttonstyle) && !messaging)
            {
                AudioSource.PlayClipAtPoint(tap, transform.position);
                t1o = false; t2o = true; t3o = false; t4o = false; t5o = false;
            }
            if (GUI.Button(new Rect(1920 / 2 - 320 + 2 * 128, 1080 / 2 + 220, 128, 20), "Tier 3", buttonstyle) && !messaging)
            {
                AudioSource.PlayClipAtPoint(tap, transform.position);
                t1o = false; t2o = false; t3o = true; t4o = false; t5o = false;
            }
            if (GUI.Button(new Rect(1920 / 2 - 320 + 3 * 128, 1080 / 2 + 220, 128, 20), "Tier 4", buttonstyle) && !messaging)
            {
                AudioSource.PlayClipAtPoint(tap, transform.position);
                t1o = false; t2o = false; t3o = false; t4o = true; t5o = false;
            }
            if (GUI.Button(new Rect(1920 / 2 - 320 + 4 * 128, 1080 / 2 + 220, 128, 20), "Tier 5", buttonstyle) && !messaging)
            {
                AudioSource.PlayClipAtPoint(tap, transform.position);
                t1o = false; t2o = false; t3o = false; t4o = false; t5o = true;
            }
        }


        if (GUI.Button(new Rect(1920 / 2 + 300, 1080 / 2 - 240, 20, 20), closetext, closestyle) && !messaging)
        {
            AudioSource.PlayClipAtPoint(closesound, transform.position);
            Time.timeScale = 1.0f;
            playerscript.menuIsUp = false;
            playerscript.isStunned = false;
            Instantiate(Resources.Load("Saved"));
            Destroy(GameObject.FindGameObjectWithTag("ItemWin"));
            Destroy(gameObject);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Pause") && !messaging)
        {
            AudioSource.PlayClipAtPoint(closesound, transform.position);
            Time.timeScale = 1.0f;
            playerscript.menuIsUp = false;
            playerscript.isStunned = false;
            Instantiate(Resources.Load("Saved"));
            Destroy(GameObject.FindGameObjectWithTag("ItemWin"));
            Destroy(gameObject);
        }
            if (confirmed)
            {
                if (speci == 'i')
                {
                    // UNLOCK!
                    playerscript.xp -= talents.Inorganic_Talents_t1[d].cost;
                    talents.Inorganic_Talents_t1[d].gotten = true;
                    if (talents.Inorganic_Talents_t1[d].itemReq != -1)
                    {
                        int where = 0;
                        for (int e = 0; e < spells.ItemCount; e++)
                        {
                            if (talents.Inorganic_Talents_t1[d].itemReq == spells.getItemIDAt(e))
                            {
                                where = e;
                            }
                        }
                        spells.RemoveItem(where);
                    }
                    if (talents.Inorganic_Talents_t1[d].spellUnlock != -1)
                    {
                        spells.LearnSpell(talents.Inorganic_Talents_t1[d].spellUnlock);
                    }
                    if (talents.Inorganic_Talents_t1[d].itemUnlock != -1)
                    {
                        spells.AddItem(talents.Inorganic_Talents_t1[d].itemUnlock);
                    }
                    if (talents.Inorganic_Talents_t1[d].treeUnlock != -1)
                    {
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeUnlock].reachable = true;
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeUnlock].visible = true;
                    }
                    if (talents.Inorganic_Talents_t1[d].treeUnlock2 != -1)
                    {
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeUnlock2].reachable = true;
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeUnlock2].visible = true;
                    }
                    if (talents.Inorganic_Talents_t1[d].treeUnlock3 != -1)
                    {
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeUnlock3].reachable = true;
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeUnlock3].visible = true;
                    }
                    if (talents.Inorganic_Talents_t1[d].treeMysteryUnlock1 != -1)
                    {
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeMysteryUnlock1].reachable = true;
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeMysteryUnlock1].visible = true;
                    }
                    if (talents.Inorganic_Talents_t1[d].treeMysteryUnlock2 != -1)
                    {
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeMysteryUnlock2].reachable = true;
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeMysteryUnlock2].visible = true;
                    }
                    if (talents.Inorganic_Talents_t1[d].treeMysteryUnlock3 != -1)
                    {
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeMysteryUnlock3].reachable = true;
                        talents.Inorganic_Talents_t1[talents.Inorganic_Talents_t1[d].treeMysteryUnlock3].visible = true;
                    }
                    confirmed = false;
                }
                else if (speci == 'o')
                {
                    // UNLOCK!
                    playerscript.xp -= talents.Organic_Talents[d].cost;
                    talents.Organic_Talents[d].gotten = true;
                    if (talents.Organic_Talents[d].itemReq != -1)
                    {
                        int where = 0;
                        for (int e = 0; e < spells.ItemCount; e++)
                        {
                            if (talents.Organic_Talents[d].itemReq == spells.getItemIDAt(e))
                            {
                                where = e;
                            }
                        }
                        spells.RemoveItem(where);
                    }
                    if (talents.Organic_Talents[d].spellUnlock != -1)
                    {
                        spells.LearnSpell(talents.Organic_Talents[d].spellUnlock);
                    }
                    if (talents.Organic_Talents[d].itemUnlock != -1)
                    {
                        spells.AddItem(talents.Organic_Talents[d].itemUnlock);
                    }
                    if (talents.Organic_Talents[d].treeUnlock != -1)
                    {
                        talents.Organic_Talents[talents.Organic_Talents[d].treeUnlock].reachable = true;
                        talents.Organic_Talents[talents.Organic_Talents[d].treeUnlock].visible = true;
                    }
                    if (talents.Organic_Talents[d].treeUnlock2 != -1)
                    {
                        talents.Organic_Talents[talents.Organic_Talents[d].treeUnlock2].reachable = true;
                        talents.Organic_Talents[talents.Organic_Talents[d].treeUnlock2].visible = true;
                    }
                    if (talents.Organic_Talents[d].treeUnlock3 != -1)
                    {
                        talents.Organic_Talents[talents.Organic_Talents[d].treeUnlock3].reachable = true;
                        talents.Organic_Talents[talents.Organic_Talents[d].treeUnlock3].visible = true;
                    }
                    if (talents.Organic_Talents[d].treeMysteryUnlock1 != -1)
                    {
                        talents.Organic_Talents[talents.Organic_Talents[d].treeMysteryUnlock1].reachable = true;
                        talents.Organic_Talents[talents.Organic_Talents[d].treeMysteryUnlock1].visible = true;
                    }
                    if (talents.Organic_Talents[d].treeMysteryUnlock2 != -1)
                    {
                        talents.Organic_Talents[talents.Organic_Talents[d].treeMysteryUnlock2].reachable = true;
                        talents.Organic_Talents[talents.Organic_Talents[d].treeMysteryUnlock2].visible = true;
                    }
                    if (talents.Organic_Talents[d].treeMysteryUnlock3 != -1)
                    {
                        talents.Organic_Talents[talents.Organic_Talents[d].treeMysteryUnlock3].reachable = true;
                        talents.Organic_Talents[talents.Organic_Talents[d].treeMysteryUnlock3].visible = true;
                    }
                    confirmed = false;
                }
                else
                {
                    // UNLOCK!
                    playerscript.xp -= talents.Biolchem_Talents[d].cost;
                    talents.Biolchem_Talents[d].gotten = true;
                    if (talents.Biolchem_Talents[d].itemReq != -1)
                    {
                        int where = 0;
                        for (int e = 0; e < spells.ItemCount; e++)
                        {
                            if (talents.Biolchem_Talents[d].itemReq == spells.getItemIDAt(e))
                            {
                                where = e;
                            }
                        }
                        spells.RemoveItem(where);
                    }
                    if (talents.Biolchem_Talents[d].spellUnlock != -1)
                    {
                        spells.LearnSpell(talents.Biolchem_Talents[d].spellUnlock);
                    }
                    if (talents.Biolchem_Talents[d].itemUnlock != -1)
                    {
                        spells.AddItem(talents.Biolchem_Talents[d].itemUnlock);
                    }
                    if (talents.Biolchem_Talents[d].treeUnlock != -1)
                    {
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeUnlock].reachable = true;
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeUnlock].visible = true;
                    }
                    if (talents.Biolchem_Talents[d].treeUnlock2 != -1)
                    {
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeUnlock2].reachable = true;
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeUnlock2].visible = true;
                    }
                    if (talents.Biolchem_Talents[d].treeUnlock3 != -1)
                    {
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeUnlock3].reachable = true;
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeUnlock3].visible = true;
                    }
                    if (talents.Biolchem_Talents[d].treeMysteryUnlock1 != -1)
                    {
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeMysteryUnlock1].reachable = true;
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeMysteryUnlock1].visible = true;
                    }
                    if (talents.Biolchem_Talents[d].treeMysteryUnlock2 != -1)
                    {
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeMysteryUnlock2].reachable = true;
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeMysteryUnlock2].visible = true;
                    }
                    if (talents.Biolchem_Talents[d].treeMysteryUnlock3 != -1)
                    {
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeMysteryUnlock3].reachable = true;
                        talents.Biolchem_Talents[talents.Biolchem_Talents[d].treeMysteryUnlock3].visible = true;
                    }
                    confirmed = false;
                }
            }
            else
            {
                confirmed = false;
            }
    }
}
