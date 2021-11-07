using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerToBuy : Singleton<TowerToBuy>
{
    [SerializeField] private TowerInfo towerInfoCard;

    private Text placeCostText;
    private Text getCostText;

    private bool wasBought;

    /*
    public TowerToBuy(TowerInfo infoCard)
    {
        towerInfoCard = infoCard;
    } 
    */

    private void Start() {
        placeCostText = transform.Find("Place Cost Text").GetComponent<Text>();
        placeCostText.text = towerInfoCard.TowerCost.ToString() + " G";
        getCostText = transform.Find("Get Cost Text").GetComponent<Text>();
        getCostText.text = towerInfoCard.GetTowerCost.ToString() + " G";
        transform.GetComponent<Image>().sprite = towerInfoCard.TowerPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void BuyThisTower()
    {
        if(LevelManager.Instance.CurrentMoney < towerInfoCard.GetTowerCost || wasBought)
        {
            return;
        }

        TowerListManager.Instance.AddAvailibleTower(towerInfoCard.TowerPrefab);
        LevelManager.Instance.CurrentMoney -= towerInfoCard.GetTowerCost;
        getCostText.text = "PURCHASED";
        ShopManager.Instance.UpdateMoneyText();

        wasBought = true;
    }

    public void SetTowerInfoCard(TowerInfo infoCard)
    {
        towerInfoCard = infoCard;
    }

}
