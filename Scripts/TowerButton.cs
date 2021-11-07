using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{

    public GameObject towerPrefab { get; set; }
    public int NumberAvailible { get; set; }

    private Text _costText;
    private Text _numberAvailibleText; 
    private Image towerImage;
    private Button thisButton;

    
    // Start is called before the first frame update
    void Start()
    {
        _costText = transform.Find("Cost Text").GetComponent<Text>();
        _costText.text = towerPrefab.GetComponent<Tower>().towerInfo.TowerCost.ToString();

        towerImage = GetComponent<Image>();
        towerImage.sprite = towerPrefab.GetComponent<SpriteRenderer>().sprite;

        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(delegate{TileMapManager.Instance.SetTowerToPlace(towerPrefab);});

        NumberAvailible = 1;
        _numberAvailibleText = transform.Find("NumberAvailibleText").GetComponent<Text>();
        _numberAvailibleText.text = NumberAvailible.ToString();
    }

    public void UpdateNumberAvailible()
    {
        _numberAvailibleText.text = NumberAvailible.ToString();
    }

}
