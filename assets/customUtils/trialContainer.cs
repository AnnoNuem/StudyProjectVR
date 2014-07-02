/*
 * This class holds the parameters and also randomizes parameters 
 * for different types of trials
*/

using System.Collections;

public class trialContainer {

	// Time limits
	public float targetTimeLimit1;
	public float targetTimeLimit2;
	public float pointTimeLimit1;

	//Stresssor paramaters
	public float ballFrequency;
	public float ballSpeed;
	public float spawnDistance;

	//Ambient light parameters
	public string lightColor;


	public trialContainer(){}

	public trialContainer(string trialType){
		switch (trialType) {

		}
	}


}
