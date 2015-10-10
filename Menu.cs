using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
    private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;
    private GameObject player;
    private PlayerController playerscript;
    private Spells spells;
    private float vol;
    public GUIStyle theStyle;
    public GUIStyle StyleDark;
    public GUIStyle ButtonStyle;
    public GUIStyle ItemStyle, styleSmall, iconStyle, closeStyle;
    public AudioClip tap, open;

    // Draw a rectangle
    public static void GUIDrawRect(Rect position, Color color)
    {
        if (_staticRectTexture == null)
        {
            _staticRectTexture = new Texture2D(1, 1);
        }

        if (_staticRectStyle == null)
        {
            _staticRectStyle = new GUIStyle();
        }

        _staticRectTexture.SetPixel(0, 0, color);
        _staticRectTexture.Apply();

        _staticRectStyle.normal.background = _staticRectTexture;

        GUI.Box(position, GUIContent.none, _staticRectStyle);


    }

	// Use this for initialization
	void Start () {
        AudioSource.PlayClipAtPoint(open, transform.position);
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
        spells = player.GetComponent(typeof(Spells)) as Spells;
        vol = playerscript.gameVolume;
        qual = playerscript.gameGraphics;
        spells.SortItems();
    }

    public Texture2D bgtext;
    public Texture2D buttonbagtx;
    public Texture2D buttonspellstx;
    public Texture2D buttonequiptx, buttonsettx, buttonbooktx;
    public Texture2D bgtext2, buttonbagtx2, buttonspellstx2, buttonequiptx2, buttonsettx2, buttonbooktx2;
    public Texture2D closetext;
    private bool bagOpen = false , spellOpen = false, equipOpen = false, bookOpen = false, setOpen = false;
    private Texture2D cBagTex, cSpellTex, cEquipTex, cSetTex, cBookTex;
    private float qual;
    private bool drawItem = false;

    public GUIStyle leftarr, rightarr;
    int bagpage = 0;
    int equippage = 0;
    int spellpage = 0;
    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUI.depth = 0;
                if (playerscript.gender == "female")
        {
            GUI.DrawTexture(new Rect(1920 / 2 - 500, 1080 / 2 - 360, 360, 640), playerscript.charbasefemale);
            GUI.DrawTexture(new Rect(1920 / 2 - 535 + 102 / 3, 1080 / 2 - 461 + 11 / 3, playerscript.hatbigsprite.width/3, playerscript.hatbigsprite.height/3), playerscript.hatbigsprite);
        }
        else if (playerscript.gender == "male")
        {
            GUI.DrawTexture(new Rect(1920 / 2 - 500, 1080 / 2 - 360, 360, 640), playerscript.charbasemale);
            GUI.DrawTexture(new Rect(1920 / 2 - 533 + 166 / 3, 1080 / 2 -466 + 120 / 3, playerscript.hatbigsprite.width / 3, playerscript.hatbigsprite.height / 3), playerscript.hatbigsprite);
        }
        else
        {
            print("Unable to determine your gender.");
        }
        GUI.DrawTexture(new Rect(1920 / 2 - 252, 1080 / 2 - 152, 505, 304), menuborder);
        GUI.Box(new Rect(1920 / 2 - 250, 1080 / 2 - 150, 500, 300), "", theStyle);
        if (bagOpen)
        {
            GUI.Box(new Rect(1920 / 2 - 170, 1080 / 2 - 150, 400, 20), "Bag", StyleDark);
            GUI.Box(new Rect(1920 / 2 - 170, 1080 / 2 + 120, 420, 20), "Bag Space: " + spells.ItemCount + "/" + spells.bagSpace + ", Page: " + (bagpage + 1) + "/5", StyleDark);
            int xpos = 1920 / 2 - 160;
            int ypos = 1080 / 2 - 130;
            if (bagpage > 0)
            {
                if (GUI.Button(new Rect(1920 / 2 - 165, 1080 / 2  + 120, 25, 25),"",leftarr))
                {
                    bagpage -= 1;
                }

            }
            if (bagpage < 4)
            {
                if (GUI.Button(new Rect(1920 / 2 - 170 + 385, 1080 / 2 + 120, 25, 25), "", rightarr))
                {
                    bagpage += 1;
                }
            }
            for (int i = 0 + bagpage*40; i < 40 + bagpage*40; i++)
            {
                Texture2D itemtext;
                if (i < spells.ItemCount)
                {
                    itemtext = spells.getItemSprite(spells.getItemIDAt(i));
                    if (GUI.Button(new Rect(xpos, ypos, 50, 50), itemtext, ItemStyle))
                    {
                        Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
                            int count = GameObject.FindGameObjectsWithTag("ItemWin").Length;
                            if (count > 0)
                            {
                                Destroy(GameObject.FindGameObjectWithTag("ItemWin"));
                            }
                            GameObject ItemDescrip = Instantiate(Resources.Load("UI/ItemWindow", typeof(GameObject))) as GameObject;
                            ItemWindowScript ItemScriptDescrip = (ItemWindowScript)ItemDescrip.GetComponent(typeof(ItemWindowScript));
                            ItemScriptDescrip.ID = spells.getItemIDAt(i);
                            ItemScriptDescrip.loc = i;
                    }
                }

                xpos += 50;
                if(xpos > 1920 / 2 + 190){
                    xpos = 1920 / 2 - 160;
                    ypos +=50;
                }
            }
        }
        else if (spellOpen)
        {
            GUI.Box(new Rect(1920 / 2 - 170, 1080 / 2 - 150, 400, 20), "Spells", StyleDark);
            GUI.Box(new Rect(1920 / 2 - 170, 1080 / 2 + 120, 420, 20), "Spell Space: " + spells.SpellCount + "/" + spells.SpellSpace + ", Page: " + (spellpage + 1) + "/5", StyleDark);
            int xpos = 1920 / 2 - 160;
            int ypos = 1080 / 2 - 130;
            if (spellpage > 0)
            {
                if (GUI.Button(new Rect(1920 / 2 - 165, 1080 / 2 + 120, 25, 25), "", leftarr))
                {
                    spellpage -= 1;
                }

            }
            if (spellpage < 4)
            {
                if (GUI.Button(new Rect(1920 / 2 - 170 + 385, 1080 / 2 + 120, 25, 25), "", rightarr))
                {
                    spellpage += 1;
                }
            }
            for (int i = 0 + spellpage*40; i < 40 + spellpage*40; i++)
            {
                Texture2D spelltext;
                if (i < spells.SpellCount)
                {
                    spelltext = spells.getSpellUI_Icon(spells.getSpellAt(i));
                    if (GUI.Button(new Rect(xpos, ypos, 50, 50), spelltext, ItemStyle))
                    {
                        Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
                        int count = GameObject.FindGameObjectsWithTag("ItemWin").Length;
                        if (count > 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("ItemWin"));
                        }
                        GameObject SpellWin = Instantiate(Resources.Load("UI/SpellWindow", typeof(GameObject))) as GameObject;
                        SpellWindow SpellWinScript = (SpellWindow)SpellWin.GetComponent(typeof(SpellWindow));
                        SpellWinScript.ID = spells.getSpellAt(i);
                        SpellWinScript.loc = i;
                    }
                }
                else
                {
                }

                xpos += 50;
                if (xpos > 1920 / 2 + 190)
                {
                    xpos = 1920 / 2 - 160;
                    ypos += 50;
                }
            }
        }
        else if (equipOpen)
        {
            GUI.Box(new Rect(1920 / 2 - 170, 1080 / 2 - 150, 400, 20), "Equipment", StyleDark);

            GUI.Label(new Rect(1920 / 2 + 110, 1080 / 2 - 130, 140, 25), "Headwear", StyleDark);
            GUI.Box(new Rect(1920 / 2 + 180 - 25, 1080 / 2 - 100, 50, 50), spells.getHatIcon(spells.EquippedHat), iconStyle);

            GUI.Label(new Rect(1920 / 2 + 110, 1080 / 2 - 40, 140, 25), "Tunic", StyleDark);
            GUI.Box(new Rect(1920 / 2 + 180 - 25, 1080 / 2 - 10, 50, 50), spells.getTunicIcon(spells.EquippedTunic), iconStyle);

            GUI.Label(new Rect(1920 / 2 + 110, 1080 / 2 + 50, 140, 25), "Amulet", StyleDark);
            GUI.Box(new Rect(1920 / 2 + 180 - 25, 1080 / 2 + 80, 50, 50), spells.getAmuletIcon(spells.EquippedAmulet), iconStyle);

            GUI.Box(new Rect(1920 / 2 - 170, 1080 / 2 + 120, 420, 20), "Equipment Page: " + (equippage + 1) + "/10", StyleDark);
            if (equippage > 0)
            {
                if (GUI.Button(new Rect(1920 / 2 - 165, 1080 / 2 + 120, 25, 25), "", leftarr))
                {
                    equippage -= 1;
                }

            }
            if (equippage < 9)
            {
                if (GUI.Button(new Rect(1920 / 2 - 170 + 385, 1080 / 2 + 120, 25, 25), "", rightarr))
                {
                    equippage += 1;
                }
            }
            int xpos = 1920 / 2 - 160;
            int ypos = 1080 / 2 - 120;

            for (int i = 0 + equippage*5; i < 5 + equippage*5; i++)
            {
                Texture2D hatText;
                if (i < spells.HatCount)
                {
                    hatText = spells.getHatIcon(spells.getHatIDAt(i));
                    if (GUI.Button(new Rect(xpos, ypos, 50, 50), hatText, ItemStyle))
                    {
                        Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
                        int count = GameObject.FindGameObjectsWithTag("ItemWin").Length;
                        if (count > 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("ItemWin"));
                        }
                        GameObject SpellWin = Instantiate(Resources.Load("UI/EquipWindow", typeof(GameObject))) as GameObject;
                        EquipWindow SpellWinScript = (EquipWindow)SpellWin.GetComponent(typeof(EquipWindow));
                        SpellWinScript.ID = spells.getHatIDAt(i);
                        SpellWinScript.loc = i;
                        SpellWinScript.type = 0;
                    }
                }
                else { }

                xpos += 50;
                if (xpos > 1920 / 2 - 155)
                {
                    xpos = 1920 / 2 - 160;
                    ypos += 50;
                }
            }


            xpos = 1920 / 2 - 100;
            ypos = 1080 / 2 - 120;

            for (int i = 0 + equippage*10; i < 10 + equippage*10; i++)
            {
                Texture2D tunicText;
                if (i < spells.TunicCount)
                {
                    tunicText = spells.getTunicIcon(spells.getTunicIDAt(i));
                    if (GUI.Button(new Rect(xpos, ypos, 50, 50), tunicText, ItemStyle))
                    {
                        Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
                        int count = GameObject.FindGameObjectsWithTag("ItemWin").Length;
                        if (count > 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("ItemWin"));
                        }
                        GameObject SpellWin = Instantiate(Resources.Load("UI/EquipWindow", typeof(GameObject))) as GameObject;
                        EquipWindow SpellWinScript = (EquipWindow)SpellWin.GetComponent(typeof(EquipWindow));
                        SpellWinScript.ID = spells.getTunicIDAt(i);
                        SpellWinScript.loc = i;
                        SpellWinScript.type = 1;
                    }
                }
                else { }

                xpos += 50;
                if (xpos > 1920 / 2 - 50)
                {
                    xpos = 1920 / 2 - 100;
                    ypos += 50;
                }
            }


            xpos = 1920 / 2 + 10;
            ypos = 1080 / 2 - 120;

            for (int i = 0 + equippage*10; i < 10 + equippage*10; i++)
            {
                Texture2D amuletText;
                if (i < spells.AmuletCount)
                {
                    amuletText = spells.getAmuletIcon(spells.getAmuletIDAt(i));
                    if (GUI.Button(new Rect(xpos, ypos, 50, 50), amuletText, ItemStyle))
                    {
                        Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
                        int count = GameObject.FindGameObjectsWithTag("ItemWin").Length;
                        if (count > 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("ItemWin"));
                        }
                        GameObject SpellWin = Instantiate(Resources.Load("UI/EquipWindow", typeof(GameObject))) as GameObject;
                        EquipWindow SpellWinScript = (EquipWindow)SpellWin.GetComponent(typeof(EquipWindow));
                        SpellWinScript.ID = spells.getAmuletIDAt(i);
                        SpellWinScript.loc = i;
                        SpellWinScript.type = 2;
                    }
                }
                else { }

                xpos += 50;
                if (xpos > 1920 / 2 + 70)
                {
                    xpos = 1920 / 2 + 10;
                    ypos += 50;
                }
            }

        }
        else if (bookOpen)
        {
            GUI.Box(new Rect(1920 / 2 - 170, 1080 / 2 - 150, 400, 20), "Level " + playerscript.level, StyleDark);
            GUI.Box(new Rect(1920 / 2 - 150, 1080 / 2 - 130, 380, 25), playerscript.name, StyleDark);
            GUI.Box(new Rect(1920 / 2 - 150, 1080 / 2 - 105, 100, 25), "Health:", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 50, 1080 / 2 - 105, 280, 25), playerscript.GetCurrentHealth() + " / " + playerscript.getMaxHealth(), styleSmall);
            GUI.Box(new Rect(1920 / 2 - 150, 1080 / 2 - 80, 100, 25), "Stamina:", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 50, 1080 / 2 - 80, 280, 25), playerscript.stamina + "", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 150, 1080 / 2 - 55, 100, 25), "Entropy:", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 50, 1080 / 2 - 55, 280, 25), playerscript.xp + " J", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 150, 1080 / 2 - 25, 100, 25), "Spell Resist:", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 50, 1080 / 2 - 25, 280, 25), playerscript.magResist * 100 + "%", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 150, 1080 / 2, 100, 25), "HP Regen:", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 50, 1080 / 2, 280, 25), (1 / playerscript.HPspeed).ToString("F2") + " HP/sec", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 150, 1080 / 2 + 25, 100, 25), "Stam Regen:", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 50, 1080 / 2 + 25, 280, 25), (1 / playerscript.staminaRecoverySpeed).ToString("F2") + " Stam/sec", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 150, 1080 / 2 + 50, 100, 25), "Mvmnt Spd:", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 50, 1080 / 2 + 50, 280, 25), ((playerscript.moveSpeed / playerscript.moveSpeedNaked) * 100).ToString("F1") + "%", styleSmall);
            GUI.Box(new Rect(1920 / 2 - 150, 1080 / 2 + 85, 50, 50), spells.getTunicIcon(spells.EquippedTunic), iconStyle);
            GUI.Box(new Rect(1920 / 2 - 150 + 190 - 25, 1080 / 2 + 85, 50, 50), spells.getHatIcon(spells.EquippedHat), iconStyle);
            GUI.Box(new Rect(1920 / 2 + 230 - 50, 1080 / 2 + 85, 50, 50), spells.getAmuletIcon(spells.EquippedAmulet), iconStyle);
        }
        else if (setOpen)
        {
            GUI.Box(new Rect(1920 / 2 - 170, 1080 / 2 - 150, 400, 20), "Settings", StyleDark);
            GUI.Box(new Rect(1920 / 2 - 20, 1080 / 2 - 110, 100, 20), "Volume", StyleDark);
            vol = GUI.HorizontalSlider(new Rect(1920 / 2 - 40, 1080 / 2 - 80, 140, 20), vol, 0f, 1f);
            playerscript.gameVolume = vol;
            GUI.Box(new Rect(1920 / 2 - 20, 1080 / 2 - 60, 100, 20), "Graphics", StyleDark);
            var qualities = QualitySettings.names;
            qual = GUI.HorizontalSlider(new Rect(1920 / 2 - 40, 1080 / 2 - 30, 140, 20), qual, 0f, qualities.Length);
            QualitySettings.SetQualityLevel(Mathf.CeilToInt(qual), true);
            playerscript.gameGraphics = qual;
            if (GUI.Button(new Rect(1920 / 2 - 40, 1080 / 2, 140, 100), "View Controls", StyleDark))
            {
                Instantiate(Resources.Load("UI/Controls"));
            }
            if (GUI.Button(new Rect(1920 / 2 - 40, 1080 / 2 + 110, 140, 25), "Quit Game", StyleDark))
            {
                Application.Quit();
            }
        }
        else
        {
            GUI.Box(new Rect(1920 / 2 - 170, 1080 / 2 - 150, 400, 20), "Menu", StyleDark);
        }

        if (!bagOpen) cBagTex = buttonbagtx; else cBagTex = buttonbagtx2;
        if (!spellOpen) cSpellTex = buttonspellstx; else cSpellTex = buttonspellstx2;
        if (!equipOpen) cEquipTex = buttonequiptx; else cEquipTex = buttonequiptx2;
        if (!bookOpen) cBookTex = buttonbooktx; else cBookTex = buttonbooktx2;
        if (!setOpen) cSetTex = buttonsettx; else cSetTex = buttonsettx2;
        if (GUI.Button(new Rect(1920 / 2 - 250, 1080 / 2 - 150, 80, 60), cBagTex, ButtonStyle))
        {
            Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
            bagOpen = true; spellOpen = false; equipOpen = false; bookOpen = false; setOpen = false;
        }
        if (GUI.Button(new Rect(1920 / 2 - 250, 1080 / 2 - 90, 80, 60), cSpellTex, ButtonStyle))
        {
            Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
            spellOpen = true; bagOpen = false; equipOpen = false; bookOpen = false; setOpen = false;
        }
        if (GUI.Button(new Rect(1920 / 2 - 250, 1080 / 2 - 30, 80, 60), cEquipTex, ButtonStyle))
        {
            Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
            equipOpen = true; bagOpen = false; spellOpen = false; bookOpen = false; setOpen = false;
        }
        if (GUI.Button(new Rect(1920 / 2 - 250, 1080 / 2 + 30, 80, 60), cBookTex, ButtonStyle))
        {
            Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
            bookOpen = true; bagOpen = false; spellOpen = false; equipOpen = false; setOpen = false;
        }
        if (GUI.Button(new Rect(1920 / 2 - 250, 1080 / 2 + 90, 80, 60), cSetTex, ButtonStyle))
        {
            Time.timeScale = 1; AudioSource.PlayClipAtPoint(tap, transform.position); 
            setOpen = true; bagOpen = false; spellOpen = false; equipOpen = false; bookOpen = false;
        }

        if (GUI.Button(new Rect(1920 / 2 + 230, 1080 / 2 - 150, 20, 20), closetext, closeStyle))
        {

            Time.timeScale = 1; AudioSource.PlayClipAtPoint(open, transform.position); 
            setOpen = false; bagOpen = false; spellOpen = false; equipOpen = false; bookOpen = false;
            Time.timeScale = 1.0f;
            playerscript.menuIsUp = false;
            Destroy(GameObject.FindGameObjectWithTag("ItemWin"));
            Destroy(gameObject);
        }
    }

    public Texture2D menuborder;
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            AudioSource.PlayClipAtPoint(open, transform.position);
            setOpen = false; bagOpen = false; spellOpen = false; equipOpen = false; bookOpen = false;
            Time.timeScale = 1.0f;
            playerscript.menuIsUp = false;
            playerscript.cantcast = false;
            Destroy(GameObject.FindGameObjectWithTag("ItemWin"));
            Destroy(gameObject);
        }
	}
}
