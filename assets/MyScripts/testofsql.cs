// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : razial
// Created          : 01-07-2015
//
// Last Modified By : razial
// Last Modified On : 01-07-2015
// ***********************************************************************
// <copyright file="testofsql.cs" company="INLUSIO">
//     Copyright (c) INLUSIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Mono.Data.SqliteClient;
using System.Data;
using System.Threading;
using UnityEngine;

/// <summary>
/// Class testofsql.
/// </summary>
public class testofsql : MonoBehaviour
{
    /// <summary>
    /// The instance
    /// </summary>
    public static testofsql Instance = null;
    /// <summary>
    /// The m connection
    /// </summary>
    private static IDbConnection mConnection = null;
    /// <summary>
    /// The m command
    /// </summary>
    private static IDbCommand mCommand = null;
    /// <summary>
    /// The m reader
    /// </summary>
    private static IDataReader mReader = null;
    /// <summary>
    /// The m SQL string
    /// </summary>
    public static string mSQLString;
    /// <summary>
    /// The m create new table
    /// </summary>
    public bool mCreateNewTable = false;
    // the id of the currentTriallistEnty is the Last_Triallist_id_Putted_In - current TrialNumber lol
    /// <summary>
    /// The last_ triallist_id_ putted_ in
    /// </summary>
    public static int Last_Triallist_id_Putted_In;
    /// <summary>
    /// The subjec t_ identifier
    /// </summary>
    public static int SUBJECT_ID;
    /// <summary>
    /// The sessio n_ identifier
    /// </summary>
    public static int SESSION_ID;
    /// <summary>
    /// The las t_ inserte d_ triallist_ identifier
    /// </summary>
    public static int LAST_INSERTED_Triallist_ID;
    /// <summary>
    /// The firs t_ inserte d_ triallist_ identifier
    /// </summary>
    public static int FIRST_INSERTED_Triallist_ID;
    /// <summary>
    /// The current_ triallist_ identifier
    /// </summary>
    public static int Current_Triallist_ID; // this needs 
    /// <summary>
    /// The curren t_ tria l_ identifier
    /// </summary>
    public static int CURRENT_TRIAL_ID;
    /// <summary>
    /// The comand sum to be execuded in the end of each trial
    /// </summary>
    public static string comandSumToBeExecudedInTheEndOfEachTrial;
    /// <summary>
    /// The easy difficulty level
    /// </summary>
    public static float EasyDifficultyLevel;
    /// <summary>
    /// The hard difficulty level
    /// </summary>
    public static float HardDifficultyLevel;



    /// <summary>
    /// Basic initialization of SQLite
    /// This will be activated by the manager script
    /// </summary>
    public static void SQLiteInit ()
    {
        // the data base is here
        string SQL_DB_LOCATION = @"URI=file:C:\temp\inlusio_data\InlusioDB.sqlite";
        // we connect to the data base
        mConnection = new SqliteConnection(SQL_DB_LOCATION);
        mCommand = mConnection.CreateCommand();
        mConnection.Open();
        //mConnection.Close();
    }

