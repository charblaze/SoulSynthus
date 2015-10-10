using UnityEngine;
using System.Collections;

public class VodCaster : Character
{
    public bool foundya = false;
    private bool isOnScreen = false;
    private SpellCastScript spellcast;
    float runtimer = 1f;
    float spelltimer = 1f;

    // Use this for initialization
    void Start()
    {
        worth = 100;
        moveSpeed = 1.5f;
        moveSpeedOriginal = moveSpeed;
        currentHealth = 45;
        maxHealth = 45;
        isStunned = false;
        isMoving = false;
        immunityFrames = false;
        stunDuration = 0;
        player = GameObject.FindGameObjectWithTag("Player");
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
		ParrySound ();
        STUNNED(2.5f);
        isstaggered = true;
        yield return new WaitForSeconds(2.5f);
        isstaggered = false;
        yield return new WaitForSeconds(2f);
        CAST = true;
    }
    IEnumerator dontgetstuck()
    {
        while (foundya)
        {
            Transform pos1 = gameObject.transform;
            yield return new WaitForSeconds(2f);
            if ((transform.position - pos1.position).magnitude < 0.4)
            {
                int rng = (int)Random.Range(0, 1);
                if (rng == 0)
                {
                    StartCoroutine(FloatInDirection("Left"));
                }
                else
                {
                    StartCoroutine(FloatInDirection("Right"));
                }
            }
            yield return new WaitForSeconds(2f);
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
    float s1 = 0, s2 = 0;

    IEnumerator CastSpear()
    {
        //hitcollider.enabled = false;
        // cast spear
        GameObject caster = Instantiate(Resources.Load("caste"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
        CastAnimScriptEnemy anms = caster.GetComponent(typeof(CastAnimScriptEnemy)) as CastAnimScriptEnemy;
        anms.howtime = 0.3f;
        anms.player = gameObject;
        yield return new WaitForSeconds(0.3f);
        GameObject spell = Instantiate(Resources.Load("EnemySpells/VOD_Caster_Spear")) as GameObject;
        Spell ps = spell.GetComponent(typeof(Spell)) as Spell;
        ps.caster = gameObject;
        yield return new WaitForSeconds(1f);
        CAST = true;
    }

    IEnumerator CastNova()
    {
        GameObject caster = Instantiate(Resources.Load("caste"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
        CastAnimScriptEnemy anms = caster.GetComponent(typeof(CastAnimScriptEnemy)) as CastAnimScriptEnemy;
        anms.howtime = 1f;
        anms.player = gameObject;
        yield return new WaitForSeconds(.75f);
        isParryable = true;
        yield return new WaitForSeconds(.25f);
        isParryable = false;
        GameObject spell = Instantiate(Resources.Load("EnemySpells/VOD_Caster_Nova")) as GameObject;
        Spell ps = spell.GetComponent(typeof(Spell)) as Spell;
        ps.caster = gameObject;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Back"));
        yield return new WaitForSeconds(0.5f);
        CAST = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (foundya && (transform.position - player.transform.position).magnitude < 2)
        {
            s2 += 0.1f;
        }
        else
        {
            s1 += 0.1f;
        }
        if (!isStunned && foundya)
        {
            // look to player and move backwards
            var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.02f);
            if ((player.transform.position - transform.position).magnitude < 5)
            {
                transform.position -= transform.up * Time.deltaTime * 0.3f;
            }
            if (CAST)
            {
                if (s1 > s2)
                {
                    currentanim = StartCoroutine(CastSpear()); CAST = false; s1 = 0;
                }
                else
                {
                    currentanim = StartCoroutine(CastNova()); CAST = false; s2 = 0;
                }
            }
        }
    }
}
