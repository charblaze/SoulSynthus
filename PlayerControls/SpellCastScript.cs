using UnityEngine;
using System.Collections;



public class SpellCastScript : MonoBehaviour {
	private Sprite[] spellEffectL;
    private Sprite[] spellEffectR;
    private Sprite[] spellEffect1;
    private Sprite[] spellEffect2;
    private bool GCD = true;
    private Spells spells;
    private PlayerController playerscript;
    private PolygonCollider2D collider;
    private Vector2 empty = new Vector2(0, 0);
    private int colliderID;
    private bool noVel = true;
    public bool RonCD = false, LonCD = false, OneonCD = false, TwoonCD = false, QonCD = false, Q2onCD = false, healoncd = false;
    public float L, R, O, T, Q, Q2;
    public float Lr, Rr, Or, Tr, Qr, Q2r;
    public bool casting = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        spells = (Spells)gameObject.GetComponent(typeof(Spells));
        playerscript = (PlayerController)gameObject.GetComponent(typeof(PlayerController));
        collider = (PolygonCollider2D)gameObject.GetComponent(typeof(PolygonCollider2D));
	}

    public int spellID()
    {
        return colliderID;
    }
    IEnumerator FireQ()
    {
        if (playerscript.getSpellQ() == -1)
        {
            GCD = true;
            yield break;
        }
        casting = true;
        if (spells.getSpellCastTime(playerscript.getSpellQ()) != 0)
        {
            GameObject caster = Instantiate(Resources.Load("cast"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
            CastAnimScript anms = caster.GetComponent(typeof(CastAnimScript)) as CastAnimScript;
            anms.howtime = spells.getSpellCastTime(playerscript.getSpellQ());
            yield return new WaitForSeconds(spells.getSpellCastTime(playerscript.getSpellQ()));
        }
        casting = false;
        Q = Time.time + spells.getSpellCD(playerscript.getSpellQ());
        QonCD = true;
        
        
        playerscript.LoseStamina(spells.getSpellCost(playerscript.getSpellQ()));
        playerscript.CastSpell(playerscript.getSpellQ());
        yield return new WaitForSeconds(0.2f);
        GCD = true;
        yield return new WaitForSeconds(spells.getSpellCD(playerscript.getSpellQ()) - 0.2f);
        QonCD = false;
        
    }

    IEnumerator FireQ2()
    {
        if (playerscript.getSpellQ2() == -1)
        {
            GCD = true;
            yield break;
        }
        casting = true;
        if (spells.getSpellCastTime(playerscript.getSpellQ2()) != 0)
        {
            GameObject caster = Instantiate(Resources.Load("cast"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
            CastAnimScript anms = caster.GetComponent(typeof(CastAnimScript)) as CastAnimScript;
            anms.howtime = spells.getSpellCastTime(playerscript.getSpellQ2());
            yield return new WaitForSeconds(spells.getSpellCastTime(playerscript.getSpellQ2()));
        }
        casting = false;
        Q2 = Time.time + spells.getSpellCD(playerscript.getSpellQ2());
        Q2onCD = true;
        playerscript.LoseStamina(spells.getSpellCost(playerscript.getSpellQ2()));
        playerscript.CastSpell(playerscript.getSpellQ2());
        yield return new WaitForSeconds(0.2f);
        GCD = true;
        yield return new WaitForSeconds(spells.getSpellCD(playerscript.getSpellQ2()) - 0.2f);
        Q2onCD = false;
    }
	IEnumerator FireLeft () {
        if (playerscript.getSpellL() == -1)
        {
            GCD = true;
            yield break;
        }
        casting = true;
        if (spells.getSpellCastTime(playerscript.getSpellL()) != 0)
        {
            GameObject caster = Instantiate(Resources.Load("cast"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
            CastAnimScript anms = caster.GetComponent(typeof(CastAnimScript)) as CastAnimScript;
            anms.howtime = spells.getSpellCastTime(playerscript.getSpellL());
            yield return new WaitForSeconds(spells.getSpellCastTime(playerscript.getSpellL()));
        }
        casting = false;
        L = Time.time + spells.getSpellCD(playerscript.getSpellL());
        LonCD = true;
        playerscript.LoseStamina(spells.getSpellCost(playerscript.getSpellL()));
        playerscript.CastSpell(playerscript.getSpellL());
        yield return new WaitForSeconds(0.2f);
        GCD = true;
        yield return new WaitForSeconds(spells.getSpellCD(playerscript.getSpellL()) - 0.2f);
        LonCD = false;
	}

    IEnumerator FireRight()
    {
        if (playerscript.getSpellR() == -1)
        {
            GCD = true;
            yield break;
        }
        casting = true;
        if (spells.getSpellCastTime(playerscript.getSpellR()) != 0)
        {
            GameObject caster = Instantiate(Resources.Load("cast"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
            CastAnimScript anms = caster.GetComponent(typeof(CastAnimScript)) as CastAnimScript;
            anms.howtime = spells.getSpellCastTime(playerscript.getSpellR());
            yield return new WaitForSeconds(spells.getSpellCastTime(playerscript.getSpellR()));
        }
        casting = false;
        R = Time.time + spells.getSpellCD(playerscript.getSpellR());
        RonCD = true;
        playerscript.LoseStamina(spells.getSpellCost(playerscript.getSpellR()));
        playerscript.CastSpell(playerscript.getSpellR());
        yield return new WaitForSeconds(0.2f);
        GCD = true;
        yield return new WaitForSeconds(spells.getSpellCD(playerscript.getSpellR()) - 0.2f);
        GCD = true;
        RonCD = false;
    }

    IEnumerator Fire1()
    {
        if (playerscript.getSpell1() == -1)
        {
            GCD = true;
            yield break;
        }
        casting = true;
        if (spells.getSpellCastTime(playerscript.getSpell1()) != 0)
        {
            GameObject caster = Instantiate(Resources.Load("cast"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
            CastAnimScript anms = caster.GetComponent(typeof(CastAnimScript)) as CastAnimScript;
            anms.howtime = spells.getSpellCastTime(playerscript.getSpell1());
            yield return new WaitForSeconds(spells.getSpellCastTime(playerscript.getSpell1()));
        }
        casting = false;
        O = Time.time + spells.getSpellCD(playerscript.getSpell1());
        OneonCD = true;
        playerscript.LoseStamina(spells.getSpellCost(playerscript.getSpell1()));
        playerscript.CastSpell(playerscript.getSpell1());
        yield return new WaitForSeconds(0.2f);
        GCD = true;
        yield return new WaitForSeconds(spells.getSpellCD(playerscript.getSpell1()) - 0.2f);
        GCD = true;
        OneonCD = false;
    }

    IEnumerator Fire2()
    {
        if (playerscript.getSpell2() == -1)
        {
            GCD = true;
            yield break;
        }
        casting = true;
        if (spells.getSpellCastTime(playerscript.getSpell2()) != 0)
        {
            GameObject caster = Instantiate(Resources.Load("cast"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
            CastAnimScript anms = caster.GetComponent(typeof(CastAnimScript)) as CastAnimScript;
            anms.howtime = spells.getSpellCastTime(playerscript.getSpell2());
            yield return new WaitForSeconds(spells.getSpellCastTime(playerscript.getSpell2()));
        }
        casting = false;
        T = Time.time + spells.getSpellCD(playerscript.getSpell2());
        TwoonCD = true;
        playerscript.LoseStamina(spells.getSpellCost(playerscript.getSpell2()));
        playerscript.CastSpell(playerscript.getSpell2());
        yield return new WaitForSeconds(0.2f);
        GCD = true;
        yield return new WaitForSeconds(spells.getSpellCD(playerscript.getSpell2()) - 0.2f);
        GCD = true;
        TwoonCD = false;
    }
    public AudioClip healsound;
    IEnumerator heal()
    {
        if (playerscript.numheals <= 0)
        {
            healoncd = true;
            casting = true;
            playerscript.SLOWED(0.2f, 1f, true);
            Instantiate(Resources.Load("castheal"), transform.position + transform.up * 0.4f, transform.rotation);
            yield return new WaitForSeconds(1f);
            casting = false;
            healoncd = false;
            AudioSource.PlayClipAtPoint(outaheals, transform.position);
            yield return new WaitForSeconds(0.2f);
            GCD = true;
            yield break;
        }
        healoncd = true;
        casting = true;
        playerscript.SLOWED(0.2f, 1f, true);
            GameObject caster = Instantiate(Resources.Load("castheal"), transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
            yield return new WaitForSeconds(1f);
        casting = false;
        healoncd = false;
        AudioSource.PlayClipAtPoint(healsound, transform.position);
        playerscript.GainHealth(playerscript.maxHealth*0.4f);
        playerscript.numheals -= 1;
        yield return new WaitForSeconds(0.2f);
        GCD = true;
    }

	// Update is called once per frame
	void Update () {
        if (casting)
        {
            playerscript.casteroni = true;
        }
        else
        {
            playerscript.casteroni = false;
        }
        Lr = L - Time.time;
        Or = O - Time.time;
        Tr = T - Time.time;
        Rr = R - Time.time;
        Qr = Q - Time.time;
        Q2r = Q2 - Time.time;
        if (Lr < 0)
        {
            Lr = 0;
        }
        if (Or < 0)
        {
            Or = 0;
        }
        if (Tr < 0)
        {
            Tr = 0;
        }
        if (Rr < 0)
        {
            Rr = 0;
        }
        if (Qr < 0)
        {
            Qr = 0;
        }
        if (Q2r < 0)
        {
            Q2r = 0;
        }
        if (GCD && !playerscript.isdying && 
		    playerscript.GetStamina() > 0 && !playerscript.isStunned && !casting && !playerscript.menuIsUp && !playerscript.cantcast)
        {
            if (Input.GetButtonDown("Fire1") && !LonCD && (playerscript.getSpellL() != -1))
            {
                StartCoroutine("FireLeft");
                GCD = false;
            }
            if (Input.GetButtonDown("Fire2") && !RonCD && (playerscript.getSpellR() != -1))
            {
                StartCoroutine("FireRight");
                GCD = false;
            }
            if (Input.GetButtonDown("FireBut1") && !OneonCD && (playerscript.getSpell1() != -1))
            {
                StartCoroutine("Fire1");
                GCD = false;
            }
            if (Input.GetButtonDown("FireBut2") && !TwoonCD && (playerscript.getSpell2() != -1))
            {
                StartCoroutine("Fire2");
                GCD = false;
            }
            if (Input.GetButtonDown("Fire3") && !QonCD && (playerscript.getSpellQ() != -1))
            {
                StartCoroutine("FireQ");
                GCD = false;
            }
            if (Input.GetButtonDown("Fire4") && !Q2onCD && (playerscript.getSpellQ2() != -1))
            {
                StartCoroutine("FireQ2");
                GCD = false;
            }
            if(Input.GetButtonDown("Heal") && !healoncd){
                StartCoroutine(heal());
                GCD = false;
            }
            if (Input.GetButtonDown("Counterattack"))
            {
                Instantiate(Resources.Load("Counterattack"));
            }
        }
	}
    public AudioClip outaheals;
}
