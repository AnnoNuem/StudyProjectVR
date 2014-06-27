using UnityEngine;
using System.Collections;

public enum Direction
{
		left,
		right}
;

public class ArrowPointingScript : MonoBehaviour
{

		Direction curDir;
		public Texture leftArrow;
		public Texture rightArrow;
		Rect arrowPosition;
		bool showArrow;
		float displayTime = 1;

		// Use this for initialization
		void Start ()
		{
				curDir = Direction.right;
				arrowPosition = new Rect (Screen.width / 2 - 20, Screen.height / 2 - 20, 40, 40);
				showArrow = true;
				Invoke ("HideArrow", displayTime);
		}
	
		// OnGUI is called once per frame
		void OnGUI ()
		{
				if (ManagerScript.state == ManagerScript.states.walking) {
			if (showArrow) {

				if (curDir == Direction.left) {
					GUI.DrawTexture (arrowPosition, leftArrow);
				} else {
					GUI.DrawTexture (arrowPosition, rightArrow);
				}
			}
		}
		}

		public void Point (Direction d)
		{
				curDir = d;
				showArrow = true;
				Invoke ("HideArrow", displayTime);
		}

		void HideArrow ()
		{
				showArrow = false;
		}



}