    /// <summary>
    /// Initials the savings to database.
    /// </summary>
    public static void InitialSavingsToDB ()
    {

        // Check if Subject_Number exists, if no, we create him
        if (QueryInt("SELECT EXISTS(SELECT * FROM Subject WHERE Subject_Number='" + ManagerScript.chiffre + "' LIMIT 1);") == 0)
        {

            // initialize the Subject
           
            ExecuteQuerry("INSERT INTO 'Subject'('Subject_id','Subject_Number','EasyDifficultyLevel','HardDifficultyLevel') VALUES (NULL,'" + ManagerScript.chiffre + "','" + SpawnLookRed.EasyDelay.ToString() + "','" + SpawnLookRed.HardDealy.ToString() + "');");
            // Here we get his number
            // alternative code would be
            SUBJECT_ID = QueryInt("SELECT Subject_id FROM Subject WHERE SUbject_Number = '" + ManagerScript.chiffre + "'");



        }
        // If he exists, lets grab his Number
        else
        {
            SUBJECT_ID = QueryInt("SELECT Subject_id FROM Subject WHERE SUbject_Number = '" + ManagerScript.chiffre + "'");
            EasyDifficultyLevel = QueryFloat("SELECT EasyDifficultyLevel FROM Subject WHERE SUbject_Number = '" + ManagerScript.chiffre + "'");
            HardDifficultyLevel = QueryFloat("SELECT HardDifficultyLevel FROM Subject WHERE SUbject_Number = '" + ManagerScript.chiffre + "'");
            SpawnLookRed.SetDinamicDifficultyFromLastSession(EasyDifficultyLevel, HardDifficultyLevel);

        }



        // initialize the session


        // check if session exists
        if (QueryInt("SELECT EXISTS(SELECT * FROM Session WHERE Subject_id='" + SUBJECT_ID + "' AND SessionNumber='" + ManagerScript.session + "'  LIMIT 1);") == 0)
        {

            ExecuteQuerry(" INSERT INTO Session (Subject_ID, Timestamp, SessionNumber) VALUES ("
                + "'" + SUBJECT_ID + "','"
                + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;") + "','"
                + ManagerScript.session + "'" + ");");
            SESSION_ID = QueryInt("SELECT last_insert_rowid()");

            // let us check if the there is a previos session from this subject, that is finished, and grab the dynamic difficulty values, so we can reuse them

            // TODO stuff 

        } else
        {


            SESSION_ID = QueryInt("SELECT Session_id FROM Session WHERE Subject_id = '" + SUBJECT_ID + "' AND SessionNumber = '" + ManagerScript.session + "'");

            // if it exists, check if it is finished
            //if finished, shit
            //if exists and not finished, hmmm ... in theory, count the remaining trials and start the generation of a new
            //trial list
            // currentlly lets just start over , also this is not a niec sollution, we need to redo it ... 

            // best case, the session is created, lets get the 

        }

        // trial list
        string SqlComands = "";

        for (int i=0; i < ManagerScript.trialList.Count - 1; i++)
        {
            SqlComands = SqlComands + "INSERT INTO Trialist (Session_id,Type) VALUES ("
                + "'" + SESSION_ID + "','"
                + ManagerScript.trialList [i].CondtionTypeVariableInContainer + "');";
        }
        ExecuteBigQuerry(SqlComands);

        LAST_INSERTED_Triallist_ID = QueryInt("SELECT last_insert_rowid()");
        FIRST_INSERTED_Triallist_ID = LAST_INSERTED_Triallist_ID - ManagerScript.trialList.Count;

    }

    /// <summary>
    /// Executes the big querry.
    /// </summary>
    /// <param name="sqlcomands">The sqlcomands.</param>
    public static void ExecuteBigQuerry (string sqlcomands)
    {
        string mSQLString2 = "BEGIN; " + sqlcomands + " COMMIT;";
        mCommand.CommandText = mSQLString2;
        mConnection.Open();
        //   Debug.Log(mSQLString2);
        mCommand.ExecuteNonQuery();
        //mConnection.Close();
    }

    /// <summary>
    /// Executes the querry.
    /// </summary>
    /// <param name="sqlcomand">The sqlcomand.</param>
    public static void ExecuteQuerry (string sqlcomand)
    {
        string mSQLString2 = sqlcomand;
        mCommand.CommandText = mSQLString2;
        mConnection.Open();
        Debug.Log(mSQLString2);
        mCommand.ExecuteNonQuery();
        //mConnection.Close();
    }

    // yellow spheres and blue spheres do call this function to add the proper querry
    // 
    /// <summary>
    /// Sums the incoming querries up.
    /// </summary>
    /// <param name="incomingString">The incoming string.</param>
    public void SumTheIncomingQuerriesUp (string incomingString)
    {

        comandSumToBeExecudedInTheEndOfEachTrial = comandSumToBeExecudedInTheEndOfEachTrial + incomingString;
    }

