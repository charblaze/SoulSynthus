using UnityEngine;
using System.Collections;

public class Vod_GenericScript : Character {
    public bool foundya = false;
    private bool isOnScreen = false;
    private SpellCastScript spellcast;
    float runtimer = 1f;
    float spelltimer = 1f;

    // Use this for initialization
    void Start()
    {
        pushback = 0.6f;
        worth = 56;
        moveSpeed = 1.5f;
        moveSpeedOriginal = moveSpeed;
        currentHealth = 60;
        maxHealth = 60;
        isStunned = false;
        isMoving = false;
        immunityFrames = false;
        stunDuration = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent(typeof(PlayerController)) as PlayerController;
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


    IEnumerator CastSpell1()
    {
        GameObject caster = Instantiate(Resources.Load("caste"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
        CastAnimScriptEnemy anms = caster.GetComponent(typeof(CastAnimScriptEnemy)) as CastAnimScriptEnemy;
        anms.howtime = 1f;
        anms.player = gameObject;
        yield return new WaitForSeconds(1f);
        CastSpell(0);
    }

    IEnumerator CastSpell2()
    {
        GameObject caster = Instantiate(Resources.Load("caste"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
        CastAnimScriptEnemy anms = caster.GetComponent(typeof(CastAnimScriptEnemy)) as CastAnimScriptEnemy;
        anms.howtime = 0.5f;
        anms.player = gameObject;
        yield return new WaitForSeconds(0.5f);
        CastSpell(2);
    }
    PlayerController pc;
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            
            pc.xp += 200;
            Destroy(gameObject);
        }
        if (isStunned)
        {
            isMoving = false;
            //foundya = false;
        }
        if (!isStunned)
        {
            if (!isMoving)
            {
            }
            if (moveSpeed < 0)
            {
                moveSpeed = 0;
            }

            if (currentHealth < maxHealth || (transform.position - player.transform.position).magnitude < 3)
            {
                foundya = true;
                var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.1f);
            }
            else
            {
            }
        }
        if (foundya && !isStunned)
        {
            var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.1f);
            runtimer -= Time.deltaTime;
            spelltimer -= Time.deltaTime;
            if ((transform.position - player.transform.position).magnitude > 1.5)
            {
                
                if (runtimer <= 0)
                {
                    StartCoroutine(FloatInDirection("Up"));
                    runtimer = 0.25f;
                }
            }
            else
            {
                if ((transform.position - player.transform.position).magnitude > 1f)
                {
                    if (runtimer <= 0)
                    {
                        StartCoroutine(FloatInDirection("Up"));
                        runtimer = 0.55f;
                    }
                } else
                {
                    int rng = (int)Random.RandomRange(0f, 3f);
                    if (rng == 0)
                    {
                        if (runtimer <= 0)
                        {
                            StartCoroutine(FloatInDirection("Up"));
                            runtimer = 1.5f;
                        }
                    }
                    else if (rng == 1)
                    {
                        if (runtimer <= 0)
                        {
                            StartCoroutine(FloatInDirection("Left"));
                            runtimer =1.5f;
                        }
                    }
                    else if (rng == 2)
                    {
                        if (runtimer <= 0)
                        {
                            StartCoroutine(FloatInDirection("Right"));
                            runtimer = 1.5f;
                        }
                    }
                    else
                    {
                        if (runtimer <= 0)
                        {
                            StartCoroutine(FloatInDirection("Back"));
                            runtimer = 1.5f;
                        }
                    }
                    if (rng == 0 || rng == 3)
                    {
                        if (spelltimer <= 1)
                        {
                            if (spelltimer <= 0)
                            {
                                StartCoroutine(CastSpell1());
                                spelltimer = 4f;
                            }
                        }
                    }
                    else
                    {
                        if (spelltimer <= 3)
                        {
                            if (spelltimer <= 0)
                            {
                                StartCoroutine(CastSpell2());
                                spelltimer = 4f;
                            }
                        }
                    }
                }
            }
        }
    }
}
