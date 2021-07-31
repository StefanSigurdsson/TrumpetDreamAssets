using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{

    [SerializeField] GameObject towerPrefab;

    private Text _costText;
    private Image towerImage;
    private Button thisButton;

    

    // Start is called before the first frame update
    void Start()
    {
        _costText = GetComponentInChildren<Text>();
        _costText.text = towerPrefab.GetComponent<Tower>().Cost.ToString();

        towerImage = GetComponent<Image>();
        towerImage.sprite = towerPrefab.GetComponent<SpriteRenderer>().sprite;

        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(delegate{TileMapManager.Instance.SetTowerToPlace(towerPrefab);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
