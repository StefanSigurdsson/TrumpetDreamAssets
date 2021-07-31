using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] List<EnemyWave> _waves = new List<EnemyWave>();
    public int NumberOfWaves;
    Waypoint _waypoint;

    private int _currentWaveIndex = 0;
    private bool duringWave = false;

    private void Start() 
    {
        _waypoint = GetComponent<Waypoint>();
        NumberOfWaves = _waves.Count;
    }

    private void Update() 
    {
#if UNITY_EDITOR
             if(!duringWave && Input.GetKeyDown(KeyCode.S))
             {
                 //StartCoroutine(StartWave());
             }
#endif        
    }

    public IEnumerator StartWave(int waveNumber)
    {
        duringWave = true;
        EnemyWave waveToSpawn = _waves[waveNumber];
        foreach(EnemyToSpawn enemyGroup in waveToSpawn._enemyTypes)
        {
            for (int i = 0; i < enemyGroup._numberToSpawn; i++)
            {
                SpawnEnemy(enemyGroup);
                yield return new WaitForSeconds(enemyGroup._spawnDelay);
            }
        }

        _currentWaveIndex ++;
        if(_currentWaveIndex >= _waves.Count)
        {
            _currentWaveIndex = 0;
        }
        duringWave = false;
    }

    private void SpawnEnemy(EnemyToSpawn enemyGroup)
    {
        Vector3 spawnPosition = new Vector3(_waypoint.Points[0].x, _waypoint.Points[0].y, 0f);
        GameObject instance = Instantiate(enemyGroup._enemyType, spawnPosition, Quaternion.identity);
        instance.transform.SetParent(transform);
        Enemy enemy = instance.GetComponent<Enemy>();
        enemy.Waypoint = _waypoint;
        
    }
}
