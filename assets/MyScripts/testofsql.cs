using UnityEngine;
using System.Data;
using Mono.Data.SqliteClient;
using System.IO;
using System.Text;

using UnityEngine;
using System.Collections.Generic;
using Action = System.Action;

using UnityEngine;
using System.Collections;
using URandom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bla
{


public class testofsql : MonoBehaviour {

		public static testofsql Instance = null;
		public bool DebugMode = false;
		/// <summary>
		/// Table name and DB actual file location
		/// </summary>
		private const string SQL_DB_NAME = "TestDB22rrrrrrrr3333323333333333422";

		// feel free to change where the DBs are stored
		// this file will show up in the Unity inspector after a few seconds of running it the first time
		private static readonly string SQL_DB_LOCATION = "URI=file:"
			+ Application.dataPath + Path.DirectorySeparatorChar
				+ "Plugins" + Path.DirectorySeparatorChar
				+ "SQLiter" + Path.DirectorySeparatorChar
				+ "Databases" + Path.DirectorySeparatorChar
				+ SQL_DB_NAME + ".db";
		/// <summary>
		/// DB objects
		/// </summary>
		private IDbConnection mConnection = null;
		private IDbCommand mCommand = null;
		private IDataReader mReader = null;
		private static string mSQLString;
		
		public bool mCreateNewTable = false;

		/// <summary>
		/// Awake will initialize the connection.  
		/// RunAsyncInit is just for show.  You can do the normal SQLiteInit to ensure that it is
		/// initialized during the AWake() phase and everything is ready during the Start() phase
		/// </summary>
		void Awake()
		{
			Debug.Log(SQL_DB_LOCATION);
			Instance = this;
			SQLiteInit();
			Debug.Log("bla");

		}

		/// <summary>
		/// Basic initialization of SQLite
		/// </summary>
		private void SQLiteInit()
		{
			Debug.Log("SQLiter - Opening SQLite Connection at " + SQL_DB_LOCATION);
			mConnection = new SqliteConnection(SQL_DB_LOCATION);
			mCommand = mConnection.CreateCommand();
			mConnection.Open();
			
			// WAL = write ahead logging, very huge speed increase
			mCommand.CommandText = "PRAGMA journal_mode = WAL;";
			mCommand.ExecuteNonQuery();
			
			// journal mode = look it up on google, I don't remember
			mCommand.CommandText = "PRAGMA journal_mode";
			mReader = mCommand.ExecuteReader();

			
			// more speed increases
			mCommand.CommandText = "PRAGMA synchronous = OFF";
			mCommand.ExecuteNonQuery();
			
			// and some more
			mCommand.CommandText = "PRAGMA synchronous";
			mReader = mCommand.ExecuteReader();


//			// here we check if the table you want to use exists or not.  If it doesn't exist we create it.
//			mCommand.CommandText = " SELECT id FROM Session WHERE id = '1' ";
//			mReader = mCommand.ExecuteReader();
//			if (!mReader.Read())
//			{
//				mCreateNewTable = true;
//			}
//			mReader.Close();

			// if we dont have allready the first session, we will create one lol

								// create new - SQLite recommendation is to drop table, not clear it
								mSQLString = " CREATE TABLE \"Session\" ( \"id\" INTEGER PRIMARY KEY AUTOINCREMENT, \"Subject_ID\" TEXT NOT NULL, \"DataTime\" DATETIME NOT NULL, \"SessionID\" INTEGER NOT NULL, \"Gender\" TEXT NOT NULL, \"Age\" INTEGER NOT NULL, \"Gamer\" BOOLEAN NOT NULL, \"VrExperience\" BOOLEAN NOT NULL, \"finished\" BOOLEAN NOT NULL );  CREATE TABLE \"Blocklist\" ( \"id\" INTEGER PRIMARY KEY AUTOINCREMENT, \"Type\" TEXT NOT NULL, \"Done\" BOOLEAN NOT NULL, \"session\" INTEGER NOT NULL REFERENCES \"Session\" (\"id\") );  CREATE INDEX \"idx_blocklist__session\" ON \"Blocklist\" (\"session\");  CREATE TABLE \"Pause\" ( \"id\" INTEGER PRIMARY KEY AUTOINCREMENT, \"PauseStartTime\" REAL NOT NULL, \"PauseEndTime\" REAL NOT NULL, \"blocklist\" INTEGER NOT NULL REFERENCES \"Blocklist\" (\"id\"), \"session\" INTEGER NOT NULL REFERENCES \"Session\" (\"id\") );  CREATE INDEX \"idx_pause__blocklist\" ON \"Pause\" (\"blocklist\");  CREATE INDEX \"idx_pause__session\" ON \"Pause\" (\"session\");  CREATE TABLE \"Trial\" ( \"id\" INTEGER PRIMARY KEY AUTOINCREMENT, \"TrialNumber\" INTEGER UNIQUE NOT NULL, \"Type\" TEXT NOT NULL, \"TrialStartTimeInSec\" REAL NOT NULL, \"TrialEndTimeInSec\" REAL NOT NULL, \"TrialPointingStartInSec\" REAL NOT NULL, \"Successfull\" BOOLEAN DEFAULT 0 NOT NULL, \"AbsoluteError\" REAL NOT NULL, \"Error\" REAL NOT NULL, \"NumberOfYellowSperesSpawned\" INTEGER NOT NULL, \"NumberOfYellowSperesDefeated\" INTEGER NOT NULL, \"NumberOfYellowSperesMissed\" INTEGER NOT NULL, \"Block\" INTEGER NOT NULL REFERENCES \"Blocklist\" (\"id\"), \"session\" INTEGER NOT NULL REFERENCES \"Session\" (\"id\") );  CREATE INDEX \"idx_trial__block\" ON \"Trial\" (\"Block\");  CREATE INDEX \"idx_trial__session\" ON \"Trial\" (\"session\");  CREATE TABLE \"BlueSphere\" ( \"id\" INTEGER PRIMARY KEY AUTOINCREMENT, \"TrialNumber\" INTEGER NOT NULL REFERENCES \"Trial\" (\"id\"), \"DegreeOfRespawn\" REAL NOT NULL, \"TimeTillReach\" REAL NOT NULL, \"Coordinats\" TEXT NOT NULL, \"session\" INTEGER NOT NULL REFERENCES \"Session\" (\"id\") );  CREATE INDEX \"idx_bluesphere__session\" ON \"BlueSphere\" (\"session\");  CREATE INDEX \"idx_bluesphere__trialnumber\" ON \"BlueSphere\" (\"TrialNumber\");  CREATE TABLE \"YelloSphere\" ( \"id\" INTEGER PRIMARY KEY AUTOINCREMENT, \"SpawnTime\" REAL NOT NULL, \"StartDefeatTime\" DATETIME NOT NULL, \"DefeatableTime\" DATETIME NOT NULL, \"DefeatedTIme\" REAL NOT NULL, \"Defeated\" BOOLEAN DEFAULT 0 NOT NULL, \"RotationSpeed\" DATETIME NOT NULL, \"TrialNumber\" INTEGER NOT NULL REFERENCES \"Trial\" (\"id\"), \"ButtonBushTime\" REAL NOT NULL, \"session\" INTEGER NOT NULL REFERENCES \"Session\" (\"id\") );  CREATE INDEX \"idx_yellosphere__session\" ON \"YelloSphere\" (\"session\");  CREATE INDEX \"idx_yellosphere__trialnumber\" ON \"YelloSphere\" (\"TrialNumber\") ";
								mCommand.CommandText = mSQLString;
								mCommand.ExecuteNonQuery ();
						 // so from here we have a perfect database, that we can save to, muhahahahahaha
			mConnection.Close();
		}


		public  void StartSavingSQL() 
		{
			

		//	System.DateTime.Now

			//		Subject_ID	ManagerScript.chiffre
//				            name = name.ToLower();

            // note - this will replace any item that already exists, overwriting them.  
            // normal INSERT without the REPLACE will throw an error if an item already exists
           mSQLString = 	"INSERT OR REPLACE INTO Session VALUES (\""+ ManagerScript.chiffre +"\" , \" "  + System.DateTime.Now + " \" , \" "  + ManagerScript.session + " \" ," + " ," + "," + "," + "," + "," + " )" ;
			            ExecuteNonQuery(mSQLString);

			//            
//
//				Debug.Log("bla");
//
//			Debug.Log("bla");
			mConnection.Close();


		}

		public void StartNewTriallistItemSQL()
		{
			LoomManager.Loom.QueueOnMainThread(() =>
			                                   {
				
			});

		}

		public void StartNewTrialSQL() 
		{
			LoomManager.Loom.QueueOnMainThread(() =>
			                                   {
				
			});
		}

		public void BlueBallRespawnSQL() 
		{
			LoomManager.Loom.QueueOnMainThread(() =>
			                                   {
				
			});

		}

		public void YellowSphereSpawnSQL() 
		{
			LoomManager.Loom.QueueOnMainThread(() =>
			                                   {
				
			});

		}

	

		public void YellowSphereRespawnSQL() 
		{
			LoomManager.Loom.QueueOnMainThread(() =>
			                                   {
				
			});
		}

		public void TrialRestartSQL()
		{
			LoomManager.Loom.QueueOnMainThread(() =>
			                                   {
				
			});

		}

		public void PauseSQL()
		{
			LoomManager.Loom.QueueOnMainThread(() =>
			                                   {
				
			});

		}

		public void FinishedTrialSQL()
		{
			LoomManager.Loom.QueueOnMainThread(() =>
			                                   {
				
			});

		}
		


}

}
			