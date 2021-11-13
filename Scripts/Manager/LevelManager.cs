using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum GamePhase
{
    BetweenWaves,
    Wave,
    Shop
};

public class LevelManager : Singleton<LevelManager>
{
    private Spawner Spawner;
    private List<Enemy> enemiesSpawned = new List<Enemy>();

    public int CurrentMoney { get; set; }
    public int CurrentLife { get; set; }

    [SerializeField] Text waveNumberText;
    [SerializeField] Text moneyText;
    [SerializeField] Text lifeText;

    [SerializeField] int startMoney = 30;
    [SerializeField] int startLife = 100;

    private bool _duringWave = false;
    private int _waveNumber;

    GamePhase currentGamePhase;

    // Start is called before the first frame update
    void Start()
    {
        Spawner = GetComponentInChildren<Spawner>();

        _waveNumber = 0;
        waveNumberText.text = "Wave: 1";
        waveNumberText.color = Color.yellow;

        CurrentMoney = startMoney;
        moneyText.text = "Money: " + CurrentMoney.ToString();
        moneyText.color = Color.yellow;

        CurrentLife = startLife;
        UpdateLifeText();

        currentGamePhase = GamePhase.BetweenWaves;
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "Money: " + CurrentMoney.ToString();
    }

    private void UpdateLifeText()
    {
        lifeText.text = CurrentLife.ToString() + " / " + startLife.ToString();
    }

    public void StartWave()
    {
        if(HasWaveEnded())
        {
            _duringWave = true;
            StartCoroutine(Spawner.StartWave(_waveNumber));
            _waveNumber ++;
            waveNumberText.text = "Wave: " + _waveNumber.ToString();
            if(_waveNumber >= Spawner.NumberOfWaves)
            {
                _waveNumber = 0;
            }
            _duringWave = false;
            currentGamePhase = GamePhase.Wave;
        } else {
            Debug.Log("wave in progress!");
        }
    }

    public bool CheckForEnemies()
    {
        int numberOfEnemies = Spawner.transform.childCount;
        if(numberOfEnemies > 0)
        {
            return true;
        }
        return false;
    }

    public bool HasWaveEnded()
    {
        if(CheckForEnemies() || _duringWave)
        {
            return false;
        }

        return true;
    }

    public void WaveHasEnded()
    {
        Debug.Log("hi from WaveHasEnded");
        if(HasWaveEnded())
        {
            currentGamePhase = GamePhase.BetweenWaves;
        }
    }

    public void OpenShop()
    {
        moneyText.text = "Money: " + CurrentMoney.ToString();
        ShopManager.Instance.ToggleActivateShop();
    }

    public void LoseLife(int lifeLost)
    {
        CurrentLife -= lifeLost;
        UpdateLifeText();
    }
}
