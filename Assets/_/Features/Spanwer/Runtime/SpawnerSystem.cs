using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnerSystem : MonoBehaviour
{

    [Header("References")] 
    public Transform m_player;
    public ScoreBehaviour m_scoreBehaviour;
    public List<GameObject> m_prefabs;
    public List<Transform> m_spawnPoints;
    
    
    [Header("Wave Settings")] 
    public List<WaveConfig> m_waveConfigs;
    public int m_maxUnitsPool;

    [Header("Debug")] 
    public List<GameObject> m_phantomPool;
    public List<PhantomBehaviour> m_livingPhantoms;
    public List<PhantomBehaviour.PhantomType> phantomTypesToSpawn = new List<PhantomBehaviour.PhantomType>();
    public WaveConfig m_currentWaveConfig;
    public int m_currentWave;
    public float m_spawnInterval;
    private bool m_isSpawning;


    private void Awake()
    {
        //m_currentWaveConfig = GetWaveConfigForWave(m_currentWave);
        InitializeSpawns();
    }

    private WaveConfig GetWaveConfigForWave(int currentWave)
    {
        // 1. Priorité aux cas uniques
        foreach (var configSet in m_waveConfigs)
        {
            if (!configSet.m_repeatEveryCentury &&
                currentWave >= configSet.m_startingWave &&
                currentWave <= configSet.m_endingWave)
            {
                return configSet;
            }
        }

        // 2. Sinon on utilise les configs cycliques
        int waveModulo = currentWave % 10;
        foreach (var configSet in m_waveConfigs)
        {
            if (configSet.m_repeatEveryCentury &&
                waveModulo >= configSet.m_startingWave &&
                waveModulo <= configSet.m_endingWave)
            {
                return configSet;
            }
        }

        Debug.LogWarning($"Aucune configuration trouvée pour la wave {currentWave}");
        return null;
    }


    private void InitializeSpawns()
    {
        for (int i = 0; i < m_maxUnitsPool; i++)
        {
            Transform spawnPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Count)];
            GameObject go = Instantiate(m_prefabs[0],
                new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y), Quaternion.identity,
                transform);
            var _pb = go.GetComponent<PhantomBehaviour>();
            _pb.m_player = m_player;
            _pb.m_livingPhantoms = m_livingPhantoms;
            _pb.m_scoreBehaviour = m_scoreBehaviour;


            go.SetActive(false);
            m_phantomPool.Add(go);
        }

    }

    private void Start()
    {
        StartCoroutine(SetPhantomActive());
    }

    private void Update()
    {
        if (!m_isSpawning && m_livingPhantoms.Count == 0)
        {
            m_isSpawning = true;
            m_scoreBehaviour.m_scoreConfig.SaveWaves(m_currentWave);
            m_currentWave++;    
            PrepareWave();
            StartCoroutine(SetPhantomActive());
        }
    }



    private void PrepareWave()
    {
        
        m_currentWaveConfig = GetWaveConfigForWave(m_currentWave);
        m_spawnInterval = m_currentWaveConfig.m_spawnInterval;
        if (m_currentWaveConfig == null)
        {
            Debug.LogError($"Aucune config trouvée pour la wave {m_currentWave}, arrêt du spawn !");
            return;
        }
        
        phantomTypesToSpawn.AddRange(Enumerable.Repeat(PhantomBehaviour.PhantomType.Easy, m_currentWaveConfig.m_phantomAmount));
        phantomTypesToSpawn.AddRange(Enumerable.Repeat(PhantomBehaviour.PhantomType.Medium, m_currentWaveConfig.m_mediumPhantomAmount));
        phantomTypesToSpawn.AddRange(Enumerable.Repeat(PhantomBehaviour.PhantomType.Hard, m_currentWaveConfig.m_hardPhantomAmount));
        phantomTypesToSpawn.AddRange(Enumerable.Repeat(PhantomBehaviour.PhantomType.Boss, m_currentWaveConfig.m_bossPhantomAmount));

        phantomTypesToSpawn = phantomTypesToSpawn.OrderBy(x => Random.value).ToList();
    }

    private IEnumerator SetPhantomActive()
    {
        WaitForSeconds wait = new WaitForSeconds(m_spawnInterval);

        for (int j = 0; j < phantomTypesToSpawn.Count; j++)
        {
        
            GameObject phantomGO = m_phantomPool.FirstOrDefault(p => !p.activeInHierarchy);
            if (phantomGO == null)
            {
                Debug.LogWarning("Pas assez de phantoms dans le pool !");
                continue;
            }

            PhantomBehaviour pb = phantomGO.GetComponent<PhantomBehaviour>();
            Transform spawnPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Count)];

            phantomGO.transform.position = spawnPoint.position;
            pb.m_currentWave = m_currentWave;
            pb.m_waveConfig = m_currentWaveConfig;
            pb.Configure(phantomTypesToSpawn[j]);
            m_livingPhantoms.Add(pb);
            phantomGO.SetActive(true);
            yield return wait;
        }

        phantomTypesToSpawn.Clear();
        m_isSpawning = false;
    }

}
