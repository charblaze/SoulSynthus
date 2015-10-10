using UnityEngine;
using System.Collections;

public class BossSpawnerText : MonoBehaviour
{

    private GameObject player;
    public GUIStyle thestyle;
    public GUIStyle optionstyle;
    private PlayerController playerscript;
    private Spells spells;
    public Texture2D npcsprite;
    public string text;

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
         text, thestyle);
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
            //GUI.DrawTexture(new Rect(1920 - npcsprite.width + 150, 1080 - npcsprite.height, npcsprite.width, npcsprite.height), npcsprite);
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
            Dialogue(text);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude > 1)
        {
            tag = "Inactive";
            chatstate = "normal";
        }
    }
}
