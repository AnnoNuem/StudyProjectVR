using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

// This script is responsable for recording coordinates. 
// it is nice example axample , so proof of concept

public class recordCoordinates : MonoBehaviour {

	//Use to store file path
	public static string filePath;

	//for degree measurment
	public float angleBetween = 999.0F;
	public float AngleBetweenDisplay ; // for testing reasons, so we see live the angle in the inspector
	public Transform target; // this will be the field in the inspector we will drag and drop our start point. 

	//boolean for button press
	private bool keyPressedK = false; 

	void Awake() {

		filePath = ManagerScript.trialFolder+ "/Trial"+ManagerScript.trialNumber+(System.DateTime.Now).ToString("MMM-ddd-d-HH-mm-ss-yyyy")+".csv";
		//Debug.Log ("File Path -->" + filePath);
		
		//Check if the file exists ( Not really needed here)
		if (!File.Exists(filePath)) {
			File.Create(filePath).Close();
		}
	}

	//FixedUpdate is used for rigid bodies and is executed independently of the configuration of system i.e. runs on regular set of intervals
	void FixedUpdate () {
		


		/*
		// code that deals with displaying the angle in the inspector
		Vector3 targetDir2 = target.position - transform.position;
		AngleBetweenDisplay = Vector3.Angle (transform.forward, targetDir2);


		// this code is needed when the subject needs to point. instead saving all the time the error we will safe only transform.forward
		// when the subject needs to point he will push k and the data will be saved. 
		if (Input.GetKeyDown (KeyCode.K)) {

				Vector3 targetDir = target.position - transform.position;
				angleBetween = Vector3.Angle (transform.forward, targetDir);
				

		}
		*/


		//code will only execute when K is pressed
		if (Input.GetKeyDown (KeyCode.K) && !keyPressedK ) {

			//2d vector definations for angle calculation (we only take x and z coordinates)
			Vector2 targetVector = new Vector2 (target.position.x, target.position.z); 
			Vector2 transformVector = new Vector2 (transform.position.x, transform.position.z);
			Vector2 forwardVector = new Vector2 (transform.forward.x, transform.forward.z);

			//Actual calculation
			Vector2 targetDir = targetVector - transformVector;
			float angle = Vector2.Angle (targetDir, forwardVector);

			keyPressedK = true;

			//HERE
			ManagerScript.abortTrial();

		}

		//flag to enable new CSV for each trial
		if(ManagerScript.trialINprocess){
			string delimiter = ",";
			//putting values for column in csv
			string[][] output = new string[][]{
				new string[]{(transform.position.x).ToString(),(transform.position.y).ToString(),(transform.position.z).ToString(),(transform.forward).ToString(),(Time.realtimeSinceStartup).ToString(),(Time.deltaTime).ToString(),(angleBetween).ToString()} 
			};
			
			int length = output.GetLength(0);

			StringBuilder sb = new StringBuilder();

			for (int index = 0; index < length; index++)
				sb.AppendLine(string.Join(delimiter, output[index]));
			File.AppendAllText(filePath, sb.ToString());
		}


		// since we presed space we need to reset the anglebetween to 999 , indicating that it is empty
		angleBetween = 999.0F;


		//MQ-test code
		//Debug.Log("Current Parameters --->"+ManagerScript.trialList[ManagerScript.trialNumber].lightColor);
		//Debug.Log("Current Trial --->"+ManagerScript.trialNumber);
	}
	
}