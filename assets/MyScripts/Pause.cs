// *********************************************************************** Assembly :
// Assembly-CSharp Author : razial Created : 12-29-2014
// 
// Last Modified By : razial Last Modified On : 01-07-2015 ***********************************************************************
// <copyright file="Pause.cs" company="INLUSIO">
//     Copyright (c) INLUSIO. All rights reserved. 
// </copyright>
// <summary>
// </summary>
// *********************************************************************** 
using UnityEngine;

/// <summary>
/// Class Pause. 
/// </summary>
public class Pause : VRGUI
{
    /// <summary>
    /// The pausekey 
    /// </summary>
    private static KeyCode pausekey = KeyCode.P;

    /// <summary>
    /// The previous state 
    /// </summary>
    private static ManagerScript.states prevState;

    /// <summary>
    /// The number of yellow spaw 
    /// </summary>
    private static int NumberOfYellowSpaw = 0;

    /// <summary>
    /// The number of yellow defeted 
    /// </summary>
    private static int NumberOfYellowDefeted = 0;

    /// <summary>
    /// The number of yellow missed 
    /// </summary>
    private static int NumberOfYellowMissed = 0;


    /// <summary>
    /// The paused 
    /// </summary>
    private static bool paused = false;

    /// <summary>
    /// The display text 
    /// </summary>
    public static string displayText = "";

    /// <summary>
    /// The skin 
    /// </summary>
    public GUISkin skin;

    /// <summary>
    /// The fake pause button 
    /// </summary>
    public bool FakePauseButton = false;

    /// <summary>
    /// The file path2 
    /// </summary>
    public string filePath2;

    /// <summary>
    /// The end time paused 
    /// </summary>
    private  string EndTimePaused;

    /// <summary>
    /// The start time paused 
    /// </summary>
    private  string StartTimePaused;

    /// <summary>
    /// Starts this instance. 
    /// </summary>
    private void Start ()
    {
        GUI.enabled = false;
        StartTimePaused = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff");
    }

    /// <summary>
    /// Updates this instance. 
    /// </summary>
    private void Update ()
    {
        // evalute if pause key is pressed if yes switch state to pause or to state before pause 
        if (FakePauseButton || Input.GetKeyDown(pausekey) || Input.GetButtonDown("360controllerButtonStart"))
        {
            if (paused)
            {
                ManagerScript.TrialMissed = true; // the trial is not saved as succeded. later you can see wich trial got paused lol
//                Debug.Log("1New trial should run now -->");
                ManagerScript.switchState(ManagerScript.states.NewTrial);
                EndTimePaused = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff");
                displayText = "";
                paused = false;

                if (ManagerScript.NumberofTrialsStartet > 0)
                {
                    testofsql.CreatePause(StartTimePaused, EndTimePaused);
                }
            } else if (!paused && ManagerScript.getState() != ManagerScript.states.startScreen && ManagerScript.getState() != ManagerScript.states.pointing && ManagerScript.getState() != ManagerScript.states.end && ManagerScript.getState() != ManagerScript.states.start)
            {
                Debug.Log("2New trial should run now -->");
                paused = true;
                ManagerScript.switchState(ManagerScript.states.pause);
                StartTimePaused = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff");
            }

            FakePauseButton = false;
        }
    }

    /// <summary>
    /// Called when [vrgui]. 
    /// </summary>
    public override void OnVRGUI ()
    {
        GUI.skin = skin;

        // show pause screen 
        if (paused || ManagerScript.getState() == ManagerScript.states.end)
        {
            GUI.enabled = true;
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical("box");
            GUILayout.FlexibleSpace();
            GUILayout.Label("<color=lime> PAUSE </color>");
            GUILayout.Label("Press Start to resume.\n");
            GUILayout.Label(displayText);
            GUILayout.Label(NumberOfYellowSpaw + " " + "Number of Yellow Spawn");
            GUILayout.Label(NumberOfYellowDefeted + " " + "Number of Yellow defeated ");
            GUILayout.Label(NumberOfYellowMissed + " " + "Number of Yellow missed ");
            GUILayout.Label(ManagerScript.realTrialNumber + " " + "you have done so many trials ");
            GUILayout.Label(ManagerScript.abortedTrials + " " + "you have missed trials ");
            GUILayout.Label(PointingScript.avarageError + " " + "your avarage error angle ");
            GUILayout.EndVertical();
            GUILayout.EndArea();
        } else
        {
            GUI.enabled = false;
        }
    }

    /// <summary>
    /// Changes the number of yellow spaw. 
    /// </summary>
    public static void ChangeNumberOfYellowSpaw ()
    {
        NumberOfYellowSpaw++;
    }

    /// <summary>
    /// Changes the number of yellow defeted. 
    /// </summary>
    public static void ChangeNumberOfYellowDefeted ()
    {
        NumberOfYellowDefeted++;
    }

    /// <summary>
    /// Changes the number of yellow missed. 
    /// </summary>
    public static void ChangeNumberOfYellowMissed ()
    {
        NumberOfYellowMissed++;
    }

    /// <summary>
    /// Pauses the between blocks. 
    /// </summary>
    /// <param name="NextBlockType"> Type of the next block. </param>
    public static void PauseBetweenBlocks (string NextBlockType)
    {
        paused = true;
        if (NextBlockType.Contains("Easy") || NextBlockType.Contains("Easy-False"))
        {
            displayText = "Block Complted.\nNext block of Trials is Easy.\n";
        }

        if (NextBlockType.Contains("Hard") || NextBlockType.Contains("Hard-False"))
        {
            displayText = "Block Complted.\nNext block of Trials is Hard.\n";
        }

        if (NextBlockType.Contains("Explain"))
        {
            displayText = "Next block of Trials is Explain.\n";
        }

        if (NextBlockType.Contains("Training"))
        {
            displayText = "Block Completed. Next block of Trials is Training.\n";
        }
        if (NextBlockType.Contains("ENDTRIAL"))
        {
            displayText = "Experiment is over, please take of the oculus rift and report to the experimenter.\n";
        }

        if (NextBlockType.Contains("EXPOVER"))
        {
            displayText = "Experiment is over, please take of the oculus rift and report to the exoerimenter.\n";
        }
    }

    /// <summary>
    /// Saves the values. 
    /// </summary>
    /// <param name="NextBlockType123"> The next block type123. </param>

    public static void SaveValues ()
    {
        testofsql.SaveStatisicsToDataBase(NumberOfYellowSpaw.ToString(), NumberOfYellowDefeted.ToString(), NumberOfYellowMissed.ToString(), ManagerScript.abortedTrials.ToString(), PointingScript.avarageError.ToString());
    }
}