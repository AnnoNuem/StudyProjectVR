using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

// This script is responsable for recording coordinates. 
// it is nice example axample , so proof of concept

public class recordCoordinatesFeelSpace : MonoBehaviour {

	//Use to store file path
	public static string filePath;

	//for degree measurment
	public float angleBetween = 999.0F;
	public Transform target; // this will be the field in the inspector we will drag and drop our start point. 

	//boolean for button press
	private bool keyPressedK = false; 

	void Awake() {
	}

	//FixedUpdate is used for rigid bodies and is executed independently of the configuration of system i.e. runs on regular set of intervals
	void FixedUpdate () {

		//code will only execute when K is pressed
		if (Input.GetKeyDown (KeyCode.K) && ManagerScriptFeelSpace.state==ManagerScriptFeelSpace.states.pointing) {
			Debug.Log("K pressed");
			recordDataFeelSpace.recordDataParameters();

			//2d vector definations for angle calculation (we only take x and z coordinates)
			Vector2 targetVector = new Vector2 (target.position.x, target.position.z); 
			Vector2 transformVector = new Vector2 (transform.position.x, transform.position.z);
			Vector2 forwardVector = new Vector2 (transform.forward.x, transform.forward.z);

			//Actual calculation
			Vector2 targetDir = targetVector - transformVector;
			angleBetween = Vector2.Angle (targetDir, forwardVector);
			keyPressedK = true;
			Debug.Log("ManagerScript.trialINprocess value -->"+ManagerScript.trialINprocess);

			//flag to enable new CSV for each trial
			//if(ManagerScript.trialINprocess){
				filePath = ManagerScriptFeelSpace.trialFolder+ "/Trial"+ManagerScript.trialNumber+".csv";
				Debug.Log ("File Path -->" + filePath);

				string delimiter = ",";
				
				//Check if the file exists ( Not really needed here)
				if (!File.Exists(filePath)) {
					File.Create(filePath).Close();

					//putting values for column in csv
					string[][] output1 = new string[][]{
					new string[]{"x-pos","y-pos","transform-z","transform-forward","time-startup","time-delta","Error angle","State"} 
					};
					
					int length1 = output1.GetLength(0);
					
					StringBuilder sb1 = new StringBuilder();
					
					for (int index = 0; index < length1; index++)
						sb1.AppendLine(string.Join(delimiter, output1[index]));
					File.AppendAllText(filePath, sb1.ToString());
					
				}	
				
				
				//putting values for column in csv
				string[][] output = new string[][]{
					new string[]{(transform.position.x).ToString(),(transform.position.y).ToString(),(transform.position.z).ToString(),(transform.forward).ToString(),(Time.realtimeSinceStartup).ToString(),(Time.deltaTime).ToString(),(angleBetween).ToString(),ManagerScript.CondtionTypeVariableInContainer} 
				};
				
				int length = output.GetLength(0);
				
				StringBuilder sb = new StringBuilder();
				
				for (int index = 0; index < length; index++)
					sb.AppendLine(string.Join(delimiter, output[index]));
				File.AppendAllText(filePath, sb.ToString());
			//}

			//HERE
			ManagerScriptFeelSpace.newTrial();
			Debug.Log(angleBetween);

		}


	}
	
}