using UnityEngine;
using System.Collections;

public class ChangeCameraColor : MonoBehaviour {


	public static string CondtionTypeVariableInContainer;
	GameObject temp1;
	GameObject temp2;
	GameObject temp3;
	// Use this for initialization
	void Start () {
	
		temp1 = GameObject.Find ("MainCameraEasy");
		temp2 = GameObject.Find ("MainCameraHard");
		temp3 = GameObject.Find ("MainCameraNoCond");
	
	}
	
	// Update is called once per frame
	void Update () {
	CondtionTypeVariableInContainer = ManagerScript.CondtionTypeVariableInContainer;

		if (CondtionTypeVariableInContainer == "Easy" || CondtionTypeVariableInContainer == "Easy-False") {
		
			GameObject.Find("MainCameraEasy").SetActive(true);
			GameObject.Find("MainCameraHard").SetActive(false);	
			GameObject.Find("MainCameraNoCond").SetActive(false);	


		}


		else if (CondtionTypeVariableInContainer == "Hard" || CondtionTypeVariableInContainer == "Hard-False") {
		
			GameObject.Find("MainCameraEasy").SetActive(false);
			GameObject.Find("MainCameraHard").SetActive(true);			                                            
			GameObject.Find("MainCameraNoCond").SetActive(false);	

		}

		else if (CondtionTypeVariableInContainer == "Training" || CondtionTypeVariableInContainer == "Explain") {
			
			temp1.SetActiveRecursively(false);
			temp2.SetActiveRecursively(false);			                                            
			temp3.SetActiveRecursively(true);	

		}

		else if (CondtionTypeVariableInContainer == "ENDTRIAL") {
			
			GameObject.Find("MainCameraEasy").SetActive(false);
			GameObject.Find("MainCameraHard").SetActive(false);
			GameObject.Find("MainCameraNoCond").SetActive(false);

		}
	}
}