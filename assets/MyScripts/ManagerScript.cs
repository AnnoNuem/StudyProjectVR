// *********************************************************************** Assembly :
// Assembly-CSharp Author : razial Created : 01-07-2015
// 
// Last Modified By : razial Last Modified On : 01-07-2015 ***********************************************************************
// <copyright file="ManagerScript.cs" company="INLUSIO">
//     Copyright (c) INLUSIO. All rights reserved. 
// </copyright>
// <summary>
// </summary>
// *********************************************************************** 

/*
 * This script manages and keeps global values and other scripts
 * relie on this for different variables and functions.
 * Also has a state machine defining the state of the trial
 */

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Ovr;

/// <summary>
/// Class ManagerScript. 
/// </summary>
public class ManagerScript : MonoBehaviour
{
    /// <summary>
    /// The condtion type variable in container 
    /// </summary>
    public static string CondtionTypeVariableInContainer;

    /// <summary>
    /// The move scale 
    /// </summary>
    private static float moveScale;

    /// <summary>
    /// The instance 
    /// </summary>
    public static ManagerScript Instance = null;

    //public static List<float> generatedAngles = new List<float> ();
    /// <summary>
    /// The generated angle
    /// </summary>
    public static float generatedAngle;

    // states for state machine to describe in which experiment state we are 
    /// <summary>
    /// Enum states 
    /// </summary>
    public enum states
    {
        /// <summary>
        /// The start screen 
        /// </summary>
        startScreen,

        /// <summary>
        /// The walking 
        /// </summary>
        walking,

        /// <summary>
        /// The pointing 
        /// </summary>
        pointing,

        /// <summary>
        /// The blockover 
        /// </summary>
        blockover,

        /// <summary>
        /// The pause 
        /// </summary>
        pause,

        /// <summary>
        /// The end 
        /// </summary>
        end,

        /// <summary>
        /// The start 
        /// </summary>
        start,

        /// <summary>
        /// The new trial 
        /// </summary>
        NewTrial
    }

    // chiffre for identification, can be changed in start screen 
    /// <summary>
    /// The chiffre 
    /// </summary>
    public static string chiffre = "";

    /// <summary>
    /// The state 
    /// </summary>
    private static states state;

    // session identifier, tree different sessions 
    /// <summary>
    /// The session 
    /// </summary>
    public static int session;

    //public list of trials
    /// <summary>
    /// The trial list
    /// </summary>
    public static List<trialContainer> trialList = new List<trialContainer>();

    //static variable tracks what trial is in process
    /// <summary>
    /// The trial folder
    /// </summary>
    public static string trialFolder;

    /// <summary>
    /// The parameter file 
    /// </summary>
    public static string parameterFile = "";

    /// <summary>
    /// The trial i nprocess 
    /// </summary>
    public static bool trialINprocess = false;

    /// <summary>
    /// The point task i nprocess 
    /// </summary>
    public static bool pointTaskINprocess = false;

    /// <summary>
    /// The timeto pointing stage 
    /// </summary>
    public static float timetoPointingStage = 0.0f;

    /// <summary>
    /// The pointing time 
    /// </summary>
    public static float pointingTime = 0.0f;

    /// <summary>
    /// The duplicate present 
    /// </summary>
    private static bool duplicatePresent = true;

    /// <summary>
    /// The aborted trials 
    /// </summary>
    public static int abortedTrials = 0;

    /// <summary>
    /// The current orientation 
    /// </summary>
    public static int CurrentOrientation; // 0 is for left , 1 is for right

    /// <summary>
    /// The trial missed 
    /// </summary>
    public static bool  TrialMissed = false;

    /// <summary>
    /// The temp123 
    /// </summary>
    public static bool temp123 = false;

    /// <summary>
    /// The real trial number 
    /// </summary>
    public static int realTrialNumber = 1;// can repeat !!! (increeases with every succesfull trial ... )

    /// <summary>
    /// The numberof trials startet 
    /// </summary>
    public static int NumberofTrialsStartet = 0;// this increase with every start of a trial. so this number will represent the current database number of the trial

    public static int debugg;

    //public PauseCoroutine pClass;