    // at the end of the trial we will write the SumQurry of blue and yellow spheres 
    // still not sure how to update but maybe i can use last insert as an argument ???
    // CHECK IF YOU CAN DO THIS
    /// <summary>
    /// Executes the sum querry.
    /// </summary>
    /// <param name="SumQuerry">The sum querry.</param>
    public void ExecuteSumQuerry (string SumQuerry)
    {
        string mSQLString2 = "BEGIN; " + SumQuerry + " COMMIT;"; // build our new command

        mCommand.CommandText = mSQLString2;// assing the comand
        mConnection.Open();
        mCommand.ExecuteNonQuery();
        //mConnection.Close();
        comandSumToBeExecudedInTheEndOfEachTrial = ""; // reset the sum of commands

    }

    /// <summary>
    /// Queries the int.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns>System.Int32.</returns>
    public static int QueryInt (string command)
    {
        int number = 0;

        mCommand.CommandText = command;
        Debug.Log(command);
        mConnection.Open();
        mReader = mCommand.ExecuteReader();
        if (mReader.Read())
            number = mReader.GetInt32(0);
        else
            Debug.Log("QueryInt - nothing to read...");


        mReader.Close();
        // mConnection.Close();
        return number;
    }

    /// <summary>
    /// Queries the float.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns>System.Single.</returns>
    public static float QueryFloat (string command)
    {
        float number = 0;

        mCommand.CommandText = command;
        mConnection.Open();
        mReader = mCommand.ExecuteReader();
        if (mReader.Read())
            number = mReader.GetFloat(0);
        else
            Debug.Log("QueryInt - nothing to read...");
        mReader.Close();
        //  mConnection.Close();
        return number;
    }

    // here we will have a function for creating trials. each time we create a trial we get its trial id and set the triallist id

    /// <summary>
    /// Creates the trial.
    /// </summary>
    /// <param name="StartTimeTrial">The start time trial.</param>
    /// <param name="TrialNumber">The trial number.</param>
    /// <param name="RealTrialNumber">The real trial number.</param>
    /// <param name="Type">The type.</param>
    public static void CreateTrial (string StartTimeTrial, string TrialNumber, string RealTrialNumber, string Type)
    {
        Current_Triallist_ID = FIRST_INSERTED_Triallist_ID + ManagerScript.realTrialNumber;
        ExecuteQuerry(" INSERT INTO Trial (Session_id,StartTimeTrial,Triallist_id,TrialNumber,RealTrialNumber,Type) VALUES ( "
            + "'" + SESSION_ID + "','"
            + StartTimeTrial + "','"
            + Current_Triallist_ID + "','" 
            + TrialNumber + "','"
            + RealTrialNumber + "','"
            + Type + "');");

        // after we create a trial, we need to knew about it
        CURRENT_TRIAL_ID = QueryInt("SELECT last_insert_rowid()");
    }

    /// <summary>
    /// Updates the trial.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="AbsoluteErrorAngle">The absolute error angle.</param>
    /// <param name="ErrorAngle">The error angle.</param>
    /// <param name="OverShoot">The over shoot.</param>
    /// <param name="StartTimePointing">The start time pointing.</param>
    /// <param name="EndTimePoining">The end time poining.</param>
    /// <param name="DurationOfPointing">The duration of pointing.</param>
    /// <param name="DurationOfWalking">The duration of walking.</param>
    /// <param name="EndTimeTrial">The end time trial.</param>
    public void UpdateTrial (string argument, string AbsoluteErrorAngle, string ErrorAngle, string OverShoot, string StartTimePointing, string EndTimePoining, string DurationOfPointing, string DurationOfWalking, string EndTimeTrial)
    {
        if (argument == "abort")
        {

            // What we have so far is Session_id,StartTimeTrial,Triallist_id,TrialNumber,RealTrialNumber,Successfull,Type :  What is missing
            mSQLString = " UPDATE 'Trial' SET 'Successfull'=0 , 'AbsoluteErrorAngle'=" + AbsoluteErrorAngle + ", 'OverShoot'= " + OverShoot + " 'ErrorAngle'= " + ErrorAngle + ", 'StartTimePointing'= " + StartTimePointing + ", 'DurationOfPointing' " + DurationOfPointing + ", 'DurationOfWalking'= " + DurationOfPointing + ", 'EndTimeTrial'= " + EndTimeTrial + "  WHERE _rowid_=" + CURRENT_TRIAL_ID + ";";


        }

        if (argument == "success")
        {

            mSQLString = " UPDATE 'Trial' SET 'Successfull'=1 , 'AbsoluteErrorAngle'=" + AbsoluteErrorAngle + ", 'OverShoot'= " + OverShoot + " 'ErrorAngle'= " + ErrorAngle + ", 'StartTimePointing'= " + StartTimePointing + ", 'DurationOfPointing' " + DurationOfPointing + ", 'DurationOfWalking'= " + DurationOfPointing + ", 'EndTimeTrial'= " + EndTimeTrial + "  WHERE _rowid_=" + CURRENT_TRIAL_ID + ";";

        }

        mCommand.CommandText = mSQLString;
        mConnection.Open();

        mCommand.ExecuteNonQuery();
        //mConnection.Close();


    }

