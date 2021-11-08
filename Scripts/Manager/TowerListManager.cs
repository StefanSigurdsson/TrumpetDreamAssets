using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerListManager : Singleton<TowerListManager>
{
    
    [SerializeField] private List<GameObject> availibleTowers = new List<GameObject>();
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] List<GameObject> testTowers = new List<GameObject>();

    private List<GameObject> towerButtons = new List<GameObject>();
  
    private void Start() {
        TestFunction();
    }


    public void AddAvailibleTower(GameObject towerToAdd)
    {
        foreach(GameObject tower in towerButtons)
        {
            if(tower.GetComponent<TowerButton>().towerPrefab.name == towerToAdd.name)
            {
                Debug.Log(tower.GetComponent<TowerButton>());
                tower.GetComponent<TowerButton>().UpdateNumberAvailible(1);
                return;
            }
        }
        availibleTowers.Add(towerToAdd);
        Vector3 newPosition;
        if(towerButtons.Count > 0)
        {
            newPosition = new Vector3(towerButtons[towerButtons.Count-1].transform.position.x + 150f, towerButtons[towerButtons.Count-1].transform.position.y, 0);
        }
        else
        {
            newPosition = transform.Find("MenuBar/TowerButtons").position;
        }
        GameObject newButton = Instantiate(buttonPrefab, newPosition, Quaternion.identity);
        newButton.GetComponent<TowerButton>().towerPrefab = towerToAdd;
        newButton.transform.SetParent(transform.Find("MenuBar/TowerButtons"));

        towerButtons.Add(newButton);
    }

    public void TestFunction()
    {
        foreach (GameObject tower in testTowers)
        {
            AddAvailibleTower(tower);
        }
    }
}
