using UnityEngine;
using UnityEngine.UI; // Required for UI components like Text and Slider
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image hpBar; // Assign in the inspector
    public TMP_Text durationTimeText = null; // Assign in the inspector
    public CDBar CD = null; // Assign in the inspector

    public GameObject lily = null; // Assign in the inspector
    private float durationTime = 0f;
    private int killCount = 0;

    private float maxHealth;
    private float maxCD = 18;
    public GameObject gameOverPanel;

    public GameObject PausePanel;
    public GameObject WinPanel;

    private bool isPaused = false;



    void Start()
    {
        Enemy.SetTargetHero(lily.GetComponent<Lily>());
        RemoteBoss.SetUI(this);
        TowerBoss.SetUI(this);
        MeleeBoss.SetUI(this);
        // Example of how you might set the max HP for the player
        maxHealth = lily.GetComponent<Lily>().HP;
        gameOverPanel.SetActive(isPaused);
        PausePanel.SetActive(isPaused);
        WinPanel.SetActive(isPaused);
        Bullet.SetTargetHero(lily.GetComponent<Lily>());
        GrowingBullet.SetTargetHero(lily.GetComponent<Lily>());
        TraceBullet.SetTargetHero(lily.GetComponent<Lily>());
        Time.timeScale = 1;
        isPaused = false;
    }

    void Update()
    {
        // Example of how you might update the duration time every frame
        durationTime += Time.deltaTime;
        durationTimeText.text = $"Time: {durationTime:F2}s";

        // Assuming you have a method to call when an enemy is killed to update the kill count
        // UpdateKillCount(); // This should be called from wherever you handle killing enemies
        
        // Example of updating the HP bar
        UpdateHP(); 
        UpdateCD();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    // Call this method when you want to update the player's HP
    public void UpdateHP()
    {
        if (!lily) return;
        if (lily.GetComponent<Lily>().HP <= 0)
        {
            GameOver();// Game over
            return ;
        }
        hpBar.fillAmount = lily.GetComponent<Lily>().HP / maxHealth;
    }
    public void UpdateCD()
    {
        if (!lily) return;
        int length = lily.GetComponent<TailController>().GetFollowedList().Count;
        if (length > 18) {
            CD.getFront().fillAmount = 1;
            CD.getFrame().material.SetFloat("_Glow", 100.0f);
            return;
        }
            CD.getFrame().material.SetFloat("_Glow", 0.0f);
        CD.getFront().fillAmount =  length / maxCD;
    }

    // Call this method to increment kill count when an enemy is killed
    // public void IncrementKillCount()
    // {
    //     killCount++;
    //     killCountText.text = $"Kills: {killCount}";
    // }

    public void GameOver() {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void Win() {
        Time.timeScale = 0;
        WinPanel.SetActive(true);
    }

    public void Pause() {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        PausePanel.SetActive(isPaused);
    }

    public void PauseTostart() {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        isPaused = false;
    }
}
