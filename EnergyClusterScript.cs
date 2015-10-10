using UnityEngine;
using System.Collections;

public class EnergyClusterScript : MonoBehaviour {
    public float worth = 100;

    private PlayerController pc;

    public string levelname;
    public AudioClip fwoosh;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        tag = "Inactive";
        levelname = Application.loadedLevelName;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent(typeof(PlayerController)) as PlayerController;
        GameObject possible = GameObject.Find("EnergyCluster(Clone)");
        if(possible != gameObject){
            Destroy(possible);
        }
        wat = transform.position;
        cld = GetComponent(typeof(CircleCollider2D)) as CircleCollider2D;
	}

    public Vector3 wat;
    private CircleCollider2D cld;
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName != levelname)
        {
            transform.position = new Vector3(-100000, -100000, -10000);
        }
        else
        {
            transform.position = wat;
            cld.radius = 0.25f;
        }
        transform.Rotate(Vector3.down * Time.deltaTime * 1000);
        if (tag == "Active")
        {
            AudioSource.PlayClipAtPoint(fwoosh, transform.position);
            GameObject ohno = Instantiate(Resources.Load("Messages/OkMessage")) as GameObject;
            OkMessage ohyes = ohno.GetComponent(typeof(OkMessage)) as OkMessage;
            ohyes.message = "Regained " + worth + " Joules";
            pc.xp += worth;
            Destroy(gameObject);
        }
	}
}
