using UnityEngine;
using System.Collections;

public class VodBoss : Character
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
        pushback = 0.2f;
        worth = 5000;
        moveSpeed = 1.5f;
        moveSpeedOriginal = moveSpeed;
        currentHealth = 2000;
        maxHealth = 2000;
        isStunned = false;
        isMoving = false;
        immunityFrames = false;
        stunDuration = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        //pc = player.GetComponent(typeof(PlayerController)) as PlayerController;
        GameObject sw = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        hitcollider = sw.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        GetComponent<AudioSource>().Stop();
        GameObject parent = this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
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
        anm.Play("Stagger");
        STUNNED(2.5f);
        isstaggered = true;
        yield return new WaitForSeconds(3f);
        isstaggered = false;
        yield return new WaitForSeconds(1f);
        CAST = true;
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
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        if (!foundya) return;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = 1080 - (screenPosition.y + 1);
        Rect border = new Rect(1920 / 2 - 452, 1080 - 302, 904, 1);
        Rect healthb = new Rect(1920 / 2 - 450, 1080 - 300, 900 * ((float)currentHealth / (float)maxHealth), 3);
        Rect HPText = new Rect(1920 / 2 - 450, 1080 - 330, 300, 30);
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
        myStyle.fontSize = 28;
        myStylefg.fontSize = 25;
        myStyle.fontStyle = FontStyle.Bold;
        myStylefg.fontStyle = FontStyle.Bold;
        GUI.Label(HPText, "Lifeless Abomination", myStyle);
        GUI.Label(HPText, "Lifeless Abomination", myStylefg);
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


    IEnumerator ChargeAttack()
    {
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SpinAttack());
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
    float RightSwingp = 0f, LeftSwingp = 0f, DoubleSwingp = 0f, ChargeAttackp = 0f, SpinAttackp = 0f, CastOneFlamep = 0f, HugeChargedSpinp = 0f, CastManyFlamesp = 0f;
    bool isParryable = false;
    public AudioClip LSwingSFX, RSwingSFX, SpinSFX, DoubleSwingSFX, RoarSFX, HugeSpinSFX, FlameSwatheSFX;

    IEnumerator HugeChargedSpin()
    {
        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE
        // make this animation so he spins many many times so he stunlocks you if you are derp
        // NOT PARRYABLE (maybe?)
        // actually just make this a go crazy move
        hitcollider.enabled = false;
		standingstill = true;
        anm.Play("HugeChargedSpin");
        AudioSource.PlayClipAtPoint(HugeSpinSFX, transform.position);
        yield return new WaitForSeconds(3.75f);
        hitcollider.enabled = true;
        yield return new WaitForSeconds(1.22f);
        hitcollider.enabled = false;
        yield return new WaitForSeconds(1f);
        CAST = true;
		standingstill = false;
    }

    IEnumerator RightSwing()
    {
        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE
        hitcollider.enabled = false;
        anm.Play("RightSwing");
        AudioSource.PlayClipAtPoint(RSwingSFX, transform.position);
        yield return new WaitForSeconds(1.35f);
        isParryable = true;
        yield return new WaitForSeconds(.15f);
        isParryable = false;
        hitcollider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        hitcollider.enabled = false;
        yield return new WaitForSeconds(1f);
        CAST = true;
    }

    IEnumerator LeftSwing()
    {        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE

        hitcollider.enabled = false;
        anm.Play("LeftSwing");
        yield return new WaitForSeconds(1.13f);
        isParryable = true;
        yield return new WaitForSeconds(0.15f);
        isParryable = false;
        AudioSource.PlayClipAtPoint(LSwingSFX, transform.position);
        hitcollider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        hitcollider.enabled = false;
        yield return new WaitForSeconds(1f);
        CAST = true;
    }
    IEnumerator SpinAttack()
    {        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE

        hitcollider.enabled = false;
        anm.Play("SpinAttack");
        yield return new WaitForSeconds(1f);
        isParryable = true;
        yield return new WaitForSeconds(0.15f);
        isParryable = false;
        AudioSource.PlayClipAtPoint(SpinSFX, transform.position);
        hitcollider.enabled = true;
        yield return new WaitForSeconds(1.5f);
        hitcollider.enabled = false;
        yield return new WaitForSeconds(1f);
        CAST = true;
    }
    IEnumerator DoubleSwing()
    {        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE

        hitcollider.enabled = false;
        anm.Play("DoubleSwing");
        yield return new WaitForSeconds(1.11f - .15f);
        isParryable = true;
        yield return new WaitForSeconds(0.15f);
        isParryable = false;
        AudioSource.PlayClipAtPoint(DoubleSwingSFX, transform.position);
        hitcollider.enabled = true;
        yield return new WaitForSeconds(0.23f);
        hitcollider.enabled = false;
        yield return new WaitForSeconds(1f);
        CAST = true;
    }

    IEnumerator CastOneFlame()
    {
        hitcollider.enabled = false;
        // cast one flame. Should be roar animation here. Not parryable btw. also make sure the timing is right here
        anm.Play("Roar");
        AudioSource.PlayClipAtPoint(RoarSFX, transform.position);
        yield return new WaitForSeconds(2f);
        AudioSource.PlayClipAtPoint(FlameSwatheSFX, transform.position);
        GameObject spell = Instantiate(Resources.Load("EnemySpells/VodBossFire")) as GameObject;
        Spell ps = spell.GetComponent(typeof(Spell)) as Spell;
        ps.caster = gameObject;
        yield return new WaitForSeconds(0.5f);
        CAST = true;
    }

    IEnumerator CastManyFlames()
    {
        hitcollider.enabled = false;
        // cast one flame. Should be roar animation here. Not parryable btw. also make sure the timing is right here
        anm.Play("Roar");
        AudioSource.PlayClipAtPoint(RoarSFX, transform.position);
        yield return new WaitForSeconds(1f);
        int rng = (int)Random.Range(5, 8);
        for (int c = 0; c < rng; ++c)
        {
            AudioSource.PlayClipAtPoint(FlameSwatheSFX, transform.position);
            GameObject spell = Instantiate(Resources.Load("EnemySpells/VodBossFire")) as GameObject;
            Spell ps = spell.GetComponent(typeof(Spell)) as Spell;
            ps.caster = gameObject;
            yield return new WaitForSeconds(0.4f);
        }
        CAST = true;
    }

    public AudioClip death;
    public AudioClip gethit;
    public AudioClip scream;
    IEnumerator deathanimation()
    {
        Time.timeScale = 0.2f;
        yield return StartCoroutine(CounterattackAnimation.CoroutineUtil.WaitForRealSeconds(3f));
        Time.timeScale = 1f;
        PlayerController pc = player.GetComponent(typeof(PlayerController)) as PlayerController;
        Spells s = player.GetComponent(typeof(Spells)) as Spells;
        s.AddItem(8); s.AddItem(9);
        pc.isTargeting = false;
        pc.xp += worth;
        GameObject ja = Instantiate(Resources.Load("JoulesAnimation"), transform.position, transform.rotation) as GameObject;
        JouleAnimation ji = ja.GetComponent(typeof(JouleAnimation)) as JouleAnimation;
        ji.xp = worth;
        Items p = player.GetComponent(typeof(Items)) as Items;
        p.bosses[0] = true;
        // you defeated!
        Destroy(gameObject);
    }
    bool isdyingrightnowomg = false;
    public override void Die()
    {
        isdyingrightnowomg = true;
        PlayDeathSound();
        StartCoroutine(deathanimation());
    }
	bool standingstill = false;
    public override void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(death, transform.position);
    }

    public override void PlayHurtSound()
    {
        AudioSource.PlayClipAtPoint(gethit, transform.position);
    }
    bool phase2 = false;
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if (!isdyingrightnowomg)
            {
                Die();
            }
        }
        if (currentHealth < maxHealth * 0.5)
        {
            phase2 = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        if (foundya && !phase2)
        {
            DoubleSwingp += 0.1f;
            RightSwingp += 0.1f;
            LeftSwingp += 0.1f;
            CastOneFlamep += 0.1f;
            SpinAttackp += 0.02f;
        }
        if ((player.transform.position - transform.position).magnitude > 5 && foundya && !phase2)
        {
            ChargeAttackp += 0.2f;
            CastOneFlamep += 0.2f;
            SpinAttackp += 0.2f;
        }
        if (foundya && phase2)
        {
            DoubleSwingp += 0.1f;
            RightSwingp += 0.1f;
            LeftSwingp += 0.1f;
            CastOneFlamep = 0;
            SpinAttackp += 0.1f;
            ChargeAttackp += 0.1f;
            HugeChargedSpinp += 0.11f;
            CastManyFlamesp += 0.3f;
        }
        if ((player.transform.position - transform.position).magnitude > 8 && foundya && phase2)
        {
            ChargeAttackp += 0.3f;
            CastManyFlamesp += 0.3f;
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
        if (currentHealth < maxHealth || (transform.position - player.transform.position).magnitude < 5)
        {
            foundya = true;
            var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.02f);
            isOnScreen = true;
        }
        if (!isStunned && foundya)
        {
			if(!standingstill){
            var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.02f);
            transform.position += transform.up * Time.deltaTime * 0.5f;
			}
            if (CAST)
            {
                float m = Mathf.Max(RightSwingp, LeftSwingp, DoubleSwingp, ChargeAttackp, SpinAttackp, CastOneFlamep, HugeChargedSpinp, CastManyFlamesp);
                if (m <= RightSwingp)
                {
                    int rng = (int)Random.RandomRange(0f, 1f);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(RightSwing()); CAST = false; RightSwingp = 0;
                    }
                    else
                    {
                        /*     
                          float max = Mathf.Max(RightSwingp, LeftSwingp, DoubleSwingp, ChargeAttackp, SpinAttackp, CastOneFlamep, HugeChargedSpinp, CastManyFlamesp);
                          if(max <= RightSwingp){
                          currentanim = StartCoroutine(RightSwing()); CAST = false; RightSwingp = 0; 
                          } else if(max <= LeftSwingp){
                            currentanim = StartCoroutine(LeftSwing()); CAST = false; LeftSwingp = 0;
                        }
                        else if (max <= DoubleSwingp)
                        {
                            currentanim = StartCoroutine(DoubleSwing()); CAST = false; DoubleSwingp = 0;
                        }
                        else if (max <= ChargeAttackp)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; ChargeAttackp = 0;
                        }
                        else if (max <= SpinAttackp)
                        {
                            currentanim = StartCoroutine(SpinAttack()); CAST = false; SpinAttackp = 0;
                        }
                        else if (max >= CastOneFlamep)
                        {
                            currentanim = StartCoroutine(CastOneFlame()); CAST = false; CastOneFlamep = 0;
                        }
                        else if (max 
                            <= HugeChargedSpinp)
                        {
                            currentanim = StartCoroutine(HugeChargedSpin()); CAST = false; HugeChargedSpinp = 0;
                        }
                        else if (max <= CastManyFlamesp)
                        {
                            currentanim = StartCoroutine(CastManyFlames()); CAST = false; CastManyFlamesp = 0;
                        }*/
                        float max = Mathf.Max(LeftSwingp, DoubleSwingp, ChargeAttackp, SpinAttackp, CastOneFlamep, HugeChargedSpinp, CastManyFlamesp);
                        if (max <= LeftSwingp)
                        {
                            currentanim = StartCoroutine(LeftSwing()); CAST = false; LeftSwingp = 0;
                        }
                        else if (max <= DoubleSwingp)
                        {
                            currentanim = StartCoroutine(DoubleSwing()); CAST = false; DoubleSwingp = 0;
                        }
                        else if (max <= ChargeAttackp)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; ChargeAttackp = 0;
                        }
                        else if (max <= SpinAttackp)
                        {
                            currentanim = StartCoroutine(SpinAttack()); CAST = false; SpinAttackp = 0;
                        }
                        else if (max >= CastOneFlamep)
                        {
                            currentanim = StartCoroutine(CastOneFlame()); CAST = false; CastOneFlamep = 0;
                        }
                        else if (max
                            <= HugeChargedSpinp)
                        {
                            currentanim = StartCoroutine(HugeChargedSpin()); CAST = false; HugeChargedSpinp = 0;
                        }
                        else if (max <= CastManyFlamesp)
                        {
                            currentanim = StartCoroutine(CastManyFlames()); CAST = false; CastManyFlamesp = 0;
                        }
                    }
                }
                else if (m <= LeftSwingp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(LeftSwing()); CAST = false; LeftSwingp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(RightSwingp, DoubleSwingp, ChargeAttackp, SpinAttackp, CastOneFlamep, HugeChargedSpinp, CastManyFlamesp);
                        if (max <= RightSwingp)
                        {
                            currentanim = StartCoroutine(RightSwing()); CAST = false; RightSwingp = 0;
                        }
                        else if (max <= DoubleSwingp)
                        {
                            currentanim = StartCoroutine(DoubleSwing()); CAST = false; DoubleSwingp = 0;
                        }
                        else if (max <= ChargeAttackp)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; ChargeAttackp = 0;
                        }
                        else if (max <= SpinAttackp)
                        {
                            currentanim = StartCoroutine(SpinAttack()); CAST = false; SpinAttackp = 0;
                        }
                        else if (max >= CastOneFlamep)
                        {
                            currentanim = StartCoroutine(CastOneFlame()); CAST = false; CastOneFlamep = 0;
                        }
                        else if (max
                            <= HugeChargedSpinp)
                        {
                            currentanim = StartCoroutine(HugeChargedSpin()); CAST = false; HugeChargedSpinp = 0;
                        }
                        else if (max <= CastManyFlamesp)
                        {
                            currentanim = StartCoroutine(CastManyFlames()); CAST = false; CastManyFlamesp = 0;
                        }
                    }
                }
                else if (m <= DoubleSwingp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(DoubleSwing()); CAST = false; DoubleSwingp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(RightSwingp, LeftSwingp, ChargeAttackp, SpinAttackp, CastOneFlamep, HugeChargedSpinp, CastManyFlamesp);
                        if (max <= RightSwingp)
                        {
                            currentanim = StartCoroutine(RightSwing()); CAST = false; RightSwingp = 0;
                        }
                        else if (max <= LeftSwingp)
                        {
                            currentanim = StartCoroutine(LeftSwing()); CAST = false; LeftSwingp = 0;
                        }
                        else if (max <= ChargeAttackp)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; ChargeAttackp = 0;
                        }
                        else if (max <= SpinAttackp)
                        {
                            currentanim = StartCoroutine(SpinAttack()); CAST = false; SpinAttackp = 0;
                        }
                        else if (max >= CastOneFlamep)
                        {
                            currentanim = StartCoroutine(CastOneFlame()); CAST = false; CastOneFlamep = 0;
                        }
                        else if (max
                            <= HugeChargedSpinp)
                        {
                            currentanim = StartCoroutine(HugeChargedSpin()); CAST = false; HugeChargedSpinp = 0;
                        }
                        else if (max <= CastManyFlamesp)
                        {
                            currentanim = StartCoroutine(CastManyFlames()); CAST = false; CastManyFlamesp = 0;
                        }
                    }
                }
                else if (m <= ChargeAttackp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(ChargeAttack()); CAST = false; ChargeAttackp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(RightSwingp, LeftSwingp, DoubleSwingp,  SpinAttackp, CastOneFlamep, HugeChargedSpinp, CastManyFlamesp);
                        if (max <= RightSwingp)
                        {
                            currentanim = StartCoroutine(RightSwing()); CAST = false; RightSwingp = 0;
                        }
                        else if (max <= LeftSwingp)
                        {
                            currentanim = StartCoroutine(LeftSwing()); CAST = false; LeftSwingp = 0; 
                        }
                        else if (max <= DoubleSwingp)
                        {
                            currentanim = StartCoroutine(DoubleSwing()); CAST = false; DoubleSwingp = 0;
                        }
                        else if (max <= SpinAttackp)
                        {
                            currentanim = StartCoroutine(SpinAttack()); CAST = false; SpinAttackp = 0;
                        }
                        else if (max >= CastOneFlamep)
                        {
                            currentanim = StartCoroutine(CastOneFlame()); CAST = false; CastOneFlamep = 0; 
                        }
                        else if (max
                            <= HugeChargedSpinp)
                        {
                            currentanim = StartCoroutine(HugeChargedSpin()); CAST = false; HugeChargedSpinp = 0; 
                        }
                        else if (max <= CastManyFlamesp)
                        {
                            currentanim = StartCoroutine(CastManyFlames()); CAST = false; CastManyFlamesp = 0;
                        }
                    }
                }
                else if (m <= SpinAttackp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(SpinAttack()); CAST = false; SpinAttackp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(RightSwingp, LeftSwingp, DoubleSwingp, ChargeAttackp, CastOneFlamep, HugeChargedSpinp, CastManyFlamesp);
                        if (max <= RightSwingp)
                        {
                            currentanim = StartCoroutine(RightSwing()); CAST = false; RightSwingp = 0;
                        }
                        else if (max <= LeftSwingp)
                        {
                            currentanim = StartCoroutine(LeftSwing()); CAST = false; LeftSwingp = 0; 
                        }
                        else if (max <= DoubleSwingp)
                        {
                            currentanim = StartCoroutine(DoubleSwing()); CAST = false; DoubleSwingp = 0;
                        }
                        else if (max <= ChargeAttackp)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; ChargeAttackp = 0; 
                        }
                        else if (max >= CastOneFlamep)
                        {
                            currentanim = StartCoroutine(CastOneFlame()); CAST = false; CastOneFlamep = 0; 
                        }
                        else if (max
                            <= HugeChargedSpinp)
                        {
                            currentanim = StartCoroutine(HugeChargedSpin()); CAST = false; HugeChargedSpinp = 0; 
                        }
                        else if (max <= CastManyFlamesp)
                        {
                            currentanim = StartCoroutine(CastManyFlames()); CAST = false; CastManyFlamesp = 0;
                        }
                    }
                }
                else if (m <= CastOneFlamep)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(CastOneFlame()); CAST = false; CastOneFlamep = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(RightSwingp, LeftSwingp, DoubleSwingp, ChargeAttackp, SpinAttackp, HugeChargedSpinp, CastManyFlamesp);
                        if (max <= RightSwingp)
                        {
                            currentanim = StartCoroutine(RightSwing()); CAST = false; RightSwingp = 0;
                        }
                        else if (max <= LeftSwingp)
                        {
                            currentanim = StartCoroutine(LeftSwing()); CAST = false; LeftSwingp = 0;
                        }
                        else if (max <= DoubleSwingp)
                        {
                            currentanim = StartCoroutine(DoubleSwing()); CAST = false; DoubleSwingp = 0;
                        }
                        else if (max <= ChargeAttackp)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; ChargeAttackp = 0;
                        }
                        else if (max <= SpinAttackp)
                        {
                            currentanim = StartCoroutine(SpinAttack()); CAST = false; SpinAttackp = 0;
                        }
                        else if (max
                            <= HugeChargedSpinp)
                        {
                            currentanim = StartCoroutine(HugeChargedSpin()); CAST = false; HugeChargedSpinp = 0;
                        }
                        else if (max <= CastManyFlamesp)
                        {
                            currentanim = StartCoroutine(CastManyFlames()); CAST = false; CastManyFlamesp = 0;
                        }
                    }
                }
                else if (m <= HugeChargedSpinp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(HugeChargedSpin()); CAST = false; HugeChargedSpinp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(RightSwingp, LeftSwingp, DoubleSwingp, ChargeAttackp, SpinAttackp, CastOneFlamep, CastManyFlamesp);
                        if (max <= RightSwingp)
                        {
                            currentanim = StartCoroutine(RightSwing()); CAST = false; RightSwingp = 0;
                        }
                        else if (max <= LeftSwingp)
                        {
                            currentanim = StartCoroutine(LeftSwing()); CAST = false; LeftSwingp = 0;
                        }
                        else if (max <= DoubleSwingp)
                        {
                            currentanim = StartCoroutine(DoubleSwing()); CAST = false; DoubleSwingp = 0;
                        }
                        else if (max <= ChargeAttackp)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; ChargeAttackp = 0;
                        }
                        else if (max <= SpinAttackp)
                        {
                            currentanim = StartCoroutine(SpinAttack()); CAST = false; SpinAttackp = 0;
                        }
                        else if (max >= CastOneFlamep)
                        {
                            currentanim = StartCoroutine(CastOneFlame()); CAST = false; CastOneFlamep = 0;
                        }
                        else if (max <= CastManyFlamesp)
                        {
                            currentanim = StartCoroutine(CastManyFlames()); CAST = false; CastManyFlamesp = 0;
                        }
                    }
                }
                else if (m <= CastManyFlamesp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(CastManyFlames()); CAST = false; CastManyFlamesp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(RightSwingp, LeftSwingp, DoubleSwingp, ChargeAttackp, SpinAttackp, CastOneFlamep, HugeChargedSpinp);
                        if (max <= RightSwingp)
                        {
                            currentanim = StartCoroutine(RightSwing()); CAST = false; RightSwingp = 0;
                        }
                        else if (max <= LeftSwingp)
                        {
                            currentanim = StartCoroutine(LeftSwing()); CAST = false; LeftSwingp = 0;
                        }
                        else if (max <= DoubleSwingp)
                        {
                            currentanim = StartCoroutine(DoubleSwing()); CAST = false; DoubleSwingp = 0;
                        }
                        else if (max <= ChargeAttackp)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; ChargeAttackp = 0;
                        }
                        else if (max <= SpinAttackp)
                        {
                            currentanim = StartCoroutine(SpinAttack()); CAST = false; SpinAttackp = 0;
                        }
                        else if (max >= CastOneFlamep)
                        {
                            currentanim = StartCoroutine(CastOneFlame()); CAST = false; CastOneFlamep = 0;
                        }
                        else if (max
                            <= HugeChargedSpinp)
                        {
                            currentanim = StartCoroutine(HugeChargedSpin()); CAST = false; HugeChargedSpinp = 0;
                        }
                    }
                }
            }
        }
    }
}