    /// <summary>
    /// Starts this instance. 
    /// </summary>
    private void Start ()
    {
        //pClass = new PauseCoroutine();
        // first we do grab the controller and assign the moveScale in a tricky way
        GameObject pController = GameObject.Find("OVRPlayerController");
        OVRPlayerController controller = pController.GetComponent<OVRPlayerController>();
        controller.GetMoveScaleMultiplier(ref moveScale);
        //Debug.Log("Value--->" + moveScale);
        // here the trialFolder path is genrated, not clear why
        trialFolder = Application.dataPath + @"\Trial" + (System.DateTime.Now).ToString("MMM-ddd-d-HH-mm-ss-yyyy");
        testofsql.SQLiteInit(); // initialisation of the Data Base
        GameObject.Find("StressorYellow").GetComponent<Stressor>().EndStressor(); // dissable the stressor in the beginning
        //Debug.Log("lol");
        ManagerScript.switchState(states.startScreen);
        //Debug.Log("lol2");
    }

    /// <summary>
    /// Updates this instance. 
    /// </summary>
    private void Update ()
    {
        if (state == states.walking)
        {
            timetoPointingStage += Time.deltaTime * 1;
        }
        if (state == states.pointing)
        {
            pointingTime += Time.deltaTime * 1;
        }
    }

    /// <summary>
    /// Aborts the trial. 
    /// </summary>
    public static void abortTrial ()
    {
        // Without stun and unstun, the aboutTrial was repeating itself in the case, the move button
        // was presssed. It is fixes like this
        ((Waypoint)(GameObject.Find("WaypointBlue").GetComponent("Waypoint"))).SaveWaypointWhenTrialMissed();
        stun();
        trialINprocess = false;
        Time.timeScale = 0;
        CameraFade.StartAlphaFade(Color.black, false, 2f, 0f); // why we need this again ?
        new WaitForSeconds(2);
        Time.timeScale = 1;
        TrialMissed = true;
        ManagerScript.switchState(states.NewTrial);

        abortedTrials++;
        unStun();
    }

