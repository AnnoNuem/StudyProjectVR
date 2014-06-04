using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class recordCoordinates : MonoBehaviour {

	//Use to store file path
	public static string filePath;

	void Awake() {

		filePath = Application.dataPath + "/Trial"+(System.DateTime.Now).ToString("MMM-ddd-d-HH-mm-ss-yyyy")+".csv";
		Debug.Log ("File Path -->" + filePath);
		
		//Check if the file exists ( Not really needed here)
		if (!File.Exists(filePath)) {
			File.Create(filePath).Close();
		}
	}

	//FixedUpdate is used for rigid bodies and is executed independently of the configuration of system i.e. runs on regular set of intervals
	void FixedUpdate () {
		
		string delimiter = ",";
		
		//putting values for column in csv
		string[][] output = new string[][]{
			new string[]{(transform.position.x).ToString(),(transform.position.y).ToString(),(transform.position.z).ToString(),(Time.realtimeSinceStartup).ToString(),(Time.deltaTime).ToString()} 
		};
		
		int length = output.GetLength(0);
		
		StringBuilder sb = new StringBuilder();
		for (int index = 0; index < length; index++)
			sb.AppendLine(string.Join(delimiter, output[index]));
		File.AppendAllText(filePath, sb.ToString());
		
	}
	
}