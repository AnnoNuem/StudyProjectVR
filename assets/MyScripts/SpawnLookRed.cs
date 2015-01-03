using System.Collections;
using UnityEngine;
using XInputDotNetPure;

public class SpawnLookRed : MonoBehaviour
{
    GameObject displaytext;
    float random;
    Vector3 v, pos;
    Vector3 rayDirection;
    bool keyPressedToEarly = false;
    float rotationSpeed = 100f;
    float rotationSpeedEasy = 50f;
    float rotationSpeedHard = 500f;
    float transformationSpeed = 15f;
    float distanceToGoal = 10;
    float spawnDistance = 40f;
    float spawnheight = 20f;
    float coolDown = 2.0f;       // How long to hide
    string SpawnTime;
    string StartDefeatTime;
    string DefeatedAtTime;
    int missedHardBalls = 0;
    int missedEasyBalls = 0;
    int catchedHardBalls = 0;
    int catchedEasyBalls = 0;
    float EasyDelay = 0.500f;
    float HardDealy = 0.300f;
    //stuff for vibrating
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    public bool FakePress = false; // this is needed for the debug player
    private UnityRandom urand;
    private int timeTillExp = 1; // how long till explosion
    float defeatableTillTime;
    public static float moveScale;
    float onsetOfDefeatAtTime;
    float durationOfResponsePeriod;
    GameObject pController;
    Transform cameraTransform = null;
    GameObject pxController;
    OVRPlayerController xcontroller;
    static string ExplosionTime;

    public enum yellowSphereStates
    {
        hidden,
        moving,
        defeatable,
        notDefeatedInTime,
        start,
        end,
        defeatedInTime,
    }

    yellowSphereStates s;
    private  float StartDefeatTimeFloat;
    private  float TimeOfDefeat;
    private  float ReactionTime;

    void Awake ()
    {
        cameraTransform = GameObject.FindWithTag("OVRcam").transform;
        displaytext = GameObject.Find("Displaytext2");
    }

    void Start ()
    {
        pxController = GameObject.Find("OVRPlayerController");
        xcontroller = pxController.GetComponent<OVRPlayerController>();
        xcontroller.GetMoveScaleMultiplier(ref moveScale);
        urand = new UnityRandom((int)System.DateTime.Now.Ticks);
        switchState(yellowSphereStates.hidden);
    }

