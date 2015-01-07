using UnityEngine;
using System.Collections;
using URandom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;



public class Pause : VRGUI
{

    private static KeyCode pausekey = KeyCode.P;
    private static ManagerScript.states prevState;
    private static int NumberOfYellowSpaw = 0;
    private static int NumberOfYellowDefeted = 0;
    private static int NumberOfYellowMissed = 0;
    private static string CondtionTypeVariableInContainer;
    private static string tempVarForCondition;
    private static bool paused = false;
    public static string displayText = "";
    public GUISkin skin;


    public bool FakePauseButton = false;

    public string filePath2;
    private  string EndTimePaused;
    private  string StartTimePaused;


    void Start ()
    {
        GUI.enabled = false;
    }

    void Update ()
    {

        // evalute if pause key is pressed if yes switch state to pause or to state before pause
        if (FakePauseButton || Input.GetKeyDown(pausekey) || Input.GetButtonDown("360controllerButtonStart"))
        {
            if (paused )
            { 
                ManagerScript.TrialMissed = true; // the trial is not saved as succeded. later you can see wich trial got paused lol
                ManagerScript.switchState(ManagerScript.states.NewTrial);
                EndTimePaused = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");
                displayText = "";
                paused = false;

                if (ManagerScript.NumberofTrialsStartet > 0)
                {
                    testofsql.CreatePause(StartTimePaused, EndTimePaused);

                }

            }

            else if (!paused && ManagerScript.getState() != ManagerScript.states.startScreen && ManagerScript.getState() != ManagerScript.states.pointing && ManagerScript.getState() != ManagerScript.states.end && ManagerScript.getState() != ManagerScript.states.start)
            {
                paused = true;
                ManagerScript.switchState(ManagerScript.states.pause);
                StartTimePaused = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");


            }

            FakePauseButton = false;

        }

    }

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
        }
        else
        {
            GUI.enabled = false;
        }
    }




    public static void ChangeNumberOfYellowSpaw ()
    {

        NumberOfYellowSpaw++;
    }

    public static void ChangeNumberOfYellowDefeted ()
    {

        NumberOfYellowDefeted++;
    }

    public static void ChangeNumberOfYellowMissed ()
    {

        NumberOfYellowMissed++;
    }

    public static void PauseBetweenBlocks ( string NextBlockType )
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

    public static void SaveValues ( string NextBlockType123 )
    {

        string path123 = ManagerScript.trialFolder + "/SubjectScores.csv";

        string temp222;

        temp222 = NextBlockType123;
        File.AppendAllText(path123, temp222);


        temp222 = NumberOfYellowSpaw + " " + "Number of Yellow Spawn" + Environment.NewLine;
        File.AppendAllText(path123, temp222);

        temp222 = NumberOfYellowDefeted + " " + "Number of Yellow defeated " + Environment.NewLine;
        File.AppendAllText(path123, temp222);

        temp222 = NumberOfYellowMissed + " " + "Number of Yellow missed " + Environment.NewLine;
        File.AppendAllText(path123, temp222);

        temp222 = ManagerScript.realTrialNumber + " " + "you have done so many trials " + Environment.NewLine;
        File.AppendAllText(path123, temp222);

        temp222 = ManagerScript.abortedTrials + " " + "you have missed trials " + Environment.NewLine;
        File.AppendAllText(path123, temp222);

        temp222 = PointingScript.avarageError + " " + "your avarage error angle ";
        File.AppendAllText(path123, temp222);



    }
}
