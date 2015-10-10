using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    PlayerController pscript;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pscript = player.GetComponent(typeof(PlayerController)) as PlayerController;
        
    }
    RaycastHit2D focus;
	public Transform target;
	public float smoothTime = 0.3f;
	
	private Vector3 velocity = Vector3.zero;
	
	void OnTriggerEnter(Collider other){
	}

	void Update () {
        focus = pscript.target;
			Vector3 goalPos = target.position;
			goalPos.z = -50;
			transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
            if (!pscript.cameraTAR)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            if (pscript.isTargeting && pscript.cameraTAR)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward, -1*(player.transform.position - focus.transform.position)), 1f*Time.deltaTime);
            }
            else if (pscript.cameraTAR)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), 1.5f * Time.deltaTime);
            }
	}
}
