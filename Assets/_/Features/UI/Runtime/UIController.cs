using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Saves.Runtime;
using Tools;
using UnityEngine;
using UnityEngine.Events;
using GameManager = Game.GameManager;

//using SaveSystem = SaveSystem;

namespace UI.Runtime
{
    public class UIController : MonoBehaviour
    {
        public UnityEvent m_saveEvent;
        [SerializeField] private SaveSystem _saveSystem;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private SavedSlotManager _savedSlotManager;
        private void Awake()
        {
            
            _saveSystem.m_onRegisterPlayerEvent.AddListener(OnRegisterPlayerComplete);
            _saveSystem.m_onDataFoundEvent.AddListener(DisplayData);
        }

        public void Save()
        {
            m_saveEvent.Invoke();
            //_saveSystem.Save();
        }

        private void DisplayData()
        {
          _savedSlotManager.PopulateSaveSlots();
        }
        public void OnRegisterPlayerComplete()
        {
            _saveSystem.LoadAllPlayersOnStart();
            
        }
        
        
        [SerializeField] private GameObject _loadData;
    }
}
