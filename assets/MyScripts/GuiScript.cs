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
	GameObject displaytext;

	int displayTime = 2;
	Rect position;

	// Use this for initialization
	void Start ()
	{

		displaytext = GameObject.Find("Displaytext");


		toSlowText = "You took to long to reach the blue sphere. \n New Trial";
		toSlowPointText = "You took to long to point to the origin. \n New Trial";
		walkToBlueBallText = "Walk to the next blue ball!";
		pointToBlueBallText = "Point to origin.";
		newTrialText = "New Trial";

//		bool showToSlowText = false;
//		bool showToSlowPointText = false;
//		bool showPointText = false;
//		bool showWalkText =false;
//		bool showNewTrial = false;

		position = new Rect (Screen.width / 2 - 150, Screen.height / 2 - 200, 300, 40);
	}
	
	// OnGUI is called once per frame
	// OnGUI is called once per frame
	void OnGUI ()
	{
		if (showToSlowText) {			
			
			displaytext.GetComponent<TextMesh>().text = toSlowText;
		}
		if (showToSlowPointText) {			
			;
			displaytext.GetComponent<TextMesh>().text = toSlowPointText;
			
		}
		if (showPointText) {			
			
			displaytext.GetComponent<TextMesh>().text = pointToBlueBallText;
			
		}
		if (showWalkText) {			
			
			displaytext.GetComponent<TextMesh>().text = walkToBlueBallText;
			
		}
		if (showNewTrial) {			
			
			displaytext.GetComponent<TextMesh>().text = newTrialText;
			
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
		displaytext.GetComponent<TextMesh>().text = "";
	}
	
	public void toSlowPoint ()
	{
		showToSlowPointText = true;
		Invoke ("HideToSlowPoint", displayTime);
	}
	void HideToSlowPoint ()
	{
		showToSlowPointText = false;
		displaytext.GetComponent<TextMesh>().text = "";
	}
	
	
	public void point ()
	{
		showPointText = true;
		Invoke ("HidePoint", displayTime);
	}
	void HidePoint ()
	{
		showPointText = false;
		displaytext.GetComponent<TextMesh>().text = "";
	}
	
	public void walk ()
	{
		showWalkText = true;
		Invoke ("HideWalk", displayTime);
	}
	void HideWalk ()
	{
		showWalkText = false;
		displaytext.GetComponent<TextMesh>().text = "";
	}
	
	public void newTrial ()
	{
		showNewTrial = true;
		Invoke ("HideNewTrial", displayTime);
	}
	void HideNewTrial ()
	{
		showNewTrial = false;
		displaytext.GetComponent<TextMesh>().text = "";
		
	}

	public void Shoot(float timer2 ){

		Invoke ("HideShoot", timer2);
		
	}

	void HideShoot(){
		
		displaytext.GetComponent<TextMesh>().text = "";

	}
}

	

