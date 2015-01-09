using UnityEngine;
using System.Collections;

public class BlackScreen : MonoBehaviour
{
    private bool active = true;

    // Use this for initialization
    void Start ()
    {

    }
	
    // Update is called once per frame
    void Update ()
    {
        if (active)
        {
            guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        }
    }
}
