using UnityEngine;
using System.Collections;
using URandom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class DebugPlayer : MonoBehaviour
{

		Transform StartPointCoordinates;
		private UnityRandom urand;
		float responceTime ;
		float reactAfter;
		float temp1 ;
		bool counter = false;
		Transform BlueBallPosition;
		bool defetablemassage = true ; 
		// Use this for initialization

	// real rinnning or just quick jumst to end
		public	bool RealPlayerOrNot = false ;


		void Start ()
		{

				StartPointCoordinates = GameObject.Find ("StartPoint").transform;
				urand = new UnityRandom ((int)System.DateTime.Now.Ticks);	
//		GameObject pController = GameObject.Find ("OVRPlayerController");
//		OVRPlayerController controller = pController.GetComponent<OVRPlayerController> ();
		}
	
		void Update ()
		{



				if (ManagerScript.getState () != ManagerScript.states.startScreen && ManagerScript.getState () != ManagerScript.states.end) {
				

						// We need to unpause the game
						if (ManagerScript.getState () == ManagerScript.states.pause || ManagerScript.getState () == ManagerScript.states.blockover) {
								((Pause)(GameObject.Find ("OVRCameraController").GetComponent ("Pause"))).FakePauseButton = true;
			
						} else {


								// move player forward
								if (ManagerScript.getState () != ManagerScript.states.pointing && ManagerScript.getState () == ManagerScript.states.walking) {
			
										BlueBallPosition = GameObject.Find ("BlueBallGLow").transform;
			
										temp1 = SpawnLookRed.GetSpeedMoveScale ();

										transform.position = Vector3.MoveTowards (transform.position, BlueBallPosition.position, (float)(temp1 * Time.deltaTime * 1.2f));
										transform.LookAt (BlueBallPosition); // lets allways face the blue ball 


								}

								if (((SpawnLookRed)(GameObject.Find ("RedBallGlow").GetComponent ("SpawnLookRed"))).GetYellowState () == SpawnLookRed.yellowSphereStates.defeatable && defetablemassage) {
										defetablemassage = false;
										GenrataTimeForDebugPlayerResponceDeley ();

								}

								if (((SpawnLookRed)(GameObject.Find ("RedBallGlow").GetComponent ("SpawnLookRed"))).GetYellowState () != SpawnLookRed.yellowSphereStates.defeatable) {
										defetablemassage = true;						
								}


								// if a sphere is in the defeatable mode, generate a random time with a function (due to some fucked up shit, the yellow spehre will do it ...)
								if (counter && Time.time > reactAfter) {

										counter = false;

										((SpawnLookRed)(GameObject.Find ("RedBallGlow").GetComponent ("SpawnLookRed"))).FakePress = true;
										Invoke ("unpush", 0.3f);
										// this will "push " the button
								}

								// lets point 
								if (ManagerScript.getState () == ManagerScript.states.pointing) {
				
				

										transform.LookAt (StartPointCoordinates);

										int temp2 = UnityEngine.Random.Range (1, 2);

										switch (temp2) {
					
					
										// the jidder should be around 5 to 15 degree in total, so we dont have so many conditions
										// lets try it with 10 degree in total
										case 1:

											//	transform.eulerAngles = new Vector3 (transform.eulerAngles.x, (float)(90), transform.eulerAngles.z);
						transform.Rotate(0,90,0,Space.Self);
												break;
										case 2:

										//		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, (float)(360 - 90), transform.eulerAngles.z);
						transform.Rotate(0,270,0,Space.Self);

	
												break;
										}

										// lets first totate and than push the button

										((recordCoordinates)(GameObject.Find ("OVRCameraController").GetComponent ("recordCoordinates"))).PointFakeButton = true;
				
										Invoke ("unpush", 0.2f);
								}

					
						}

		

				}
		}

		void unpush ()
		{

				((recordCoordinates)(GameObject.Find ("OVRCameraController").GetComponent ("recordCoordinates"))).PointFakeButton = false;
				((SpawnLookRed)(GameObject.Find ("RedBallGlow").GetComponent ("SpawnLookRed"))).FakePress = false;

		

		}

		public void  GenrataTimeForDebugPlayerResponceDeley ()
		{

				//generate here the time
				responceTime = (float)urand.Range (25, 58, UnityRandom.Normalization.STDNORMAL, 1.0f);
				responceTime = responceTime / 100;
				counter = true;
				reactAfter = Time.time + responceTime;
				Debug.Log (responceTime);

		}

	 

}
