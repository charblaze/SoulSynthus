using UnityEngine;
using System.Collections;

public class VodGiantLIGHTNIGN : Spell
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

        transform.position = caster.transform.position + caster.transform.up * 2f;
        transform.rotation = caster.transform.rotation;
            isEnemy = true;
        spriterender = GetComponent<Renderer>() as SpriteRenderer;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.SetParent(caster.transform);
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        spriterender.enabled = true;
        for (int c = 0; c < Effect.Length; c++)
        {
            spriterender.sprite = Effect[c];
            yield return new WaitForSeconds(.05f);
        }
        Destroy(gameObject);
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
                    ge.STUNNED(3f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (caster != null)
        {

            transform.position = caster.transform.position + caster.transform.up * 2f;
            transform.rotation = caster.transform.rotation;
        }
    }
}
