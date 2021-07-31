using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyWave")]
public class EnemyWave : ScriptableObject
{

    public List<EnemyToSpawn> _enemyTypes = new List<EnemyToSpawn>();
    [SerializeField] int _numberOfEnemies;
    [SerializeField] float _spawnDelay = 1f;

    //public List<EnemyToSpawn> EnemyTypes { get; set; }
    public int NumberOfEnemies { get; set; }
    public float SpawnDelay { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //EnemyTypes = _enemyTypes;
        NumberOfEnemies = _numberOfEnemies;
        SpawnDelay = _spawnDelay;
    }

}

[System.Serializable]
public class EnemyToSpawn
{
    [SerializeField] public GameObject _enemyType;
    [SerializeField] public int _numberToSpawn;
    [SerializeField] public float _spawnDelay;
}
