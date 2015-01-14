// *********************************************************************** Assembly :
// Assembly-CSharp Author : razial Created : 12-29-2014
// 
// Last Modified By : razial Last Modified On : 01-07-2015 ***********************************************************************
// <copyright file="PointingScript.cs" company="INLUSIO">
//     Copyright (c) INLUSIO. All rights reserved. 
// </copyright>
// <summary>
// </summary>
// *********************************************************************** 
using UnityEngine;

/// <summary>
/// Class PointingScript. 
/// </summary>
public class PointingScript : MonoBehaviour
{
    /// <summary>
    /// The time for pointing 
    /// </summary>
    private int timeForPointing = 8;

    /// <summary>
    /// The displaytext 
    /// </summary>
    private GameObject displaytext;

    /// <summary>
    /// The file path 
    /// </summary>
    public static string filePath;

    /// <summary>
    /// The point fake button 
    /// </summary>
    public bool PointFakeButton = false;

    /// <summary>
    /// The angle between 
    /// </summary>
    public static float angleBetween = 0;

    /// <summary>
    /// The number of pointings 
    /// </summary>
    public static int numberOfPointings = 0;

    /// <summary>
    /// The sum of errors 
    /// </summary>
    public static float sumOfErrors = 0.0f;

    /// <summary>
    /// The avarage error 
    /// </summary>
    public static float avarageError = 0.0f;

    /// <summary>
    /// The target 
    /// </summary>
    public Transform target;
    public static GameObject  start;

    public static  float AbsoluteErrorAngle;
    public static  string EndTimePoining;

    /// <summary>
    /// Starts this instance. 
    /// </summary>
    private void Start ()
    {
        displaytext = GameObject.Find("Displaytext");
        target = GameObject.Find("StartPoint").transform;


        start = GameObject.Find("CenterEyeAnchor");

    }

    public static void ChangeTheObjectForDebugger ()
    {
        start = GameObject.Find("OVRPlayerController");

        
    }

    /// <summary>
    /// Updates this instance. 
    /// </summary>
    void Update ()
    {
        if (ManagerScript.getState() == ManagerScript.states.pointing)
        {
            if ((PointFakeButton || Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("360controllerButtonA")))
            {
                //2d vector definations for angle calculation (we only take x and z coordinates)
                Vector2 targetVector = new Vector2(target.position.x, target.position.z);
                Vector2 transformVector = new Vector2(start.transform.position.x, start.transform.position.z);
                Vector2 forwardVector = new Vector2(start.transform.forward.x, start.transform.forward.z);
                Vector2 targetDir = targetVector - transformVector;
                angleBetween = Vector3.Angle(targetDir, forwardVector);
                Vector3 cross = Vector3.Cross(targetDir, forwardVector);

                if (cross.z < 0)
                    angleBetween = -angleBetween;

                AbsoluteErrorAngle = Mathf.Abs(angleBetween);

                SaveAngleBetweenOldWay();
                UpdateErrorAngleStatistics();
                EndTimePoining = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff");

                testofsql.UpdateTriallist(testofsql.CURRENT_TRIAL_ID.ToString());
                testofsql.UpdateAndIncrease_Current_Triallist_ID();
                CancelInvoke("toLongPoint");

                ManagerScript.switchState(ManagerScript.states.NewTrial);

                PointFakeButton = false;
            }
        }
    }

    /// <summary>
    /// News the pointing. 
    /// </summary>
    public void NewPointing ()
    {
        Invoke("toLongPoint", timeForPointing);
        displaytext.GetComponent<TextMesh>().text = "Point to Origin";
        Invoke("clearGUItext", 1f);
    }

    /// <summary>
    /// To the long point. 
    /// </summary>
    private void toLongPoint ()
    {
        // recordData.recordDataParameters(0, "999"); 
        ManagerScript.abortTrial();
    }

    /// <summary>
    /// Clears the gu itext. 
    /// </summary>
    private void clearGUItext ()
    {
        displaytext.GetComponent<TextMesh>().text = "";
    }

    /// <summary>
    /// Updates the error angle statistics. 
    /// </summary>
    private void UpdateErrorAngleStatistics ()
    {
        sumOfErrors = sumOfErrors + Mathf.Abs(angleBetween);
        numberOfPointings++;
        avarageError = sumOfErrors / numberOfPointings;
    }

    /// <summary>
    /// Saves the angle between old way. 
    /// </summary>
    private void SaveAngleBetweenOldWay ()
    {
        if (!ManagerScript.TrialMissed)
        {
            // recordData.recordDataParameters(1, (angleBetween).ToString()); 
        } else
        {
            // recordData.recordDataParameters(2, (angleBetween).ToString()); 
        }
    }
}