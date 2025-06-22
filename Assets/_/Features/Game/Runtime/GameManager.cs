using System;
using System.Collections;
using System.Collections.Generic;
using Player.Runtime;
using Saves.Runtime;
using Shared;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        [SerializeField]
        private SaveSystem _saveSystem;
        
        void Awake()
        {
            
            //_saveSystem.m_onRegisterPlayerEvent.AddListener(StartGame);
            _saveSystem.m_onDataFoundEvent.AddListener(OnDataFound);
            _saveSystem.m_onDataSet.AddListener(StartGame);
            //_playerSaveDataList = _saveSystem.m_PlayerSaveData;
            DontDestroyOnLoad(gameObject);

        }
        
        private void OnDataFound()
        {
            //m_canPlay = true;
        }
        private void StartGame()
        {
            
                //var pd = _saveSystem.LoadPlayerData();
                var pd = _saveSystem.LoadSelectedData();
                var go = _player;
                var pc = _player.AddComponent<PlayerData>();
                InitializePlayerConfig(pc,pd);
                go.GetComponent<PlayerBehaviour>().InitializePlayer();
                //pc. = _saveSystem.LoadPlayerData()
                go.name = pc.m_playerName;
                go.SetActive(true);
               
        }

        private void InitializePlayerConfig(PlayerData pc,PlayerSaveData pd)
        {
            pc.m_playerName = pd.m_playerName;
            pc.m_playerUUID = pd.m_playerUUID;
            pc.m_timePlayed = pd.m_timePlayed;
        }
        
    }
}
