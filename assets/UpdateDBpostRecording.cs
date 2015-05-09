using Mono.Data.SqliteClient;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System;
using System.IO;

public class UpdateDBpostRecording : MonoBehaviour
{

    public static UpdateDBpostRecording Instance = null;

    
    /// <summary>
    /// The mConnection is the IDbConnection Object we will use to connect to the Database 
    /// </summary>
    private static IDbConnection mConnection = null;
    
    /// <summary>
    /// The mCommand is the IDbCommand Object, we will assign querry`s we want to run in the DB,
    /// without returning values
    /// </summary>
    private static IDbCommand mCommand = null;
    
    /// <summary>
    /// The mReader is the IDataReader Object, we will assign querry`s we want to run in the DB,
    /// with returning values
    /// </summary>
    private static IDataReader mReader = null;
    
    /// <summary>
    /// The mSQL string is a string we use frequently to put remporaly the querries to be execuded 
    /// </summary>
    public static string mSQLString;

    static string SQL_DB_LOCATION = @"URI=file:C:\Users\lab-admin\Downloads\InlusioDB_260225_new_block_fix_lukas.sqlite";

    int NumberOfTrialsWithStartTimePointingNotZero;

    void Awake ()
    {
        Instance = this;
    }

    public static void SQLiteInit ()
    {

        
        // we connect to the data base 
        mConnection = new SqliteConnection(SQL_DB_LOCATION);
        mCommand = mConnection.CreateCommand();
        mConnection.Open();
        ExecuteQuerry("PRAGMA page_size = " + "4096" + ";");
        ExecuteQuerry("PRAGMA synchronous = " + "0" + ";");
        mConnection.Close();
        
    }

    public string QueryString (string bla)
    {
        string text = "Not Found";
        mConnection.Open();
        mCommand.CommandText = bla;
        mReader = mCommand.ExecuteReader();
        if (mReader.Read())
            text = mReader.GetString(0);
        else
            Debug.Log("QueryString - nothing to read...");
        mReader.Close();
        mConnection.Close();
        return text;
    }



    // Use this for initialization
    void Start ()
    {
        SQLiteInit();

        NumberOfTrialsWithStartTimePointingNotZero = Convert.ToInt32(QueryString("SELECT COUNT(*) FROM TRIAL WHERE StartTimePointing IS NOT NULL"));
   
        int[] IDS = new int[NumberOfTrialsWithStartTimePointingNotZero];

        int i = 0;
        mConnection.Open(); 
        mCommand.CommandText = "SELECT * FROM TRIAL WHERE StartTimePointing IS NOT NULL  ; ";
        
        mReader = mCommand.ExecuteReader();

        while (mReader.Read())
        {
            
            
            IDS [i] = Convert.ToInt32(mReader.GetString(0));
            Debug.Log(IDS [i]);
            i++;
        }




        for (int b = 0; b < NumberOfTrialsWithStartTimePointingNotZero; b++)
        {

            ExecuteQuerry("UPDATE Trial SET DurationOfPointing = ((JULIANDAY(EndTimePoining)-JULIANDAY(StartTimePointing))*24*3600) , DurationOfWalking = ((JULIANDAY(StartTimePointing)-JULIANDAY(StartTimeTrial))*24*3600) Where Trial_id = " + IDS [b] + "; ");

        }

        Debug.Log("i am ready bitch");
    }
	
    // Update is called once per frame
    void Update ()
    {
	
    }


    public static int QueryInt (string command)
    {
        int number = 0;
        
        mCommand.CommandText = command;
        // 
        mReader = mCommand.ExecuteReader();
        if (mReader.Read())
            number = mReader.GetInt32(0);
        else
            Debug.Log("QueryInt - nothing to read...");
        mReader.Close();
        return number;
    }


    public static void ExecuteQuerry (string sqlcomand)
    {
        //        Debug.Log(sqlcomand);
        mCommand.CommandText = sqlcomand;
        Debug.Log(sqlcomand);
        mCommand.ExecuteNonQuery();
    }

    private void OnApplicationQuit ()
    {
        mConnection.Close();
        SQLiteClose();
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
