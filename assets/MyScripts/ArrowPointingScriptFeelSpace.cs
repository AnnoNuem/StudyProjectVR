﻿using UnityEngine;
using System.Collections;

public enum Direction2
{
		left,
		right}
;

public class ArrowPointingScriptFeelSpace : MonoBehaviour
{

		Direction2 curDir;
		public Texture leftArrow;
		public Texture rightArrow;
		Rect arrowPosition1;
		Rect arrowPosition2;

		bool showArrow;
		float displayTime = 1;

		// Use this for initialization
		void Start ()
		{
				curDir = Direction2.right;
		arrowPosition1 = new Rect (((Screen.width/4)-20), Screen.height / 2 - 20, 40, 40);
		arrowPosition2 = new Rect (((Screen.width - Screen.width/4) -20), Screen.height / 2 - 20, 40, 40);

		}
	
		// OnGUI is called once per frame
		void OnGUI ()
		{
				if (ManagerScriptFeelSpace.state == ManagerScriptFeelSpace.states.walking) {
			if (showArrow) {

				if (curDir == Direction2.left) {
					GUI.DrawTexture (arrowPosition1, leftArrow);
					GUI.DrawTexture (arrowPosition2, leftArrow);

				} else {
					GUI.DrawTexture (arrowPosition1, rightArrow);
					GUI.DrawTexture (arrowPosition2, rightArrow);
				}
			}
		}
		}

		public void Point (Direction2 d)
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
