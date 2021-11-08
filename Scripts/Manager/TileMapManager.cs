using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMapManager : Singleton<TileMapManager>
{
    public Grid grid;
    private GameObject _towerHover;
    private GameObject _attackRange;

    [SerializeField] GameObject TowerToPlace;

    private bool towerToPlaceActive = false;
    private float placeCooldown = 0.5f;
    private Color attackColor;
    private List<GameObject> placedTowers = new List<GameObject>();

    public float PlaceCooldown { get; set; }

    private void Start() 
    {
        _towerHover = transform.Find("TowerHover").gameObject;
        _attackRange = _towerHover.transform.Find("AttackRange").gameObject;
        attackColor = _attackRange.GetComponent<SpriteRenderer>().color;
        PlaceCooldown = Time.time;
    }

    public void Update()
    {
        if(towerToPlaceActive)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
            Vector3 coordinateCenter = coordinate + new Vector3(0.5f, 0.5f, 0);
            
            _towerHover.transform.position = coordinateCenter;
            _towerHover.GetComponent<SpriteRenderer>().sprite = TowerToPlace.GetComponent<SpriteRenderer>().sprite;


            if(!IsTileEmpty(coordinateCenter))
            {
                attackColor = Color.red;
                attackColor.a = 0.6f;
                _attackRange.GetComponent<SpriteRenderer>().color = attackColor;
            }
            else
            {
                attackColor = Color.gray;
                attackColor.a = 0.6f;
                _attackRange.GetComponent<SpriteRenderer>().color = attackColor;
            }

            if(Input.GetMouseButtonDown(0))
            {
                ClickTile(coordinateCenter, IsTileEmpty(coordinateCenter));
                _towerHover.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

    private void ClickTile(Vector3 coordinateCenter, bool isEmpty)
    {
        if(TowerToPlace && isEmpty && Time.time >= placeCooldown + PlaceCooldown)
        {
            GameObject newTower = Instantiate(TowerToPlace, coordinateCenter, Quaternion.identity);
            newTower.transform.SetParent(transform);
            placedTowers.Add(newTower);

            towerToPlaceActive = false;
            _attackRange.SetActive(false);
            PlaceCooldown = Time.time;
        } 
    }

    public void SetTowerToPlace(GameObject towerToAssign, ref int numberAvailible, ref Text numberText)
    {
        if(numberAvailible<=0)
        {
            return;
        }
        TowerToPlace = towerToAssign;

        float attackRange = TowerToPlace.GetComponent<Tower>().towerInfo.AttackRange;
        _attackRange.transform.localScale = new Vector3(attackRange, attackRange, 1f);
        _attackRange.SetActive(true);
        towerToPlaceActive = true;

        PlaceCooldown = Time.time;
        numberAvailible--;
        numberText.text = numberAvailible.ToString();
    }

    private bool IsTileEmpty(Vector3 coordinate)
    {
        foreach (GameObject tower in placedTowers)
        {
            float distanceBetweenTowers = (tower.transform.position - coordinate).magnitude;
            if(distanceBetweenTowers <= 0.5f)
            {
                return false;
            }
        }

        return true;
    }
}