    // the state machine 
    /// <summary>
    /// Switches the state. 
    /// </summary>
    /// <param name="newState"> The new state. </param>
    public static void switchState (states newState)
    {
        Debug.Log(newState);

        switch (newState)
        {
            case states.startScreen:
                Time.timeScale = 1;
                ManagerScript.state = states.startScreen;
                break;

            case states.start:
                ManagerScript.state = states.start;

                ManagerScript.trialINprocess = true;

                trialFolder = @"C:/temp/inlusio_data/subject_" + ManagerScript.chiffre;

                if (!Directory.Exists(trialFolder))
                {
                    Directory.CreateDirectory(trialFolder);
                }

                // recordData.recordDataParametersInit(); 
                generateTrials();

                if (debugg == 1)
                {
                    GameObject.Find("OVRPlayerController").GetComponent<OVRPlayerController>().HmdRotatesY = false;

                    GameObject.Find("OVRPlayerController").GetComponent<DebugPlayer>().enabled = true;
                } else
                {
                    GameObject.Find("OVRPlayerController").GetComponent<OVRPlayerController>().HmdRotatesY = true;
                    GameObject.Find("OVRPlayerController").GetComponent<DebugPlayer>().enabled = false;
                }

                // lets create the initial savings 
                testofsql.InitialSavingsToDB(chiffre.ToString(), Stressor.EasyDelay.ToString(), Stressor.HardDealy.ToString(), session.ToString(), trialList);

                Pause.PauseBetweenBlocks(trialList [realTrialNumber + 1].CondtionTypeVariableInContainer);
                switchState(states.pause);

                break;

            case states.walking:

                //fades Screen
                ((OVRScreenFade)(GameObject.Find("LeftEyeAnchor").GetComponent("OVRScreenFade"))).fadeTime = 0.25f;
                ((OVRScreenFade)(GameObject.Find("RightEyeAnchor").GetComponent("OVRScreenFade"))).fadeTime = 0.25f;
                ((OVRScreenFade)(GameObject.Find("LeftEyeAnchor").GetComponent("OVRScreenFade"))).OnEnable();
                ((OVRScreenFade)(GameObject.Find("RightEyeAnchor").GetComponent("OVRScreenFade"))).OnEnable();

                // recordData.recordDataSmallspread("PF", ""); 

                ResetPositionRorationPlayerWaypoint();
                unStun();

                Time.timeScale = 1;
                ManagerScript.state = states.walking;

                ((Waypoint)(GameObject.Find("WaypointBlue").GetComponent("Waypoint"))).switchState(Waypoint.WayPointStates.NewTrial);

                //Activate or deactivate the Stressor according to the current CondtionTypeVariableInContainer
                if (ManagerScript.CondtionTypeVariableInContainer != "Explain"
                    && ManagerScript.CondtionTypeVariableInContainer != "Dummy"
                    && ManagerScript.CondtionTypeVariableInContainer != "Training")
                {
                    GameObject.Find("StressorYellow").GetComponent<Stressor>().StartStressor();
                } else
                {
                    GameObject.Find("StressorYellow").GetComponent<Stressor>().EndStressor();
                }
                break;

            case states.pause:

                ManagerScript.state = states.pause;


                Time.timeScale = 0;
                

                if (debugg == 1)
                {

                    UnPause();
                } 
                    
                break;

            case states.pointing:
                Time.timeScale = 1;
                ((Stressor)(GameObject.Find("StressorYellow").GetComponent("Stressor"))).switchState(Stressor.yellowSphereStates.end);
                ((PointingScript)(GameObject.Find("helperObject").GetComponent("PointingScript"))).NewPointing();
                stun();
                ManagerScript.state = states.pointing;
                break;

            case states.NewTrial:

                ((Stressor)(GameObject.Find("StressorYellow").GetComponent("Stressor"))).switchState(Stressor.yellowSphereStates.end);
                ManagerScript.state = states.NewTrial;

                NumberofTrialsStartet++;

                if (!TrialMissed)
                {
                    realTrialNumber++;
                    testofsql.UpdateTriallist(testofsql.CURRENT_TRIAL_ID.ToString());
                } else
                    TrialMissed = false;

                // Time.timeScale = 1; 

                timetoPointingStage = 0.0f;
                pointingTime = 0.0f;
                // new WaitForSeconds(2) 

                ((PointingScript)(GameObject.Find("helperObject").GetComponent("PointingScript"))).PointFakeButton = false;
                CondtionTypeVariableInContainer = trialList [realTrialNumber].CondtionTypeVariableInContainer;

                trialINprocess = true;
                Time.timeScale = 0;

                testofsql.CreateTrial(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;"), NumberofTrialsStartet.ToString(), realTrialNumber.ToString(), CondtionTypeVariableInContainer);

                if (trialList [realTrialNumber].CondtionTypeVariableInContainer == "BLOCKOVER")
                    switchState(states.blockover);
                else if (trialList [realTrialNumber].CondtionTypeVariableInContainer == "ENDTRIAL")
                {
                    testofsql.UpdateSession(); // this session is over
                    switchState(states.end);
                } else 
                    switchState(states.walking);

                Debug.Log("trial type" + ManagerScript.CondtionTypeVariableInContainer);
                break;

            case states.blockover:
                realTrialNumber++;
                ManagerScript.state = states.blockover;

                testofsql.SetDynamicDifficulty(Stressor.EasyDelay.ToString(), Stressor.HardDealy.ToString());
                testofsql.SaveDynamicDifficultyEvent(); 

                //Pause.SaveValues(trialList [realTrialNumber + 1].CondtionTypeVariableInContainer);
                Pause.PauseBetweenBlocks(trialList [realTrialNumber + 1].CondtionTypeVariableInContainer);
                ((Stressor)(GameObject.Find("StressorYellow").GetComponent("Stressor"))).ResetBallsCounterForDynamicDifficulty();

                if (debugg == 1)
                {
                    
                    UnPause();
                } 

                break;

            case states.end:
                ManagerScript.state = states.end;
                Pause.PauseBetweenBlocks("EXPOVER");
                Pause.displayText = "--Fine--\nAll trials completed.\n";
                Time.timeScale = 0;
                break;
        }
    }

    /// <summary>
    /// Resets the position and roration of player and the waypoint. 
    /// </summary>
    private static void ResetPositionRorationPlayerWaypoint ()
    {

        Debug.Log("reseting the position and rotation of the OVRPlayerController");
        GameObject.Find("OVRPlayerController").transform.position = GameObject.Find("StartPoint").transform.position;
        GameObject.Find("OVRPlayerController").transform.rotation = GameObject.Find("StartPoint").transform.rotation;
        GameObject.FindWithTag("OVRcam").transform.rotation = GameObject.Find("StartPoint").transform.rotation;
        GameObject.Find("OVRPlayerController").transform.position = GameObject.Find("StartPoint").transform.position;
        GameObject.Find("WaypointBlue").transform.rotation = GameObject.Find("StartPoint").transform.rotation;
    }

    /// <summary>
    /// Stuns this instance. 
    /// </summary>
    private static void stun ()
    {
        GameObject pController = GameObject.Find("OVRPlayerController");
        OVRPlayerController controller = pController.GetComponent<OVRPlayerController>();
        controller.SetMoveScaleMultiplier(0.0f);
    }

    /// <summary>
    /// Uns the stun. 
    /// </summary>
    private static void unStun ()
    {
        GameObject pController = GameObject.Find("OVRPlayerController");
        OVRPlayerController controller = pController.GetComponent<OVRPlayerController>();
        controller.SetMoveScaleMultiplier(1.5f);
    }

    /// <summary>
    /// Gets the state. 
    /// </summary>
    /// <returns> states. </returns>
    public static states getState ()
    {
        return state;
    }

    /// <summary>
    /// Generates the trials. 
    /// </summary>
    public static void generateTrials ()
    {
        trialContainer blockTrial = new trialContainer("BLOCKOVER");
        trialContainer endTrial = new trialContainer("ENDTRIAL");

        if (session == 1)
        {
            for (int i=0; i < 10; i++)
            {
                trialContainer tempTrial = new trialContainer("Explain");
                trialList.Add(tempTrial);
            }
            trialList.Add(blockTrial);
            for (int i=0; i < 40; i++)
            {
                trialContainer tempTrial = new trialContainer("Training");
                trialList.Add(tempTrial);
            }

            trialList.Add(blockTrial);

            for (int i=0; i < 5; i++)
            {
                trialContainer tempTrial = new trialContainer("Easy");
                trialList.Add(tempTrial);
            }

            trialList.Add(blockTrial);

            for (int i=0; i < 5; i++)
            { //20
                trialContainer tempTrial = new trialContainer("Hard");
                trialList.Add(tempTrial);
            }

            trialList.Add(blockTrial);

            for (int i=0; i < 45; i++)
            {
                trialContainer tempTrial = new trialContainer("Easy");
                trialList.Add(tempTrial);
            }

            trialList.Add(blockTrial);

            for (int i=0; i < 45; i++)
            { //20
                trialContainer tempTrial = new trialContainer("Hard");
                trialList.Add(tempTrial);
            }

            trialList.Add(blockTrial);

            List<trialContainer> easyBlock1 = new List<trialContainer>();

            for (int i=0; i < 36; i++)
            { //20
                trialContainer tempTrial = new trialContainer("Easy");
                easyBlock1.Add(tempTrial);
            }

            for (int i=0; i < 9; i++)
            {
                trialContainer tempTrial = new trialContainer("Easy-False");
                easyBlock1.Add(tempTrial);
            }

            while (duplicatePresent)
            {
                easyBlock1.Shuffle();
                for (int i=0; i < easyBlock1.Count - 1; i++)
                {
                    if (easyBlock1 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock1 [i + 1].CondtionTypeVariableInContainer == "Easy-False")
                    {
                        duplicatePresent = true;
                        break;
                    }
                    duplicatePresent = false;
                }
            }

            trialList.AddRange(easyBlock1);
            trialList.Add(blockTrial);
            duplicatePresent = true;

            List<trialContainer> hardBlock1 = new List<trialContainer>();

            for (int i=0; i < 36; i++)
            { //20
                trialContainer tempTrial = new trialContainer("Hard");
                hardBlock1.Add(tempTrial);
            }

            for (int i=0; i < 9; i++)
            {
                trialContainer tempTrial = new trialContainer("Hard-False");
                hardBlock1.Add(tempTrial);
            }

            while (duplicatePresent)
            {
                hardBlock1.Shuffle();
                for (int i=0; i < easyBlock1.Count - 1; i++)
                {
                    if (hardBlock1 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock1 [i + 1].CondtionTypeVariableInContainer == "Hard-False")
                    {
                        duplicatePresent = true;
                        break;
                    }
                    duplicatePresent = false;
                }
            }

            trialList.AddRange(hardBlock1);
            trialList.Add(blockTrial);
            duplicatePresent = true;

            for (int i=0; i < 40; i++)
            {
                trialContainer tempTrial = new trialContainer("Training");
                trialList.Add(tempTrial);
            }
            trialList.Add(blockTrial);

            trialList.Add(endTrial);
        } else if (session == 2)
        {
            //CHANGE!!!
            for (int i=0; i < 2; i++)
            {
                trialContainer tempTrial = new trialContainer("Training");
                trialList.Add(tempTrial);
            }

            trialList.Add(blockTrial);

            List<int> orderNumbers = new List<int> { 1, 2 };
            orderNumbers.Shuffle();

            switch (orderNumbers [1])
            {
                case 1:

                    List<trialContainer> easyBlock1 = new List<trialContainer>();

                    for (int i=0; i < 36; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Easy");
                        easyBlock1.Add(tempTrial);
                    }

                    for (int i=0; i < 9; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Easy-False");
                        easyBlock1.Add(tempTrial);
                    }

                    while (duplicatePresent)
                    {
                        easyBlock1.Shuffle();
                        for (int i=0; i < easyBlock1.Count - 1; i++)
                        {
                            if (easyBlock1 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock1 [i + 1].CondtionTypeVariableInContainer == "Easy-False")
                            {
                                duplicatePresent = true;
                                break;
                            }
                            duplicatePresent = false;
                        }
                    }

                    trialList.AddRange(easyBlock1);
                    trialList.Add(blockTrial);
                    duplicatePresent = true;

                    List<trialContainer> hardBlock1 = new List<trialContainer>();

                    for (int i=0; i < 36; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Hard");
                        hardBlock1.Add(tempTrial);
                    }

                    for (int i=0; i < 9; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Hard-False");
                        hardBlock1.Add(tempTrial);
                    }

                    while (duplicatePresent)
                    {
                        hardBlock1.Shuffle();
                        for (int i=0; i < hardBlock1.Count - 1; i++)
                        {
                            if (hardBlock1 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock1 [i + 1].CondtionTypeVariableInContainer == "Hard-False")
                            {
                                duplicatePresent = true;
                                break;
                            }
                            duplicatePresent = false;
                        }
                    }

                    trialList.AddRange(hardBlock1);
                    trialList.Add(blockTrial);
                    duplicatePresent = true;

                    List<trialContainer> easyBlock2 = new List<trialContainer>();

                    for (int i=0; i < 36; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Easy");
                        easyBlock2.Add(tempTrial);
                    }

                    for (int i=0; i < 9; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Easy-False");
                        easyBlock2.Add(tempTrial);
                    }

                    while (duplicatePresent)
                    {
                        easyBlock2.Shuffle();
                        for (int i=0; i < easyBlock2.Count - 1; i++)
                        {
                            if (easyBlock2 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock2 [i + 1].CondtionTypeVariableInContainer == "Easy-False")
                            {
                                duplicatePresent = true;
                                break;
                            }
                            duplicatePresent = false;
                        }
                    }

                    trialList.AddRange(easyBlock2);
                    trialList.Add(blockTrial);
                    duplicatePresent = true;

                    List<trialContainer> hardBlock2 = new List<trialContainer>();

                    for (int i=0; i < 36; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Hard");
                        hardBlock2.Add(tempTrial);
                    }

                    for (int i=0; i < 9; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Hard-False");
                        hardBlock2.Add(tempTrial);
                    }

                    while (duplicatePresent)
                    {
                        hardBlock2.Shuffle();
                        for (int i=0; i < hardBlock2.Count - 1; i++)
                        {
                            if (hardBlock2 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock2 [i + 1].CondtionTypeVariableInContainer == "Hard-False")
                            {
                                duplicatePresent = true;
                                break;
                            }
                            duplicatePresent = false;
                        }
                    }

                    trialList.AddRange(hardBlock2);
                    trialList.Add(blockTrial);
                    duplicatePresent = true;
                    break;

                case 2:

                    List<trialContainer> hardBlock3 = new List<trialContainer>();

                    for (int i=0; i < 36; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Hard");
                        hardBlock3.Add(tempTrial);
                    }

                    for (int i=0; i < 9; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Hard-False");
                        hardBlock3.Add(tempTrial);
                    }

                    while (duplicatePresent)
                    {
                        hardBlock3.Shuffle();
                        for (int i=0; i < hardBlock3.Count - 1; i++)
                        {
                            if (hardBlock3 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock3 [i + 1].CondtionTypeVariableInContainer == "Hard-False")
                            {
                                duplicatePresent = true;
                                break;
                            }
                            duplicatePresent = false;
                        }
                    }

                    trialList.AddRange(hardBlock3);
                    trialList.Add(blockTrial);
                    duplicatePresent = true;

                    List<trialContainer> easyBlock3 = new List<trialContainer>();

                    for (int i=0; i < 36; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Easy");
                        easyBlock3.Add(tempTrial);
                    }

                    for (int i=0; i < 9; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Easy-False");
                        easyBlock3.Add(tempTrial);
                    }

                    while (duplicatePresent)
                    {
                        easyBlock3.Shuffle();
                        for (int i=0; i < easyBlock3.Count - 1; i++)
                        {
                            if (easyBlock3 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock3 [i + 1].CondtionTypeVariableInContainer == "Easy-False")
                            {
                                duplicatePresent = true;
                                break;
                            }
                            duplicatePresent = false;
                        }
                    }

                    trialList.AddRange(easyBlock3);
                    trialList.Add(blockTrial);
                    duplicatePresent = true;

                    List<trialContainer> hardBlock4 = new List<trialContainer>();

                    for (int i=0; i < 36; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Hard");
                        hardBlock4.Add(tempTrial);
                    }

                    for (int i=0; i < 9; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Hard-False");
                        hardBlock4.Add(tempTrial);
                    }

                    while (duplicatePresent)
                    {
                        hardBlock4.Shuffle();
                        for (int i=0; i < hardBlock4.Count - 1; i++)
                        {
                            if (hardBlock4 [i].CondtionTypeVariableInContainer == "Hard-False" && hardBlock4 [i + 1].CondtionTypeVariableInContainer == "Hard-False")
                            {
                                duplicatePresent = true;
                                break;
                            }
                            duplicatePresent = false;
                        }
                    }

                    trialList.AddRange(hardBlock4);
                    trialList.Add(blockTrial);
                    duplicatePresent = true;

                    List<trialContainer> easyBlock4 = new List<trialContainer>();

                    for (int i=0; i < 36; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Easy");
                        easyBlock4.Add(tempTrial);
                    }

                    for (int i=0; i < 9; i++)
                    {
                        trialContainer tempTrial = new trialContainer("Easy-False");
                        easyBlock4.Add(tempTrial);
                    }

                    while (duplicatePresent)
                    {
                        easyBlock4.Shuffle();
                        for (int i=0; i < easyBlock4.Count - 1; i++)
                        {
                            if (easyBlock4 [i].CondtionTypeVariableInContainer == "Easy-False" && easyBlock4 [i + 1].CondtionTypeVariableInContainer == "Easy-False")
                            {
                                duplicatePresent = true;
                                break;
                            }
                            duplicatePresent = false;
                        }
                    }

                    trialList.AddRange(easyBlock4);
                    trialList.Add(blockTrial);
                    duplicatePresent = true;

                    break;
            }

            for (int i=0; i < 40; i++)
            {
                trialContainer tempTrial = new trialContainer("Training");
                trialList.Add(tempTrial);
            }

            trialList.Add(blockTrial);

            trialList.Add(endTrial);
        } else if (session == 666)
        {
            for (int i=0; i < 2; i++)
            {
                trialContainer tempTrial = new trialContainer("Easy");
                trialList.Add(tempTrial);
            }

            trialList.Add(blockTrial);

            for (int i=0; i < 15; i++)
            { //20
                trialContainer tempTrial = new trialContainer("Hard");
                trialList.Add(tempTrial);
            }

            trialList.Add(blockTrial);

            trialList.Add(endTrial);
        } else
            Application.Quit();
    }

    static void UnPause ()
    {

        ((Pause)(GameObject.Find("CenterEyeAnchor").GetComponent("Pause"))).FakePauseButton = true;

    }
}