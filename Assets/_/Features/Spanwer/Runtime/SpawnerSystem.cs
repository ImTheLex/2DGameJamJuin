using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnerSystem : MonoBehaviour
{
    
    public List<GameObject> m_prefabs;
    public List<Transform> m_spawnPoints;
    
    public Transform m_player;
    public float m_distananceOfSpawn;
    public int m_spawnCount;
    public int m_currentWave;
    public float m_radius;

    
    private void Start()
    {
        SpawnAroundPoint();
    }

    private void SpawnAroundPoint()
    {
        for (int i = 0; i < m_spawnCount; i++)
        {
            var position = Random.Range(0, m_spawnPoints.Count);
            
            float segment = 2 * Mathf.PI / m_spawnCount;
            float x = m_distananceOfSpawn * Mathf.Cos(segment);
            float z = m_distananceOfSpawn * Mathf.Sin(segment);
            Vector2 dirValue = new Vector2(x, z);
            Vector2 worldPos = (Vector2)m_player.transform.position + dirValue * m_radius;
            
            GameObject go = Instantiate(m_prefabs[0], worldPos, Quaternion.identity, transform);
            go.transform.position = m_spawnPoints[position].position;
            go.GetComponent<PhantomBehaviour>().m_player = m_player;
        
        }
    }

}
