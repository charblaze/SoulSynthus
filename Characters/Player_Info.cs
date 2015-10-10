using UnityEngine;
using System.Collections;

public class Player_Info : MonoBehaviour {
    private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;
    private GameObject player;
    private PlayerController playerscript;
    private Spells spells;
    private SpellCastScript spellcast;
    private GameObject flame;
    public GUIStyle countdown;
    public Sprite Default;
    public Sprite one;
    public Sprite two;
    public Sprite L;
    public Sprite R;

    private float timeL, timeR, time1, time2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = (PlayerController)player.GetComponent(typeof(PlayerController));
        spells = (Spells)player.GetComponent(typeof(Spells));
        spellcast = player.GetComponent(typeof(SpellCastScript)) as SpellCastScript;
        if (playerscript.getSpellL() != -1)
        timeL = spells.getSpellCD(playerscript.getSpellL());
        if (playerscript.getSpellR() != -1)
        timeR = spells.getSpellCD(playerscript.getSpellR());
        if (playerscript.getSpell1() != -1)
        time1 = spells.getSpellCD(playerscript.getSpell1());
        if (playerscript.getSpell2() != -1)
        time2 = spells.getSpellCD(playerscript.getSpell2());
    }

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

    void DrawStaminaBar()
    {
        Rect StaminaBarBG = new Rect(23, 48, playerscript.max_stamina + 4, 19);
        Rect StaminaBarBG2 = new Rect(22, 47, playerscript.max_stamina + 6, 21);
        Color bgcolor = new Color(0, 0, 0);

        GUIDrawRect(StaminaBarBG2, new Color(1, 1, 1));
        GUIDrawRect(StaminaBarBG, bgcolor);
        Rect StaminaBar = new Rect(25, 50, 100, 15);
        if (playerscript.GetStamina() > 0)
        {
            StaminaBar.width = playerscript.GetStamina();
        }
        else if (playerscript.GetStamina() < 0)
        {
            StaminaBar.width = 0;
            bgcolor = new Color(255, 0, 0);
            GUIDrawRect(StaminaBarBG, bgcolor);
        }
        else
        {
            StaminaBar.width = 0;
        }
        Color green = new Color(.055f,.6588f , 0);
        GUIDrawRect(StaminaBar, green);
    }

    void DrawHealthBar()
    {
        Rect HealthBarBG = new Rect(23, 23, playerscript.getMaxHealth()+4, 19);
        Rect HealthBarBG2 = new Rect(22, 22, playerscript.getMaxHealth() + 6, 21);
        Color bgcolor = new Color(0, 0, 0);
        GUIDrawRect(HealthBarBG2, new Color(0.7f, 0.1f, 0));
        GUIDrawRect(HealthBarBG, bgcolor);
        if (player == null)
        {
            Debug.Log("Unable to locate player to draw health bar");
        }
        Rect HealthBar = new Rect(25, 25, playerscript.getMaxHealth(), 15);
        if (playerscript.GetCurrentHealth() > 0)
        {
            HealthBar.width = playerscript.GetCurrentHealth();
        }
        else
        {
            HealthBar.width = 0;
        }
        Color green = new Color(0.6588f, 0.086f, 0);
        GUIDrawRect(HealthBar, green);
        if (playerscript.immunityFrames)
        {
            GUIDrawRect(HealthBar, new Color(1, 1, 1));
        }
    }

    IEnumerator Shade(Rect where)
    {
        GUIDrawRect(where, new Color(0, 0, 0, 30));
        yield return new WaitForSeconds(0.5f);
        yield break;
    }


    void DrawSpellLeftClick()
    {
        // also draw num heals
        


        Rect SpellLClickBorder = new Rect(1920 - 250, 1080-125, 100, 100);
        GUIDrawRect(SpellLClickBorder, new Color(0, 0, 0, 0));
        if (playerscript.getSpellL() == -1)
        {
            return;
        }
        Sprite toDraw = spells.getSpellIcon(playerscript.getSpellL());
        Texture t = toDraw.texture;
        GUI.DrawTexture(SpellLClickBorder, t);
        if (spellcast.LonCD)
        {

            GUIDrawRect(new Rect(1920 - 250, 1080 - 125, 100, 100 * (spellcast.Lr / spells.getSpellCD(playerscript.getSpellL()))), new Color(0, 0, 0, 0.5f));
            GUI.Label(new Rect(1920 - 250, 1080 - 125, 100, 20), "" + spellcast.Lr.ToString("F2"), countdown); 
        }
    }

    void DrawSpellQ()
    {
        Rect SpellLClickBorder = new Rect(275, 1080 - 125, 100, 100);
        GUIDrawRect(SpellLClickBorder, new Color(0, 0, 0, 0));
        if (playerscript.getSpellQ() == -1)
        {
            return;
        }
        Sprite toDraw = spells.getSpellIcon(playerscript.getSpellQ());
        Texture t = toDraw.texture;
        GUI.DrawTexture(SpellLClickBorder, t);
        if (spellcast.QonCD)
        {

            GUIDrawRect(new Rect(275, 1080 - 125, 100, 100 * (spellcast.Qr / spells.getSpellCD(playerscript.getSpellQ()))), new Color(0, 0, 0, 0.5f));
            GUI.Label(new Rect(275, 1080 - 125, 100, 20), "" + spellcast.Qr.ToString("F2"), countdown);
        }
    }

    void DrawSpellQ2()
    {
        Rect SpellLClickBorder = new Rect(400, 1080 - 125, 100, 100);
        GUIDrawRect(SpellLClickBorder, new Color(0, 0, 0, 0));
        if (playerscript.getSpellQ2() == -1)
        {
            return;
        }
        Sprite toDraw = spells.getSpellIcon(playerscript.getSpellQ2());
        Texture t = toDraw.texture;
        GUI.DrawTexture(SpellLClickBorder, t);
        if (spellcast.Q2onCD)
        {

            GUIDrawRect(new Rect(400, 1080 - 125, 100, 100 * (spellcast.Q2r / spells.getSpellCD(playerscript.getSpellQ2()))), new Color(0, 0, 0, 0.5f));
            GUI.Label(new Rect(400, 1080 - 125, 100, 20), "" + spellcast.Q2r.ToString("F2"), countdown);
        }
    }

    void DrawSpellRightClick()
    {
        Rect SpellRClickBorder = new Rect(1920 - 125, 1080 - 125, 100, 100);
        GUIDrawRect(SpellRClickBorder, new Color(0, 0, 0, 0));
        if (playerscript.getSpellR() == -1)
        {
            return;
        }
        Sprite toDraw = spells.getSpellIcon(playerscript.getSpellR());
        Texture t = toDraw.texture;
        GUI.DrawTexture(SpellRClickBorder, t);
        if (spellcast.RonCD)
        {
            GUIDrawRect(new Rect(1920 - 125, 1080 - 125, 100, 100 * (spellcast.Rr / spells.getSpellCD(playerscript.getSpellR()))), new Color(0, 0, 0, 0.5f));
            GUI.Label(new Rect(1920 - 125, 1080 - 125, 100, 20), "" + spellcast.Rr.ToString("F2"), countdown); 
        }
    }
    public Texture2D healthicon;
    public GUIStyle numhls;
    void DrawSpell1()
    {
        GUI.DrawTexture(new Rect(25, 1080 - 125 - 100 - 25, 100, 100), healthicon);
        int todraw = playerscript.numheals;
        if (todraw < 0)
        {
            todraw = 0;
        }
        GUI.Box(new Rect(25 + 62, 1080 - 125 - 100- 25 +5, 32, 28), todraw + "",numhls);


        Rect Spell1ClickBorder = new Rect(25, 1080 - 125, 100, 100);
        GUIDrawRect(Spell1ClickBorder, new Color(0, 0, 0,0));
        if (playerscript.getSpell1() == -1)
        {
            return;
        }
        Sprite toDraw = spells.getSpellIcon(playerscript.getSpell1());
        Texture t = toDraw.texture;
        GUI.DrawTexture(Spell1ClickBorder, t);
        if (spellcast.OneonCD)
        {
            GUIDrawRect(new Rect(25, 1080 - 125, 100, 100 * (spellcast.Or / spells.getSpellCD(playerscript.getSpell1()))), new Color(0, 0, 0, 0.5f));
            GUI.Label(new Rect(25, 1080 - 125, 100, 20), "" + spellcast.Or.ToString("F2"), countdown); 
        }
    }

    void DrawSpell2()
    {
        Rect Spell2ClickBorder = new Rect(150 , 1080-125, 100, 100);
        GUIDrawRect(Spell2ClickBorder, new Color(0, 0, 0,0));
        if (playerscript.getSpell2() == -1)
        {
            return;
        }
        Sprite toDraw = spells.getSpellIcon(playerscript.getSpell2());
        Texture t = toDraw.texture;
        GUI.DrawTexture(Spell2ClickBorder, t);
        if (spellcast.TwoonCD)
        {
            GUIDrawRect(new Rect(150, 1080 - 125, 100, 100 * (spellcast.Tr / spells.getSpellCD(playerscript.getSpell2()))), new Color(0, 0, 0, 0.5f));
            GUI.Label(new Rect(150, 1080 - 125, 100, 20), "" + spellcast.Tr.ToString("F2"), countdown); 
        }
    }
    public GUIStyle controlstyle;
    void DrawControls()
    {
        Rect rL = new Rect(115, 1080 - 135, 25, 25);
        GUI.Label(rL, "1", controlstyle);
        Rect rR = new Rect(240, 1080 - 135, 25, 25);
        GUI.Label(rR, "2", controlstyle);
        Rect r1 = new Rect(1920 - 160, 1080 - 135, 25, 25);
        GUI.Label(r1, "L", controlstyle);
        Rect r2 = new Rect(1920 - 35, 1080 - 135, 25, 25);
        GUI.Label(r2, "R", controlstyle);
        Rect qb = new Rect(365, 1080 - 135, 25, 25);
        GUI.Label(qb, "Q", controlstyle);
        Rect q2b = new Rect(490, 1080 - 135, 25, 25);
        GUI.Label(q2b, "R", controlstyle);
    }

    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        if (isE)
        {
            GUI.Label(new Rect(1920 / 2 - 100, 1080/2, 25, 25), "E", controlstyle);
        }
        GUI.depth = 1;
        DrawStaminaBar();
        DrawHealthBar();
        DrawSpellLeftClick();
        DrawSpellRightClick();
        DrawSpell1();
        DrawSpell2();
        DrawSpellQ();
        DrawSpellQ2();
        DrawControls();

    }

    private bool newCDR, newCDL, newCD1, newCD2;
    private bool isE = false;
    void Update()
    {
        Vector3 hit = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit2.collider != null && (player.transform.position - hit2.collider.transform.position).magnitude < 1)
        {
            if (hit2.collider.tag == "Inactive" || hit2.collider.tag == "Active")
            {
                isE = true;
            }
            else
            {
                isE = false;
            }
        }
        else
        {
            isE = false;
        }
        if (spellcast.RonCD)
        {
            if (newCDR)
            {
                timeR = spells.getSpellCD(playerscript.getSpellR());
                newCDR = false;
            }
            timeR -= Time.deltaTime;
        }
        if (timeR < 0)
        {
            timeR = 0;
            newCDR = true;
        }
        if (spellcast.LonCD)
        {
            if (newCDL)
            {
                timeL = spells.getSpellCD(playerscript.getSpellL());
                newCDL = false;
            }
            timeL -= Time.deltaTime;
        }
        if (timeL < 0)
        {
            timeL = 0;
            newCDL = true;
        }
        if (spellcast.OneonCD)
        {
            if (newCD1)
            {
                time1 = spells.getSpellCD(playerscript.getSpell1());
                newCD1 = false;
            }
            time1 -= Time.deltaTime;
        }
        if (time1 < 0)
        {
            time1 = 0;
            newCD1 = true;
        }
        if (spellcast.TwoonCD)
        {
            if (newCD2)
            {
                time2 = spells.getSpellCD(playerscript.getSpell2());
                newCD2 = false;
            }
            time2 -= Time.deltaTime;
        }
        if (time2 < 0)
        {
            time2 = 0;
            newCD2 = true;
        }
    }
}
