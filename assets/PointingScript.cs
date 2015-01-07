using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class PointingScript : MonoBehaviour
{
    int timeForPointing = 8;
    GameObject displaytext;
    public static string filePath;
    public bool PointFakeButton = false;
    public float angleBetween = 0;
    public static int numberOfPointings = 0;
    public static float sumOfErrors = 0.0f;
    public static float avarageError = 0.0f;
    public Transform target; 

    void Start ()
    {
        displaytext = GameObject.Find("Displaytext");
    }

    void Update ()
    {
        if (ManagerScript.getState() == ManagerScript.states.pointing)
        {
            if ((PointFakeButton || Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("360controllerButtonA")))
            {
                //2d vector definations for angle calculation (we only take x and z coordinates)
                Vector2 targetVector = new Vector2(target.position.x, target.position.z);
                Vector2 transformVector = new Vector2(transform.position.x, transform.position.z);
                Vector2 forwardVector = new Vector2(transform.forward.x, transform.forward.z);
                Vector2 targetDir = targetVector - transformVector;
                angleBetween = Vector3.Angle(targetDir, forwardVector);
                Vector3 cross = Vector3.Cross(targetDir, forwardVector);

                if (cross.z < 0)
                    angleBetween = -angleBetween;

                SaveAngleBetweenOldWay();
                UpdateErrorAngleStatistics();
                ManagerScript.switchState(ManagerScript.states.NewTrial);
                CancelInvoke("toLongPoint");
                PointFakeButton = false;
            }
        }

    }

    public void NewPointing ()
    {
        Invoke("toLongPoint", timeForPointing);
        displaytext.GetComponent<TextMesh>().text = "Point to Origin";
        Invoke("clearGUItext", 1f);
    }

    void toLongPoint ()
    {
        recordData.recordDataParameters(0, "999");
        ManagerScript.abortTrial();
    }

    void clearGUItext ()
    {
        displaytext.GetComponent<TextMesh>().text = "";
    }

    private void UpdateErrorAngleStatistics ()
    {
        sumOfErrors = sumOfErrors + Mathf.Abs(angleBetween);
        numberOfPointings++;
        avarageError = sumOfErrors / numberOfPointings;
    }

    private void SaveAngleBetweenOldWay ()
    {

        if (!ManagerScript.TrialMissed)
        {

            recordData.recordDataParameters(1, (angleBetween).ToString());
        }
        else
        {
            recordData.recordDataParameters(2, (angleBetween).ToString());
        }
    }

}


