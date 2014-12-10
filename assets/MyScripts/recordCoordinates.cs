using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

// This script is responsable for recording coordinates. 
// it is nice example axample , so proof of concept

public class recordCoordinates : MonoBehaviour
{

		//Use to store file path
		public static string filePath;
		public static string filePath1;
//		string delimiter = ",";
		public bool PointFakeButton = false; 
		//for degree measurment
		public float angleBetween = 999.0F;
		public float angleBetween1 = 999.0F;
		public static int numberOfPointings = 0 ;
		public static float sumOfErrors = 0.0f ;
		public static float avarageError = 0.0f ;
		public Transform target; // this will be the field in the inspector we will drag and drop our start point. 
	
		void Awake ()
		{
		}

		//FixedUpdate is used for rigid bodies and is executed independently of the configuration of system i.e. runs on regular set of intervals
		void FixedUpdate ()
		{

				float angleBetween12 = 999.0F;
				//2d vector definations for angle calculation (we only take x and z coordinates)
				Vector2 targetVector1 = new Vector2 (target.position.x, target.position.z); 
				Vector2 transformVector1 = new Vector2 (transform.position.x, transform.position.z);
				Vector2 forwardVector1 = new Vector2 (transform.forward.x, transform.forward.z);
				
				Vector2 targetDir1 = targetVector1 - transformVector1;
				
				//Old calculation which does not shows -ve pr +ve angles
				//angleBetween12 = Vector2.Angle (targetDir1, forwardVector1);
				angleBetween1 = Vector3.Angle (targetDir1, forwardVector1);
				//angleBetween12 = Mathf.Asin (Vector3.Cross(targetDir1, forwardVector1).magnitude);
				//angleBetween12 = Vector3.Angle (targetDir1, forwardVector1);
				Vector3 cross1 = Vector3.Cross (targetDir1, forwardVector1);
				
				//Debug.Log (angleBetween);
				
						if (cross1.z < 0)
								angleBetween1 = -angleBetween1;
						
					
			angleBetween12 = Mathf.Atan2(targetDir1.y, forwardVector1.y)* Mathf.Rad2Deg;
								
				

//		Debug.Log ("Angle => "+angleBetween1+ "Abs Angle => "+angleBetween12 );

			//code will only execute when K is pressed
				if (( PointFakeButton || Input.GetKeyDown (KeyCode.K) || Input.GetButtonDown ("360controllerButtonA")) && ManagerScript.getState () == ManagerScript.states.pointing) {
						
						//2d vector definations for angle calculation (we only take x and z coordinates)
						Vector2 targetVector = new Vector2 (target.position.x, target.position.z); 
						Vector2 transformVector = new Vector2 (transform.position.x, transform.position.z);
						Vector2 forwardVector = new Vector2 (transform.forward.x, transform.forward.z);
						
						Vector2 targetDir = targetVector - transformVector;
						
						//Old calculation which does not shows -ve pr +ve angles
						//angleBetween = Vector2.Angle (targetDir, forwardVector);
						angleBetween = Vector3.Angle (targetDir, forwardVector);
						Vector3 cross = Vector3.Cross (targetDir, forwardVector);
						sumOfErrors = sumOfErrors + angleBetween;
						Debug.Log ("Winkel" + " "  +  angleBetween);
						
						if (cross.z < 0)
								angleBetween = -angleBetween;

						if (ManagerScript.temp123) {

								recordData.recordDataParameters (1, (angleBetween).ToString());
						} else {
								recordData.recordDataParameters (3, (angleBetween).ToString());
								ManagerScript.temp123 = true;
						}


						/*


						filePath = ManagerScript.trialFolder + "/Trial-Error-Angles.csv";
						
						if (!File.Exists (filePath)) {
								File.Create (filePath).Close ();
					
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
				
						
						string[][] output = new string[][]{
					new string[]{
					System.DateTime.Now.ToString ("o"),
					ManagerScript.trialNumber.ToString (),
						//(transform.position.x).ToString(),
						//(transform.position.y).ToString(),
						//(transform.position.z).ToString(),
						//(transform.forward).ToString(),
					(Time.realtimeSinceStartup).ToString (),
						//(Time.deltaTime).ToString(),
					(angleBetween).ToString (),
					conditionVal
						//ManagerScript.CondtionTypeVariableInContainer
				} 
				};
				
						int length = output.GetLength (0);
				
						StringBuilder sb = new StringBuilder ();
				
						for (int index = 0; index < length; index++)
								sb.AppendLine (string.Join (delimiter, output [index]));
						File.AppendAllText (filePath, sb.ToString ());
						//}
					*/
						//HERE
						numberOfPointings++;
						avarageError = sumOfErrors / numberOfPointings;
						ManagerScript.newTrial ();
						//Debug.Log(angleBetween);
			PointFakeButton = false;
				}


		}
	
}