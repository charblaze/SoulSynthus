using UnityEngine;
using System.Collections;

public class SEAMONSTER : Character
{
    public bool foundya = false;
    private bool isOnScreen = false;
    private SpellCastScript spellcast;
    float runtimer = 1f;
    float spelltimer = 1f;
    
    Animator anm;

    GameObject laserL, laserM, laserR, jointL, jointM, jointR, tentacleL, tentacleM, tentacleR;
    GameObject groundslam;

    // Use this for initialization
    void Start()
    {
        pushback = 0.2f;
        worth = 5000;
        moveSpeed = 2f;
        moveSpeedOriginal = moveSpeed;
        currentHealth = 1800;
        maxHealth = 2000;
        isStunned = false;
        isMoving = false;
        immunityFrames = false;
        stunDuration = 0;
        candrown = false;
        player = GameObject.FindGameObjectWithTag("Player");
        //pc = player.GetComponent(typeof(PlayerController)) as PlayerController;
        groundslam = transform.GetChild(0).transform.GetChild(0).transform.GetChild(7).gameObject;
        anm = transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Animator>();
        laserL = transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).gameObject;
        tentacleL = transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).gameObject;
        jointL = transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject;

        laserM = transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).transform.GetChild(1).gameObject;
        tentacleM = transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).gameObject;
        jointM = transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).transform.GetChild(1).gameObject;

        laserR = transform.GetChild(0).transform.GetChild(0).transform.GetChild(5).transform.GetChild(0).transform.GetChild(1).gameObject;
        tentacleR = transform.GetChild(0).transform.GetChild(0).transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).gameObject;
        jointR = transform.GetChild(0).transform.GetChild(0).transform.GetChild(5).transform.GetChild(1).gameObject;

        groundslam.SetActive(false);
        laserL.SetActive(false);
        laserR.SetActive(false);
        laserM.SetActive(false);
        jointL.SetActive(false);
        jointM.SetActive(false);
        jointR.SetActive(false);
        tentacleL.SetActive(false);
        tentacleR.SetActive(false);
        tentacleM.SetActive(false);

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
        ParrySound();
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
        GUI.Label(HPText, "Malformed Squid", myStyle);
        GUI.Label(HPText, "Malformed Squid", myStylefg);
        Rect StunnedText = new Rect(1920 / 2 + 352, 1080 - 302 + 75, 100, 25);
        Rect SlowedText = new Rect(1920 / 2 + 352, 1080 - 302 + 50, 100, 25);
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
        Rect HealthSub = new Rect(1920 / 2 + 352, 1080 - 330, 100, 25);
        if (damageTaken != 0)
        {
            GUI.Label(HealthSub, "-" + damageTaken, myStyle);
            GUI.Label(HealthSub, "-" + damageTaken, myStylefg);
        }
        Rect HealthHeal = new Rect(1920 / 2 + 352, 1080 - 370, 100, 25);
        if (healingTaken != 0)
        {
            GUI.Label(HealthHeal, "+" + healingTaken, myStyle);
            GUI.Label(HealthHeal, "+" + healingTaken, myStylefg);
        }
    }

    Coroutine currentanim;


    PlayerController pc;
    bool CAST = true;
    float HugeSlamp = 0f, NormalAttacksp = 0f, Slamp = 0f, Chargep = 0f, Laserp = 0f, Gocrazyp = 0f, Flybackp = 0f, Flyforwardp = 0f, Attackp = 0f;
    bool isParryable = false;
    public AudioClip HugeSlamSFX, NormalAttacksSFX, SlamSFX, ChargeSFX, LaserSFX, GoCrazySFX, FlylaserSFX;

    IEnumerator HugeSlam()
    {
        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE
        // make this animation so he spins many many times so he stunlocks you if you are derp
        // NOT PARRYABLE (maybe?)
        // actually just make this a go crazy move
        standingstill = true;
        anm.Play("HugeSlam");
        AudioSource.PlayClipAtPoint(HugeSlamSFX, transform.position);
        
        yield return new WaitForSeconds(1.58f);
        groundslam.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        groundslam.SetActive(false);

        yield return new WaitForSeconds(2f);

        CAST = true;
        standingstill = false;
    }

    IEnumerator NormalAttacks()
    {
        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE
        anm.Play("NormalAttacks");
        AudioSource.PlayClipAtPoint(NormalAttacksSFX, transform.position);
        yield return new WaitForSeconds(0.33f);
        tentacleM.SetActive(true); jointM.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        tentacleM.SetActive(false); jointM.SetActive(false);
        tentacleL.SetActive(true); tentacleR.SetActive(true); jointL.SetActive(true); jointR.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        tentacleL.SetActive(false); tentacleR.SetActive(false); tentacleM.SetActive(false); jointL.SetActive(false); jointR.SetActive(false); jointM.SetActive(false);
        yield return new WaitForSeconds(2f);
        CAST = true;
    }

    IEnumerator Slam()
    {        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE

        anm.Play("Slam");
        AudioSource.PlayClipAtPoint(SlamSFX, transform.position);
        yield return new WaitForSeconds(0.4f);
        tentacleL.SetActive(true); tentacleR.SetActive(true); tentacleM.SetActive(true); jointL.SetActive(true); jointR.SetActive(true); jointM.SetActive(true);

        yield return new WaitForSeconds(0.23f);
        tentacleL.SetActive(false); tentacleR.SetActive(false); tentacleM.SetActive(false); jointL.SetActive(false); jointR.SetActive(false); jointM.SetActive(false);
        yield return new WaitForSeconds(3f);
        CAST = true;
    }

    IEnumerator movealotforward()
    {
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Up"));
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator movealotbackwards()
    {
        StartCoroutine(FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FloatInDirection("Back"));
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator ChargeAttack()
    {        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE
        notrotating = true;
        anm.Play("Charge");
        AudioSource.PlayClipAtPoint(ChargeSFX, transform.position);
        yield return new WaitForSeconds(1.85f);
        tentacleL.SetActive(true); tentacleR.SetActive(true); tentacleM.SetActive(true); jointL.SetActive(true); jointR.SetActive(true); jointM.SetActive(true);
        // 1.15 seconds
        StartCoroutine(movealotforward());
        yield return new WaitForSeconds(1.15f);
        tentacleL.SetActive(false); tentacleR.SetActive(false); tentacleM.SetActive(false); jointL.SetActive(false); jointR.SetActive(false); jointM.SetActive(false);

        yield return new WaitForSeconds(2f);
        CAST = true;
        notrotating = false;
    }
    IEnumerator Lasers()
    {        // ******************** NEEDS PROPER TIMING AFTER ANIMATIONS ARE DONE
        anm.Play("Laser");
        AudioSource.PlayClipAtPoint(LaserSFX, transform.position);
        yield return new WaitForSeconds(1.1f);
        laserL.SetActive(true); laserM.SetActive(true); laserR.SetActive(true);
        StartCoroutine(laserL.GetComponent<SeaMonsterLaser>().DoAnimation());
        StartCoroutine(laserR.GetComponent<SeaMonsterLaser>().DoAnimation());
        StartCoroutine(laserM.GetComponent<SeaMonsterLaser>().DoAnimation());
        yield return new WaitForSeconds(3f);
        
        CAST = true;
    }

    IEnumerator GoCrazy()
    {
        // cast one flame. Should be roar animation here. Not parryable btw. also make sure the timing is right here
        anm.Play("GoCrazy");
        AudioSource.PlayClipAtPoint(GoCrazySFX, transform.position);
        yield return new WaitForSeconds(2.75f);
        tentacleL.SetActive(true); tentacleR.SetActive(true); tentacleM.SetActive(true); jointL.SetActive(true); jointR.SetActive(true); jointM.SetActive(true);
        yield return new WaitForSeconds(1.08f);
        tentacleL.SetActive(false); tentacleR.SetActive(false); tentacleM.SetActive(false); jointL.SetActive(false); jointR.SetActive(false); jointM.SetActive(false);
        yield return new WaitForSeconds(2f);
        CAST = true;
    }
    bool notrotating = false;
    IEnumerator FlyBack()
    {
        // cast one flame. Should be roar animation here. Not parryable btw. also make sure the timing is right here
        anm.Play("Flybacklaser");

        AudioSource.PlayClipAtPoint(FlylaserSFX, transform.position);
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(movealotbackwards());
        yield return new WaitForSeconds(3.01f);
        laserL.SetActive(true); laserM.SetActive(true); laserR.SetActive(true);
        StartCoroutine(laserL.GetComponent<SeaMonsterLaser>().DoAnimation());
        StartCoroutine(laserR.GetComponent<SeaMonsterLaser>().DoAnimation());
        StartCoroutine(laserM.GetComponent<SeaMonsterLaser>().DoAnimation());
        yield return new WaitForSeconds(3f);
        CAST = true;
    }

    IEnumerator Attack()
    {
        // cast one flame. Should be roar animation here. Not parryable btw. also make sure the timing is right here
        anm.Play("Flybacklaser");

        AudioSource.PlayClipAtPoint(FlylaserSFX, transform.position);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(movealotbackwards());
        yield return new WaitForSeconds(3.01f);
        laserL.SetActive(true); laserM.SetActive(true); laserR.SetActive(true);
        StartCoroutine(laserL.GetComponent<SeaMonsterLaser>().DoAnimation());
        StartCoroutine(laserR.GetComponent<SeaMonsterLaser>().DoAnimation());
        StartCoroutine(laserM.GetComponent<SeaMonsterLaser>().DoAnimation());
        yield return new WaitForSeconds(3f);
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
        s.AddItem(12); s.AddItem(16);
        pc.isTargeting = false;
        pc.xp += worth;
        GameObject ja = Instantiate(Resources.Load("JoulesAnimation"), transform.position, transform.rotation) as GameObject;
        JouleAnimation ji = ja.GetComponent(typeof(JouleAnimation)) as JouleAnimation;
        ji.xp = worth;
        Items p = player.GetComponent(typeof(Items)) as Items;
        p.bosses[2] = true;
        // you defeated!
        Destroy(gameObject);
    }
    public override void Die()
    {
        GameObject x = GameObject.Find("Ophelia1(Clone)");
        if (x!= null){
            Destroy(x);
        }
        // 3 - ophelia doesn't spawn near fountain
        // 4 - ophelia spawns in gardens
        player.GetComponent<Items>().bosses[3] = true;
        player.GetComponent<Items>().bosses[4] = true;
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
    bool isdyingrightnowomg = false;
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if (!isdyingrightnowomg)
            {
                isdyingrightnowomg = true;
                Die();
            }
        }
        if (currentHealth < maxHealth * 0.5)
        {
            phase2 = true;
        }
        //transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        if (foundya && !phase2)
        {
            HugeSlamp += 0.1f;
            NormalAttacksp += 0.1f;
            Slamp += 0.1f;
            Laserp+= 0.1f;
            Chargep += 0.1f;
            // TEST PURPOSES
            /*Gocrazyp += 0.11f;
            Flybackp += 0.15f;*/
        }
        if ((player.transform.position - transform.position).magnitude > 5 && foundya && !phase2)
        {
            Chargep += 0.2f;
            Laserp += 0.2f;
        }
        if (foundya && phase2)
        {
            HugeSlamp += 0.1f;
            NormalAttacksp += 0.1f;
            Slamp += 0.1f;
            Laserp += 0.1f;
            Chargep += 0.1f;
            Gocrazyp += 0.11f;
            Flybackp += 0.15f;
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
            if (!notrotating)
            {
                var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.02f);
            }
            if (CAST)
            {
                float m = Mathf.Max(HugeSlamp, NormalAttacksp, Slamp, Chargep, Laserp, Gocrazyp, Flybackp, Attackp);
                if (m <= HugeSlamp)
                {
                    int rng = (int)Random.RandomRange(0f, 1f);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(HugeSlam()); CAST = false; HugeSlamp = 0;
                    }
                    else
                    {
                        /*     
                          float max = Mathf.Max(HugeSlamp, NormalAttacksp, Slamp, Chargep, Laserp, Gocrazyp, Flybackp, Attackp);
                          if(max <= HugeSlamp){
                          currentanim = StartCoroutine(HugeSlam()); CAST = false; HugeSlamp = 0; 
                          } else if(max <= NormalAttacksp){
                            currentanim = StartCoroutine(NormalAttacks()); CAST = false; NormalAttacksp = 0;
                        }
                        else if (max <= Slamp)
                        {
                            currentanim = StartCoroutine(Slam()); CAST = false; Slamp = 0;
                        }
                        else if (max <= Chargep)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; Chargep = 0;
                        }
                        else if (max <= Laserp)
                        {
                            currentanim = StartCoroutine(Lasers()); CAST = false; Laserp = 0;
                        }
                        else if (max >= Gocrazyp)
                        {
                            currentanim = StartCoroutine(GoCrazy()); CAST = false; Gocrazyp = 0;
                        }
                        else if (max 
                            <= Flybackp)
                        {
                            currentanim = StartCoroutine(FlyBack()); CAST = false; Flybackp = 0;
                        }
                        else if (max <= Attackp)
                        {
                            currentanim = StartCoroutine(Attack()); CAST = false; Attackp = 0;
                        }*/
                        float max = Mathf.Max(NormalAttacksp, Slamp, Chargep, Laserp, Gocrazyp, Flybackp, Attackp);
                        if (max <= NormalAttacksp)
                        {
                            currentanim = StartCoroutine(NormalAttacks()); CAST = false; NormalAttacksp = 0;
                        }
                        else if (max <= Slamp)
                        {
                            currentanim = StartCoroutine(Slam()); CAST = false; Slamp = 0;
                        }
                        else if (max <= Chargep)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; Chargep = 0;
                        }
                        else if (max <= Laserp)
                        {
                            currentanim = StartCoroutine(Lasers()); CAST = false; Laserp = 0;
                        }
                        else if (max >= Gocrazyp)
                        {
                            currentanim = StartCoroutine(GoCrazy()); CAST = false; Gocrazyp = 0;
                        }
                        else if (max
                            <= Flybackp)
                        {
                            currentanim = StartCoroutine(FlyBack()); CAST = false; Flybackp = 0;
                        }
                        else if (max <= Attackp)
                        {
                            currentanim = StartCoroutine(Attack()); CAST = false; Attackp = 0;
                        }
                    }
                }
                else if (m <= NormalAttacksp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(NormalAttacks()); CAST = false; NormalAttacksp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(HugeSlamp, Slamp, Chargep, Laserp, Gocrazyp, Flybackp, Attackp);
                        if (max <= HugeSlamp)
                        {
                            currentanim = StartCoroutine(HugeSlam()); CAST = false; HugeSlamp = 0;
                        }
                        else if (max <= Slamp)
                        {
                            currentanim = StartCoroutine(Slam()); CAST = false; Slamp = 0;
                        }
                        else if (max <= Chargep)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; Chargep = 0;
                        }
                        else if (max <= Laserp)
                        {
                            currentanim = StartCoroutine(Lasers()); CAST = false; Laserp = 0;
                        }
                        else if (max >= Gocrazyp)
                        {
                            currentanim = StartCoroutine(GoCrazy()); CAST = false; Gocrazyp = 0;
                        }
                        else if (max
                            <= Flybackp)
                        {
                            currentanim = StartCoroutine(FlyBack()); CAST = false; Flybackp = 0;
                        }
                        else if (max <= Attackp)
                        {
                            currentanim = StartCoroutine(Attack()); CAST = false; Attackp = 0;
                        }
                    }
                }
                else if (m <= Slamp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(Slam()); CAST = false; Slamp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(HugeSlamp, NormalAttacksp, Chargep, Laserp, Gocrazyp, Flybackp, Attackp);
                        if (max <= HugeSlamp)
                        {
                            currentanim = StartCoroutine(HugeSlam()); CAST = false; HugeSlamp = 0;
                        }
                        else if (max <= NormalAttacksp)
                        {
                            currentanim = StartCoroutine(NormalAttacks()); CAST = false; NormalAttacksp = 0;
                        }
                        else if (max <= Chargep)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; Chargep = 0;
                        }
                        else if (max <= Laserp)
                        {
                            currentanim = StartCoroutine(Lasers()); CAST = false; Laserp = 0;
                        }
                        else if (max >= Gocrazyp)
                        {
                            currentanim = StartCoroutine(GoCrazy()); CAST = false; Gocrazyp = 0;
                        }
                        else if (max
                            <= Flybackp)
                        {
                            currentanim = StartCoroutine(FlyBack()); CAST = false; Flybackp = 0;
                        }
                        else if (max <= Attackp)
                        {
                            currentanim = StartCoroutine(Attack()); CAST = false; Attackp = 0;
                        }
                    }
                }
                else if (m <= Chargep)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(ChargeAttack()); CAST = false; Chargep = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(HugeSlamp, NormalAttacksp, Slamp, Laserp, Gocrazyp, Flybackp, Attackp);
                        if (max <= HugeSlamp)
                        {
                            currentanim = StartCoroutine(HugeSlam()); CAST = false; HugeSlamp = 0;
                        }
                        else if (max <= NormalAttacksp)
                        {
                            currentanim = StartCoroutine(NormalAttacks()); CAST = false; NormalAttacksp = 0;
                        }
                        else if (max <= Slamp)
                        {
                            currentanim = StartCoroutine(Slam()); CAST = false; Slamp = 0;
                        }
                        else if (max <= Laserp)
                        {
                            currentanim = StartCoroutine(Lasers()); CAST = false; Laserp = 0;
                        }
                        else if (max >= Gocrazyp)
                        {
                            currentanim = StartCoroutine(GoCrazy()); CAST = false; Gocrazyp = 0;
                        }
                        else if (max
                            <= Flybackp)
                        {
                            currentanim = StartCoroutine(FlyBack()); CAST = false; Flybackp = 0;
                        }
                        else if (max <= Attackp)
                        {
                            currentanim = StartCoroutine(Attack()); CAST = false; Attackp = 0;
                        }
                    }
                }
                else if (m <= Laserp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(Lasers()); CAST = false; Laserp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(HugeSlamp, NormalAttacksp, Slamp, Chargep, Gocrazyp, Flybackp, Attackp);
                        if (max <= HugeSlamp)
                        {
                            currentanim = StartCoroutine(HugeSlam()); CAST = false; HugeSlamp = 0;
                        }
                        else if (max <= NormalAttacksp)
                        {
                            currentanim = StartCoroutine(NormalAttacks()); CAST = false; NormalAttacksp = 0;
                        }
                        else if (max <= Slamp)
                        {
                            currentanim = StartCoroutine(Slam()); CAST = false; Slamp = 0;
                        }
                        else if (max <= Chargep)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; Chargep = 0;
                        }
                        else if (max >= Gocrazyp)
                        {
                            currentanim = StartCoroutine(GoCrazy()); CAST = false; Gocrazyp = 0;
                        }
                        else if (max
                            <= Flybackp)
                        {
                            currentanim = StartCoroutine(FlyBack()); CAST = false; Flybackp = 0;
                        }
                        else if (max <= Attackp)
                        {
                            currentanim = StartCoroutine(Attack()); CAST = false; Attackp = 0;
                        }
                    }
                }
                else if (m <= Gocrazyp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(GoCrazy()); CAST = false; Gocrazyp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(HugeSlamp, NormalAttacksp, Slamp, Chargep, Laserp, Flybackp, Attackp);
                        if (max <= HugeSlamp)
                        {
                            currentanim = StartCoroutine(HugeSlam()); CAST = false; HugeSlamp = 0;
                        }
                        else if (max <= NormalAttacksp)
                        {
                            currentanim = StartCoroutine(NormalAttacks()); CAST = false; NormalAttacksp = 0;
                        }
                        else if (max <= Slamp)
                        {
                            currentanim = StartCoroutine(Slam()); CAST = false; Slamp = 0;
                        }
                        else if (max <= Chargep)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; Chargep = 0;
                        }
                        else if (max <= Laserp)
                        {
                            currentanim = StartCoroutine(Lasers()); CAST = false; Laserp = 0;
                        }
                        else if (max
                            <= Flybackp)
                        {
                            currentanim = StartCoroutine(FlyBack()); CAST = false; Flybackp = 0;
                        }
                        else if (max <= Attackp)
                        {
                            currentanim = StartCoroutine(Attack()); CAST = false; Attackp = 0;
                        }
                    }
                }
                else if (m <= Flybackp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(FlyBack()); CAST = false; Flybackp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(HugeSlamp, NormalAttacksp, Slamp, Chargep, Laserp, Gocrazyp, Attackp);
                        if (max <= HugeSlamp)
                        {
                            currentanim = StartCoroutine(HugeSlam()); CAST = false; HugeSlamp = 0;
                        }
                        else if (max <= NormalAttacksp)
                        {
                            currentanim = StartCoroutine(NormalAttacks()); CAST = false; NormalAttacksp = 0;
                        }
                        else if (max <= Slamp)
                        {
                            currentanim = StartCoroutine(Slam()); CAST = false; Slamp = 0;
                        }
                        else if (max <= Chargep)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; Chargep = 0;
                        }
                        else if (max <= Laserp)
                        {
                            currentanim = StartCoroutine(Lasers()); CAST = false; Laserp = 0;
                        }
                        else if (max >= Gocrazyp)
                        {
                            currentanim = StartCoroutine(GoCrazy()); CAST = false; Gocrazyp = 0;
                        }
                        else if (max <= Attackp)
                        {
                            currentanim = StartCoroutine(Attack()); CAST = false; Attackp = 0;
                        }
                    }
                }
                else if (m <= Attackp)
                {
                    int rng = (int)Random.Range(0, 1);
                    if (rng == 0)
                    {
                        currentanim = StartCoroutine(Attack()); CAST = false; Attackp = 0;
                    }
                    else
                    {
                        float max = Mathf.Max(HugeSlamp, NormalAttacksp, Slamp, Chargep, Laserp, Gocrazyp, Flybackp);
                        if (max <= HugeSlamp)
                        {
                            currentanim = StartCoroutine(HugeSlam()); CAST = false; HugeSlamp = 0;
                        }
                        else if (max <= NormalAttacksp)
                        {
                            currentanim = StartCoroutine(NormalAttacks()); CAST = false; NormalAttacksp = 0;
                        }
                        else if (max <= Slamp)
                        {
                            currentanim = StartCoroutine(Slam()); CAST = false; Slamp = 0;
                        }
                        else if (max <= Chargep)
                        {
                            currentanim = StartCoroutine(ChargeAttack()); CAST = false; Chargep = 0;
                        }
                        else if (max <= Laserp)
                        {
                            currentanim = StartCoroutine(Lasers()); CAST = false; Laserp = 0;
                        }
                        else if (max >= Gocrazyp)
                        {
                            currentanim = StartCoroutine(GoCrazy()); CAST = false; Gocrazyp = 0;
                        }
                        else if (max
                            <= Flybackp)
                        {
                            currentanim = StartCoroutine(FlyBack()); CAST = false; Flybackp = 0;
                        }
                    }
                }
            }
        }
    }
}
