using UnityEngine;
using System.Collections;

public class ChangeRenderSettings : MonoBehaviour
{

	Color fogColorNormal = Color.black;
	Color fogColorEasy = new Color (0.0F / 255, 13F / 255 , 2F / 255);
	Color fogColorHard = new Color (15F / 255, 0.0F / 255, 0.0F / 255);
	Color ambientLightColorNormal = new Color (82F / 255, 82F / 255, 82F / 255);
	Color ambientLightColorEasy = new Color (61F / 255, 145F / 255, 81F / 255);
	Color ambientLightColorHard = new Color (145F / 255, 61F / 255, 61F / 255);

	Camera cam1;
	Camera cam2;

		// Use this for initialization
		void Start ()
		{
		cam1 = GameObject.Find("CameraRight").camera;    
		cam2 = GameObject.Find("CameraLeft").camera;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void switchEasy ()
		{
		RenderSettings.ambientLight = ambientLightColorEasy;
		RenderSettings.fogColor = fogColorEasy;
		RenderSettings.fogDensity = 0.02f;
		RenderSettings.fog = true;
		RenderSettings.fogMode = FogMode.ExponentialSquared;
		cam1.backgroundColor = fogColorEasy;
		cam2.backgroundColor = fogColorEasy;

		
		}

		public void switchHard ()
		{
		RenderSettings.ambientLight = ambientLightColorHard;
		RenderSettings.fogColor = fogColorHard;
		RenderSettings.fogDensity = 0.02f;
		RenderSettings.fog = true;
		RenderSettings.fogMode = FogMode.ExponentialSquared;
		cam1.backgroundColor = fogColorHard;
		cam2.backgroundColor = fogColorHard;

	}
	
		public void switchNormal ()
		{
		RenderSettings.ambientLight = ambientLightColorNormal;
		RenderSettings.fogColor = fogColorNormal;
		RenderSettings.fogDensity = 0.02f;
		RenderSettings.fog = true;
		RenderSettings.fogMode = FogMode.ExponentialSquared;
		cam1.backgroundColor = fogColorNormal;
		cam2.backgroundColor = fogColorNormal;

	}
	
}