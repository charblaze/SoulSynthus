using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class VodBossFire : Spell
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
        transform.rotation = caster.transform.rotation;
        spriterender = GetComponent<Renderer>() as SpriteRenderer;
        player = GameObject.FindGameObjectWithTag("Player");
        spells = player.GetComponent(typeof(Spells)) as Spells;
        int rng = (int)Random.Range(0, 1);
        if (rng == 0)
        {
            transform.position = player.transform.position + player.transform.right * 0.4f;
        }
        else
        {
            transform.position = player.transform.position - player.transform.right * 0.4f;
        }
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        CircleCollider2D bc = GetComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        bc.enabled = false;
        spriterender.enabled = true;
        for (int c = 0; c < 8; c++)
        {
            spriterender.sprite = Effect[c];
            yield return new WaitForSeconds(.03f);
        }
        yield return new WaitForSeconds(0.5f);
        bc.enabled = true;
        for (int c = 8; c < Effect.Length; ++c)
        {
            spriterender.sprite = Effect[c];
            yield return new WaitForSeconds(.04f);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != caster && other.gameObject != null)
        {

            Character ge = other.gameObject.GetComponent(typeof(Character)) as Character;
            if (ge != null)
            {
                ge.LoseHealth(30);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
