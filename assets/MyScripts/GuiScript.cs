using UnityEngine;
using System.Collections;

public class GuiScript : MonoBehaviour {

	string toSlowText;
	string walkToBlueBallText;
	string pointToBlueBallText;
	string newTrialText;
	string toSlowPointText;

	bool showToSlowText;
	bool showPointText;
	bool showWalkText;
	bool showNewTrial;
	bool showToSlowPointText;

	int displayTime = 2;
	Rect position;
	
	// Use this for initialization
	void Start ()
	{
		toSlowText = "You took to long to reach the blue sphere. \n New Trial";
		toSlowPointText = "You took to long to point to the origin. \n New Trial";
		walkToBlueBallText = "Walk to the next blue ball!";
		pointToBlueBallText = "Point to origin.";
		newTrialText = "New Trial";

		bool showToSlowText = false;
		bool showToSlowPointText = false;
		bool showPointText = false;
		bool showWalkText =false;
		bool showNewTrial = false;
		bool bla = false;

		position = new Rect (Screen.width / 2 - 150, Screen.height / 2 - 200, 300, 40);
	}
	
	// OnGUI is called once per frame
	void OnGUI ()
	{
		if (showToSlowText) {			
			GUI.Label(position, toSlowText);
		}
		if (showToSlowPointText) {			
			GUI.Label(position, toSlowPointText);
		}
		if (showPointText) {			
			GUI.Label(position, pointToBlueBallText);
		}
		if (showWalkText) {			
			GUI.Label(position, walkToBlueBallText);
		}
		if (showNewTrial) {			
			GUI.Label(position, newTrialText);
		}
	}
	
	public void toSlow ()
	{
		showToSlowText = true;
		Invoke ("HideToSlow", displayTime);
	}
	void HideToSlow ()
	{
		showToSlowText = false;
	}

	public void toSlowPoint ()
	{
		showToSlowPointText = true;
		Invoke ("HideToSlowPoint", displayTime);
	}
	void HideToSlowPoint ()
	{
		showToSlowPointText = false;
	}


	public void point ()
	{
		showPointText = true;
		Invoke ("HidePoint", displayTime);
	}
	void HidePoint ()
	{
		showPointText = false;
	}

	public void walk ()
	{
		showWalkText = true;
		Invoke ("HideWalk", displayTime);
	}
	void HideWalk ()
	{
		showWalkText = false;
	}

	public void newTrial ()
	{
		showNewTrial = true;
		Invoke ("HideNewTrial", displayTime);
	}
	void HideNewTrial ()
	{
		showNewTrial = false;
	}
}
	

