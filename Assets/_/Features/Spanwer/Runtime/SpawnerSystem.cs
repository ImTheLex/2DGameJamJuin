using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnerSystem : MonoBehaviour
{
    
    public List<GameObject> m_prefabs;
    public List<Transform> m_spawnPoints;

    public List<GameObject> m_phantomPool;
    
    public Transform m_player;
    
    public float m_spawnInterval;
    public int m_maxUnitsPerWave;
    public int m_currentWave;
    private float m_radius;
    private float m_distananceOfSpawn;


    private void Awake()
    {
        InitializeSpawns();
    }

    private void InitializeSpawns()
    {
        for (int i = 0; i < m_maxUnitsPerWave; i++)
        {
            Transform spawnPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Count)];
            GameObject go = Instantiate(m_prefabs[0], new Vector3(spawnPoint.transform.position.x,spawnPoint.transform.position.y), Quaternion.identity, transform);
            go.GetComponent<PhantomBehaviour>().m_player = m_player;
            go.SetActive(false);
            m_phantomPool.Add(go);
        }
    }

    private void Start()
    {
        StartCoroutine(SetPhantomActive());
    }

    private IEnumerator SetPhantomActive()
    {
        for (int i = 0; i < m_phantomPool.Count; i++)
        {
            WaitForSeconds wait = new WaitForSeconds(m_spawnInterval);
            
            m_phantomPool[i].SetActive(true);
            
            yield return wait;


        }
    }
    
    

}