    void Update ()
    {
        if (ManagerScript.getState() == ManagerScript.states.walking)
        {
            switch (s)
            {
                case yellowSphereStates.moving:

                    move();
                    if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("360controllerButtonB"))
                    {
                        keyPressedToEarly = true;
                    }
                    break;
                case yellowSphereStates.defeatable:

                    move();
                    if (FakePress || (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("360controllerButtonB")) && !keyPressedToEarly)
                    {
                        switchState(yellowSphereStates.defeatedInTime);
                        Pause.ChangeNumberOfYellowDefeted();
                        FakePress = false;

                        if (ManagerScript.CondtionTypeVariableInContainer == "Easy")
                        {
                            catchedEasyBalls++;
                        }
                        else if (ManagerScript.CondtionTypeVariableInContainer == "Hard")
                        {
                            catchedHardBalls++;
                        }

                        
                    
                        
                    }
                    break;

                case yellowSphereStates.notDefeatedInTime:
                    if (FakePress || (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("360controllerButtonB")) && !keyPressedToEarly) {
                        ReactionTime = Time.time;

                    }

                    move();
                    break;
            }
        }
        else
        {
            renderer.enabled = false;
            displaytext.GetComponent<TextMesh>().text = "";
            CancelInvoke("startExp");
        }
    }

    private void StressorDefeatable ()
    {
        switchState(yellowSphereStates.defeatable);
        StartDefeatTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");
        StartDefeatTimeFloat = Time.time;
        DefeatableTimeWindow =Time.time + durationOfResponsePeriod;
    }

    private void NotDefeatedInTime ()
    {
        switchState(yellowSphereStates.notDefeatedInTime);
    }

    void reset ()
    {
        renderer.enabled = false;
        CancelInvoke("startExp");
        keyPressedToEarly = false;
        //Invoke this shit after the coolDown time, basicaly after the coolDown
        Invoke("StartMoving", coolDown);

    }

    void StartMoving ()
    {
        switchState(yellowSphereStates.moving);
    }

    public void EndStressor ()
    {
        switchState(yellowSphereStates.end);
    }

    public void StartStressor ()
    {
        switchState(yellowSphereStates.start);

    }

    // this is the function that respawns the yellow sphere
    void MoveAndShow ()
    {

        //position yellow sphere
        random = urand.Range(-10, 10, UnityRandom.Normalization.STDNORMAL, 0.1f);
        rayDirection = cameraTransform.TransformDirection(Vector3.forward);
        pos.x = (cameraTransform.position.x + rayDirection.x * spawnDistance) + random;
        pos.z = (cameraTransform.position.z + rayDirection.z * spawnDistance) - random;
        pos.y = spawnheight;
        transform.position = pos;

        renderer.enabled = true;
        recordData.recordDataSmallspread("S", "");
        Pause.ChangeNumberOfYellowSpaw();
        SpawnTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");
    }

    // this jidders the onset between 0.8 and 2.5 seconds
    void GenerateTimeOnsetOfDefeatTime ()
    {
        onsetOfDefeatAtTime = urand.Range(8, 25, UnityRandom.Normalization.STDNORMAL, 1.0f);
        onsetOfDefeatAtTime = onsetOfDefeatAtTime / 10;
    }

    void GenerateTimeWindowForResponce ()
    {

        if (catchedEasyBalls > 10 && EasyDelay > 0.400f)
        {

            EasyDelay = EasyDelay - 0.030f;
        }

        if (catchedHardBalls > 5 && HardDealy > 0.179f)
        {
            HardDealy = HardDealy - 0.030f;
        }

        if (missedEasyBalls > 5)
        {
            EasyDelay = EasyDelay + 0.030f;
        }

        if (missedHardBalls > 5)
        {
            HardDealy = HardDealy + 0.030f;
        }

        ResetBallsCounterForDynamicDifficulty();

        if (ManagerScript.CondtionTypeVariableInContainer == "Easy" || ManagerScript.CondtionTypeVariableInContainer == "Hard-False")
        {

            durationOfResponsePeriod = EasyDelay + (Random.Range(1f, 200)) / 1000;
            rotationSpeed = rotationSpeedEasy;
        }
        else if (ManagerScript.CondtionTypeVariableInContainer == "Hard" || ManagerScript.CondtionTypeVariableInContainer == "Easy-False")
        {
            durationOfResponsePeriod = HardDealy + (Random.Range(1f, 100)) / 1000;
            rotationSpeed = rotationSpeedHard;
        }
    }

    void DataSavingAfterExplosion ()
    {
        recordData.recordDataSmallspread("M", "");
        ExplosionTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");

        string bla =
       " INSERT INTO 'Stressors' 'Stressors_id','SpawnTime','StartDefeatTime','HowLongDefeatable','DefeatedAtTime','Defeated','RotationSpeed','ButtonToEarlyPushed','Type','DefeatableTimeWindow','ReactionTime','Trial_id','ExplosionTime') VALUES"
  + "(" + "NULL" + ","
  + "'" + SpawnTime + "',"
  + "'" + StartDefeatTime + "',"
  + "'" + durationOfResponsePeriod + "',"
  + "NULL" + ","
  + "'" + 0 + "',"
  + "'" + rotationSpeed + "',"
  + "'" + keyPressedToEarly + "',"
  + "'" + ManagerScript.CondtionTypeVariableInContainer + "',"
  + "'" + DefeatableTimeWindow + "',"
  + "'" + ReactionTime + "',"
  + "'" + testofsql.CURRENT_TRIAL_ID + "',"
  + "'" + ExplosionTime+ "'" + ");" ;

        ((testofsql)(GameObject.Find("OVRPlayerController").GetComponent("testofsql"))).ExecuteQuerry(bla);
        

    }

    void DataSavingAfterDefeate ()
    {
        recordData.recordDataSmallspread("D", "");
        DefeatedAtTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");
        TimeOfDefeat = Time.time;
        ReactionTime = StartDefeatTimeFloat - TimeOfDefeat;

        string bla =  " INSERT INTO 'Stressors' 'Stressors_id','SpawnTime','StartDefeatTime','HowLongDefeatable','DefeatedAtTime','Defeated','RotationSpeed','ButtonToEarlyPushed','Type','DefeatableTimeWindow','ReactionTime','Trial_id','ExplosionTime') VALUES"
          + "(" + "NULL" + ","
          + "'" + SpawnTime + "',"
          + "'" + StartDefeatTime + "',"
          + "'" + durationOfResponsePeriod + "',"
          + "'" + DefeatedAtTime + "',"
          + "'" + 1 + "',"
          + "'" + rotationSpeed + "',"
          + "'" + keyPressedToEarly + "',"
          + "'" + ManagerScript.CondtionTypeVariableInContainer + "',"
          + "'" + DefeatableTimeWindow + "',"
          + "'" + ReactionTime + "',"
          + "'" + testofsql.CURRENT_TRIAL_ID + "',"
                    + "NULL" + ");" ;

        ((testofsql)(GameObject.Find("OVRPlayerController").GetComponent("testofsql"))).ExecuteQuerry(bla);

    }

    void startExp ()
    {
        DataSavingAfterExplosion();
        StartCoroutine(stunForSeconds(2));
        StartCoroutine(vibrateController());
        ((Detonator)(GetComponent("Detonator"))).Explode();
        switchState(yellowSphereStates.hidden);
    }

    IEnumerator vibrateController ()
    {
        Debug.Log("vibrate controller");
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
        prevState = state;
        state = GamePad.GetState(playerIndex);

        // Set vibration according to triggers
        //GamePad.SetVibration (playerIndex, state.Triggers.Left, state.Triggers.Right);
        GamePad.SetVibration(0, 1.0f, 1.0f);
        yield return new WaitForSeconds(0.2f);
        GamePad.SetVibration(0, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.01f);
        GamePad.SetVibration(0, 1.0f, 1.0f);
        yield return new WaitForSeconds(0.2f);
        GamePad.SetVibration(0, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.01f);
        GamePad.SetVibration(0, 1.0f, 1.0f);
        yield return new WaitForSeconds(0.8f);
        GamePad.SetVibration(0, 0.0f, 0.0f);
    }

    IEnumerator stunForSeconds ( int sec )
    {

        xcontroller.SetMoveScaleMultiplier(0.0f);
        yield return new WaitForSeconds(sec);
        moveScale = moveScale * 0.5f;
        xcontroller.SetMoveScaleMultiplier(moveScale);
        float temp = 0.0f;
        xcontroller.GetMoveScaleMultiplier(ref temp);
    }

    public void NewTrial ()
    {
        switchState(yellowSphereStates.start);
    }

    void move ()
    {
        v = cameraTransform.position;
        rayDirection = cameraTransform.TransformDirection(Vector3.forward);
        v.x = v.x + rayDirection.x * distanceToGoal + Mathf.Sin(Time.time) * 2;
        v.z = v.z + rayDirection.z * distanceToGoal + Mathf.Sin(Time.time) * 2;
        v.y = 7 + Mathf.Sin(Time.time) * 2;
        transform.position = Vector3.MoveTowards(transform.position, v, (transformationSpeed * Time.deltaTime));
        transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
    }

    void switchState ( yellowSphereStates newState )
    {
        displaytext.GetComponent<TextMesh>().text = "";
        Debug.Log(newState);
        switch (newState)
        {
            case yellowSphereStates.defeatable:

                displaytext.GetComponent<TextMesh>().text = "SHOOT";
                s = yellowSphereStates.defeatable;
                recordData.recordDataSmallspread("Onset", durationOfResponsePeriod.ToString());
                Invoke("NotDefeatedInTime", durationOfResponsePeriod);
                break;
            case yellowSphereStates.hidden:
                s = yellowSphereStates.hidden;
                CancelInvoke("NotDefeatedInTime"); // if 
                GenerateTimeWindowForResponce(); // we randomize the ball parapeters lol 
                reset();
                break;
            case yellowSphereStates.moving:
                // here we get a rondom value for the jidder of the onset
                GenerateTimeOnsetOfDefeatTime();
                MoveAndShow();
                s = yellowSphereStates.moving;
                Invoke("StressorDefeatable", onsetOfDefeatAtTime); // after some time we can defeat the stressor

                break;
            case yellowSphereStates.notDefeatedInTime:
                Pause.ChangeNumberOfYellowMissed();
                Invoke("startExp", timeTillExp); // this activates the data saving
                s = yellowSphereStates.notDefeatedInTime;
                if (ManagerScript.CondtionTypeVariableInContainer == "Easy")
                {
                    missedEasyBalls++;
                }
                else if (ManagerScript.CondtionTypeVariableInContainer == "Hard")
                {
                    missedHardBalls++;
                }


                break;
            case yellowSphereStates.defeatedInTime:
                s = yellowSphereStates.defeatedInTime;
                DataSavingAfterDefeate();
                switchState(yellowSphereStates.hidden);
                break;

            case yellowSphereStates.start: // if the stressor should spawn, we set it to the start state
                s = yellowSphereStates.start;
                moveScale = 3.0f;
                xcontroller.SetMoveScaleMultiplier(3.0f);
                switchState(yellowSphereStates.hidden);
                break;
            case yellowSphereStates.end: // if we want the stressor to stop, we set it to the end state
                s = yellowSphereStates.end;
                renderer.enabled = false;
                break;
        }
    }

    // this schould happen every time we switch the blocks
    public void ResetBallsCounterForDynamicDifficulty ()
    {
        missedHardBalls = 0;
        missedEasyBalls = 0;
        catchedHardBalls = 0;
        catchedEasyBalls = 0;

    }

    public static float GetSpeedMoveScale ()
    {
        return moveScale;
    }

    public yellowSphereStates GetYellowState ()
    {
        return s;
    }


    public float DefeatableTimeWindow { get; set; }
}


