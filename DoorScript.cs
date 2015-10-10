using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    GameObject player;
    public bool unlocked;
    public int Key;
    Spells spells;
    public bool LeadsToDifferentScene = false;
    public string LevelItLeadsTo;
    public Vector3 LocationItLeadsToInThatLevel;
	public bool quantumLeap = false;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spells = player.GetComponent(typeof(Spells)) as Spells;
    }
    public AudioClip lockedsound;
    public AudioClip opensound;
    // Update is called once per frame
    void Update()
    {
       if (tag == "Active" && unlocked)
        {
            AudioSource.PlayClipAtPoint(opensound, transform.position);
            if (!LeadsToDifferentScene && !quantumLeap)
            {
                player.transform.position += player.transform.up * 1f;
            }
            else if (LeadsToDifferentScene && !quantumLeap)
            {
				player.transform.localPosition = LocationItLeadsToInThatLevel;
                StartCoroutine(LoadingScreen.LoadLevelSCREEN(LevelItLeadsTo));
                
            } else{
                StartCoroutine(player.GetComponent<BlackCrossfade>().FADE(0.5f));
				player.transform.position = LocationItLeadsToInThatLevel;
			}
            tag = "Inactive";
        }
        else if (tag == "Active" && !unlocked)
        {
            if (spells.LookForKey(Key))
            {
                unlocked = true;
                // play unlocke sound?
                AudioSource.PlayClipAtPoint(opensound, transform.position);
                if (!LeadsToDifferentScene)
                {
                    player.transform.position += player.transform.up * 1f;
                }
				else if (LeadsToDifferentScene && !quantumLeap)
				{
					player.transform.localPosition = LocationItLeadsToInThatLevel;
                    StartCoroutine(LoadingScreen.LoadLevelSCREEN(LevelItLeadsTo));

				} else{
                    StartCoroutine(player.GetComponent<BlackCrossfade>().FADE(0.5f));
					player.transform.position = LocationItLeadsToInThatLevel;
				}
                tag = "Inactive";
            }
            else
            {
                AudioSource.PlayClipAtPoint(lockedsound, transform.position);
				GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
				OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
				ohyes.message = "Locked";
                tag = "Inactive";
            }
        }
    }
}
