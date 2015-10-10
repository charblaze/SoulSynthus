using UnityEngine;
using System.Collections;

public class PlayerController : Character
{
    public AudioClip shuffle;
    public string name;
    public string gender = "female";
    public string CheckpointLevel = "";
    public string CheckpointName = "";
    public bool bagisFull = false;
    private bool isBackstepping = false;
    public GUIStyle fullstyle;
    public int equippedSpellL = -1;
    public int equippedSpellR = -1;
    public int equippedSpell1 = -1;
    public int equippedSpell2 = -1;
    public int equippedSpellQ = -1, equippedSpellQ2 = -1;
    public float xp = 0;
    Spells spells;
    public float gameVolume = .5f;
    public float gameGraphics = 3f;
    public Texture2D hatbigsprite;
    public Texture2D charbasemale, charbasefemale;
    public int level = 1;
    public int numheals = 0;
    public int savefile = 1;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        spells = (Spells)GetComponent(typeof(Spells));

        stamina = 100;
        moveSpeedOriginal = moveSpeed;
        StartCoroutine(Recover_Stamina_Passive());
        currentHealth = 100;
        maxHealth = 100;
        StartCoroutine(StartHealthRegen());
        recover = 2;
        isStunned = false;
        isMoving = false;
        stunDuration = 0;
        moveSpeedModifier = 0;
        maxHealthModifier = 0;
        magResistAdjust = 0;
        stamRecovSpeedModifier = 0;
        HPSpeedModifier = 0;
        spells.addHat(1);
        HasRegen = true;
        staminaRecoverySpeed = 0.025f;
        hasHPRegen = true;
        HPspeed = 1000f;
        moveSpeedNaked = 1.5f;
        stamRecovSpeedNaked = 0.025f;
        hpRecovSpeedNaked = 1000f;
        maxHealthNaked = 100;
        magResistNaked = 0f;
    }




    public override void Die()
    {
        isTargeting = false;

        // you defeated!!!
    }


    public void equipSpelltoQ(int ID)
    {
        equippedSpellQ = ID;
    }

    public void equipSpelltoQ2(int id)
    {
        equippedSpellQ2 = id;
    }

    public int getSpellQ()
    {
        return equippedSpellQ;
    }

    public int getSpellQ2()
    {
        return equippedSpellQ2;
    }

    public void equipSpelltoL(int ID)
    {
        equippedSpellL = ID;
    }

    public void equipSpelltoR(int ID)
    {
        equippedSpellR = ID;
    }

    public void equipSpellto1(int ID)
    {
        equippedSpell1 = ID;
    }

    public void equipSpellto2(int ID)
    {
        equippedSpell2 = ID;
    }

    public int getSpellL()
    {
        return equippedSpellL;
    }

    public int getSpellR()
    {
        return equippedSpellR;
    }

    public int getSpell1()
    {
        return equippedSpell1;
    }

    public int getSpell2()
    {
        return equippedSpell2;
    }

    public void unequipSpellL()
    {
        equippedSpellL = -1;
    }

    public void unequipSpellR()
    {
        equippedSpellR = -1;
    }

    public void unequipSpell1()
    {
        equippedSpell1 = -1;
    }

    public void unequipSpell2()
    {
        equippedSpell2 = -1;
    }

    public void unequipSpellQ()
    {
        equippedSpellQ = -1;
    }

    public void unequipSpellQ2()
    {
        equippedSpellQ2 = -1;
    }






    IEnumerator Backstep()
    {
        isBackstepping = true;
        for (float c = 0; c <= 1; c = c + 0.05f)
        {
            transform.position += -1 * transform.up * Time.deltaTime * (1 - c) * 2;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        isBackstepping = false;
    }

    IEnumerator BackstepUp()
    {
        isBackstepping = true;
        for (float c = 0; c <= 1; c = c + 0.05f)
        {
            transform.position += transform.up * Time.deltaTime * (1 - c) * 2;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        isBackstepping = false;
    }

    IEnumerator BackstepLeft()
    {
        isBackstepping = true;
        for (float c = 0; c <= 1; c = c + 0.05f)
        {
            transform.position += -1 * transform.right * Time.deltaTime * (1.5f - c) * 2;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        isBackstepping = false;
    }
    IEnumerator BackstepRight()
    {
        isBackstepping = true;
        for (float c = 0; c <= 1; c = c + 0.05f)
        {
            transform.position += transform.right * Time.deltaTime * (1.5f - c) * 2;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        isBackstepping = false;
    }


    private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;
    private GUIStyle myStyle;
    private GUIStyle myStylefg;
    private GUIStyle myStylefgRed;
    private GUIStyle myStylefgGreen;
    private GUIStyle myStylefgYellow;
    private GUIStyle myStylefgBlue;
    public Font myFont;


    //draw text of a specified color, with a specified outline color
    public static void DrawOutline(Rect position, string text, GUIStyle style, Color outColor, Color inColor)
    {
        var backupStyle = style;
        style.normal.textColor = outColor;
        position.x--;
        GUI.Label(position, text, style);
        position.x += 2;
        GUI.Label(position, text, style);
        position.x--;
        position.y--;
        GUI.Label(position, text, style);
        position.y += 2;
        GUI.Label(position, text, style);
        position.y--;
        style.normal.textColor = inColor;
        GUI.Label(position, text, style);
        style = backupStyle;
    }
    bool running = false;
    public bool cantcast = false;
    void OnGUI()
    {
        if (isTargeting)
        {
            Vector3 tarpos = transform.position;
            tarpos = Camera.main.WorldToScreenPoint(target.transform.position);
            GUI.DrawTexture(new Rect(tarpos.x - 50, Screen.height - tarpos.y - 50, 100, 100), tareticle);
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - (screenPosition.y + 1);
        myStyle = new GUIStyle();
        myStylefg = new GUIStyle();
        myStyle.font = myFont;
        myStylefg.font = myFont;
        myStyle.normal.textColor = new Color(0, 0, 0);
        myStylefg.normal.textColor = new Color(1, 1, 1);
        myStyle.fontSize = 16;
        myStylefg.fontSize = 15;
        myStyle.fontStyle = FontStyle.Bold;
        myStylefg.fontStyle = FontStyle.Bold;
        myStylefgBlue = new GUIStyle();
        myStylefgBlue.font = myFont;
        myStylefgBlue.fontSize = 15;
        myStylefgBlue.fontStyle = FontStyle.Bold;
        myStylefgGreen = new GUIStyle();
        myStylefgGreen.font = myFont;
        myStylefgGreen.fontSize = 15;
        myStylefgGreen.fontStyle = FontStyle.Bold;
        myStylefgRed = new GUIStyle();
        myStylefgRed.font = myFont;
        myStylefgRed.fontSize = 15;
        myStylefgRed.fontStyle = FontStyle.Bold;
        myStylefgYellow = new GUIStyle();
        myStylefgYellow.font = myFont;
        myStylefgYellow.fontSize = 15;
        myStylefgYellow.fontStyle = FontStyle.Bold;
        myStylefgBlue.normal.textColor = new Color(0, 0.83f, .87f);
        myStylefgGreen.normal.textColor = new Color(0.22f, 0.8f, 0);
        myStylefgRed.normal.textColor = new Color(0.85f, 0, 0);
        myStylefgYellow.normal.textColor = new Color(0.88f, 0.76f, 0);
        Rect StunnedText = new Rect(screenPosition.x - 30, screenPosition.y + 20, 100, 25);
        Rect SlowedText = new Rect(screenPosition.x - 25, screenPosition.y + 40, 100, 25);
        if (isStunned)
        {
            GUI.Label(StunnedText, "Stunned!", myStyle);
            GUI.Label(StunnedText, "Stunned!", myStylefgYellow);
        }
        if (isSlowed)
        {
            GUI.Label(SlowedText, "Slowed!", myStyle);
            GUI.Label(SlowedText, "Slowed!", myStylefgBlue);
        }
        Rect HealthSub = new Rect(screenPosition.x + 25, screenPosition.y - 23, 100, 25);
        if (damageTaken != 0)
        {
            GUI.Label(HealthSub, "-" + damageTaken, myStyle);
            GUI.Label(HealthSub, "-" + damageTaken, myStylefgRed);
        }
        Rect HealthHeal = new Rect(screenPosition.x + 25, screenPosition.y - 5, 100, 25);
        if (healingTaken != 0)
        {
            GUI.Label(HealthHeal, "+" + healingTaken, myStyle);
            GUI.Label(HealthHeal, "+" + healingTaken, myStylefgGreen);
        }
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUIStyle soulstyle = myStyle;
        soulstyle.alignment = TextAnchor.MiddleRight;
        DrawOutline(new Rect(1920 - 200, 1080 - 180, 180, 25), xp + " J", myStyle, new Color(0, 0, 0), new Color(1, 1, 1));
        if (spells.bagIsFull)
        {
            DrawOutline(new Rect(23, 75, 180, 25), "Bag is Full!", fullstyle, new Color(0, 0, 0), new Color(0.8f, 0.2f, 0));
        }
        if (spells.AmuletIsFull)
        {
            DrawOutline(new Rect(23, 175, 180, 25), "Jewelry Box is Full!", fullstyle, new Color(1, 1, 1), new Color(0.1f, 0.1f, 0.1f));
        }
        if (spells.HatIsFull)
        {
            DrawOutline(new Rect(23, 125, 180, 25), "Hat Box is Full!", fullstyle, new Color(1, 1, 1), new Color(0.1f, 0.1f, 0.1f));
        }
        if (spells.SpellBookIsFull)
        {
            DrawOutline(new Rect(23, 100, 180, 25), "Max Spells Learned!", fullstyle, new Color(1, 1, 1), new Color(0.9f, 0.1f, 0.9f));
        }
        if (spells.TunicIsFull)
        {
            DrawOutline(new Rect(23, 150, 180, 25), "Can't Carry Any More Tunics!", fullstyle, new Color(1, 1, 1), new Color(0.1f, 0.1f, 0.1f));
        }
    }

    public bool menuIsUp = false;

    public void equipAmulet(int id)
    {
        spells.EquippedAmulet = id;
        stamRecovSpeedModifier = spells.getAmuletStamRegen(spells.EquippedAmulet);
        HPSpeedModifier = spells.getAmuletHealthRegen(spells.EquippedAmulet);
        staminaRecoverySpeed = stamRecovSpeedModifier;
        HPspeed = HPSpeedModifier;
    }
    Coroutine rcv;
    public void LoseStamina(int amount, float speed = 1f)
    {
        stamina -= amount;
        canrecover = false;
        if (rcv != null)
        {
            StopCoroutine(rcv);
        }
        rcv = StartCoroutine(CanRecov(speed));
    }

    IEnumerator CanRecov(float speed)
    {
        yield return new WaitForSeconds(speed);
        canrecover = true;
    }

    public void equipTunic(int id)
    {
        spells.EquippedTunic = id;
        maxHealthModifier = Mathf.CeilToInt(spells.getTunicHPIncrease(spells.EquippedTunic));
        moveSpeedModifier = spells.getTunicMoveSpeed(spells.EquippedTunic);
        magResistAdjust = spells.getTunicResistIncrease(spells.EquippedTunic);
        maxHealth = maxHealth + maxHealthModifier;
        moveSpeed = moveSpeed + moveSpeedModifier;
        magResist = magResist + magResistAdjust;
    }

    public void equipHat(int id)
    {
        spells.EquippedHat = id;
        SpriteRenderer ths = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        ths.sprite = spells.getHatSprite(spells.EquippedHat);
        hatbigsprite = spells.getHatBigSprite(spells.EquippedHat);
    }

    public void UnequipAmulet()
    {
        spells.EquippedAmulet = 1;
        staminaRecoverySpeed = stamRecovSpeedNaked;
        HPspeed = hpRecovSpeedNaked;
    }

    public void UnequipTunic()
    {
        spells.EquippedTunic = 1;
        maxHealth = maxHealthNaked;
        moveSpeed = moveSpeedNaked;
        magResist = magResistNaked;
    }
    public Texture2D tareticle;
    public bool isTargeting = false;
    Quaternion newRotation;
    public bool isdying = false;
    public RaycastHit2D target;
    public bool cameraTAR = true;
    void dii()
    {

    }
    // Update is called once per frame
    void Update()
    {

        if (target == null || target.collider == null || (target.transform.position - transform.position).magnitude > 10)
        {
            isTargeting = false;
        }
        if (currentHealth <= 0 && !isdying)
        {
            isdying = true;
            Instantiate(Resources.Load("Death"), transform.position, transform.rotation);

        }
        if (stamina > 0)
        {
            HasRegen = true;
        }
        AudioListener.volume = gameVolume;
        if (Input.GetButtonDown("Pause") && !menuIsUp)
        {
            GameObject menu = Instantiate(Resources.Load("UI/Menu", typeof(GameObject))) as GameObject;
            if (menu != null)
            {
                menuIsUp = true;

            }
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        /*if (moveSpeedOriginal < moveSpeed)
        {
            moveSpeed = moveSpeedOriginal;
        }*/
        if (Input.GetButtonDown("Target"))
        {
            if (!isTargeting)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit != null && hit.collider != null && hit.collider.tag == "Enemy")
                {
                    target = hit;
                    isTargeting = true;
                }

            }
            else
            {
                isTargeting = false;
            }
        }
        if (!isStunned && !isdying)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!isTargeting)
            {
                newRotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
            }
            else
            {
                newRotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, speed * Time.deltaTime);
            if (Input.GetButtonDown("Interact"))
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                for (int c = 0; c < hit.Length; ++c)
                {
                    if (hit[c].collider != null && (transform.position - hit[c].collider.transform.position).magnitude < 1.6 && (hit[c].collider.tag == "Inactive" || hit[c].collider.tag == "Checkpoint1" || hit[c].collider.tag == "Checkpoint2" || hit[c].collider.tag == "Checkpoint3" || hit[c].collider.tag == "Checkpoint4"))
                    {
                        hit[c].collider.tag = "Active";
                    }
                }
            }
            if (stamina < 0)
            {
                StartCoroutine(StaminaReachesBelowZero(recover));
            }
            if (stamina >= -500)
            {
                if (Input.GetButtonDown("Backward") || Input.GetButtonDown("StrafeLeft") || Input.GetButtonDown("StrafeRight") || Input.GetButtonDown("Forward"))
                {
                    gameObject.GetComponent<AudioSource>().time = 2.6f;
                    GetComponent<AudioSource>().Play();
                }
                if (Input.GetButton("StrafeRight"))
                {
                    StopCoroutine(FloatInDirection("Right"));
                    if (running)
                    {
                        transform.position += transform.right * Time.deltaTime * moveSpeed * 1.8f;
                    }
                    else
                    {
                        transform.position += transform.right * Time.deltaTime * moveSpeed;
                    }
                }
                else if (Input.GetButton("StrafeLeft"))
                {
                    StopCoroutine(FloatInDirection("Left"));
                    if (running)
                    {
                        transform.position += -1 * transform.right * Time.deltaTime * moveSpeed * 1.8f;
                    }
                    else
                    {
                        transform.position += -1 * transform.right * Time.deltaTime * moveSpeed;
                    }
                }
                else if (Input.GetButton("Forward"))
                {
                    StopCoroutine(FloatInDirection("Up"));
                    if (running)
                    {

                        transform.position += transform.up * Time.deltaTime * moveSpeed * 1.8f;
                    }
                    else
                    {
                        transform.position += transform.up * Time.deltaTime * moveSpeed;
                    }
                }

                else if (Input.GetButton("Backward"))
                {
                    StopCoroutine(FloatInDirection("Back"));
                    transform.position += -1 * transform.up * Time.deltaTime * moveSpeed * 0.8f;
                }
                else
                {
                    gameObject.GetComponent<AudioSource>().Pause();
                }
                /*if (Input.GetButtonUp("Forward"))
                {
                    StopCoroutine(FloatInDirection("Up"));
                    StartCoroutine(FloatInDirection("Up"));
                }
                else if (Input.GetButtonUp("StrafeRight"))
                {
                    StopCoroutine(FloatInDirection("Right"));
                    StartCoroutine(FloatInDirection("Right"));
                }
                else if (Input.GetButtonUp("StrafeLeft"))
                {
                    StopCoroutine(FloatInDirection("Left"));
                    StartCoroutine(FloatInDirection("Left"));
                }
                else if (Input.GetButtonUp("Backward"))
                {
                    StopCoroutine(FloatInDirection("Back"));
                    StartCoroutine(FloatInDirection("Back"));
                }*/
                if (Input.GetButtonDown("Backstep") && !isBackstepping && stamina > 0)
                {
                    AudioSource.PlayClipAtPoint(shuffle, transform.position);
                    LoseStamina(12, 0.6f);
                    StopCoroutine(FloatInDirection("Up"));
                    StopCoroutine(FloatInDirection("Right"));
                    StopCoroutine(FloatInDirection("Left"));
                    StopCoroutine(FloatInDirection("Back"));
                    StartCoroutine(Immunity(0.3f));
                    StartCoroutine(Dodge(0.3f));
                    if (Input.GetButton("StrafeRight"))
                    {
                        StartCoroutine(BackstepRight());
                    }
                    else if (Input.GetButton("StrafeLeft"))
                    {
                        StartCoroutine(BackstepLeft());
                    }
                    else if (Input.GetButton("Forward"))
                    {
                        StartCoroutine(BackstepUp());
                    }
                    else
                    {
                        StartCoroutine(Backstep());
                    }
                }

                if (Input.GetButton("Run") && !isBackstepping && canrunagain)
                {
                    if (stamina > 0)
                    {
                        running = true;

                    }
                    else
                    {
                        running = false;
                        canrunagain = false;
                        StartCoroutine(ranoutarun());
                    }
                    stamina -= Mathf.CeilToInt(1f * Time.deltaTime);
                }
                else
                {
                    running = false;

                }
            }
        } // if isStunned
        if (running)
        {
            gameObject.GetComponent<AudioSource>().pitch = 1.5f;
            gameObject.GetComponent<AudioSource>().volume = 5f;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().pitch = 1;
        }
    } // Update
    bool canrunagain = true;

    public AudioClip f1, f2, f3, f4, f5, f6, f7, f8, f9;
    public AudioClip m1, m2, m3, m4, m5, m6, m7, m8, m9;
    public override void PlayHurtSound()
    {
        if (gender == "male")
        {
            int rng = (int)Random.RandomRange(0f, 9f);
            switch (rng)
            {
                case 0: AudioSource.PlayClipAtPoint(m1, transform.position); break;
                case 1: AudioSource.PlayClipAtPoint(m2, transform.position); break;
                case 2: AudioSource.PlayClipAtPoint(m3, transform.position); break;
                case 3: AudioSource.PlayClipAtPoint(m4, transform.position); break;
                case 4: AudioSource.PlayClipAtPoint(m5, transform.position); break;
                case 5: AudioSource.PlayClipAtPoint(m6, transform.position); break;
                case 6: AudioSource.PlayClipAtPoint(m7, transform.position); break;
                case 7: AudioSource.PlayClipAtPoint(m8, transform.position); break;
                case 8: AudioSource.PlayClipAtPoint(m9, transform.position); break;
                default: AudioSource.PlayClipAtPoint(m1, transform.position); break;
            }
        }
        else
        {
            int rng = (int)Random.RandomRange(0f, 9f);
            switch (rng)
            {
                case 0: AudioSource.PlayClipAtPoint(f1, transform.position); break;
                case 1: AudioSource.PlayClipAtPoint(f2, transform.position); break;
                case 2: AudioSource.PlayClipAtPoint(f3, transform.position); break;
                case 3: AudioSource.PlayClipAtPoint(f4, transform.position); break;
                case 4: AudioSource.PlayClipAtPoint(f5, transform.position); break;
                case 5: AudioSource.PlayClipAtPoint(f6, transform.position); break;
                case 6: AudioSource.PlayClipAtPoint(f7, transform.position); break;
                case 7: AudioSource.PlayClipAtPoint(f8, transform.position); break;
                case 8: AudioSource.PlayClipAtPoint(f9, transform.position); break;
                default: AudioSource.PlayClipAtPoint(f1, transform.position); break;
            }
        }
    }

    IEnumerator ranoutarun()
    {
        yield return new WaitForSeconds(2f);
        canrunagain = true;
    }
} // Class
