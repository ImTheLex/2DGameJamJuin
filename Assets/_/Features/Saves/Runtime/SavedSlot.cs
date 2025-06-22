using System.Collections;
using System.Collections.Generic;
using Shared;
using Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Saves.Runtime
{
    public class SavedSlot : MonoBehaviour
    {
        public TextMeshProUGUI playerNameText;

        public void Setup(PlayerSaveData data, UnityAction<PlayerSaveData> onClickAction)
        {
            playerNameText.text = $"{data.m_playerName}<br>Score: {data.m_score}<br>Waves: {data.m_waveCount}";
        }
    }
}
