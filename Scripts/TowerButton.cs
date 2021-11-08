using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{

    public GameObject towerPrefab { get; set; }
    public int numberAvailible;

    private Text _numberAvailibleText; 
    private Image towerImage;
    private Button thisButton;

    
    // Start is called before the first frame update
    void Start()
    {
        towerImage = GetComponent<Image>();
        towerImage.sprite = towerPrefab.GetComponent<SpriteRenderer>().sprite;

        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(delegate{TileMapManager.Instance.SetTowerToPlace(towerPrefab, ref numberAvailible, ref _numberAvailibleText);});

        numberAvailible = 1;
        _numberAvailibleText = transform.Find("NumberAvailibleText").GetComponent<Text>();
        _numberAvailibleText.text = numberAvailible.ToString();
    }

    public void UpdateNumberAvailible(int change)
    {
        numberAvailible += change;
        _numberAvailibleText.text = numberAvailible.ToString();
    }

    public bool AnyAvailible()
    {
        if(numberAvailible > 0)
        {
            return true;
        }
        return false;
    }

}
