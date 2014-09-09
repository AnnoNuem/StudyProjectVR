using UnityEngine;
using System.Collections;

public class ChangeRenderSettings : MonoBehaviour {

	Color fogColorNormal = black;
	Color fogColorEasy = new Color(0,26,11);
	Color fogColorHard = new Color(26,0,0);

	Color ambientLightColorNormal = new Color(82,82,82);
	Color ambientLightColorEasy = new Color(51,135,71);
	Color ambientLightColorHard = new Color(135,51,51);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void switchEasy() {
	RenderSettings.ambientLight = ambientLightColorEasy;
	RenderSettings.fogColor = fogColorEasy;
}

void switchHard() {
	RenderSettings.ambientLight = ambientLightColorHard;
	RenderSettings.fogColor = fogColorHard;
}

void switchNormal() {
	RenderSettings.ambientLight = ambientLightColorNormal;
	RenderSettings.fogColor = fogColorNormal;
}

}