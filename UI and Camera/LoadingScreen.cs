using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Texture2D progbg;
    public Texture2D progbar;
	public Texture2D loadingbg;
    public static AsyncOperation async = null;
    public Texture2D[] possibleloadingscreens;
    public static bool rnging = false;

    public static IEnumerator LoadLevelSCREEN(string name)
    {
        rnging = true;

        async = Application.LoadLevelAsync(name);
        yield return async;
    }

    public static IEnumerator LoadLevelSCREEN(int name)
    {
        rnging = true;
        async = Application.LoadLevelAsync(name);
        yield return async;
    }

    void cookie()
    {

		int rng = Mathf.RoundToInt (Random.Range (0, possibleloadingscreens.Length));
        loadingbg = possibleloadingscreens[rng];
    }
    
    void OnGUI()
    {
        if (rnging)
        {
            cookie();
            rnging = false;
        }
		float rx = Screen.width / 1920f;
		float ry = Screen.height / 1080f;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
		GUI.depth = -1000;
        if (async != null)
        {
            GUI.DrawTexture(new Rect(0 , 0, 1920, 1080), loadingbg);
            GUI.DrawTexture(new Rect(0, 0, 1920, 10), progbg);
            GUI.DrawTexture(new Rect(1, 1, 1918 * async.progress, 8), progbar);
        }
    }

	public void OnLevelWasLoaded(){
		async = null;
	}
}