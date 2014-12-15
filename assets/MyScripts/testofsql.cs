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
		public class testofsql : MonoBehaviour
		{
		
				public static testofsql Instance = null;
				public bool DebugMode = false;
				/// <summary>
				/// Table name and DB actual file location
				/// </summary>

				// feel free to change where the DBs are stored
				// this file will show up in the Unity inspector after a few seconds of running it the first time
	
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
				void Awake ()
				{
						Instance = this;

				}
		
				/// <summary>
				/// Basic initialization of SQLite
				/// </summary>
				public void SQLiteInit ()
				{


					string SQL_DB_LOCATION = "URI=file:"
					+ ManagerScript.trialFolder 
					+ "blablabla" + ".db";

						Debug.Log ("SQLiter - Opening SQLite Connection at " + SQL_DB_LOCATION);
						mConnection = new SqliteConnection (SQL_DB_LOCATION);
						mCommand = mConnection.CreateCommand ();
						mConnection.Open ();
			
						// WAL = write ahead logging, very huge speed increase
						mCommand.CommandText = "PRAGMA journal_mode = WAL;";
						mCommand.ExecuteNonQuery ();
			
						// journal mode = look it up on google, I don't remember
						mCommand.CommandText = "PRAGMA journal_mode";
						mReader = mCommand.ExecuteReader ();
			
			
						// more speed increases
						mCommand.CommandText = "PRAGMA synchronous = OFF";
						mCommand.ExecuteNonQuery ();
			
						// and some more
						mCommand.CommandText = "PRAGMA synchronous";
						mReader = mCommand.ExecuteReader ();

			
						mSQLString = 	"CREATE TABLE \"Session\" ( "
										+ "  \"id\" INTEGER PRIMARY KEY AUTOINCREMENT, "
										+ "  \"Subject_ID\" TEXT NOT NULL, "
										+ "  \"DataTime\" DT DATETIME(6) NOT NULL, "
										+ "  \"SessionID\" INTEGER NOT NULL, "
										+ "  \"Gender\" TEXT , "
										+ "  \"Age\" INTEGER, "
										+ "  \"Gamer\" BOOLEAN, "
										+ "  \"VrExperience\" BOOLEAN, "
										+ "  \"finished\" BOOLEAN "
										+ " ); ";
		
						mCommand.CommandText = mSQLString;
						mCommand.ExecuteNonQuery ();


						mSQLString = 	" CREATE TABLE \"Blocklist\" ( "
										+" \"id\" INTEGER PRIMARY KEY AUTOINCREMENT, "
										+" \"Type\" TEXT NOT NULL, "
										+" \"Done\" BOOLEAN NOT NULL, "
										+" \"session\" INTEGER NOT NULL REFERENCES \"Session\" (\"id\") "
										+" ); " ;
						Debug.Log (mSQLString);
						mCommand.CommandText = mSQLString;
						mCommand.ExecuteNonQuery ();

						mSQLString = 	" CREATE INDEX \"idx_blocklist__session\" ON \"Blocklist\" (\"session\"); " ;

						mCommand.CommandText = mSQLString;
						mCommand.ExecuteNonQuery ();
						
						mSQLString = 	" CREATE TABLE \"Pause\" ( "
											+ "  \"id\" INTEGER PRIMARY KEY AUTOINCREMENT, "
											+ "  \"PauseStartTime\" DT DATETIME(6) NOT NULL, "
											+ "  \"PauseEndTime\" DT DATETIME(6) NOT NULL, "
											+ "  \"blocklist\" INTEGER NOT NULL REFERENCES \"Blocklist\" (\"id\"), "
											+ "  \"session\" INTEGER NOT NULL REFERENCES \"Session\" (\"id\") "
											+ " ); ";
						
						mCommand.CommandText = mSQLString;
						mCommand.ExecuteNonQuery ();

						mSQLString = 	" CREATE INDEX \"idx_pause__blocklist\" ON \"Pause\" (\"blocklist\"); " ; 
						
						mCommand.CommandText = mSQLString;
						mCommand.ExecuteNonQuery ();

						mSQLString = 	" CREATE INDEX \"idx_pause__session\" ON \"Pause\" (\"session\"); " ; 
						
						mCommand.CommandText = mSQLString;
						mCommand.ExecuteNonQuery ();

						mSQLString = 	" CREATE TABLE \"Trial\" ( "
										+"  \"id\" INTEGER PRIMARY KEY AUTOINCREMENT,  "
										+"  \"TrialNumber\" INTEGER NOT NULL,"
										+"  \"Type\" TEXT NOT NULL,"
										+" \"StartTime\" DT DATETIME(6) NOT NULL,"
									//	+"  \"TrialStartTimeInSec\" REAL NOT NULL,"
										+"  \"TrialEndTimeInSec\" REAL,"
										+"  \"TrialPointingStartInSec\" REAL,"
										+"  \"Successfull\" BOOLEAN DEFAULT 0,"
										+"  \"AbsoluteError\" REAL,"
										+"  \"Error\" REAL,"
										+"  \"NumberOfYellowSperesSpawned\" INTEGER,"
										+"  \"NumberOfYellowSperesDefeated\" INTEGER,"
										+"  \"NumberOfYellowSperesMissed\" INTEGER,"
										+"  \"Block\" INTEGER  REFERENCES \"Blocklist\" (\"id\"),"
										+"  \"session\" INTEGER  REFERENCES \"Session\" (\"id\")"
										+" ); " ;
						
						mCommand.CommandText = mSQLString;
						mCommand.ExecuteNonQuery ();


						mSQLString = 	" CREATE INDEX \"idx_trial__block\" ON \"Trial\" (\"Block\"); " ; 
						
						mCommand.CommandText = mSQLString;
						mCommand.ExecuteNonQuery ();
						
						mSQLString = 	" CREATE INDEX \"idx_trial__session\" ON \"Trial\" (\"session\"); " ; 
						
						mCommand.CommandText = mSQLString;
						mCommand.ExecuteNonQuery ();


						mConnection.Close ();



				}



				public  void StartSavingSQL ()
				{
								

					LoomManager.Loom.QueueOnMainThread (() =>
					                                    {
								mConnection.Open ();

								mSQLString = 	" INSERT INTO Session (Subject_ID, DataTime, SessionID) VALUES (" 
												+ "'" + ManagerScript.chiffre+"','"
												+ System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;")+"','"
												+ ManagerScript.session+"'" + ");" ;
								mCommand.CommandText = mSQLString;
								mCommand.ExecuteNonQuery ();
								mConnection.Close ();

					                                        
								});                        
				}

					public void SaveTrialListtoDatabaseSQL ()
					{
						LoomManager.Loom.QueueOnMainThread (() =>
						                                    {
							mConnection.Open ();

							for (int i=0; i < ManagerScript.trialList.Count-1; i++) {
							


								mSQLString = 	" INSERT INTO Blocklist (Type, Done, session) VALUES (" 
												+ "'" + ManagerScript.trialList [i].CondtionTypeVariableInContainer +"','"
												+ "0" +"','"
												+ ManagerScript.session+"'" + ");" ;
												mCommand.CommandText = mSQLString;
												mCommand.ExecuteNonQuery ();
									
							}
							mConnection.Close ();




						});
						
					}	



				public void StartNewTriallistItemSQL ()
				{
						LoomManager.Loom.QueueOnMainThread (() =>
						{
					
						});
				
				}
			
				public void StartNewTrialSQL ()
				{
						LoomManager.Loom.QueueOnMainThread (() =>
						{
					
				mConnection.Open ();

				int temp = ManagerScript.realTrialNumber;
				mSQLString = 	" INSERT INTO Trial (TrialNumber, Type, StartTime) VALUES (" 
								+ "'" + ManagerScript.realTrialNumber +"','"
								+ ManagerScript.trialList[temp].CondtionTypeVariableInContainer + "','"
								+ System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;")+"'" + ");" ;
				Debug.Log(mSQLString);
				mCommand.CommandText = mSQLString;
				mCommand.ExecuteNonQuery ();
				mConnection.Close ();




						});
				}
			
				public void BlueBallRespawnSQL ()
				{
						LoomManager.Loom.QueueOnMainThread (() =>
						{
					
						});
				
				}
			
				public void YellowSphereSpawnSQL ()
				{
						LoomManager.Loom.QueueOnMainThread (() =>
						{
					
						});
				
				}
			
				public void YellowSphereRespawnSQL ()
				{
						LoomManager.Loom.QueueOnMainThread (() =>
						{
					
						});
				}
			
				public void TrialRestartSQL ()
				{
						LoomManager.Loom.QueueOnMainThread (() =>
						{
					
						});
				
				}
			
				public void PauseSQL ()
				{
						LoomManager.Loom.QueueOnMainThread (() =>
						{
					
						});
				
				}
			
				public void FinishedTrialSQL (int abc)
				{
						LoomManager.Loom.QueueOnMainThread (() =>
						{
				mConnection.Open ();

				mSQLString = 	" UPDATE `Trial` SET `Successfull`=1 WHERE _rowid_=" + abc + ";" ;
				Debug.Log(mSQLString);
				Debug.Log(mSQLString);
				mCommand.CommandText = mSQLString;
				mCommand.ExecuteNonQuery ();
				mConnection.Close ();	

						});
				
				}
			

			
		}
			
}
