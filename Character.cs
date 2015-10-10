using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public float moveSpeed;
    public float moveSpeedModifier;
    public Vector3 moveDirection;
    public float speed; public int stamina;
    public int maxHealth;
    public int maxHealthModifier;
    public int currentHealth; public float recover;
    public float moveSpeedOriginal;
    public float magResist = 0;
    public float magResistAdjust;
    public bool isStunned;
    public bool isMoving;
    public float stunDuration;
    public bool HasRegen = true;
    public float staminaRecoverySpeed = 0.025f;
    public float stamRecovSpeedModifier;
    public bool hasHPRegen = true;
    public float HPspeed = 2f;
    public float HPSpeedModifier; public float moveSpeedNaked = 1.5f;
    public float stamRecovSpeedNaked = 0.025f;
    public float hpRecovSpeedNaked = 2f;
    public int maxHealthNaked = 100;
    public float magResistNaked = 0f;
    public int worth = 0;
    public GameObject player;
    public bool isstaggered = false;
    public AudioSource hurt;
    public bool candrown = true;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	public void ParrySound(){
		AudioSource.PlayClipAtPoint ((AudioClip)Resources.Load("Sounds/Boom"), transform.position);
	}
	// Update is called once per frame
	void Update () {
	
	}
    Coroutine stn;
    public void STUNNED(float c)
    {
        if (!dodging)
        {
            isStunned = true;
            if (stn != null)
            {
                StopCoroutine(stn);
            }
            stn = StartCoroutine(stunned(c));
        }
    }

    public IEnumerator stunned(float c)
    {
        yield return new WaitForSeconds(c);
        isStunned = false;
    }

    public void DOTTED(int amount, float duration)
    {
        if (!dodging)
        {
            StartCoroutine(getDoTted(amount, duration));
        }
    }

    public IEnumerator getDoTted(int amount, float duration)
    {
        bool isDotting = true;
        int dps;
        if (duration == 0)
        {
            dps = amount;
        }
        else
        {
            dps = Mathf.CeilToInt(amount / duration);
        }
        while (isDotting)
        {
            if (duration <= 0)
            {
                isDotting = false;
            }
            currentHealth = currentHealth - Mathf.CeilToInt(dps * (1 - magResist));
            StartCoroutine(TakingDamage(Mathf.CeilToInt(dps * (1 - magResist))));
            yield return new WaitForSeconds(1);
            duration = duration - 1f;
        }
    }

    public void HOTTED(int amount, float duration)
    {

        StartCoroutine(getHoTted(amount, duration));
    }

    public IEnumerator getHoTted(int amount, float duration)
    {
        bool isDotting = true;
        int dps;
        if (duration == 0)
        {
            dps = amount;
        }
        else
        {
            dps = Mathf.CeilToInt(amount / duration);
        }
        while (isDotting)
        {
            if (duration <= 0)
            {
                isDotting = false;
            }
            currentHealth = currentHealth + dps;
            StartCoroutine(TakingHealing(dps));
            yield return new WaitForSeconds(1);
            duration = duration - 1f;
        }
    }
    public float SPELLPOWERBONUS = 0;

    public IEnumerator FloatInDirection(string Direction, float modifier = 1f)
    {
        if (Direction == "Up")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += transform.up * Time.deltaTime * (moveSpeed - c) * modifier;
                yield return new WaitForSeconds(0.01f);
            }
        }
        if (Direction == "Back")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += -1 * transform.up * Time.deltaTime * (moveSpeed - c) * modifier;
                yield return new WaitForSeconds(0.01f);
            }
        }
        if (Direction == "Left")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += -1 * transform.right * Time.deltaTime * (moveSpeed - c) * modifier;
                yield return new WaitForSeconds(0.01f);
            }
        }
        if (Direction == "Right")
        {
            for (float c = 0; c <= moveSpeed; c = c + 0.05f)
            {
                transform.position += transform.right * Time.deltaTime * (moveSpeed - c) * modifier;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    public bool isSlowed = false;
    Coroutine slwd;
    public void SLOWED(float amount, float duration, bool castinglel = false)
    {
        if (!dodging)
        {
            if (slwd != null)
            {
                StopCoroutine(slwd);
            }
            slwd = StartCoroutine(getMoveSpeedReduced(amount, duration, castinglel));
        }
    }

    public IEnumerator getMoveSpeedReduced(float amount, float duration, bool castinglel)
    {
        if (amount < 1 && !castinglel)
        {
            isSlowed = true;
        }
        moveSpeed = moveSpeed * amount;
        yield return new WaitForSeconds(duration);
        moveSpeed = moveSpeedOriginal;
        isSlowed = false;
    }



    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void setMaxHealth(int x)
    {
        maxHealth = x;
    }

    public void setCurrentHealth(int x)
    {
        currentHealth = x;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public IEnumerator StartHealthRegen()
    {
        while (hasHPRegen || !hasHPRegen)
        {
            if (currentHealth < maxHealth && hasHPRegen)
            {
                currentHealth += 1;
            }
            yield return new WaitForSeconds(HPspeed);
        }
    }

    public void GainHealth(float x)
    {
        currentHealth += Mathf.CeilToInt(x);
        StartCoroutine(TakingHealing(Mathf.FloorToInt(x)));
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool immunityFrames = false;

    public IEnumerator Immunity(float duration)
    {
        immunityFrames = true;
        yield return new WaitForSeconds(duration);
        immunityFrames = false;
    }
    public bool dodging = false;

    public IEnumerator Dodge(float duration)
    {
        dodging = true;
        yield return new WaitForSeconds(duration);
        dodging = false;
    }

    public virtual void PlayHurtSound()
    {
    }

    public float pushback = 1f;
    public void LoseHealth(int x)
    {
        if (!immunityFrames)
        {
            immunityFrames = true;
            PlayHurtSound();
            currentHealth = currentHealth - x + Mathf.CeilToInt(x * magResist);
            StartCoroutine(TakingDamage(x - Mathf.CeilToInt(x * magResist)));
            StartCoroutine(FloatInDirection("Back", pushback));
            StartCoroutine(Immunity(.1f));
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public virtual void PlayDeathSound()
    {

    }

    public virtual void Die()
    {
        PlayDeathSound();
        PlayerController pc = player.GetComponent(typeof(PlayerController)) as PlayerController;
        pc.isTargeting = false;
        pc.xp += worth;
        GameObject ja = Instantiate(Resources.Load("JoulesAnimation"), transform.position, transform.rotation) as GameObject;
        JouleAnimation ji = ja.GetComponent(typeof(JouleAnimation)) as JouleAnimation;
        ji.xp = worth;
        Destroy(gameObject);
    }

    public int GetStamina()
    {
        return stamina;
    }
    public bool canrecover = true;
    public bool casteroni = false;
    public void SetStamina(int x)
    {
        stamina = x;
    }
    public int max_stamina = 100;
    public IEnumerator Recover_Stamina_Passive()
    {
        while (HasRegen || !HasRegen)
        {
            if (stamina < max_stamina && HasRegen && canrecover && !casteroni)
            {
                stamina += 1;
            }
            yield return new WaitForSeconds(staminaRecoverySpeed);
        }
    }

    public IEnumerator StaminaReachesBelowZero(float recover_time)
    {
        //HasRegen = false;
        yield return new WaitForSeconds(recover_time);
        /*if (stamina > 0)
        {
            HasRegen = true;
            yield break;
        }
        stamina = 0;
        HasRegen = true;*/
    }
    public bool isTakingDamage = false;
    public int damageTaken = 0;
    public bool isTakingHealing = false;
    public int healingTaken = 0;

    public IEnumerator TakingDamage(int amount)
    {
        isTakingDamage = true;
        damageTaken = amount;
        yield return new WaitForSeconds(0.5f);
        damageTaken = 0;
        isTakingDamage = false;
    }

    public IEnumerator TakingHealing(int amount)
    {
        isTakingHealing = true;
        healingTaken = amount;
        yield return new WaitForSeconds(0.5f);
        healingTaken = 0;
        isTakingHealing = false;
    }


    public void CastSpell(int id)
    {
        GameObject spell = Instantiate(Resources.Load("Spells/" + id)) as GameObject;
        Spell ps = spell.GetComponent(typeof(Spell)) as Spell;
        ps.caster = gameObject;
    }

}
