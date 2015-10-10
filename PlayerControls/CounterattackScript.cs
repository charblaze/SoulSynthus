using UnityEngine;
using System.Collections;

public class CounterattackScript : MonoBehaviour {

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
    CircleCollider2D collider;
    Spells spells;
    bool noVel;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + player.transform.up * 0.3f;
        transform.rotation = player.transform.rotation;
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
        spells = player.GetComponent(typeof(Spells)) as Spells;
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        collider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != player && other.gameObject != null)
        {

            Character ge = other.gameObject.GetComponent(typeof(Character)) as Character;
            if (ge != null)
            {
                if (ge.isstaggered)
                {
                    ge.isstaggered = false;
                    GameObject ogg = Instantiate(Resources.Load("CAAnimation")) as GameObject;
                    CounterattackAnimation canm = ogg.GetComponent(typeof(CounterattackAnimation)) as CounterattackAnimation;
                    canm.user = player;
                    canm.target = other.gameObject;
                    Destroy(gameObject);
                }
            }
        }
    }
    bool set = false;
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + player.transform.up * 0.3f;
            transform.rotation = player.transform.rotation;
            
        }
    }
}
