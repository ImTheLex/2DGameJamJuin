using System;
using System.Collections;
using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace Saves.Runtime
{
    public class SavedSlotManager : MonoBehaviour
    {

        //public UnityEvent m_onDataLoaded;
        [SerializeField] private GameObject _slotPrefab; // ton SaveSlotItem prefab
        [SerializeField] private Transform _contentParent; // le GameObject avec VerticalLayoutGroup

        private void Awake()
        {
            if(_contentParent == null) _contentParent = transform;
        }

        private void Start()
        {
            PopulateSaveSlots();
        }

        public void PopulateSaveSlots()
        {
            
            foreach (Transform child in _contentParent)
            {
                Destroy(child.gameObject);
            }
            
            var sortedList = new List<PlayerSaveData>(SaveSystem.Instance.m_PlayerSaveData);
            sortedList.Sort((a, b) => b.m_score.CompareTo(a.m_score));

            foreach (var data in sortedList)
            {
                GameObject slot = Instantiate(_slotPrefab, _contentParent);
                var slotUI = slot.GetComponent<SavedSlot>();
                slotUI.Setup(data, OnSaveSlotClicked);
            }
        }

        private void OnSaveSlotClicked(PlayerSaveData data)
        {
            SaveSystem.Instance.SetData(data);
            Debug.Log("Chargement de la sauvegarde : " + data.m_playerName);
            //m_onDataLoaded.Invoke();
            // Appelle une méthode pour charger ce joueur, ou aller à la scène suivante
        }
    }
}