    /// <summary>
    /// Creates the stressor.
    /// </summary>
    /// <param name="Stressors_id">The stressors_id.</param>
    /// <param name="SpawnTime">The spawn time.</param>
    /// <param name="StartDefeatTime">The start defeat time.</param>
    /// <param name="HowLongDefeatable">The how long defeatable.</param>
    /// <param name="DefeatedAtTime">The defeated at time.</param>
    /// <param name="Defeated">The defeated.</param>
    /// <param name="RotationSpeed">The rotation speed.</param>
    /// <param name="ButtonToEarlyPushed">The button to early pushed.</param>
    /// <param name="Type">The type.</param>
    /// <param name="DefeatableTimeWindow">The defeatable time window.</param>
    /// <param name="ReactionTime">The reaction time.</param>
    /// <param name="Trial_id">The trial_id.</param>
    /// <param name="ExplosionTime">The explosion time.</param>
    public void CreateStressor (string Stressors_id, string SpawnTime, string StartDefeatTime, string HowLongDefeatable, string DefeatedAtTime, string Defeated, string RotationSpeed, string ButtonToEarlyPushed, string Type, string DefeatableTimeWindow, string ReactionTime, string Trial_id, string ExplosionTime)
    {
        string bla = 
                   " INSERT INTO 'Stressors' ('Stressors_id','SpawnTime','StartDefeatTime','HowLongDefeatable','DefeatedAtTime','Defeated','RotationSpeed','ButtonToEarlyPushed','Type','DefeatableTimeWindow','ReactionTime','Trial_id','ExplosionTime') VALUES"
            + "(" + "NULL" + ","
            + "'" + SpawnTime + "',"
            + "'" + StartDefeatTime + "',"
            + "'" + HowLongDefeatable + "',"
            + "'" + DefeatedAtTime + "',"
            + "'" + Defeated + "',"
            + "'" + RotationSpeed + "',"
            + "'" + ButtonToEarlyPushed + "',"
            + "'" + Type + "',"
            + "'" + DefeatableTimeWindow + "',"
            + "'" + ReactionTime + "',"
            + "'" + Trial_id + "',"
            + "'" + ExplosionTime + "');";
        ExecuteQuerry(bla);
    }
    /// <summary>
    /// Creates the waypoint.
    /// </summary>
    /// <param name="Waypoints_id">The waypoints_id.</param>
    /// <param name="DegreeOfRespawn">The degree of respawn.</param>
    /// <param name="TimeWhenRespawned">The time when respawned.</param>
    /// <param name="GlobalCoordinats">The global coordinats.</param>
    /// <param name="TransformRotation">The transform rotation.</param>
    /// <param name="NumberInTrial">The number in trial.</param>
    /// <param name="Trial_id">The trial_id.</param>
    /// <param name="reached">The reached.</param>
    /// <param name="TimeWhenReached">The time when reached.</param>
    public void CreateWaypoint (string Waypoints_id, string DegreeOfRespawn, string TimeWhenRespawned, string GlobalCoordinats, string TransformRotation, string NumberInTrial, string Trial_id, string reached, string TimeWhenReached)
    {


        string bla = "INSERT INTO 'Waypoints'('Waypoints_id','DegreeOfRespawn','TimeWhenRespawned','GlobalCoordinats','TransformRotation','NumberInTrial','Trial_id','reached','TimeWhenReached') VALUES"
            + "(" + Waypoints_id + ",'"
            + DegreeOfRespawn + "','"
            + TimeWhenRespawned + "','"
            + GlobalCoordinats + "','"
            + TransformRotation + "','"
            + NumberInTrial + "','"
            + Trial_id + "','"
            + reached + "','"
            + TimeWhenReached + "');";
        ExecuteQuerry(bla);


    }

