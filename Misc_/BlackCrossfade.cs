using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class BlackCrossfade : MonoBehaviour
{
    public Texture2D black;
    public static AsyncOperation async = null;
    bool visible;
    public float alpha = 0f;
    public IEnumerator FADE(float time)
    {
        for (int c = 0; c < 10; ++c)
        {
            alpha += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(time);

        for (int c = 0; c < 10; ++c)
        {
            alpha -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    void OnGUI()
    {
        GUI.depth = -1000;
        Color slam = GUI.color;
        slam.a = alpha;
        GUI.color = slam;
        GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), black);
    }
}