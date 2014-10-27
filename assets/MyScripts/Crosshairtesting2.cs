using UnityEngine;
using System.Collections;

public class Crosshairtesting2 : MonoBehaviour {
	
	public bool drawCrosshairSmall = true;
	public bool drawCrosshairBig = true;

	
	public Color BigcrosshairColor = Color.magenta;   //The crosshair color
	public Color SmallcrosshairColor = Color.white;   //The crosshair color

	
	public int SmalllineLength = 10; // Length of the crosshair line (in pixels)
	public int SmalllineWidth = 1; // Width of the crosshair line (in pixels)
	public int Smallspread = 20; // Spread of the crosshair lines (in pixels)

	public int BiglineLength = 10; // Length of the crosshair line (in pixels)
	public int BiglineWidth = 1; // Width of the crosshair line (in pixels)
	public int Bigspread = 20; // Spread of the crosshair lines (in pixels)

	
	private Texture2D texSmall;
	private Texture2D texBig;

	private GUIStyle lineStyle;
	
	void Awake ()
	{
		
	}
	
	void OnGUI ()
	{
		if (ManagerScript.getState() == ManagerScript.states.walking || ManagerScript.getState() == ManagerScript.states.pointing) {
			Vector2 centerPoint = new Vector2 (Screen.width / 2, Screen.height / 2);
		
			if (drawCrosshairSmall) {

				texSmall = new Texture2D(1,1);
				SetTextureColor(texSmall, SmallcrosshairColor); //Set color
				lineStyle = new GUIStyle();
				lineStyle.normal.background = texSmall;


				GUI.Box (new Rect (centerPoint.x,
			                 centerPoint.y - SmalllineLength - Smallspread,
			                 SmalllineWidth,
			                 SmalllineLength), GUIContent.none, lineStyle);
				GUI.Box (new Rect (centerPoint.x,
			                 centerPoint.y + Smallspread,
			                 SmalllineWidth,
			                 SmalllineLength), GUIContent.none, lineStyle);
				GUI.Box (new Rect (centerPoint.x + Smallspread,
			                 centerPoint.y,
			                 SmalllineLength,
			                 SmalllineWidth), GUIContent.none, lineStyle);
				GUI.Box (new Rect (centerPoint.x - Smallspread - SmalllineLength,
			                 centerPoint.y,
			                 SmalllineLength,
			                 SmalllineWidth), GUIContent.none, lineStyle);
			}

			if (drawCrosshairBig) {

				texBig = new Texture2D(1,1);
				SetTextureColor(texBig, BigcrosshairColor); //Set color
				
				lineStyle = new GUIStyle();
				
				lineStyle.normal.background = texBig;
			

					GUI.Box (new Rect (centerPoint.x,
				                   centerPoint.y - BiglineLength - Bigspread,
				                   BiglineWidth,
				                   BiglineLength), GUIContent.none, lineStyle);
					GUI.Box (new Rect (centerPoint.x,
				                   centerPoint.y + Bigspread,
				                   BiglineWidth,
				                   BiglineLength), GUIContent.none, lineStyle);
				GUI.Box (new Rect (centerPoint.x + Bigspread,
					                   centerPoint.y,
				                   BiglineLength,
				                   BiglineWidth), GUIContent.none, lineStyle);
				GUI.Box (new Rect (centerPoint.x - Bigspread - BiglineLength,
					                   centerPoint.y,
				                   BiglineLength,
				                   BiglineWidth), GUIContent.none, lineStyle);
			}
		}
	}
	
	//Applies color to a texture
	void SetTextureColor(Texture2D texture, Color color){
		for (int y = 0; y < texture.height; y++){
			for (int x = 0; x < texture.width; x++){
				texture.SetPixel(x, y, color);
			}
		}
		texture.Apply();
	}

	public void SmallCrosshair() {
			drawCrosshairSmall = true;
			drawCrosshairBig = false;

		}

	public void BigCrosshair() {
		drawCrosshairSmall = false;
		drawCrosshairBig = true;

	}

}

