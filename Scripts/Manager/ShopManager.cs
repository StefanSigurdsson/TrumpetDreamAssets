using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{

    [SerializeField] List<TowerInfo> purchaseableTowers = new List<TowerInfo>();
    [SerializeField] int rowHeight;
    [SerializeField] int columnWidth;
    [SerializeField] Vector3 startSpawningHere;

    [SerializeField] GameObject towerToBuyButton;

    Text currentMoneyText;
    List<GameObject> activeTowers = new List<GameObject>();

    private void Start() {
        currentMoneyText = transform.Find("ShopPanel/CurrentMoneyText").GetComponent<Text>();
        currentMoneyText.text = "Current Money: " + LevelManager.Instance.CurrentMoney.ToString();

        GameObject shopPanel = transform.Find("ShopPanel").gameObject;
        shopPanel.SetActive(false);
    }

    private void GenerateTowersToBuy()
    {
        if(activeTowers.Count > 0)
        {
            ClearShop();
        }
        Transform buttonParent = transform.Find("ShopPanel/Tower Area");
        for (int i = 0; i < 6; i++)
        {
            Vector3 spawnPos = new Vector3(i%3 * columnWidth - 300, i%2 * rowHeight - 150, 0);
            GameObject button = Instantiate(towerToBuyButton, buttonParent.position + spawnPos, Quaternion.identity);
            button.transform.SetParent(buttonParent);
            activeTowers.Add(button);
            int randomChoice = Random.Range(0, purchaseableTowers.Count);
            button.GetComponent<TowerToBuy>().SetTowerInfoCard(purchaseableTowers[randomChoice]);
        }
    }

    public void UpdateMoneyText()
    {
        currentMoneyText.text = "Current Money: " + LevelManager.Instance.CurrentMoney.ToString();
    }
    
    public void ToggleActivateShop()
    {
        GameObject shopPanel = transform.Find("ShopPanel").gameObject;
        if(shopPanel.activeSelf)
        {
            shopPanel.SetActive(false);
        }else {
            shopPanel.SetActive(true);
        }

        UpdateMoneyText();

        GenerateTowersToBuy();
    }

    private void ClearShop()
    {
        foreach(GameObject towerToClear in activeTowers)
        {
            Destroy(towerToClear);
        }
    }

}
