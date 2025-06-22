using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    [System.Serializable]
    public  class PlayerSaveData 
    {
            public string m_playerUUID;
            public string m_playerName;
            public float m_timePlayed;
            public float m_score;
            public int m_waveCount;
            public string m_saveDate;
            //public PlayerInventory m_playerInventory;

    }
}
