using UnityEngine;
using System.Collections;

public class BiochemEnemy : Character
{
    public bool foundya = false;
    private bool isOnScreen = false;
    private SpellCastScript spellcast;
    float runtimer = 1f;
    float spelltimer = 1f;
    PlayerController playerscript;
    SpellCastScript scs;
    // Use this for initialization
    void Start()
    {
        worth = 100;
        moveSpeed = 1.5f;
        moveSpeedOriginal = moveSpeed;
        currentHealth = 450;
        maxHealth = 450;
        isStunned = false;
        isMoving = false;
        immunityFrames = false;
        stunDuration = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
        scs = player.GetComponent(typeof(SpellCastScript)) as SpellCastScript;
        StartCoroutine(dontgetstuck());
        //pc = player.GetComponent(typeof(PlayerController)) as PlayerController;
    }
    bool isParryable = false;
    Coroutine currentanim;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ParrySpell" && isParryable)
        {
            isParryable = false;
            StopCoroutine(currentanim);
            StartCoroutine(staggered());
        }
    }

    IEnumerator staggered()
    {
        STUNNED(2.5f);
        magResist = -1.5f;
        yield return new WaitForSeconds(2.5f);
        magResist = 0;
        yield return new WaitForSeconds(2f);
        CAST = true;
    }
    float dodgetimer = 5f;
    IEnumerator dontgetstuck()
    {
        while (foundya)
        {
            if ((player.transform.position - transform.position).magnitude < 0.5 && dodgetimer < 0)
            {
                yield return new WaitForSeconds(0.4f);
                StartCoroutine(FloatInDirection("Back"));
                yield return new WaitForSeconds(0.4f);
                StartCoroutine(FloatInDirection("Back"));
                dodgetimer = 5f;
            }
        }
    }

    void OnBecameVisible()
    {
        isOnScreen = true;
    }

    void OnBecameInvisible()
    {
        isOnScreen = false;
    }

    private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;
    private GUIStyle myStyle;
    private GUIStyle myStylefg;
    public Font myFont;

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

    void OnGUI()
    {
        if (!isOnScreen || !foundya) return;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - (screenPosition.y + 1);
        Rect border = new Rect(screenPosition.x - 38, screenPosition.y - 30, 76, 4);
        Rect healthb = new Rect(screenPosition.x - 37, screenPosition.y - 29, 74 * ((float)currentHealth / (float)maxHealth), 2);
        Rect HPText = new Rect(screenPosition.x - 50, screenPosition.y - 32, 100, 25);
        GUIDrawRect(border, new Color(0, 0, 0));
        if (!immunityFrames)
        {
            GUIDrawRect(healthb, new Color(1, 1, 1));
        }
        else
        {
            GUIDrawRect(healthb, new Color(1f, .75f, .75f));
        }

        myStyle = new GUIStyle();
        myStylefg = new GUIStyle();
        myStyle.font = myFont;
        myStylefg.font = myFont;
        myStyle.normal.textColor = new Color(0, 0, 0);
        myStylefg.normal.textColor = new Color(1, 1, 1);
        myStyle.fontSize = 12;
        myStylefg.fontSize = 11;
        myStyle.fontStyle = FontStyle.Bold;
        myStylefg.fontStyle = FontStyle.Bold;
        GUI.Label(HPText, "HP", myStyle);
        GUI.Label(HPText, "HP", myStylefg);
        Rect StunnedText = new Rect(screenPosition.x - 50, screenPosition.y - 43, 100, 25);
        Rect SlowedText = new Rect(screenPosition.x - 5, screenPosition.y - 43, 100, 25);
        if (isStunned)
        {
            GUI.Label(StunnedText, "Stunned", myStyle);
            GUI.Label(StunnedText, "Stunned!", myStylefg);
        }
        if (moveSpeedOriginal > moveSpeed)
        {
            GUI.Label(SlowedText, "Slowed!", myStyle);
            GUI.Label(SlowedText, "Slowed!", myStylefg);
        }
        Rect HealthSub = new Rect(screenPosition.x - 53, screenPosition.y - 23, 100, 25);
        if (damageTaken != 0)
        {
            GUI.Label(HealthSub, "-" + damageTaken, myStyle);
            GUI.Label(HealthSub, "-" + damageTaken, myStylefg);
        }
        Rect HealthHeal = new Rect(screenPosition.x - 53, screenPosition.y - 10, 100, 25);
        if (healingTaken != 0)
        {
            GUI.Label(HealthHeal, "+" + healingTaken, myStyle);
            GUI.Label(HealthHeal, "+" + healingTaken, myStylefg);
        }
    }

    PlayerController pc;
    bool CAST = true;
    float heme = 0, myo = 0, heal = 0;
    float healcd = 0;
    
    IEnumerator CastHeme()
    {
        //hitcollider.enabled = false;
        // cast spear
        GameObject caster = Instantiate(Resources.Load("caste"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
        CastAnimScriptEnemy anms = caster.GetComponent(typeof(CastAnimScriptEnemy)) as CastAnimScriptEnemy;
        anms.howtime = 0.3f;
        anms.player = gameObject;
        yield return new WaitForSeconds(0.3f);
        GameObject spell = Instantiate(Resources.Load("Spells/45")) as GameObject;
        S45 ps = spell.GetComponent(typeof(S45)) as S45;
        ps.caster = gameObject;
        yield return new WaitForSeconds(1f);
        CAST = true;
    }

    IEnumerator CastMyo()
    {
        //hitcollider.enabled = false;
        // cast spear
        GameObject spell = Instantiate(Resources.Load("Spells/50")) as GameObject;
        S49 ps = spell.GetComponent(typeof(S49)) as S49;
        ps.caster = gameObject;
        ps.isEnemy = true;
        yield return new WaitForSeconds(1f);
        CAST = true;
    }

    IEnumerator CastHeal()
    {
        //hitcollider.enabled = false;
        // cast spear
        GameObject caster = Instantiate(Resources.Load("caste"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
        CastAnimScriptEnemy anms = caster.GetComponent(typeof(CastAnimScriptEnemy)) as CastAnimScriptEnemy;
        anms.howtime = 1f;
        anms.player = gameObject;
        yield return new WaitForSeconds(1f);
        GameObject spell = Instantiate(Resources.Load("Spells/46")) as GameObject;
        S46 ps = spell.GetComponent(typeof(S46)) as S46;
        ps.caster = gameObject;
        yield return new WaitForSeconds(1f);
        CAST = true;
    }
    public override void Die()
    {
        PlayerController pc = player.GetComponent(typeof(PlayerController)) as PlayerController;
        Spells s = player.GetComponent(typeof(Spells)) as Spells;
        s.AddItem(14);
        pc.isTargeting = false;
        pc.xp += worth;
        GameObject ja = Instantiate(Resources.Load("JoulesAnimation"), transform.position, transform.rotation) as GameObject;
        JouleAnimation ji = ja.GetComponent(typeof(JouleAnimation)) as JouleAnimation;
        ji.xp = worth;
        Items p = player.GetComponent(typeof(Items)) as Items;
        p.bosses[1] = true;
        // you defeated!
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        dodgetimer -= Time.deltaTime;
        healcd -= Time.deltaTime;
        if (currentHealth <= 0)
        {
            Die();
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (isStunned)
        {
            isMoving = false;
            //foundya = false;
        }
        if (currentHealth < maxHealth || (transform.position - player.transform.position).magnitude < 4 && !foundya)
        {
            foundya = true;
            var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.02f);
            isOnScreen = true;
        }
        if (foundya && currentHealth <= maxHealth*0.5)
        {
            heme += 0.12f;
            heal += 0.1f;
        }
        else if (foundya && currentHealth > maxHealth * 0.5)
        {
            myo += 0.1f;
            heme += 0.04f;
        }
        if (currentHealth < maxHealth * 0.25)
        {
            heal += 1f;
        }
        if (!isStunned && foundya)
        {
            // look to player and move backwards
            var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.02f);
            if ((player.transform.position - transform.position).magnitude > 0.7)
            {
                transform.position += transform.up * Time.deltaTime * 0.3f;
            }
            
            if (CAST)
            {
                if (Mathf.Max(myo, heme, heal) == myo)
                {
                    currentanim = StartCoroutine(CastMyo()); CAST = false; myo = 0;
                }
                else if (Mathf.Max(myo, heme, heal) == heme)
                {
                    currentanim = StartCoroutine(CastHeme()); CAST = false; heme = 0;
                }
                else if (Mathf.Max(myo, heme, heal) == heal && healcd <= 0)
                {
                    currentanim = StartCoroutine(CastHeal()); CAST = false; heal = 0; healcd = 20f;
                }
                else
                {
                    currentanim = StartCoroutine(CastHeme()); CAST = false; heme = 0;
                }
            }
        }
    }
}
