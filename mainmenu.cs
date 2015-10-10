using UnityEngine;
using System.Collections;

public class mainmenu : MonoBehaviour {

    public Texture2D titletext, maletext, femaletext;
    public GUIStyle genderchoose;
    string s1name,  s1spec, s2name,  s2spec, s3name,  s3spec, s4name,  s4spec, s5name,  s5spec;
    int s1level, s2level, s3level, s4level, s5level;
	// Use this for initialization

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    float native_width = 1920;
    float native_height = 1080;
    bool LOADING = false;
	void Start () {

        if (System.IO.File.Exists(Application.streamingAssetsPath + "\\1.uml"))
        {
            SaveData data = SaveData.Load(Application.streamingAssetsPath + "\\1.uml");
            s1spec = "Undeclared";
            if (data.GetValue<string>("declaration") == "Organic")
            {
                s1spec = "Organic";
            }
            else if (data.GetValue<string>("declaration") == "Inorganic")
            {
                s1spec = "Inorganic";
            }
            else if (data.GetValue<string>("declaration") == "Biochemistry")
            {
                s1spec = "Biochemist";
            }
            s1name = data.GetValue<string>("name"); 
            s1level = data.GetValue<int>("level");
        }
        if (System.IO.File.Exists(Application.streamingAssetsPath + "\\2.uml"))
        {
            SaveData data = SaveData.Load(Application.streamingAssetsPath + "\\2.uml");
            s2spec = "Undeclared";
            if (data.GetValue<string>("declaration") == "Organic")
            {
                s2spec = "Organic";
            }
            else if (data.GetValue<string>("declaration") == "Inorganic")
            {
                s2spec = "Inorganic";
            }
            else if (data.GetValue<string>("declaration") == "Biochemistry")
            {
                s2spec = "Biochemist";
            }
            s2name = data.GetValue<string>("name");
            s2level = data.GetValue<int>("level");
        }
        if (System.IO.File.Exists(Application.streamingAssetsPath + "\\3.uml"))
        {
            SaveData data = SaveData.Load(Application.streamingAssetsPath + "\\3.uml");
            s3spec = "Undeclared";
            if (data.GetValue<string>("declaration") == "Organic")
            {
                s3spec = "Organic";
            }
            else if (data.GetValue<string>("declaration") == "Inorganic")
            {
                s3spec = "Inorganic";
            }
            else if (data.GetValue<string>("declaration") == "Biochemistry")
            {
                s3spec = "Biochemist";
            }
            s3name = data.GetValue<string>("name");
            s3level = data.GetValue<int>("level");
        }
        if (System.IO.File.Exists(Application.streamingAssetsPath + "\\4.uml"))
        {
            SaveData data = SaveData.Load(Application.streamingAssetsPath + "\\4.uml");
            s4spec = "Undeclared";
            if (data.GetValue<string>("declaration") == "Organic")
            {
                s4spec = "Organic";
            }
            else if (data.GetValue<string>("declaration") == "Inorganic")
            {
                s4spec = "Inorganic";
            }
            else if (data.GetValue<string>("declaration") == "Biochemistry")
            {
                s4spec = "Biochemist";
            }
            s4name = data.GetValue<string>("name");
            s4level = data.GetValue<int>("level");
        }
        if (System.IO.File.Exists(Application.streamingAssetsPath + "\\5.uml"))
        {
            SaveData data = SaveData.Load(Application.streamingAssetsPath + "\\5.uml");
            s5spec = "Undeclared";
            if (data.GetValue<string>("declaration") == "Organic")
            {
                s5spec = "Organic";
            }
            else if (data.GetValue<string>("declaration") == "Inorganic")
            {
                s5spec = "Inorganic";
            }
            else if (data.GetValue<string>("declaration") == "Biochemistry")
            {
                s5spec = "Biochemist";
            }
            s5name = data.GetValue<string>("name");
            s5level = data.GetValue<int>("level");
        }
	}
    public Texture2D background, left, right, middle, genderprompt;
    public GUIStyle newg, loadg, settings, exitg, confirm, returns, genericstyle, loadstyle;
    private string currmen = "main";
    private string chosengender, chosenname = "Alex";
    void OnGUI()
    {
        float rx = Screen.width / 1920f;
        float ry = Screen.height / 1080f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));


        GUI.DrawTexture(new Rect(0, 0, 1920, 1080), background);
        if (currmen == "main")
        {
            GUI.DrawTexture(new Rect(left.width / 2, left.height / 4, middle.width, middle.height), middle);
            GUI.DrawTexture(new Rect(-100, -100, left.width, left.height), left);
            GUI.DrawTexture(new Rect(1920 - right.width, -50, right.width, right.height), right);
            if (GUI.Button(new Rect(left.width / 2 + 300 - 276, left.height / 4 + 40, 215, 35), "", newg))
            {
                currmen = "new";
            }
            if (GUI.Button(new Rect(left.width / 2 + 300 - 276 + 215 - 50, left.height / 4 + 40, 215, 35), "", loadg))
            {
                currmen = "load";
            }
            if (GUI.Button(new Rect(left.width / 2 + 300 - 276 + 320, left.height / 4 + 40 + 2, 215, 35), "", settings))
            {
                currmen = "settings";
            }
            if (GUI.Button(new Rect(left.width / 2 + 300 - 276 + 435, left.height / 4 + 40, 215, 35), "", exitg))
            {
                Application.Quit();
            }
        }
        else if (currmen == "new")
        {
            GUI.DrawTexture(new Rect(left.width / 2, left.height / 4, middle.width, middle.height), middle);
            GUI.DrawTexture(new Rect(-100, -100, left.width, left.height), left);
            GUI.DrawTexture(new Rect(left.width / 2 + 300 - 276, left.height / 4 + 40, genderprompt.width, genderprompt.height),genderprompt);
            if (GUI.Button(new Rect(-100, -100, left.width / 3, left.height), "", genderchoose))
            {
                chosengender = "male";
                currmen = "name";
            }
            if (GUI.Button(new Rect(-100 + left.width / 3, -100, left.width / 2, left.height), "", genderchoose))
            {
                chosengender = "female";
                currmen = "name";
            }
            if (GUI.Button(new Rect(left.width / 2 + 300 - 276 + 320, left.height / 4 + 40 + 2, 215, 35), "", returns))
            {
                currmen = "main";
            }
        }
        else if (currmen == "name")
        {
            GUI.DrawTexture(new Rect(left.width / 2, left.height / 4, middle.width, middle.height), middle);
            GUI.DrawTexture(new Rect(-100, -100, left.width, left.height), left);
            GUI.Label(new Rect(left.width / 2 + 330 - 276, left.height / 4 + 87, 200, 20), "Gender Chosen: " + chosengender + "\nChoose a name:", genericstyle);
            chosenname = GUI.TextField(new Rect(left.width / 2 + 330 - 276, left.height / 4 + 68 + 90, 200, 20), chosenname, 15, genericstyle);
            if (GUI.Button(new Rect(left.width / 2 + 300 - 276 + 320, left.height / 4 + 40 + 2, 215, 35), "", confirm))
            {
                if(chosenname != ""){
                    int savetocreate = 1;
                    for (; savetocreate <= 5; ++savetocreate)
                    {
                        if (!System.IO.File.Exists(Application.streamingAssetsPath + "\\" + savetocreate +".uml"))
                        {
                            break;
                        }
                    }
                    if (savetocreate > 5)
                    {
                        GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
                        OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
                        ohyes.message = "You have too many save files. The Maximum is 5. Please delete one of your saves in the StreamingAssets folder.";
                    }
                    else
                    {
                        GameObject sax = GameObject.Find("saveassigner");
                        SaveAssigner sa = sax.GetComponent(typeof(SaveAssigner)) as SaveAssigner;
                        sa.savefile = savetocreate;
                        sa.name = chosenname;
                        sa.gender = chosengender;
                        StartCoroutine(LoadingScreen.LoadLevelSCREEN(1));
                    }
                }
            }
            if (GUI.Button(new Rect(left.width / 2 + 300 - 276 + 435, left.height / 4 + 43, 215, 35), "", returns))
            {
                currmen = "new";
            }
        }
        else if (currmen == "load")
        {
            GUI.DrawTexture(new Rect(left.width / 2, left.height / 4, middle.width, middle.height), middle);
            GUI.DrawTexture(new Rect(-100, -100, left.width, left.height), left);
            GUI.DrawTexture(new Rect(1920 - right.width, -50, right.width, right.height), right);
            if (!LOADING)
            {
                if (System.IO.File.Exists(Application.streamingAssetsPath + "\\1.uml"))
                {
                    if (GUI.Button(new Rect(1920 / 2 - 627, left.height / 4 + 68 + 90, 250, 120), s1name + "\nLevel " + s1level + " " + s1spec, loadstyle))
                    {
                        LOADING = true;
                        GameObject sax = GameObject.Find("saveassigner");
                        SaveAssigner sa = sax.GetComponent(typeof(SaveAssigner)) as SaveAssigner;
                        sa.isLoading = true;
                        sa.savefile = 1;
                        StartCoroutine(LoadingScreen.LoadLevelSCREEN(1));
                    }
                }
                if (System.IO.File.Exists(Application.streamingAssetsPath + "\\2.uml"))
                {

                    if (GUI.Button(new Rect(1920 / 2 - 627 + 251, left.height / 4 + 68 + 90, 250, 120), s2name + "\nLevel " + s2level + " " + s2spec, loadstyle))
                    {

                        LOADING = true;
                        GameObject sax = GameObject.Find("saveassigner");
                        SaveAssigner sa = sax.GetComponent(typeof(SaveAssigner)) as SaveAssigner;
                        sa.isLoading = true;
                        sa.savefile = 2;
                        StartCoroutine(LoadingScreen.LoadLevelSCREEN(1));
                    }
                }
                if (System.IO.File.Exists(Application.streamingAssetsPath + "\\3.uml"))
                {
                    if (GUI.Button(new Rect(1920 / 2 - 627 + 502, left.height / 4 + 68 + 90, 250, 120), s3name + "\nLevel " + s3level + " " + s3spec, loadstyle))
                    {

                        LOADING = true;
                        GameObject sax = GameObject.Find("saveassigner");
                        SaveAssigner sa = sax.GetComponent(typeof(SaveAssigner)) as SaveAssigner;
                        sa.isLoading = true;
                        sa.savefile = 3;
                        StartCoroutine(LoadingScreen.LoadLevelSCREEN(1));
                    }
                }
                if (System.IO.File.Exists(Application.streamingAssetsPath + "\\4.uml"))
                {
                    if (GUI.Button(new Rect(1920 / 2 - 627 + 753, left.height / 4 + 68 + 90, 250, 120), s4name + "\nLevel " + s4level + " " + s4spec, loadstyle))
                    {

                        LOADING = true;
                        GameObject sax = GameObject.Find("saveassigner");
                        SaveAssigner sa = sax.GetComponent(typeof(SaveAssigner)) as SaveAssigner;
                        sa.isLoading = true;
                        sa.savefile = 4;
                        StartCoroutine(LoadingScreen.LoadLevelSCREEN(1));
                    }
                }
                if (System.IO.File.Exists(Application.streamingAssetsPath + "\\5.uml"))
                {
                    if (GUI.Button(new Rect(1920 / 2 - 627 + 1004, left.height / 4 + 68 + 90, 250, 120), s5name + "\nLevel " + s5level + " " + s5spec, loadstyle))
                    {

                        LOADING = true;
                        GameObject sax = GameObject.Find("saveassigner");
                        SaveAssigner sa = sax.GetComponent(typeof(SaveAssigner)) as SaveAssigner;
                        sa.isLoading = true;
                        sa.savefile = 5;
                        StartCoroutine(LoadingScreen.LoadLevelSCREEN(1));
                    }
                }
            }
            if (GUI.Button(new Rect(left.width / 2 + 300 - 276 + 320, left.height / 4 + 40 + 2, 215, 35), "", returns))
            {
                currmen = "main";
            }
        }
        else if (currmen == "settings")
        {
            GUI.DrawTexture(new Rect(left.width / 2, left.height / 4, middle.width, middle.height), middle);
            GUI.DrawTexture(new Rect(-100, -100, left.width, left.height), left);
            GUI.DrawTexture(new Rect(1920 - right.width, -50, right.width, right.height), right);

            if (GUI.Button(new Rect(1920 / 2 - 107, 1080 / 2, 215, 70), "View Controls", loadstyle))
            {
                Instantiate(Resources.Load("UI/Controls"));
            }
            if (GUI.Button(new Rect(left.width / 2 + 300 - 276 + 320, left.height / 4 + 40 + 2, 215, 35), "", returns))
            {
                currmen = "main";
            }
        }
    }

	// Update is called once per frame
	void Update () {
	}
}
