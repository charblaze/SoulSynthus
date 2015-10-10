using UnityEngine;
using System.Collections;

public class ExitCheckpointScript : MonoBehaviour
{
    private PlayerController playerscript;
    private Spells spells;
    private PlayerTalents talents;
    
        public bool[] mystery = new bool[40];
        public bool[] reachable= new bool[40];
        public bool[] visible= new bool[40];
        public bool[] gotten =new bool[40];
    

    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject souls = GameObject.Find("EnergyCluster(Clone)");
        playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
        spells = player.GetComponent(typeof(Spells)) as Spells;
        talents = player.GetComponent(typeof(PlayerTalents)) as PlayerTalents;
        Items other = player.GetComponent(typeof(Items)) as Items;
        SaveData data = new SaveData("" + playerscript.savefile);
        data["name"] = playerscript.name;
        data["gender"] = playerscript.gender;
        data["checkpointlevel"] = playerscript.CheckpointLevel;
        data["checkpointname"] = playerscript.CheckpointName;
        data["bagisfull"] = playerscript.bagisFull;
        data["equippedspellL"] = playerscript.equippedSpellL;
        data["equippedspellR"] = playerscript.equippedSpellR;
        data["equippedspellQ"] = playerscript.equippedSpellQ;
        data["equippedspellQ2"] = playerscript.equippedSpellQ2;
        data["equippedspell1"] = playerscript.equippedSpell1;
        data["equippedspell2"] = playerscript.equippedSpell2;
        data["xp"] = playerscript.xp;
        data["level"] = playerscript.level;
        data["maxhealth"] = playerscript.maxHealth;
        data["maxstamina"] = playerscript.max_stamina;
        data["unlockedspells"] = spells.UnlockedSpells;
        data["unlockedhats"] = spells.UnlockedHats;
        data["unlockedtunics"] = spells.UnlockedTunics;
        data["unlockedamulets"] = spells.UnlockedAmulets;
        data["pickedupitems"] = spells.PickedUpItems;
        data["equippedhat"] = spells.EquippedHat;
        data["equippedtunic"] = spells.EquippedTunic;
        data["equippedamulet"] = spells.EquippedAmulet;
        data["itemcount"] = spells.ItemCount;
        data["bagspace"] = spells.bagSpace;
        data["bagisfull"] = spells.bagIsFull;
        data["spellcount"] = spells.SpellCount;
        data["spellspace"] = spells.SpellSpace;
        data["spellbookisfull"] = spells.SpellBookIsFull;
        data["hatcount"] = spells.HatCount;
        data["hatspace"] = spells.HatSpace;
        data["hatisfull"] = spells.HatIsFull;
        data["amuletcount"] = spells.AmuletCount;
        data["amuletspace"] = spells.AmuletSpace;
        data["amuletisfull"] = spells.AmuletIsFull;
        data["tuniccount"] = spells.TunicCount;
        data["tunicspace"] = spells.TunicSpace;
        data["tunicisfull"] = spells.TunicIsFull;
        data["declared"] = talents.Declared;
        data["declaration"] = talents.declaration;
        data["spellpower"] = playerscript.SPELLPOWERBONUS;
        if (talents.Declared)
        {
            if (talents.declaration == "Inorganic")
            {
                for (int c = 0; c < talents.Inorganic_Talents_t1.Length; ++c)
                {
                    mystery[c] = talents.Inorganic_Talents_t1[c].mystery;
                    reachable[c] = talents.Inorganic_Talents_t1[c].reachable;
                    visible[c] = talents.Inorganic_Talents_t1[c].visible;
                    gotten[c] = talents.Inorganic_Talents_t1[c].gotten;
                }
            }
            else if (talents.declaration == "Organic")
            {
                for (int c = 0; c < talents.Organic_Talents.Length; ++c)
                {
                    mystery[c] = talents.Organic_Talents[c].mystery;
                    reachable[c] = talents.Organic_Talents[c].reachable;
                    visible[c] = talents.Organic_Talents[c].visible;
                    gotten[c] = talents.Organic_Talents[c].gotten;
                }
            }
            else
            {
                for (int c = 0; c < talents.Biolchem_Talents.Length; ++c)
                {
                    mystery[c] = talents.Biolchem_Talents[c].mystery;
                    reachable[c] = talents.Biolchem_Talents[c].reachable;
                    visible[c] = talents.Biolchem_Talents[c].visible;
                    gotten[c] = talents.Biolchem_Talents[c].gotten;
                }
            }
        }
        data["talentsmystery"] = mystery;
        data["talentsreachable"] = reachable;
        data["talentsvisible"] = visible;
        data["talentsgotten"] = gotten;
        data["items"] = other.VodExterior;
        data["bosses"] = other.bosses;
        if (souls != null)
        {
            EnergyClusterScript ecs = souls.GetComponent<EnergyClusterScript>();
            data["energyclusterloc"] = ecs.transform.position;
            data["energyclusterlevel"] = ecs.levelname;
            data["energyclusterworth"] = ecs.worth;
        }
        else
        {
        }
        /*data["inorg"] = talents.Inorganic_Talent_t1;
        data["org"] = talents.Organic_Talents;
        data["bio"] = talents.Biolchem_Talents;*/
        data.Save();
        print("DONE!");
    }
}