    /// <summary>
    /// Updates the session.
    /// </summary>
    public void UpdateSession ()
    {
    }

    /// <summary>
    /// Updates the triallist.
    /// </summary>
    public void UpdateTriallist ()
    {
    }

    /// <summary>
    /// Creates the pause.
    /// </summary>
    /// <param name="StartTimePause">The start time pause.</param>
    /// <param name="EndTimePause">The end time pause.</param>
    public static void CreatePause (string StartTimePause, string EndTimePause)
    { 
    
        string mSQLString1 = "INSERT INTO 'Pause'('trial','StartTimePause','EndTimePause') VALUES" +
            "('" + CURRENT_TRIAL_ID + "','" 
            + StartTimePause + "','"     
            + EndTimePause + "')";
        ExecuteQuerry(mSQLString1);
    }

    /// <summary>
    /// Sets the dynamic difficulty.
    /// </summary>
    public static void SetDynamicDifficulty () // each block we should update the difficulty of the subject in the data base lol
    {
        string mSQLString1 = 
            " UPDATE 'Subject' SET 'EasyDifficultyLevel'=" + SpawnLookRed.EasyDelay.ToString() + " WHERE SUbject_Number = '" + ManagerScript.chiffre + "';";
        string mSQLString2 = 
            " UPDATE 'Subject' SET 'HardDifficultyLevel'=" + SpawnLookRed.HardDealy.ToString() + " WHERE SUbject_Number = '" + ManagerScript.chiffre + "';";
        ExecuteQuerry(mSQLString1);
        ExecuteQuerry(mSQLString2);

    }

    /// <summary>
    /// Dynamics the difficulty event.
    /// </summary>
    public void DynamicDifficultyEvent ()
    {
    
    
    }


    /// <summary>
    /// Called when [destroy].
    /// </summary>
    void OnDestroy ()
    {
        SQLiteClose();
    }

    /// <summary>
    /// Called when [application quit].
    /// </summary>
    void OnApplicationQuit ()
    {
        mConnection.Close();
    }

    /// <summary>
    /// Clean up everything for SQLite
    /// </summary>
    private void SQLiteClose ()
    {
        if (mReader != null && !mReader.IsClosed)
            mReader.Close();
        mReader = null;
    
        if (mCommand != null)
            mCommand.Dispose();
        mCommand = null;
    
        if (mConnection != null && mConnection.State != ConnectionState.Closed)
            mConnection.Close();
        mConnection = null;
    }

   


}



//      public string QueryString( string table_name ,string column, string value )
//      {
//          string text = "Not Found";
//          mConnection.Open();
//          mCommand.CommandText = "SELECT " + column + " FROM " + table_name + " WHERE " + column + "='" + value + "'";
//          mReader = mCommand.ExecuteReader();
//          if (mReader.Read())
//              text = mReader.GetString(0);
//          else
//              Debug.Log("QueryString - nothing to read...");
//          mReader.Close();
//          mConnection.Close();
//          return text;
//      }
//      
//      /// <summary>
//      /// Supply the column and the value you're trying to find, and it will use the primary key to query the result
//      /// </summary>
//      /// <param name="column"></param>
//  
//      public short QueryShort(string table_name ,string column, string value)
//      {
//          short sel = -1;
//          mConnection.Open();
//          mCommand.CommandText = "SELECT " + column + " FROM " + table_name + " WHERE " +  + "='" + value + "'";
//          mReader = mCommand.ExecuteReader();
//          if (mReader.Read())
//              sel = mReader.GetInt16(0);
//          else
//              Debug.Log("QueryShort - nothing to read...");
//          mReader.Close();
//          mConnection.Close();
//          return sel;
//      }





