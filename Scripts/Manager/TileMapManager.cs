using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapManager : Singleton<TileMapManager>
{
    public Grid grid; 

    [SerializeField] GameObject TowerToPlace;

    private bool towerToPlaceActive = false;


    public void Update()
    {
        if(towerToPlaceActive)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
            if(Input.GetMouseButtonDown(0))
            {
                ClickTile(coordinate);
            }
        }
    }

    private void ClickTile(Vector3Int coordinate)
    {
        
        if(TowerToPlace)
        {
            Instantiate(TowerToPlace, coordinate, Quaternion.identity);
            towerToPlaceActive = false;
        }
    }

    public void SetTowerToPlace(GameObject towerToAssign)
    {
        TowerToPlace = towerToAssign;
        towerToPlaceActive = true;
    }
}
