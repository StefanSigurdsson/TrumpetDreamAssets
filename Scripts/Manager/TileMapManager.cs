using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMapManager : Singleton<TileMapManager>
{
    public Grid grid;
    private GameObject _towerHover;

    [SerializeField] GameObject TowerToPlace;

    private bool towerToPlaceActive = false;

    private void Start() 
    {
        _towerHover = transform.Find("TowerHover").gameObject;
    }

    public void Update()
    {
        if(towerToPlaceActive)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
            _towerHover.transform.position = coordinate;
            _towerHover.GetComponent<SpriteRenderer>().sprite = TowerToPlace.GetComponent<SpriteRenderer>().sprite;

            if(Input.GetMouseButtonDown(0))
            {
                ClickTile(coordinate);
                _towerHover.GetComponent<SpriteRenderer>().sprite = null;
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
