/*
 * This file will serve as a utility class to help write down the values into CSVs
*/
  
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public static class recordData {

	/*
	public static void recordDataParameters(){
	}
	*/
	public static void recordDataStressors(string status){
		string filePath = ManagerScript.trialFolder+ "/Trial"+ManagerScript.trialNumber+"-Stressors.csv";

		//Check if the file exists
		if (!File.Exists(filePath)) {
			File.Create(filePath).Close();
		}

		string delimiter = ",";
		//putting values for column in csv
		string[][] output = new string[][]{
			new string[]{(Time.realtimeSinceStartup).ToString(),status} 
		};
		
		int length = output.GetLength(0);
		
		StringBuilder sb = new StringBuilder();
		
		for (int index = 0; index < length; index++)
			sb.AppendLine(string.Join(delimiter, output[index]));
		File.AppendAllText(filePath, sb.ToString());
	}

}
