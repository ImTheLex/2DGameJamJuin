using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Shared;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Saves.Runtime
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance;
        
        public UnityEvent m_onRegisterPlayerEvent;
        public UnityEvent m_onDataFoundEvent;
        public UnityEvent m_onDataSet;
        public UnityEvent m_onSaving;
        public List<PlayerSaveData> m_PlayerSaveData = new();

        public PlayerSaveData m_selectedSaveData;
        public string m_saveDate;


        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllPlayersOnStart();
        }

        public void LoadAllPlayersOnStart()
        {
            m_PlayerSaveData.Clear(); // Nettoyage si la scène se recharge
            string rootPath = Application.persistentDataPath;
            string[] directories = Directory.GetDirectories(rootPath);
            List<PlayerSaveData> loadedDataList = new();

            foreach (var dir in directories)
            {
                string playerFile = Path.Combine(dir, "player.json");
                if (File.Exists(playerFile))
                {
                    string json = File.ReadAllText(playerFile);
                    PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);
                    loadedDataList.Add(data);
                }
            }

            loadedDataList.Sort((a, b) =>
            {
                int scoreCompare = a.m_score.CompareTo(b.m_score);
                if (scoreCompare != 0) return scoreCompare;

                DateTime dateA, dateB;
                if (!DateTime.TryParse(a.m_saveDate, out dateA)) dateA = DateTime.MinValue;
                if (!DateTime.TryParse(b.m_saveDate, out dateB)) dateB = DateTime.MinValue;

                return dateA.CompareTo(dateB);
            });

            // Supprime les plus mauvaises si trop
            while (loadedDataList.Count > _maxSaveCount)
            {
                var toRemove = loadedDataList[0];
                string folder = $"{rootPath}/{toRemove.m_playerUUID}-{toRemove.m_playerName}";
                if (Directory.Exists(folder))
                {
                    Directory.Delete(folder, true);
                    Debug.Log($"[SaveSystem] Sauvegarde supprimée (score bas) : {toRemove.m_playerName}");
                }
                loadedDataList.RemoveAt(0);
            }

            m_PlayerSaveData = loadedDataList;
            m_onDataFoundEvent.Invoke();
            Debug.Log($"[SaveSystem] {m_PlayerSaveData.Count} sauvegardes de joueur chargées.");
        }


        public void RegisterPlayerData(TMP_InputField inputField)
        {
            var name = inputField.text;
           
            
            // Vérifie si un joueur avec ce nom existe déjà
            if (m_PlayerSaveData.Exists(p => p.m_playerUUID == _playerUUID))
            {
                Debug.LogWarning($"You have already saved your game");
                return;
            }
            var playerData = new PlayerSaveData();
            _playerUUID = Guid.NewGuid().ToString();


            playerData.m_saveDate = DateTime.Now.ToString("O");
            playerData.m_playerName = name;
            playerData.m_playerUUID = _playerUUID;
            playerData.m_score = _scoreConfig.m_scoreValue;
            playerData.m_waveCount = _scoreConfig.m_currentWave;
            m_PlayerSaveData.Add(playerData);
            m_selectedSaveData = playerData;
            Save();
            m_onRegisterPlayerEvent.Invoke();
            
        }
        
     
        
        
        public void SavePlayerData(PlayerSaveData data)
        {
            _playerUUID = data.m_playerUUID;
            _playerName = data.m_playerName;
            
            
            string folder = Application.persistentDataPath + "/" + data.m_playerUUID + "-" + data.m_playerName;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(folder + "/player.json", json);
        }

        public void Save()
        {
            Debug.Log("Saving");
            m_onSaving.Invoke();
            SavePlayerData(m_selectedSaveData);
        }

        public PlayerSaveData LoadPlayerData()
        {
            string folder = Application.persistentDataPath + "/" + _playerUUID + "-" + _playerName;
            string path = folder + "/player.json";

            if (!File.Exists(path))
            {
                Debug.LogWarning($"Aucune sauvegarde trouvée pour le joueur {_playerUUID}");
                return null;
            }

            string json = File.ReadAllText(path);
            PlayerSaveData loadedData = JsonUtility.FromJson<PlayerSaveData>(json);
            return loadedData;
        }

        public void SetData(PlayerSaveData data)
        {
            m_selectedSaveData = data;
            m_onDataSet.Invoke();
        }
        public PlayerSaveData LoadSelectedData()
        {
            return m_selectedSaveData;
        }

        [ContextMenu("ResetData")]
        private void ResetAllData()
        {
            string rootPath = Application.persistentDataPath;
            string[] directories = Directory.GetDirectories(rootPath);

            foreach (string dir in directories)
            {
                Directory.Delete(dir, true);
            }

            m_PlayerSaveData.Clear();
            m_selectedSaveData = null;
            m_onDataFoundEvent.Invoke();
            Debug.Log("[SaveSystem] Toutes les sauvegardes ont été supprimées.");
        }

        private string _playerUUID;
        private string _playerName;
        [SerializeField] private int _maxSaveCount;
        [SerializeField] private ScoreConfig _scoreConfig;
    }
}
