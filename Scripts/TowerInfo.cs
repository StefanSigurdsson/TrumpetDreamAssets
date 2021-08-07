using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TowerInfo")]
public class TowerInfo : ScriptableObject
{
    [SerializeField] public int TowerCost;
    [SerializeField] public int AttackRange;
    [SerializeField] public GameObject TowerPrefab;
}
