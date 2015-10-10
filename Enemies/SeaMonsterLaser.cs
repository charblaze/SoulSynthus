using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class SeaMonsterLaser : Spell
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

    // Use this for initialization
    void Start()
    {
        BoxCollider2D col = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        col.enabled = false;
        spriterender = GetComponent<SpriteRenderer>();
    }

    public IEnumerator DoAnimation()
    {
        spriterender = GetComponent<SpriteRenderer>();
        spriterender.enabled = true;
        for (int c = 0; c < 8; c++)
        {
            spriterender.sprite = Effect[c];
            yield return new WaitForSeconds(.05f);
        }
        BoxCollider2D col = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        col.enabled = true;
        for (int c = 8; c < Effect.Length; c++)
        {
            spriterender.sprite = Effect[c];
            yield return new WaitForSeconds(.05f);
        }
        col.enabled = false;
        spriterender.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != caster && other.gameObject != null)
        {

            Character ge = other.gameObject.GetComponent(typeof(Character)) as Character;
            if (ge != null)
            {
                ge.LoseHealth(110);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
