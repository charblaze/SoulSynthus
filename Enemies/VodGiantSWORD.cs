using UnityEngine;
using System.Collections;

public class VodGiantSWORD : Spell
{

    int id;
    public Sprite[] Effect;
    int basedmg;
    float movespeedreduc;
    float reducdoration;
    float stun;
    int dot;
    float dotspread;
    int heal;
    int hot;
    float hotspread;
    float velocity;
    Texture2D icon;
    float cd;
    GameObject player;
    SpriteRenderer spriterender;
    Spells spells;
    bool noVel;

    public bool isEnemy = false;

    // Use this for initialization
    void Start()
    {
        caster = gameObject;
            isEnemy = true;
        spriterender = GetComponent<Renderer>() as SpriteRenderer;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != caster && other.gameObject != null)
        {
            if ((!isEnemy) || (isEnemy && other.gameObject.tag != "Enemy"))
            {
                Character ge = other.gameObject.GetComponent(typeof(Character)) as Character;
                if (ge != null)
                {
                    ge.LoseHealth(30);
                    ge.STUNNED(0.2f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
