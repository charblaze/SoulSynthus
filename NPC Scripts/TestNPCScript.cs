using UnityEngine;
using System.Collections;

public class TestNPCScript : MonoBehaviour {

    private GameObject player;
    public GUIStyle thestyle;
    public GUIStyle optionstyle;
    private PlayerController playerscript;
    private Spells spells;
    public Texture2D npcsprite;

    public string WHICHNPC;


	// Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
        spells = player.GetComponent(typeof(Spells)) as Spells;
    }

    private string chatstate = "normal";

    public void Dialogue(string text)
    {
        GUI.Box(new Rect(1920 / 2 - 300, 1080 - 400, 600, 200),
         text , thestyle);
    }

    public void Option1(string text, string outcome)
    {
        if (GUI.Button(new Rect(1920 / 2 - 300, 1080 - 200, 600, 20), text, optionstyle))
        {
            chatstate = outcome;
        }
    }
    public void Option2(string text, string outcome)
    {
        if (GUI.Button(new Rect(1920 / 2 - 300, 1080 - 180, 600, 20), text, optionstyle))
        {
            chatstate = outcome;
        }
    }

    public void Option3(string text, string outcome)
    {
        if (GUI.Button(new Rect(1920 / 2 - 300, 1080 - 160, 600, 20), text, optionstyle))
        {
            chatstate = outcome;
        }
    }

    public void Option4(string text, string outcome)
    {
        if (GUI.Button(new Rect(1920 / 2 - 300, 1080 - 140, 600, 20), text, optionstyle))
        {
            chatstate = outcome;
        }
    }

    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        GUI.depth = 0;
        if (tag == "Active")
        {
            GUI.DrawTexture(new Rect(1920 - npcsprite.width + 150 , 1080 - npcsprite.height, npcsprite.width, npcsprite.height), npcsprite);
            if (playerscript.gender == "male")
            {
                GUI.DrawTexture(new Rect(playerscript.charbasemale.width - 60, 1080 - playerscript.charbasemale.height,
                    -playerscript.charbasemale.width, playerscript.charbasemale.height), playerscript.charbasemale);
                GUI.DrawTexture(new Rect(playerscript.charbasemale.width + 25 - 60, 1080 - playerscript.charbasemale.height - 120,
                    -playerscript.hatbigsprite.width / 1.81f, playerscript.hatbigsprite.height / 1.81f), playerscript.hatbigsprite);
            }
            else
            {
                GUI.DrawTexture(new Rect(playerscript.charbasefemale.width - 60, 1080 - playerscript.charbasefemale.height,
                   -playerscript.charbasefemale.width, playerscript.charbasefemale.height), playerscript.charbasefemale);
                GUI.DrawTexture(new Rect(playerscript.charbasefemale.width + 11 - 60, 1080 - playerscript.charbasefemale.height - 166,
                    -playerscript.hatbigsprite.width / 1.81f, playerscript.hatbigsprite.height / 1.81f), playerscript.hatbigsprite);
            }
            if (WHICHNPC == "Ophelia1")
            {
                if (chatstate == "normal")
                {
                    Dialogue("Greetings, " + playerscript.name + ". My name is Ophelia and I now guard this ghastly fortress. I implore you to turn back. Your mission is no longer relevant. Terrible atrocities have been committed in the castle since you have been gone.");
                    Option1("Do you have anything for sale?", "sale");
                    Option2("How do you know my name?", "name");
                    Option3("What kind of atrocities?", "what");
                }

                    /// do you have anything for sale?
                else if (chatstate == "sale")
                {
                    if (playerscript.xp >= 500)
                    {
                        Dialogue("An odd question. But indeed, I have managed to accumulate some supplies. I am willing to trade for 500 Joules. If not, I implore you to be off.");
                        Option1("Fine. (Trade 500 J for Health Potion)", "gethealthpot");
                    }
                    else
                    {
                        Dialogue("Please be off, " + playerscript.name + ".");
                    }
                }
                else if (chatstate == "gethealthpot")
                {
                    spells.AddItem(0);
                    playerscript.xp -= 500;
                    chatstate = "afterhealthpot";
                }
                else if (chatstate == "afterhealthpot")
                {
                    Dialogue("There. Now be off, " + playerscript.name + "\nObtained 1 Stamina Potion");
                    if (playerscript.xp >= 500)
                    {
                        Option1("I want another. (Trade 500 J for Health Potion)", "gethealthpot");
                    }
                }

                    // how do you know my name? 
                else if (chatstate == "name")
                {
                    Dialogue("As the representative of Vod and associate to the Magister I am expected to know the names and faces of all my constituents. Now please leave this region. Power hungry Synthus users have now taken over these lands, and their power grows stronger.");
                }
                else if (chatstate == "what")
                {

                    Dialogue("Simply put, the Magister is dead and most of the students are now mindless mutants. If you proceed past here you will be attacked. Therefore, I implore you to leave and go far away.");
                    Option1("I wish to investigate.", "inv");
                }
                else if (chatstate == "inv")
                {
                    Dialogue("Know that I will not make the effort to recover your corpse. The castle went on lockdown when the incident transpired. I am also unaware of where the keys are, although I know many fled to the docks to swim away and escape.");
                    Option1("Don't you have any keys?", "keys");
                    Option2("Why did this happen?", "why");
                }
                else if (chatstate == "keys")
                {
                    Dialogue("Unfortunately, during the panic I lost most of my keys. I do however still retain my room key. It may not be particularly useful but if you manage to get in there should be some supplies in there.");
                    if (!spells.LookForKey(10))
                    {

                        spells.AddItem(10);
                    }
                }
                else if (chatstate == "why")
                {
                    Dialogue("I simply do not know");
                }
                else
                {
                    Dialogue("Be off, " + playerscript.name + ".");
                }
            }
            else if (WHICHNPC == "Ophelia2")
            {
                switch (chatstate)
                {
                    case "normal": Dialogue("I see you have bested the creature that appears in the ritual site. Quite impressive. You may prove useful.");
                        Option1("What are you still doing here", "doinghere");
                        Option2("The squid seemed to have dropped a key and the Magister's Soul", "drop");
                        Option3("What do you mean, useful?", "useful"); break;
                    case "doinghere": Dialogue("I am simply paying my respects. The gardens still remain a peaceful haven despite the incident. I must prepare for my journey soon after.");
                        Option1("What journey?", "journey");
                        Option2("The squid seemed to have dropped a key and the Magister's Soul", "drop");
                        Option3("So before, what do you mean by 'useful'?", "useful"); break;
                    case "journey": Dialogue("A journey of self-discovery. You and me are probably the only real humans left in this region. For that reason I urge you to tread lightly.");
                        Option1("The squid seemed to have dropped a key and the Magister's Soul", "drop");
                        Option2("So before, what do you mean by 'useful'?", "useful"); break;
                    case "drop": Dialogue("It is quite evident that the creature you defeated was a result of the Magister's stupidity. Do what you will with them. I know various chemicals can be produced with clustered soul energies, so I urge you to save that soul.");
                        Option1("So what are you still doing here?", "doinghere");
                        Option2("So before, what do you mean by 'useful'?", "useful"); break;
                    case "useful": Dialogue("Your power grows stronger yet. Each soul you claim with that Synthus puts you closer to the truth. The Magister was a powerful being in this region, but he pales in comparison to the others that congregate here.");
                        Option1("The truth?", "truth");
                        Option2("What others?", "others");break;
                    case "truth": Dialogue("The truth of the universe. It is said that the most powerful Synthus users become closer to Phota herself, with the more souls that they claim. This is understandable, since during the process you are basically emulating her. You are yet very far from the truth, however. You need more souls.");
                        Option1("Who else resides in this region", "others"); break;
                    case "others": Dialogue("The most powerful Synthus users in the world congregate to the south, past the forest, near here. They intend to perform a ritual, but I do not know much about it. I will be leaving soon to investigate as well. You seem to be able to hold your own. I urge you to head south and investigate as well. Farewell, " + playerscript.name + ".");
                        player.GetComponent<Items>().bosses[4] = false;
                        player.GetComponent<Items>().bosses[5] = true;
                        break;
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if ((gameObject.transform.position - player.transform.position).magnitude > 1)
        {
            tag = "Inactive";
            chatstate = "normal";
            playerscript.cantcast = false;
        }
        if (tag == "Active")
        {
            playerscript.cantcast = true;
            var newRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.1f);
        }
	}
}
