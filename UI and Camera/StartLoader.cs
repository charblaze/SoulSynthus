using UnityEngine;
using System.Collections;

public class StartLoader : MonoBehaviour {
    GameObject pp;
	GameObject player;

    EnergyClusterScript ecs;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () {
        GameObject sax = GameObject.Find("saveassigner");
        SaveAssigner sa = sax.GetComponent(typeof(SaveAssigner)) as SaveAssigner;
        int file = -1;
		pp = GameObject.FindGameObjectWithTag("Player");
        if (sa.isLoading)
        {
            file = sa.savefile;
            SaveData data = SaveData.Load(Application.streamingAssetsPath + "\\" + file + ".uml");
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
            Spells spells = player.GetComponent(typeof(Spells)) as Spells;
            PlayerTalents talents = player.GetComponent(typeof(PlayerTalents)) as PlayerTalents;
            Items other = player.GetComponent(typeof(Items)) as Items;

            playerscript.name = data.GetValue<string>("name");
            playerscript.gender = data.GetValue<string>("gender");
            playerscript.CheckpointLevel = data.GetValue<string>("checkpointlevel");
            playerscript.CheckpointName = data.GetValue<string>("checkpointname");
            playerscript.bagisFull = data.GetValue<bool>("bagisfull");
            playerscript.equipSpelltoL(data.GetValue<int>("equippedspellL"));
			playerscript.equipSpelltoR(data.GetValue<int>("equippedspellR"));
			playerscript.equipSpellto1(data.GetValue<int>("equippedspell1"));
			playerscript.equipSpellto2(data.GetValue<int>("equippedspell2"));
			playerscript.equipSpelltoQ(data.GetValue<int>("equippedspellQ"));
			playerscript.equipSpelltoQ2(data.GetValue<int>("equippedspellQ2"));
            playerscript.xp = data.GetValue<float>("xp");
            playerscript.level = data.GetValue<int>("level");
            playerscript.maxHealth = data.GetValue<int>("maxhealth");
            playerscript.max_stamina = data.GetValue<int>("maxstamina");
            spells.UnlockedSpells = data.GetValue<int[]>("unlockedspells");
            spells.UnlockedHats = data.GetValue<int[]>("unlockedhats");
            spells.UnlockedAmulets = data.GetValue<int[]>("unlockedamulets");
            spells.UnlockedTunics = data.GetValue<int[]>("unlockedtunics");
            spells.PickedUpItems = data.GetValue<int[]>("pickedupitems");
            spells.ItemCount = data.GetValue<int>("itemcount");
            playerscript.equipHat(data.GetValue<int>("equippedhat"));
            playerscript.equipTunic(data.GetValue<int>("equippedtunic"));
            playerscript.equipAmulet(data.GetValue<int>("equippedamulet"));
            spells.bagSpace = data.GetValue<int>("bagspace");
            spells.bagIsFull = data.GetValue<bool>("bagisfull");
            spells.SpellCount = data.GetValue<int>("spellcount");
            spells.SpellSpace = data.GetValue<int>("spellspace");
            spells.SpellBookIsFull = data.GetValue<bool>("spellbookisfull");
            spells.HatCount = data.GetValue<int>("hatcount");
            spells.HatSpace = data.GetValue<int>("hatspace");
            spells.HatIsFull = data.GetValue<bool>("hatisfull");
            spells.AmuletCount = data.GetValue<int>("amuletcount");
            spells.AmuletIsFull = data.GetValue<bool>("amuletisfull");
            spells.AmuletSpace = data.GetValue<int>("amuletspace");
            spells.TunicCount = data.GetValue<int>("tuniccount");
            spells.TunicSpace = data.GetValue<int>("tunicspace");
            spells.TunicIsFull = data.GetValue<bool>("tunicisfull");

            talents.Declared = data.GetValue<bool>("declared");
            talents.declaration = data.GetValue<string>("declaration");
			playerscript.numheals = 5;


            if (talents.Declared)
            {
                bool[] mys = data.GetValue<bool[]>("talentsmystery");
                bool[] rea = data.GetValue<bool[]>("talentsreachable");
                bool[] vis = data.GetValue<bool[]>("talentsvisible");
                bool[] gotten = data.GetValue<bool[]>("talentsgotten");
                if (talents.declaration == "Inorganic")
                {
                    for (int c = 0; c < talents.Inorganic_Talents_t1.Length; ++c)
                    {
                        talents.Inorganic_Talents_t1[c].mystery = mys[c];
                        talents.Inorganic_Talents_t1[c].reachable = rea[c];
                        talents.Inorganic_Talents_t1[c].visible = vis[c];
                        talents.Inorganic_Talents_t1[c].gotten = gotten[c];
                    }
                }
                else if (talents.declaration == "Organic")
                {
                    for (int c = 0; c < talents.Organic_Talents.Length; ++c)
                    {
                        talents.Organic_Talents[c].mystery = mys[c];
                        talents.Organic_Talents[c].reachable = rea[c];
                        talents.Organic_Talents[c].visible = vis[c];
                        talents.Organic_Talents[c].gotten = gotten[c];
                    }
                }
                else
                {
                    for (int c = 0; c < talents.Biolchem_Talents.Length; ++c)
                    {
                        talents.Biolchem_Talents[c].mystery = mys[c];
                        talents.Biolchem_Talents[c].reachable = rea[c];
                        talents.Biolchem_Talents[c].visible = vis[c];
                        talents.Biolchem_Talents[c].gotten = gotten[c];
                    }
                }
            }
            bool[] wat = data.GetValue<bool[]>("items");
            for (int c = 0; c < wat.Length; ++c)
            {
                other.VodExterior[c] = wat[c];
            }
            wat = data.GetValue<bool[]>("bosses");
            for (int c = 0; c < wat.Length; ++c)
            {
                other.bosses[c] = wat[c];
            }
            if (data.HasKey("spellpower"))
            {
                playerscript.SPELLPOWERBONUS = data.GetValue<float>("spellpower");
            }
            playerscript.currentHealth = playerscript.maxHealth;
            playerscript.savefile = file;
            StartCoroutine(LoadingScreen.LoadLevelSCREEN(playerscript.CheckpointLevel));
            if (data.HasKey("energyclusterloc"))
            {
                Vector3 pos = data.GetValue<Vector3>("energyclusterloc");
                GameObject wa;
                if (pos != null)
                {
                    wa = Instantiate(Resources.Load("EnergyCluster")) as GameObject;
                    ecs = wa.GetComponent<EnergyClusterScript>();
                    wa.transform.position = pos;
                    ecs.levelname = data.GetValue<string>("energyclusterlevel");
                    ecs.worth = data.GetValue<float>("energyclusterworth");
                }
            }
            StartCoroutine(checkpointerputter(playerscript.CheckpointName));
            if (data.HasKey("energyclusterlevel"))
            {
                StartCoroutine(delayassigner(data.GetValue<string>("energyclusterlevel"), ecs));
            }
        }
        else
        {
            // new game
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
            Spells s = player.GetComponent(typeof(Spells)) as Spells;
            playerscript.CheckpointLevel = "Castle_Vod";
            playerscript.CheckpointName = "Checkpoint1";
            playerscript.currentHealth = playerscript.maxHealth;
            playerscript.savefile = sa.savefile;
            playerscript.name = sa.name;
            s.AddItem(11);
            playerscript.gender = sa.gender;
            Instantiate(Resources.Load("Saved"));
            StartCoroutine(checkpointerputter("Checkpoint1"));
			StartCoroutine(LoadingScreen.LoadLevelSCREEN("Castle_Vod"));
            //StartCoroutine(LoadingScreen.LoadLevelSCREEN("Castle_Vod");
        }
	}
    IEnumerator delayassigner(string wat, EnergyClusterScript he)
    {
        yield return new WaitForSeconds(0.5f);
        he.levelname = wat;
    }

    IEnumerator checkpointerputter(string cpname)
    {
		pp.transform.position = Spells.checkpointDatabase (cpname);
		yield return new WaitForEndOfFrame();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
