using UnityEngine;
using System.Collections;

public class VodGiant : Character
{
    public bool foundya = false;
    private bool isOnScreen = false;
    private SpellCastScript spellcast;
    float runtimer = 1f;
    float spelltimer = 1f;
    BoxCollider2D hitcollider;
    Animator anm;

    // Use this for initialization
    void Start()
    {
        pushback = 0.3f;
        worth = 160;
        moveSpeed = 1.5f;
        moveSpeedOriginal = moveSpeed;
        currentHealth = 200;
        maxHealth = 200;
        isStunned = false;
        isMoving = false;
        immunityFrames = false;
        stunDuration = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        //pc = player.GetComponent(typeof(PlayerController)) as PlayerController;
        GameObject sw = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        hitcollider = sw.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        GameObject parent = this.gameObject.transform.GetChild(0).gameObject;
        anm = parent.GetComponent(typeof(Animator)) as Animator;
        hitcollider.enabled = false;
        anm.StopPlayback();
    }

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
        anm.StopPlayback();
        anm.Play("StaggerGiant");
        STUNNED(2.5f);
        //magResist = -1.5f;
        isstaggered = true;
        yield return new WaitForSeconds(2.5f);
        //magResist = 0;
        isstaggered = false;
        yield return new WaitForSeconds(2f);
        CAST = true;
    }


    IEnumerator FloatInDirection(string Direction)
    {
        if (Direction == "Up")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += transform.up * Time.deltaTime * (moveSpeed - c);
                yield return new WaitForSeconds(0.01f);
            }
        }
        if (Direction == "Back")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += -1 * transform.up * Time.deltaTime * (moveSpeed - c);
                yield return new WaitForSeconds(0.01f);
            }
        }
        if (Direction == "Left")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += -1 * transform.right * Time.deltaTime * (moveSpeed - c);
                yield return new WaitForSeconds(0.01f);
            }
        }
        if (Direction == "Right")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += transform.right * Time.deltaTime * (moveSpeed - c);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    /*IEnumerator advance()
    {
        isMoving = true;
        while (isMoving && !isStunned && foundya)
        {
            CastSpell(0);
            if ((transform.position - player.transform.position).magnitude < 3)
            {
                StartCoroutine(FloatInDirection("Up"));
                yield return new WaitForSeconds(4f);
            }
            else
            {
                yield return new WaitForSeconds(5f);
            }
        }
        isMoving = false;
    }*/

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
        if (!isOnScreen) return;
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

    Coroutine currentanim;


    IEnumerator Charge()
    {
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(HugeSwing());
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
        
    }
    PlayerController pc;
    bool CAST = true;
    float s1 = 0.1f, s2 = 0.1f, s3 = 0.1f, s4 = 0;
    bool isParryable = false;
    IEnumerator HugeSwing()
    {
        hitcollider.enabled = false;
        anm.Play("ENEMY_Giantswing");
        gameObject.GetComponent<AudioSource>().PlayOneShot(swordswipe);
        yield return new WaitForSeconds(0.25f);
        isParryable = true;
        yield return new WaitForSeconds(0.25f);
        isParryable = false;
        hitcollider.enabled = true;
        yield return new WaitForSeconds(1f);
        hitcollider.enabled = false;
        yield return new WaitForSeconds(3f);
        CAST = true;
    }

    IEnumerator HugeSwing2()
    {
        hitcollider.enabled = false;
        anm.Play("ENEMY_GS2");
        yield return new WaitForSeconds(.75f);
        isParryable = true;
        yield return new WaitForSeconds(0.25f);
        isParryable = false;
        gameObject.GetComponent<AudioSource>().PlayOneShot(swordswipedouble);
        hitcollider.enabled = true;
        yield return new WaitForSeconds(0.45f);
        hitcollider.enabled = false;
        yield return new WaitForSeconds(3f);
        CAST = true;
    }

    IEnumerator CastLightning()
    {
        hitcollider.enabled = false;
        // cast lightning
        GameObject caster = Instantiate(Resources.Load("caste"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
        CastAnimScriptEnemy anms = caster.GetComponent(typeof(CastAnimScriptEnemy)) as CastAnimScriptEnemy;
        anms.howtime = 1f;
        anms.player = gameObject;
        yield return new WaitForSeconds(.75f);
        isParryable = true;
        yield return new WaitForSeconds(0.25f);
        isParryable = false;
        GameObject spell = Instantiate(Resources.Load("EnemySpells/GiantLightning")) as GameObject;
        Spell ps = spell.GetComponent(typeof(Spell)) as Spell;
        ps.caster = gameObject;
        yield return new WaitForSeconds(2f);
        CAST = true;
    }

    public AudioClip death;
    public AudioClip gethit;
    public AudioClip scream;
    public AudioClip swordswipedouble;
    public AudioClip swordswipe;

    public override void PlayDeathSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(death);
    }

    public override void PlayHurtSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gethit);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentHealth <= 0)
        {
            Die();
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (foundya)
        {
            s1 += 0.1f;
            s2 += 0.1f;
            s3 += 0.1f;
        }
        if ((player.transform.position - transform.position).magnitude > 3 && foundya)
        {
            s4 += 1f;
        }
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
        if (currentHealth < maxHealth || (transform.position - player.transform.position).magnitude < 2)
        {
            foundya = true;
            var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.02f);
            isOnScreen = true;
        }
        if (!isStunned && foundya)
        {
            var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.02f);
            transform.position += transform.up * Time.deltaTime * 0.5f;
            if (CAST)
            {
                if (Mathf.Max(s1, s2, s3, s4) == s4)
                {
                    int rng = (int)Random.RandomRange(0f, 1f);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(Charge()); CAST = false; s4 = 0;
                    }
                    else
                    {
                        currentanim = StartCoroutine(CastLightning()); CAST = false; s3 = 0;
                    }
                }
                else if (Mathf.Max(s1, s2, s3) == s1)
                {
                    if (Mathf.Max(s2, s3) == s2)
                    {
                        int rng = (int)Random.RandomRange(0f, 1f);
                        if (rng == 0)
                        {
                            currentanim = StartCoroutine(HugeSwing()); CAST = false; s1 = 0;
                        }
                        else
                        {
                            currentanim = StartCoroutine(HugeSwing2()); CAST = false; s2 = 0;
                        }
                    }
                    else
                    {
                        int rng = (int)Random.RandomRange(0f, 1f);
                        if (rng == 0)
                        {
                            currentanim = StartCoroutine(HugeSwing()); CAST = false; s1 = 0;
                        }
                        else
                        {
                            currentanim = StartCoroutine(CastLightning()); CAST = false; s3 = 0;
                        }
                    }
                    
                }
                else if (Mathf.Max(s1,s2,s3) == s2)
                {
                    if (Mathf.Max(s1, s3) == s1)
                    {
                        int rng = (int)Random.RandomRange(0f, 1f);
                        if (rng == 0)
                        {
                            currentanim = StartCoroutine(HugeSwing()); CAST = false; s1 = 0;
                        }
                        else
                        {
                            currentanim = StartCoroutine(HugeSwing2()); CAST = false; s2 = 0;
                        }
                    }
                    else
                    {
                        int rng = (int)Random.RandomRange(0f, 1f);
                        if (rng == 0)
                        {
                            currentanim = StartCoroutine(HugeSwing2()); CAST = false; s2 = 0;
                        }
                        else
                        {
                            currentanim = StartCoroutine(CastLightning()); CAST = false; s3 = 0;
                        }
                    }
                }
                else
                {
                    currentanim = StartCoroutine(CastLightning()); CAST = false; s3 = 0;
                }
            }
        }
    }
}
