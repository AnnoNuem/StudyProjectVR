using Mono.Data.SqliteClient;
using System.Data;
using System.Threading;
using UnityEngine;

public class testofsql : MonoBehaviour
{
    public static testofsql Instance = null;
    private static IDbConnection mConnection = null;
    private static IDbCommand mCommand = null;
    private static IDataReader mReader = null;
    public static string mSQLString;
    public bool mCreateNewTable = false;
    // the id of the currentTriallistEnty is the Last_Triallist_id_Putted_In - current TrialNumber lol
    public static int Last_Triallist_id_Putted_In;
    public static int SUBJECT_ID;
    public static int SESSION_ID;
    public static int LAST_INSERTED_Triallist_ID;
    public static int FIRST_INSERTED_Triallist_ID;
    public static int Current_Triallist_ID; // this needs 
    public static int CURRENT_TRIAL_ID;
    public static string comandSumToBeExecudedInTheEndOfEachTrial;
    public static float EasyDifficultyLevel;
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

    public static void ExecuteBigQuerry (string sqlcomands)
    {
        string mSQLString2 = "BEGIN; " + sqlcomands + " COMMIT;";
        mCommand.CommandText = mSQLString2;
        mConnection.Open();
        //   Debug.Log(mSQLString2);
        mCommand.ExecuteNonQuery();
        //mConnection.Close();
    }

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
    public void SumTheIncomingQuerriesUp (string incomingString)
    {

        comandSumToBeExecudedInTheEndOfEachTrial = comandSumToBeExecudedInTheEndOfEachTrial + incomingString;
    }

    // at the end of the trial we will write the SumQurry of blue and yellow spheres 
    // still not sure how to update but maybe i can use last insert as an argument ???
    // CHECK IF YOU CAN DO THIS
    public void ExecuteSumQuerry (string SumQuerry)
    {
        string mSQLString2 = "BEGIN; " + SumQuerry + " COMMIT;"; // build our new command

        mCommand.CommandText = mSQLString2;// assing the comand
        mConnection.Open();
        mCommand.ExecuteNonQuery();
        //mConnection.Close();
        comandSumToBeExecudedInTheEndOfEachTrial = ""; // reset the sum of commands

    }

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

    public void UpdateSession ()
    {
    }

    public void UpdateTriallist ()
    {
    }

    public static void CreatePause (string StartTimePause, string EndTimePause)
    { 
    
        string mSQLString1 = "INSERT INTO 'Pause'('trial','StartTimePause','EndTimePause') VALUES" +
            "('" + CURRENT_TRIAL_ID + "','" 
            + StartTimePause + "','"     
            + EndTimePause + "')";
        ExecuteQuerry(mSQLString1);
    }

    public static void SetDynamicDifficulty () // each block we should update the difficulty of the subject in the data base lol
    {
        string mSQLString1 = 
            " UPDATE 'Subject' SET 'EasyDifficultyLevel'=" + SpawnLookRed.EasyDelay.ToString() + " WHERE SUbject_Number = '" + ManagerScript.chiffre + "';";
        string mSQLString2 = 
            " UPDATE 'Subject' SET 'HardDifficultyLevel'=" + SpawnLookRed.HardDealy.ToString() + " WHERE SUbject_Number = '" + ManagerScript.chiffre + "';";
        ExecuteQuerry(mSQLString1);
        ExecuteQuerry(mSQLString2);

    }

    public void DynamicDifficultyEvent ()
    {
    
    
    }


    void OnDestroy ()
    {
        SQLiteClose();
    }

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





