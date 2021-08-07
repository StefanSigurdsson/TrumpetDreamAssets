using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{

    public GameObject towerPrefab { get; set; }

    private Text _costText;
    private Image towerImage;
    private Button thisButton;

    
    // Start is called before the first frame update
    void Start()
    {
        _costText = GetComponentInChildren<Text>();
        _costText.text = towerPrefab.GetComponent<Tower>().towerInfo.TowerCost.ToString();

        towerImage = GetComponent<Image>();
        towerImage.sprite = towerPrefab.GetComponent<SpriteRenderer>().sprite;

        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(delegate{TileMapManager.Instance.SetTowerToPlace(towerPrefab);});
    }

}
