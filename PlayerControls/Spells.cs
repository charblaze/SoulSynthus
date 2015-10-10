using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spells : MonoBehaviour {
    public AudioClip getitema, getspella;
    public Sprite methane1icon, methanol1icon;
    public Sprite[] methane1effect, methanol1effect;
    public Sprite hatdefault, hatredoverworld;
    public Texture2D methane1iconUI, methanol1iconUI, bluehatui, redhatui, hpotui, stampotui, sapphui, tunicui, nullui, hatredbig;
    private const int MAX_SPELL_SIZE = 500;
    private Spell[] AllSpells = new Spell[MAX_SPELL_SIZE];
    private Hat[] AllHats = new Hat[MAX_SPELL_SIZE];
    private Tunic[] AllTunics = new Tunic[MAX_SPELL_SIZE];
    private Amulet[] AllAmulets = new Amulet[MAX_SPELL_SIZE];
    private Item[] AllItems = new Item[MAX_SPELL_SIZE];
	public int[] UnlockedSpells = new int[MAX_SPELL_SIZE];
	public int[] UnlockedHats = new int[MAX_SPELL_SIZE];
	public int[] UnlockedTunics = new int[MAX_SPELL_SIZE];
	public int[] UnlockedAmulets = new int[MAX_SPELL_SIZE];
	public int[] PickedUpItems = new int[MAX_SPELL_SIZE];

    public static Vector3 checkpointDatabase(string cp)
    {
		if (cp == "Checkpoint1") {
						return new Vector3 (1.79f, -5.59f, 0);
				} else if (cp == "Checkpoint2") {
						return new Vector3 (27.75f, 44.53f, 0);
				} else {
						return new Vector3 (0, 0, 0);
				}
    }
    public int EquippedHat;
    public int EquippedTunic;
    public int EquippedAmulet;

    public int ItemCount = 0;
    public int bagSpace = 200;
    public bool bagIsFull = false;
    public int SpellCount = 0;
    public int SpellSpace = 200;
    public bool SpellBookIsFull = false;
    public int HatCount = 0;
    public int HatSpace = 50;
    public bool HatIsFull = false;
    public int AmuletCount = 0;
    public int AmuletSpace = 100;
    public bool AmuletIsFull = false;
    public int TunicCount = 0;
    public int TunicSpace = 100;
    public bool TunicIsFull = false;

    public class Hat
    {
        public int hatID;
        public string hatName;
        public string hatDescrip;
        public Texture2D HatIcon;
        public Sprite HatInGame;
        public Texture2D HatBigSprite;
    }

    private void HatSet(Hat hat, int id, string name, string descrip, Texture2D icon, Sprite ingame, Texture2D bs)
    {
        hat.hatID = id;
        hat.hatName = name;
        hat.hatDescrip = descrip;
        hat.HatIcon = icon;
        hat.HatInGame = ingame;
        hat.HatBigSprite = bs;
    }

    public void addHat(int id)
    {
        if (!HatIsFull)
        {
            UnlockedHats[HatCount] = id;
            AudioSource.PlayClipAtPoint(getitema, transform.position);
            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
            ohyes.message = "You got a new hat: " + getHatName(id);
            HatCount++;
        }
    }

    public void RemoveHat(int pos)
    {
        if (pos == HatCount - 1)
        {
            HatCount--;
        }
        else
        {
            for (int c = pos + 1; c <= HatCount - 1; c++)
            {
                UnlockedHats[c - 1] = UnlockedHats[c];
            }
            HatCount--;
        }
    }

    public int getHatIDAt(int pos)
    {
        return UnlockedHats[pos];
    }

    public void GetHat(int id)
    {
        if (!HatIsFull)
        {
            UnlockedHats[HatCount] = id;
            HatCount++;
        }
    }

    public string getHatName(int id)
    {
        return AllHats[id].hatName;
    }

    public string getHatDescrip(int id)
    {
        return AllHats[id].hatDescrip;
    }

    public Texture2D getHatIcon(int id)
    {
        return AllHats[id].HatIcon;
    }

    public Sprite getHatSprite(int id)
    {
        return AllHats[id].HatInGame;
    }

    public Texture2D getHatBigSprite(int id)
    {
        return AllHats[id].HatBigSprite;
    }

	public class Tunic
    {
        public int tunicID;
        public Texture2D tunicIcon;
        public string tunicDescription;
        public string tunicName;
        public float tunicResist;
        public float moveSpeed;
        public float maxhealth;
    }

    private void TunicSet(Tunic tunic, int id, Texture2D icon, string name, string descrip, float resist, float moveSpeed, float Maxhealth)
    {
        tunic.tunicID = id;
        tunic.tunicIcon = icon;
        tunic.tunicName = name;
        tunic.tunicDescription = descrip;
        tunic.tunicResist = resist;
        tunic.moveSpeed = moveSpeed;
        tunic.maxhealth = Maxhealth;
    }

    public void addTunic(int id)
    {
        if (!TunicIsFull)
        {
            UnlockedTunics[TunicCount] = id; AudioSource.PlayClipAtPoint(getitema, transform.position);
            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
            ohyes.message = "You got a new tunic: " + getTunicName(id);
            TunicCount++;
        }
    }

    public void RemoveTunic(int pos)
    {
        if (pos == TunicCount - 1)
        {
            TunicCount--;
        }
        else
        {
            for (int c = pos + 1; c <= TunicCount - 1; c++)
            {
                UnlockedTunics[c - 1] = UnlockedTunics[c];
            }
            TunicCount--;
        }
    }

    public int getTunicIDAt(int pos)
    {
        return UnlockedTunics[pos];
    }

    public string getTunicName(int id)
    {
        return AllTunics[id].tunicName;
    }

    public Texture2D getTunicIcon(int id)
    {
        return AllTunics[id].tunicIcon;
    }

    public string getTunicDescrip(int id)
    {
        return AllTunics[id].tunicDescription;
    }

    public float getTunicResistIncrease(int id)
    {
        return AllTunics[id].tunicResist;
    }

    public float getTunicMoveSpeed(int id)
    {
        return AllTunics[id].moveSpeed;
    }

    public float getTunicHPIncrease(int id)
    {
        return AllTunics[id].maxhealth;
    }

	public class Amulet
    {
        public int amuletID;
        public Texture2D amuletIcon;
        public string amuletName;
        public string amuletDescrip;
        public float healthRegen;
        public float stamRegen;
    }

    private void AmuletSet(Amulet amulet, int id, Texture2D icon, string name, string descrip, float stam, float hpregen)
    {
        amulet.amuletID = id;
        amulet.amuletIcon = icon;
        amulet.amuletName = name;
        amulet.amuletDescrip = descrip;
        amulet.healthRegen = hpregen;
        amulet.stamRegen = stam;

    }

    public void addAmulet(int id)
    {
        if (!AmuletIsFull)
        {
            UnlockedAmulets[AmuletCount] = id; AudioSource.PlayClipAtPoint(getitema, transform.position);
            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
            ohyes.message = "You got a new amulet: " + getAmuletName(id);
            AmuletCount++;
        }
    }

    public void removeAmulet(int pos)
    {
        if (pos == AmuletCount - 1)
        {
            AmuletCount--;
        }
        else
        {
            for (int c = pos + 1; c <= AmuletCount - 1; c++)
            {
                UnlockedAmulets[c - 1] = UnlockedAmulets[c];
            }
            AmuletCount--;
        }
    }

    public int getAmuletIDAt(int pos)
    {
        return UnlockedAmulets[pos];
    }

    public string getAmuletName(int id)
    {
        return AllAmulets[id].amuletName;
    }

    public Texture2D getAmuletIcon(int id)
    {
        return AllAmulets[id].amuletIcon;
    }

    public string getAmuletDescrip(int id)
    {
        return AllAmulets[id].amuletDescrip;
    }

    public float getAmuletHealthRegen(int id)
    {
        return AllAmulets[id].healthRegen;
    }

    public float getAmuletStamRegen(int id)
    {
        return AllAmulets[id].stamRegen;
    }

    public class Item
    {
        public int itemID;
        public string itemName;
        public string itemGroup;
        public string itemDescription;
        public bool isReactable;
        public float hpRestore;
        public float stmRestore;
        public float xpgain;
        public Texture2D itemSprite;
        public bool dotRestore;
        public bool isKey;
    };

    public void SortItems()
    {
		if (ItemCount < 1) {
						return;
				}
        List<int> curr = new List<int>();
       List<int> alreadysorted = new List<int>();
        // first are consumables
        for (int c = 0; c < ItemCount; ++c)
        {
            if (AllItems[PickedUpItems[c]].itemGroup == "Consumable")
            {
                curr.Add(PickedUpItems[c]);
            }
        }
        curr.Sort();
        alreadysorted.AddRange(curr);
        curr.Clear();

        // then resonants
        for (int c = 0; c < ItemCount; ++c)
        {
            if (AllItems[PickedUpItems[c]].itemGroup == "Resonant")
            {
                curr.Add(PickedUpItems[c]);
            }
        }
        curr.Sort();
        alreadysorted.AddRange(curr);
        curr.Clear();

        for (int c = 0; c < ItemCount; ++c)
        {
            if (AllItems[PickedUpItems[c]].itemGroup == "Soul")
            {
                curr.Add(PickedUpItems[c]);
            }
        }
        curr.Sort();
        alreadysorted.AddRange(curr);
        curr.Clear();

        for (int c = 0; c < ItemCount; ++c)
        {
            if (AllItems[PickedUpItems[c]].itemGroup == "Key")
            {
                curr.Add(PickedUpItems[c]);
            }
        }
        curr.Sort();
        alreadysorted.AddRange(curr);
        curr.Clear();

        for (int c = 0; c < ItemCount; ++c)
        {
			if (AllItems[PickedUpItems[c]].itemGroup != "Resonant" && AllItems[PickedUpItems[c]].itemGroup != "Soul" && AllItems[PickedUpItems[c]].itemGroup != "Key" && AllItems[PickedUpItems[c]].itemGroup != "Consumable")
            {
                curr.Add(PickedUpItems[c]);
            }
        }
        curr.Sort();
        alreadysorted.AddRange(curr);
        curr.Clear();
		for (int c = 0; c < ItemCount; ++c) {
						PickedUpItems [c] = alreadysorted [c];
				}

    }

    public void AddItem(int id)
    {
        if (!bagIsFull)
        {
            PickedUpItems[ItemCount] = id; AudioSource.PlayClipAtPoint(getitema, transform.position);
                GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                ohyes.message = "You got a new item: " + getItemName(id);
            ItemCount++;
        }
    }

    public void RemoveItem(int pos)
    {
        if (pos == ItemCount - 1)
        {
            ItemCount--;
        }
        else
        {
            for (int c = pos + 1; c <= ItemCount - 1; c++)
            {
                PickedUpItems[c - 1] = PickedUpItems[c];
            }
            ItemCount--;
        }
    }

    public int getItemIDAt(int pos)
    {
        return PickedUpItems[pos];
    }

    public void LearnSpell(int id)
    {
        if (!SpellBookIsFull)
        {
            UnlockedSpells[SpellCount] = id; AudioSource.PlayClipAtPoint(getitema, transform.position);
            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
            ohyes.message = "You learned a new spell: " + getSpellName(id);
            SpellCount++;
        }
    }

    public void UnlearnSpell(int pos)
    {
        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
        ohyes.message = "Succesfully disposed of spell.";
        if (pos == SpellCount - 1)
        {
            SpellCount--;
        }
        else
        {
            for (int c = pos + 1; c <= SpellCount - 1; c++)
            {
                UnlockedSpells[c - 1] = UnlockedSpells[c];
            }
            SpellCount--;
        }
    }

    public int getSpellAt(int pos)
    {
        return UnlockedSpells[pos];
    }

    public string getItemName(int id)
    {
        return AllItems[id].itemName;
    }

    public string getItemGroup(int id)
    {
        return AllItems[id].itemGroup;
    }

    public string getItemDescrip(int id)
    {
        return AllItems[id].itemDescription;
    }

    public bool getIsReactable(int id)
    {
        return AllItems[id].isReactable;
    }

    public float getItemHPRestore(int id)
    {
        return AllItems[id].hpRestore;
    }

    public bool LookForKey(int id)
    {
        bool wat = false;
        for (int c = 0; c < ItemCount; ++c)
        {
            if (PickedUpItems[c] == id)
            {
                wat = true; break;
            }
        }
        return wat;
    }
    public float getItemStamRestore(int id)
    {
        return AllItems[id].stmRestore;
    }

    public float getItemXPGain(int id)
    {
        return AllItems[id].xpgain;
    }

    public Texture2D getItemSprite(int id)
    {
        return AllItems[id].itemSprite;
    }

    public bool getItemDoTRestore(int id)
    {
        return AllItems[id].dotRestore;
    }

    public bool getItemIsKey(int id)
    {
        return AllItems[id].isKey;
    }

    private void ItemSet(Item item, int id, Texture2D sprite, string name, string group, string descrip, bool react, float hp, float st, bool dot, bool key, float xp)
    {
        item.itemID = id;
        item.itemName = name;
        item.itemGroup = group;
        item.itemDescription = descrip;
        item.isReactable = react;
        item.hpRestore = hp;
        item.stmRestore = st;
        item.itemSprite = sprite;
        item.dotRestore = dot;
        item.isKey = key;
        item.xpgain = xp;
    }


    public class Spell
    {
        public int spellID;
        public string spellName;
        public bool isOrganic;
        public Sprite spellIcon;
        public Texture2D spellUIIcon;
        public Sprite[] spellEffect;
        public string spellDescription;
        public int spellCost;
        public Vector2 hitBoxSize;
        public Vector2 hitBoxCenter;
        public int baseDmg;
        public float moveSpeedReduc;
        public float moveSpeedReducDuration;
        public float stunDuration;
        public int DoTDamage;
        public float DoTSpread;
        public int baseHeal;
        public int HoTHeal;
        public float HoTSpread;
        public float velocity;
        public float cooldown;
        public float cast;
    };

    private void SpellSet(Spell spell, int ID, string name, bool organic, Sprite icon, string description, int cost, int basedmg, float movespeedreduc, float reducduration, float stun, int dot, float dotspread, int heal, int hot, float hotspread, float v, Texture2D uicon, float cd, float castTime)
    {
        spell.spellID = ID;
        spell.spellName = name;
        spell.isOrganic = organic;
        spell.spellIcon = icon;
        spell.spellDescription = description;
        spell.spellCost = cost;
        spell.baseDmg = basedmg;
        spell.moveSpeedReduc = movespeedreduc;
        spell.moveSpeedReducDuration = reducduration;
        spell.stunDuration = stun;
        spell.DoTDamage = dot;
        spell.DoTSpread = dotspread;
        spell.baseHeal = heal;
        spell.HoTHeal = hot;
        spell.HoTSpread = hotspread;
        spell.velocity = v;
        spell.spellUIIcon = uicon;
        spell.cooldown = cd;
        spell.cast = castTime;
    }

    public float getSpellCastTime(int id)
    {
        return AllSpells[id].cast;
    }

    public float getSpellCD(int id)
    {
        return AllSpells[id].cooldown;
    }

    public Texture2D getSpellUI_Icon(int id)
    {
        return AllSpells[id].spellUIIcon;
    }

    public float getVelocity(int id)
    {
        return AllSpells[id].velocity;
    }

    public int getBaseDmg(int id)
    {
        return AllSpells[id].baseDmg;
    }

    public float getMoveSpeedReducDuration(int id)
    {
        return AllSpells[id].moveSpeedReducDuration;
    }

    public float getMoveSpeedReduc(int id)
    {
        return AllSpells[id].moveSpeedReduc;
    }

    public float getStunDuration(int id)
    {
        return AllSpells[id].stunDuration;
    }

    public int getDoTDamage(int id)
    {
        return AllSpells[id].DoTDamage;
    }

    public float getDoTSpread(int id)
    {
        return AllSpells[id].DoTSpread;
    }

    public int getBaseHeal(int id)
    {
        return AllSpells[id].baseHeal;
    }

    public int getHoT(int id){
        return AllSpells[id].HoTHeal;
    }

    public float getHoTSpread(int id){
        return AllSpells[id].HoTSpread;
    }

    public int getSpellCost(int id)
    {
        return AllSpells[id].spellCost;
    }

    public string getSpellName(int id)
    {
        return AllSpells[id].spellName;
    }

    public bool getIsOrganic(int id)
    {
        return AllSpells[id].isOrganic;
    }

    public Sprite getSpellIcon(int id)
    {
        if (id == -1)
        {
            return new Sprite { };
        }
        return AllSpells[id].spellIcon;
    }

    public Sprite[] getSpellEffect(int id)
    {
        return AllSpells[id].spellEffect;
    }

    public string getSpellDescription(int id)
    {
        return AllSpells[id].spellDescription;
    }

    public Vector2 getSpellHitBox(int id)
    {
        return AllSpells[id].hitBoxSize;
    }

    public Vector2 getSpellHitBoxCenter(int id)
    {
        return AllSpells[id].hitBoxCenter;
    }

    public Texture2D smallchimeui, chemicalui, key1ui, key2ui, boss1soului, key3ui, synthusui, key4ui, pendantui;

    public Texture2D bluehatbig, brownhatbig, blueberetbig;
    public Texture2D brownhaticon, bluebereticon;
    public Sprite brownhatig, blueberetig;

    public Texture2D ethane1iconUI, ccl4ui, cf4ui, etholui, propaneui, isopropanolui, propanolui, cfmethaneui, dcmui, octofui, pentaneui, hexaneui;
    public Texture2D acetyleneui, benzeneui, orthotolueneui, phenolui, phenylacetyleneui, ethyleneglycerolui, tetrafui, propylglycolui, diethglycolui, phenylacetateui;
    public Texture2D tolueneui, nitrotolueneui, trinitrotolueneui, nitrobenzoicacidui, benzoicacidui, benzotrichlorideui, benzoylchlorideui, rdxui, co2ui, octanitrocubaneui;
    public Texture2D so2ui, petnui, chloroformui, odfui, fluoroformui, octogenui;
    public Sprite ethane1icon, ccl4icon, cf4icon, etholicon, propaneicon, isopropanolicon, propanolicon, cfmethaneicon, dcmicon, octoficon, pentaneicon, hexaneicon;
    public Sprite acetyleneicon, benzeneicon, orthotolueneicon, phenolicon, phenylacetyleneicon, ethyleneglycerolicon, tetraficon, propylglycolicon, diethglycolicon, phenylacetateicon;
    public Sprite tolueneicon, nitrotolueneicon, trinitrotolueneicon, nitrobenzoicacidicon, benzoicacidicon, benzotrichlorideicon, benzoylchlorideicon, rdxicon, co2icon, octanitrocubaneicon;
    public Sprite so2icon, petnicon, chloroformicon, odficon, fluoroformicon, octogenicon;

    public Sprite bronzeswordicon, bronzesword2icon, bronzegsicon, bronzedagicon, lithiumicon;
    public Texture2D bronzeswordUI, bronzesword2UI, bronzegsUI, bronzedagUI, lithiumUI;

	public Sprite hemeicon, influenzaicon, aspirinicon, ibuicon, chlorinegasicon, myoglobicon;
	public Texture2D hemeui, influenzaui, aspirinui, ibui, chlorinegasui, myoglobui;

   

	// Use this for initialization
	void Start () {
        /* ---- SPELLS ----- */
        PlayerController playerscript = GetComponent(typeof(PlayerController)) as PlayerController;
        // ORGANIC
        // Methyl Blast
	    Spell Methane1 = new Spell();
        SpellSet(Methane1, 0, "Methyl Blast", true, methane1icon, "Simple Methane explosion with 25 base power with a 1 second cast time.", 13,10,0,0,0,0,0,0,0,0,0,methane1iconUI,.8f, 1f);
        AllSpells[0] = Methane1;
        // Methanol Burst
        Spell Methanol1 = new Spell();
        SpellSet(Methanol1, 1, "Methanol Blast", true, methanol1icon, "Simple Methanol explosion with 25 base power.", 13,15,0,0,0,0,0,0,0,0,0,methanol1iconUI,2f, 1f);
        AllSpells[1] = Methanol1;
        // Ethane Burst
        Spell Ethane1 = new Spell();
        SpellSet(Ethane1, 2, "Ethyl Burst", true, ethane1icon, "Counterattack spell.\n\nConcentrated ethane explosion. Has a quarter second cast time with 25 base power.", 5, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, ethane1iconUI, 5f, .25f);
        AllSpells[2] = Ethane1;

        Spell CCl4 = new Spell();
        SpellSet(CCl4, 3, "Carbon Tetrachloride", true, ccl4icon, "Chills and deals 10 base damage to enemies within a wide radius. Carbon Tetrachloride is known for its refirdgerant properties.", 0, 5, 0.5f, 2f, 0, 0, 0, 0, 0, 0, 0, ccl4ui, 10, 0.3f);
        AllSpells[3] = CCl4;

        Spell CF4 = new Spell();
        SpellSet(CF4, 4, "Carbon Tetraflouride", true, cf4icon, "Throws an icicle spear that deals 10 base damage and snares the enemy it hits.", 10, 10, 0.2f, 2f, 0, 0, 0, 0, 0, 0, 5f, cf4ui, 5, 0f);
        AllSpells[4] = CF4;

        Spell Ethol = new Spell();
        SpellSet(Ethol, 5, "Ethanol Burst", true, etholicon, "More concentrated explosion that deals 35 base damage.", 20, 35, 0, 0, 0, 0, 0, 0, 0, 0, 0, etholui, 10, 1f);
        AllSpells[5] = Ethol;

        Spell Propane = new Spell();
        SpellSet(Propane, 6, "Propane Torch", true, propaneicon, "Extremely concentrated explosion that deals 50 base damage.", 25, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, propaneui, 10, 1f);
        AllSpells[6] = Propane;

        Spell Isopropanol = new Spell();
        SpellSet(Isopropanol, 7, "Isopropanol Wall", true, isopropanolicon, "Wall of Fire", 50, 0, 0, 0, 0, 40, 5, 0, 0, 0, 0, isopropanolui, 20, 1.5f);
        AllSpells[7] = Isopropanol;

        Spell propanol = new Spell();
        SpellSet(propanol, 8, "1-Propanol Circle", true, propanolicon, "circle of flame", 50, 0, 0, 0, 0, 40, 5, 0, 0, 0, 0, propanolui, 15, 1.5f);
        AllSpells[8] = propanol;

        Spell cfmethane = new Spell();
        SpellSet(cfmethane, 9, "Chlorofluoromethane Nova", true, cfmethaneicon, "frost nova", 40, 30, 0, 2, 0, 0, 0, 0, 0, 0, 0, cfmethaneui, 10, 0f);
        AllSpells[9] = cfmethane;

        Spell dcm = new Spell();
        SpellSet(dcm, 10, "Dichloromethane Wall", true, dcmicon, "anivia wall", 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, dcmui, 10, 1f);
        AllSpells[10] = dcm;

        Spell octof = new Spell();
        SpellSet(octof, 11, "Octofluoromethane Lance", true, octoficon, "ice lance", 60, 20, 0.4f, 2f, 0, 0, 0, 0, 0, 0, 0, octofui, 1f, 0f);
        AllSpells[11] = octof;

        Spell pentane = new Spell();
        SpellSet(pentane, 12, "Pentane Explosion", true, pentaneicon, "boom", 120, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, pentaneui, 30, 1.5f);
        AllSpells[12] = pentane;

        Spell hexane = new Spell();
        SpellSet(hexane, 13, "Hexane Boom", true, hexaneicon, "smaller boom", 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, hexaneui, 5f, 1f);
        AllSpells[13] = hexane;

        Spell acetylene = new Spell();
        SpellSet(acetylene, 14, "Acetylene Laser", true, acetyleneicon, "laser", 40, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, acetyleneui, 10f, 3f);
        AllSpells[14] = acetylene;

        Spell benzene = new Spell();
        SpellSet(benzene, 15, "Benzene Blast", true, benzeneicon, "boomer", 25, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, benzeneui, 2f, 1f);
        AllSpells[15] = benzene;

        Spell Otoluene = new Spell();
        SpellSet(Otoluene, 16, "Orthotoluidine Sear", true, orthotolueneicon, "small boomer", 40, 75, 0, 0, 0, 0, 0, 0, 0, 0, 0, orthotolueneui, 20f, 1f);
        AllSpells[16] = Otoluene;

        Spell Phenol = new Spell();
        SpellSet(Phenol, 17, "Phenol Ball", true, phenolicon, "fireball pun", 30, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, phenolui, 5, 1f);
        AllSpells[17] = Phenol;

        Spell ethglyc = new Spell();
        SpellSet(ethglyc, 19, "Ethylene Glycerol Wave", true, ethyleneglycerolicon, "ice wave", 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ethyleneglycerolui, 10, 0f);
        AllSpells[19] = ethglyc;

        Spell tetraf = new Spell();
        SpellSet(tetraf, 20, "1,1,1,2-Tetrafluoroethane Spear", true, tetraficon, "spear", 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, tetrafui, 1, 0f);
        AllSpells[20] = tetraf;

        Spell propylglyc = new Spell();
        SpellSet(propylglyc, 21, "Propyl Glycerol Laser", true, propylglycolicon, "ice aser", 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, propylglycolui, 0.6f, 1.5f);
        AllSpells[21] = propylglyc;

        Spell diethglyc = new Spell();
        SpellSet(diethglyc, 22, "Diethylene Glycerol Spikes", true, diethglycolicon, "ice spikes", 140, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, diethglycolui, 60, 3f);
        AllSpells[22] = diethglyc;

        Spell phenylac = new Spell();
        SpellSet(phenylac, 23, "Phenyl Acetylene Plumes", true, phenylacetyleneicon, "lava plumes", 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, phenylacetyleneui, 60, 3f);
        AllSpells[23] = phenylac;

        Spell toluene = new Spell();
        SpellSet(toluene, 24, "Toluene Radiate", true, tolueneicon, "aoe all directions", 40, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, tolueneui, 15, 1.5f);
        AllSpells[24] = toluene;

        Spell nitrotoluene = new Spell();
        SpellSet(nitrotoluene, 25, "p-nitrotoluene flamethrower", true, nitrotolueneicon, "flamethrower", 70, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, nitrotolueneui, 30, 1.5f);
        AllSpells[25] = nitrotoluene;

        Spell trinitrotoluene = new Spell();
        SpellSet(trinitrotoluene, 26, "Trinitrotoluene Speed Boost", true, trinitrotolueneicon, "speed", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, trinitrotolueneui, 30, 0f);
        AllSpells[26] = trinitrotoluene;

        Spell nitrobenzoicacid = new Spell();
        SpellSet(nitrobenzoicacid, 27, "4-Nitrobenzoic Acid Smash", true, nitrobenzoicacidicon, "smash symbol", 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, nitrobenzoicacidui, 30, 2f);
        AllSpells[27] = nitrobenzoicacid;

        Spell benzoicacid = new Spell();
        SpellSet(benzoicacid, 28, "Benzoic Acid Concentrate", true, benzoicacidicon, "concentrate", 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, benzoicacidui, 15, 5f);
        AllSpells[28] = benzoicacid;

        Spell benzotrichloride = new Spell();
        SpellSet(benzotrichloride, 29, "Benzotrichloride Spears", true, benzotrichlorideicon, "spears all directions", 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, benzotrichlorideui, 10, 2f);
        AllSpells[29] = benzotrichloride;

        Spell benzoylchloride = new Spell();
        SpellSet(benzoylchloride, 30, "Benzoylchloride Blasts", true, benzoylchlorideicon, "ice mist 3 directions", 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, benzoylchlorideui, 10, 1f);
        AllSpells[30] = benzoylchloride;

        Spell rdx = new Spell();
        SpellSet(rdx, 31, "RDX Detonation", true, rdxicon, "huge boom", 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, rdxui, 2, 1.5f);
        AllSpells[31] = rdx;

        Spell co2 = new Spell();
        SpellSet(co2, 32, "CO2 Armor", true, co2icon, "Frost armor", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, co2ui, 45, 0f);
        AllSpells[32] = co2;

        Spell octanitrocubane = new Spell();
        SpellSet(octanitrocubane, 33, "Octanitrocubane Comets", true, octanitrocubaneicon, "firestorm", 500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, octanitrocubaneui, 60, 2f);
        AllSpells[33] = octanitrocubane;

        Spell so2 = new Spell();
        SpellSet(so2, 34, "SO2 Icestorm", true, so2icon, "aoeice slow", 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, so2ui, 30, 2f);
        AllSpells[34] = so2;

        Spell detn = new Spell();
        SpellSet(detn, 35, "DETN Bomb", true, petnicon, "living bomb", 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, petnui, 5, 0.5f);
        AllSpells[35] = detn;

        Spell chloroform = new Spell();
        SpellSet(chloroform, 36, "Chloroform Hail", true, chloroformicon, "hail storm", 300, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chloroformui, 60, 2f);
        AllSpells[36] = chloroform;

        Spell odf = new Spell();
        SpellSet(odf, 37, "ODF Concentrate", true, odficon, "most concentrated ever", 1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, odfui, 120, 5f);
        AllSpells[37] = odf;

        Spell fluoroform = new Spell();
        SpellSet(fluoroform, 38, "Fluoroform Lance", true, fluoroformicon, "ice lance better", 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, fluoroformui, 2, 0f);
        AllSpells[38] = fluoroform;

        Spell octogen = new Spell();
        SpellSet(octogen, 39, "Octogen Swathe", true, octogenicon, "flame swathe", 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, octogenui, 10, 0f);
        AllSpells[39] = octogen;




        // INORGANIC

        Spell Bronze_Sw = new Spell();
        SpellSet(Bronze_Sw, 40, "Bronze Swings", false, bronzeswordicon, "Two Sword Swings that stun for half a second.", 15, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, bronzeswordUI, 1f, 0f);
        AllSpells[40] = Bronze_Sw;

        Spell Bronze_Sweep = new Spell();
        SpellSet(Bronze_Sweep, 41, "Bronze Sweep", false, bronzesword2icon, "One sword swing that stuns for 1 second.", 20, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, bronzesword2UI, 2f, 0f);
        AllSpells[41] = Bronze_Sweep;

        Spell Bronze_GS = new Spell();
        SpellSet(Bronze_GS, 42, "Bronze Greatsword", false, bronzegsicon, "Summon a greatsword that performs a large sweep and stuns for 2 seconds.", 30, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, bronzegsUI, 6f, 0f);
        AllSpells[42] = Bronze_GS;

        Spell Bronze_dagd = new Spell();
        SpellSet(Bronze_dagd, 43, "Bronze Dagger Throw", false, bronzedagicon, "Counterattack spell.\n\nDagger throw that deals 5 damage. If timed correctly right before some enemies attack, they can stagger and can then be counterattacked", 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, bronzedagUI, 2f, 0f);
        AllSpells[43] = Bronze_dagd;

        Spell Lithium = new Spell();
        SpellSet(Lithium, 44, "Lithium Spark", false, lithiumicon, "Lithium Spark that stuns for 5 seconds and deals 5 damage. Has 1 second cast time.", 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, lithiumUI, 30f, 1f);
        AllSpells[44] = Lithium;


        // BIOCHEM

        Spell Heme1 = new Spell();
        SpellSet(Heme1, 45, "Hemoglobin Drain", true, hemeicon, "Counterattack spell.\n\nThrow a bolt of blood and if it hits recover 5% of your health and deal twice that amount. If timed correctly right before some enemies attack, they can stagger and can then be counterattacked\nHemoglobin is the main oxygen carrier in the blood, and when disrupted can cause major damage.", 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, hemeui, 0.5f, 0.5f);
        AllSpells[45] = Heme1;

        Spell Aspirin = new Spell();
        SpellSet(Aspirin, 46, "Aspirin Rejuvination", true, aspirinicon, "Restore a portion of your life over time.", 80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, aspirinui, 30f, 1.5f);
        AllSpells[46] = Aspirin;

        Spell Ibuprofen = new Spell();
        SpellSet(Ibuprofen, 47, "Ibuprophen Heal", true, ibuicon, "A 5 second cast heal spell.", 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ibui, 45f, 5f);
        AllSpells[47] = Ibuprofen;

        Spell Influenza = new Spell();
        SpellSet(Influenza, 48, "Influenza Cloud", true, influenzaicon, "Summon a miasma of influenza directly in front of the caster, dealing damage over time.", 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, influenzaui, 10f, 2f);
        AllSpells[48] = Influenza;

        Spell ChlorineGas = new Spell();
        SpellSet(ChlorineGas, 49, "Chlorine Gas Cloud", false, chlorinegasicon, "Summon a chlorine gas cloud, covering a large area and dealing a bit of damage over time to enemies.", 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chlorinegasui, 15f, 3f);
        AllSpells[49] = ChlorineGas;

		Spell Myoglobin = new Spell ();
		SpellSet (Myoglobin, 50, "Myoglobin Scimitar", true, myoglobicon, "Sacrifice 10% of your maximum HP to summon a blood scimitar, which swings once and deals damage.", 13, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, myoglobui, 0.7f, 0f);
		AllSpells [50] = Myoglobin;


        /* ---- ITEMS ---- */
        // Health potion 
        Item hpot = new Item();
        ItemSet(hpot, 0, hpotui, "Health Potion", "Consumable", "A consumable health elixir. Restores 40 health.\n\nInvented by the biochemists of Kratzoff, this concoction of known restorative herbs and drugs allows temporary relief of pain.", false, 40, 0, false, false, 0);
        AllItems[0] = hpot;

        // Stamina potion
        Item spot = new Item();
        ItemSet(spot, 1, stampotui, "Stamina Potion", "Consumable", "A consumable stamina elixir that grants 40 stamina the cost of 20 health.\n\nA highly concentrated solution of ethanol, with a strong aromatic flavoring added to cover up the smell. Alcoholic beverages were quickly discovered to allow for more spellcasts on Synthuses. The exact chemical mechanism of this phenomenon is unknown, but many speculate that it has to do with the ethereal psychological link between the Syntus and its user.", false, -20, 40, false, false, 0);
        AllItems[1] = spot;

        Item smallchime = new Item();
        ItemSet(smallchime, 2, smallchimeui, "Small Resonant Chime", "Resonant", "Ring the chime to resonate with the Multiverse.\nThis small chime should grant a small amount of soul energy for the Synthus.\nWhen the chime is rung, the soul energies of alternate universes can be resonated with and brought to this universe. To compensate for the energy gained in this universe, the chime is completely consumed. Resonant instruments are quite rare and expensive and are crafted in a very special way such that the inaudible sound they produce can resonate with the Multiverse.", false, 0, 0, false, false, 300);
        AllItems[2] = smallchime;

        Item chlorinereactable = new Item();
        ItemSet(chlorinereactable, 3, chemicalui, "Concentrated Chlorine", "Reactable", "A highly concentrated solution of chlorine. The container is highly pressurized and as a result the Chlorine is in its liquid state. Halogenation reactions are often performed rather easily with more concentrated solutions like these.", false, 0, 0, false, false, 0);
        AllItems[3] = chlorinereactable;

        Item Lithium_Hydroxide = new Item();
        ItemSet(Lithium_Hydroxide, 4, chemicalui, "Lithium Hydroxide", "Reactable", "A highly concentrated solution of Lithium Hydroxide. It would make a perfect electrolyte for a Lithium battery cell. Lithium hydroxide is often mass produced in inorganic societies, and as a result has spread across the land.", false, 0, 0, false, false, 0);
        AllItems[4] = Lithium_Hydroxide;

        Item culture = new Item();
        ItemSet(culture, 5, chemicalui, "Culture Catalyst", "Reactable", "A collection of special minerals and nutrients that are said to speed up the growth of certain microbiological cultures. It is a shame that so much effort was put into the development of deadly diseases and organisms by biochemists instead of researching medicine for their fellow man.", true, 0, 0, false, false, 0);
        AllItems[5] = culture;
		 
        Item key1 = new Item();
        ItemSet(key1, 6, key1ui, "Castle Courtyard Key", "Key", "A master key to the courtyard of Castle Vod. Castle Vod, originating from the Cosmosian word for 'Life', is supposedly devoted to study the life aspects of water. As a result, water is seen as a holy medium in the region, and is believed to be the most important gift from hyperspace and Phota, the giver of life.", false, 0, 0, false, true, 0);
        AllItems[6] = key1;

        Item key2 = new Item();
        ItemSet(key2, 7, key2ui, "Ornate Key", "Key", "An ornate looking key, possibly only carried by high ranking members of Vod.\nOnly the most elite of the Vodists know of the Magister's secrets, which are hidden away in the courtyard shack. Only high ranking officers are ever seen entering or exiting it.", false, 0, 0, false, true, 0);
        AllItems[7] = key2;

        Item abominationsoul = new Item();
        ItemSet(abominationsoul, 8, boss1soului, "Lifeless Soul", "Soul", "Soul essence of the Lifeless Abomination, the mysterious creature lurking in the Castle Vod Undercroft. It is nothing at all like a human soul, or like any living creature's soul.\n\nEspecially large souls have a hard time funneling into the Synthus, and need to be physically crushed to give it that final push. Use to gain a large amount of Energy or save to use for making new Spells at the Lab.", false, 0, 0, false, false, 800);
        AllItems[8] = abominationsoul;

        Item key3 = new Item();
        ItemSet(key3, 9, key3ui, "Dormitory Key", "Key", "Key to the front door of the Dormitories of Castle Vod. The floors are ordered in accordance to rank. Even the highest ranking Aqua Regia are known to live with minimal opulence. The Magister of each Castle usually resides in a seperate building from the others.", false, 0, 0, false, true, 0);
        AllItems[9] = key3;

		Item key4 = new Item ();
		ItemSet (key4, 10, key3ui, "Ophelia's Key", "Key", "Key to Ophelia's room.", false, 0, 0, false, true, 0);
		AllItems [10] = key4;

		Item synthus = new Item ();
		ItemSet (synthus, 11, synthusui, "Synthus", "", "Crush the Synthus to consume all of your energy and reappear in a parallel universe at the previous checkpoint. \nNicknamed the 'Soul Synthus', the mechanism behind how this device turns chemicals into weapons is still a mystery. The user is also said to have quantum immortality and the power to absorb the soul essences of others, two phenomena that are still shrouded in mystery. Although very fragile, its ability to grow in power and to be easily mass produced make it the strongest chemical weapon ever conceived.", false, -50000, 0, false, false, 0);
		AllItems [11] = synthus;

        Item TowerKey = new Item();
        ItemSet(TowerKey, 12, key4ui, "Tower Key", "Key", "Key to the western Vod Tower and adobe of the Magister.", false, 0, 0, false, true, 0);
        AllItems[12] = TowerKey;

        Item goldpendant = new Item();
        ItemSet(goldpendant, 13, pendantui, "Gold Pendant", "Key", "A very expensive looking pendant, likely belonging to the Magister of Castle Vod. Magisters of Regia Castles usually come from a long line of nobility, and it is said that each line passes along a knightly piece of jewelry to complete the coronation.", false, 0, 0, false, true, 0);
        AllItems[13] = goldpendant;

        Item bloodykey = new Item();
        ItemSet(bloodykey, 14, key3ui, "Blood Stained Key", "Key", "A blood stained dormitory key, belonging to the biochemist Aslan of the West who was temporarily performing joint research with the Vod. Biochemists often require the use of their own blood to cast most Synthus spells, as a result the state of their belongings after combat may become a bit messy.", false, 0, 0, false, true, 0);
        AllItems[14] = bloodykey;

        Item ritualsitekey = new Item();
        ItemSet(ritualsitekey, 15, key2ui, "Ritual Site Key", "Key", "Key to the Vod ritual site. Castle Vod is the only Regia establishment with a large open area dubbed a 'Ritual Site'. The Site was constructed by special request of the Magister. Its function is unknown", false, 0, 0, false, true, 0);
        AllItems[15] = ritualsitekey;

        Item squidsoul = new Item();
        ItemSet(squidsoul, 16, boss1soului, "Squid Soul", "Soul", "Soul essence of the Lifeless Abomination, the mysterious creature lurking in the Castle Vod Undercroft. It is nothing at all like a human soul, or like any living creature's soul.\n\nEspecially large souls have a hard time funneling into the Synthus, and need to be physically crushed to give it that final push. Use to gain a large amount of Energy or save to use for making new Spells at the Lab.", false, 0, 0, false, false, 800);
        AllItems[16] = squidsoul;

        /* ---- HATS ---- */
        Hat redHat = new Hat();
        HatSet(redHat, 0, "Crimson Crusader Topper", "A hat worn by the group known as the crimson crusaders who were renowned for their undetakings in blood chemistry. Formerly Regia disciples who focused on biochemistry, very little is known about those who experiment with blood. There are tales of Crimson Crusaders who had the ability to completely pop every artery of their enemies, but such stories are believed to be fictitious.", redhatui, hatredoverworld, hatredbig);
        AllHats[0] = redHat;

        Hat blueHat = new Hat();
        HatSet(blueHat, 1, "Aqua Regia Hat", "Hat worn by the disciples of Aqua Regia, who devote their life to studying the properties of water, due to its importance in nature. The Regia people were once known as peaceful nomads, and have built several castles and villages across the land as they hope to spread their beliefs of water. Recent developments in discoveries about using water as an effective weapon have changed that, however.", bluehatui, hatdefault, bluehatbig);
        AllHats[1] = blueHat;

        Hat brownHat = new Hat();
        HatSet(brownHat, 2, "Sancta Hat", "Worn by low ranking disciples of Sancta. Followers of the Sanct have a very strict intolerance to the chemical magics. Instead, they worship the Sancta Choir, a mysterious group of high ranking members of the Sanct whose singing is said to somehow destructively interfere with the Synthus.", brownhaticon, brownhatig, brownhatbig);
        AllHats[2] = brownHat;

        Hat blueber = new Hat();
        HatSet(blueber, 3, "Kratzhoff Beret", "Beret and Mask worn by the Kratzhoff, a people who are said to live in the far West. They are most notorious for their aptitude in Biochemistry and healing. It is said that Kratzhoff encampments are the final beacons of Healing and Repose for humanity, amongst the seemingly endless Wars. The mask is said to distance the healer from the disease of their patient.", bluebereticon, blueberetig, blueberetbig);
        AllHats[3] = blueber;

        /* --- TUNICS --- */
        Tunic vest1 = new Tunic();
        TunicSet(vest1, 0, tunicui, "Tattered Tunic", "Old tunic that has very limited defensive capabilities. Leather tunics have been produced for several centuries now, due to their moderate defensive capabilities and relatively high flexibility. This particular one seems to have been worn out by some sort of battle.", 0.1f, -0.1f, 0);
        AllTunics[0] = vest1;

        Tunic nullVest = new Tunic();
        TunicSet(nullVest, 1, nullui, "", "", 0f, 0f, 0f);
        AllTunics[1] = nullVest;

        /* -- AMULETS -- */
        Amulet ammy1 = new Amulet();
        AmuletSet(ammy1, 0, sapphui, "Sapphire Amulet", "Amulet embedded with a sapphire. Quite common amongst aqua regia soldiers. The sapphire was thought to contain secrets in the studies of water, but was later found out that its distinctive color was simply due to impurities of the Aluminum Oxide that it contains.", 0.01f, 500f);
        AllAmulets[0] = ammy1;

        Amulet nullAmmy = new Amulet();
        AmuletSet(nullAmmy, 1, nullui, "", "", 0.025f, 100000f);
        AllAmulets[1] = nullAmmy;











        EquippedAmulet = 1;
        EquippedHat = 1;
        EquippedTunic = 1;
	}

	// Update is called once per frame
	void Update () {


        if (ItemCount >= bagSpace)
        {
            bagIsFull = true;
        }
        else
        {
            bagIsFull = false;
        }
        if (SpellCount >= SpellSpace)
        {
            SpellBookIsFull = true;
        }
        else
        {
            SpellBookIsFull = false;
        }
        if (AmuletCount >= AmuletSpace)
        {
            AmuletIsFull = true;
        }
        else
        {
            AmuletIsFull = false;
        }
        if (TunicCount >= TunicSpace)
        {
            TunicIsFull = true;
        }
        else { TunicIsFull = false; }
        if (HatCount >= HatSpace)
        {
            HatIsFull = true;
        }
        else
        {
            HatIsFull = false;
        }
	}
}
