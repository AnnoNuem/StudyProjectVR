using UnityEngine;
using System.Collections;

public class ChangeCameraColor : MonoBehaviour {


	public static string CondtionTypeVariableInContainer;
	GameObject camEasy;
	GameObject camHard;
	GameObject camNoCond;
	// Use this for initialization
	void Start () {
		camEasy = GameObject.Find ("MainCameraEasy");
		camHard = GameObject.Find ("MainCameraHard");
		camNoCond = GameObject.Find ("MainCameraNoCond");
		camEasy.SetActive(false);
		camHard.SetActiveRecursively(false);			                                            
		camNoCond.SetActiveRecursively(false);
	
	}
	
	// Update is called once per frame
	void Update () {
	CondtionTypeVariableInContainer = ManagerScript.CondtionTypeVariableInContainer;

		if (CondtionTypeVariableInContainer == "Easy" || CondtionTypeVariableInContainer == "Easy-False") {
		//	Debug.Log("easy camera");
			camEasy.SetActive(true);
			camHard.SetActiveRecursively(false);			                                            
			camNoCond.SetActiveRecursively(false);
		}


		else if (CondtionTypeVariableInContainer == "Hard" || CondtionTypeVariableInContainer == "Hard-False") {
			//Debug.Log("hard camera");
			camEasy.SetActive(false);
			camHard.SetActiveRecursively(true);			                                            
			camNoCond.SetActiveRecursively(false);
		}

		else if (CondtionTypeVariableInContainer == "Training" || CondtionTypeVariableInContainer == "Explain") {
		//	Debug.Log("no cond camera");	
			camEasy.SetActive(false);
			camHard.SetActiveRecursively(false);			                                            
			camNoCond.SetActiveRecursively(true);	
		}

		else if (CondtionTypeVariableInContainer == "ENDTRIAL") {

		//	Debug.Log("no camera");
			camEasy.SetActive(false);
			camHard.SetActiveRecursively(false);			                                            
			camNoCond.SetActiveRecursively(false);
		}
	}
}