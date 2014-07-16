/*
 * This file will serve as a utility class to help write down the values into CSVs
*/
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public static class recordDataFeelSpace
{
		static string delimiter = ",";
		
		//creates file for saving parameters
		public static void recordDataParametersInit ()
		{
				string filePath = ManagerScriptFeelSpace.trialFolder + "/Parameters.csv";
				//Check if the file exists
				if (!File.Exists (filePath)) {
						File.Create (filePath).Close ();
						ManagerScriptFeelSpace.parameterFile = filePath;
				}
				//"Trial Number","Spawn Distance","Cool Down","Timer Red","Color","Generated Angle"

				string[][] output = new string[][]{
				new string[] {
							"Trial Number",
							"Spawn Distance",
							"Cool Down",
							"Timer Red",
							"Color",
							"Generated Angle","State",
					} 
				};
			
				int length = output.GetLength (0);
			
				StringBuilder sb = new StringBuilder ();
			
				for (int index = 0; index < length; index++)
						sb.AppendLine (string.Join (delimiter, output [index]));
				File.AppendAllText (filePath, sb.ToString ());
			
		}
		
		//records the paramters
		public static void recordDataParameters ()
		{
				//putting values for column in csv
				string[][] output = new string[][]{
			new string[] {
				ManagerScriptFeelSpace.trialNumber.ToString (),
				ManagerScriptFeelSpace.spawnDistance.ToString (),
				ManagerScriptFeelSpace.CoolDown.ToString (),
				ManagerScriptFeelSpace.timer_red.ToString (),
				ManagerScriptFeelSpace.bColor.ToString (),
				ManagerScriptFeelSpace.generatedAngle.ToString (),
				ManagerScriptFeelSpace.CondtionTypeVariableInContainer
				} 
		};

				int length = output.GetLength (0);
		
				StringBuilder sb = new StringBuilder ();
		
				for (int index = 0; index < length; index++)
						sb.AppendLine (string.Join (delimiter, output [index]));
		File.AppendAllText (ManagerScriptFeelSpace.parameterFile, sb.ToString ());
	
		}
		
		// Updates the csv for stressors with marker of spawned , destroyed or missed
		public static void recordDataStressors (string status)
		{
		string filePath = ManagerScriptFeelSpace.trialFolder + "/Trial" + ManagerScriptFeelSpace.trialNumber + "-Stressors.csv";

				//Check if the file exists
				if (!File.Exists (filePath)) {
						File.Create (filePath).Close ();
						
						//putting values for column in csv
						string[][] output1 = new string[][]{
							new string[]{"Time-startup","status"} 
						};
						
						int length1 = output1.GetLength (0);
						
						StringBuilder sb1 = new StringBuilder ();
						
						for (int index = 0; index < length1; index++)
							sb1.AppendLine (string.Join (delimiter, output1 [index]));
						File.AppendAllText (filePath, sb1.ToString ());
						
				}


				//putting values for column in csv
				string[][] output = new string[][]{
			new string[]{(Time.realtimeSinceStartup).ToString (),status} 
		};
		
				int length = output.GetLength (0);
		
				StringBuilder sb = new StringBuilder ();
		
				for (int index = 0; index < length; index++)
						sb.AppendLine (string.Join (delimiter, output [index]));
				File.AppendAllText (filePath, sb.ToString ());
		}

}
