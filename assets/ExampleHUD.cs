using UnityEngine;
using System.Collections;

public class ExampleHUD : VRGUI 
{
	public GUISkin skin;

	int count = 0;

	public override void OnVRGUI()
	{
		GUI.skin = skin;

		// NG 
		//GUI.Label(new Rect(0f, 0f, 600f, 100f), "<color=lime>Time: " +  Time.time+"</color> ");
		// OK
		GUI.Button(new Rect(100f, 10f, 600f, 100f), " <color=lime> Time: " +  Time.time+"</color> ");

		/*if(GUI.Button (new Rect (0, 0, Screen.width, Screen.height), "<color=red>This is Screen Size Button  ["  +count +"]</color>"))
		{
			count++;
		}*/


		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		GUILayout.BeginVertical ("box");
		if (GUILayout.Button ("---")) {
			count--;
		}
		if (GUILayout.Button ("+++")) {
			count++;
		}

		GUILayout.EndVertical ();

		GUILayout.BeginHorizontal();
		GUILayout.Label ("<color=lime>[ "+count+" ]</color>");

		if (GUILayout.Button ("Reset",GUILayout.ExpandHeight(true))) {
			count=0;
		}
		GUILayout.EndHorizontal();


		GUILayout.EndArea ();




	}
}
