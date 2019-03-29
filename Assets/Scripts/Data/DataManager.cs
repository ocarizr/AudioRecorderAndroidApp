using System;
using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Singleton;

namespace Assets.Scripts.Data
{
    public class DataManager : Singleton<DataManager>
    {
        public List<AudioClipData> Items;
        public static string DbPath;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            DbPath = Path.Combine(Application.persistentDataPath, "DataStorage.sqlite");
            Items = new List<AudioClipData>();

            if (!File.Exists(DbPath)) CreateNewDatabase();
            else PopulateItems();
        }

        private static void CreateNewDatabase()
        {
            SqliteConnection.CreateFile(DbPath);
            using (var connection = new SqliteConnection($"Data Source= {DbPath}"))
            {
                string query = "CREATE TABLE AudioDB (Id INT, ClipData TEXT)";
                using (var cmd = new SqliteCommand(query, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                } 
            }
        }

        private void PopulateItems()
        {
            using (var connection = new SqliteConnection($"Data Source= {DbPath}"))
            {
                const string query = "SELECT * FROM AudioDB";
                using (var cmd = new SqliteCommand(query, connection))
                {
                    connection.Open();
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = JsonUtility.FromJson<AudioClipData>(reader.GetValue(1) as string); // alterar para BinaryFomatter
                            Items.Add(item);
                            ApplicationManager.Instance.ItemList.AddFromDatabase(item);
                        }
                    }
                }
            }
        }

        public void SaveRecordedAudio(AudioClipData clipData)
        {
            using (var connection = new SqliteConnection($"Data Source= {DbPath}"))
            {
                string data = JsonUtility.ToJson(clipData); // alterar para BinaryFormatter
                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO AudioDB(Id, ClipData) ");
                query.Append("VALUES (");
                query.Append(Items.Count);
                query.Append(", ");
                query.Append(data);
                query.Append(")");

                using (var cmd = new SqliteCommand(query.ToString(), connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Items.Add(clipData);
                }
            }
        }
    }
}
