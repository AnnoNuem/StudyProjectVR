using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

// This script is responsable for recording coordinates. 
// it is nice example axample , so proof of concept

public class recordCoordinates : MonoBehaviour {

	//Use to store file path
	public static string filePath;
	public static string filePath1;
	string delimiter = ",";

	//for degree measurment
	public float angleBetween = 999.0F;
	public Transform target; // this will be the field in the inspector we will drag and drop our start point. 

	//boolean for button press
	private bool keyPressedK = false; 

	void Awake() {
	}

	//FixedUpdate is used for rigid bodies and is executed independently of the configuration of system i.e. runs on regular set of intervals
	void FixedUpdate () {

		/*
		filePath1 = ManagerScript.trialFolder+ "/Trial-"+ ManagerScript.trialNumber +"-Flow.csv";
		string delimiter = ",";

		if (!File.Exists (filePath)) {
						File.Create (filePath).Close ();
				}

		string[][] output1 = new string[][]{
			new string[]{
				//ManagerScript.trialNumber.ToString (),
				(transform.position.x).ToString(),
				(transform.position.y).ToString(),
				(transform.position.z).ToString(),}
				//(transform.forward).ToString(),
				//(Time.realtimeSinceStartup).ToString(),
				//(Time.deltaTime).ToString(),
				//(angleBetween).ToString(),
				//ManagerScript.CondtionTypeVariableInContainer} 
		};
		
		int length1 = output1.GetLength(0);
		
		StringBuilder sb1 = new StringBuilder();
		
		for (int index = 0; index < length1; index++)
			sb1.AppendLine(string.Join(delimiter, output1[index]));
		File.AppendAllText(filePath1, sb1.ToString());

		*/

		//code will only execute when K is pressed
		if ((Input.GetKeyDown (KeyCode.K) || Input.GetButtonDown("360controllerButtonA") ) && ManagerScript.state==ManagerScript.states.pointing) {
			//Debug.Log("K pressed");
			recordData.recordDataParameters();

			//2d vector definations for angle calculation (we only take x and z coordinates)
			Vector2 targetVector = new Vector2 (target.position.x, target.position.z); 
			Vector2 transformVector = new Vector2 (transform.position.x, transform.position.z);
			Vector2 forwardVector = new Vector2 (transform.forward.x, transform.forward.z);

			//Actual calculation
			Vector2 targetDir = targetVector - transformVector;
			angleBetween = Vector2.Angle (targetDir, forwardVector);
			keyPressedK = true;
			//Debug.Log("ManagerScript.trialINprocess value -->"+ManagerScript.trialINprocess);

			//flag to enable new CSV for each trial
			//if(ManagerScript.trialINprocess){
				//filePath = ManagerScript.trialFolder+ "/Trial"+ManagerScript.trialNumber+".csv";
				filePath = ManagerScript.trialFolder+ "/Trial-Error-Angles.csv";
				//Debug.Log ("File Path -->" + filePath);

				//string delimiter = ",";
				
				//Check if the file exists ( Not really needed here)
				if (!File.Exists(filePath)) {
					File.Create(filePath).Close();
					
				/*
					//putting values for column in csv
					string[][] output1 = new string[][]{
					new string[]{"x-pos","y-pos","transform-z","transform-forward","time-startup","time-delta","Error angle","State"} 
					};
					
					int length1 = output1.GetLength(0);
					
					StringBuilder sb1 = new StringBuilder();
					
					for (int index = 0; index < length1; index++)
						sb1.AppendLine(string.Join(delimiter, output1[index]));
					File.AppendAllText(filePath, sb1.ToString());
				*/	
				}	

			string conditionVal = "";
			
			if (ManagerScript.CondtionTypeVariableInContainer == "Easy") {
				conditionVal = "1";
			} else if (ManagerScript.CondtionTypeVariableInContainer == "Hard") {
				conditionVal = "2";
			} else if (ManagerScript.CondtionTypeVariableInContainer == "Easy-False") {
				conditionVal = "3";
			} else if (ManagerScript.CondtionTypeVariableInContainer == "Hard-False") {
				conditionVal = "4";
			} else if (ManagerScript.CondtionTypeVariableInContainer == "Training") {
				conditionVal = "5";
			} else if (ManagerScript.CondtionTypeVariableInContainer == "ENDTRIAL") {
				conditionVal = "6";
			} else {
				conditionVal = "0";
			}
				
				//putting values for column in csv
				string[][] output = new string[][]{
					new string[]{
					ManagerScript.trialNumber.ToString (),
					//(transform.position.x).ToString(),
					//(transform.position.y).ToString(),
					//(transform.position.z).ToString(),
					//(transform.forward).ToString(),
					(Time.realtimeSinceStartup).ToString(),
					//(Time.deltaTime).ToString(),
					(angleBetween).ToString(),
					conditionVal
					//ManagerScript.CondtionTypeVariableInContainer
				} 
				};
				
				int length = output.GetLength(0);
				
				StringBuilder sb = new StringBuilder();
				
				for (int index = 0; index < length; index++)
					sb.AppendLine(string.Join(delimiter, output[index]));
				File.AppendAllText(filePath, sb.ToString());
			//}

			//HERE
			ManagerScript.newTrial();
			//Debug.Log(angleBetween);

		}


	}
	
}