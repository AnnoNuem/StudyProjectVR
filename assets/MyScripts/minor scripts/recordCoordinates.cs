//using UnityEngine;
//using System.Collections;
//using System.IO;
//using System.Text;

//public class recordCoordinates : MonoBehaviour
//{

		


//        public void  CalculateAngleBetween (  )
//        {

//            if ((PointFakeButton || Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("360controllerButtonA")) && ManagerScript.getState() == ManagerScript.states.pointing)
//            {

//                //2d vector definations for angle calculation (we only take x and z coordinates)
//                Vector2 targetVector = new Vector2(target.position.x, target.position.z);
//                Vector2 transformVector = new Vector2(transform.position.x, transform.position.z);
//                Vector2 forwardVector = new Vector2(transform.forward.x, transform.forward.z);

//                Vector2 targetDir = targetVector - transformVector;
//                angleBetween = Vector3.Angle(targetDir, forwardVector);
//                Vector3 cross = Vector3.Cross(targetDir, forwardVector);

//                if (cross.z < 0)
//                    angleBetween = -angleBetween;

//                SaveAngleBetweenOldWay();

//                UpdateErrorAngleStatistics();

//                ManagerScript.newTrial();

//                PointFakeButton = false;
//            }

//        }

//        private void UpdateErrorAngleStatistics ()
//        {
//            sumOfErrors = sumOfErrors + Mathf.Abs(angleBetween);
//            numberOfPointings++;
//            avarageError = sumOfErrors / numberOfPointings;
//        }

//        private void SaveAngleBetweenOldWay ()
//        {

//            if (ManagerScript.TrialMissed)
//            {

//                recordData.recordDataParameters(1, (angleBetween).ToString());
//            }
//            else
//            {
//                recordData.recordDataParameters(2, (angleBetween).ToString());
//                ManagerScript.TrialMissed = true;
//            }
//        }
	
//}
