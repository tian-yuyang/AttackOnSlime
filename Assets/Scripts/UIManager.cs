using UnityEngine;
using UnityEngine.UI; // Required for UI components like Text and Slider
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image hpBar; // Assign in the inspector
    public TMP_Text durationTimeText = null; // Assign in the inspector
    public TMP_Text length = null; // Assign in the inspector

    public GameObject lily = null; // Assign in the inspector
    private float durationTime = 0f;
    private int killCount = 0;

    private float maxHealth;
    private float maxCD;
    public GameObject gameOverPanel;

    public GameObject PausePanel;

    private bool isPaused = false;


    public Slider slider;


    void Awake()
    {
        Enemy.SetTargetHero(lily.GetComponent<Lily>());
        // Example of how you might set the max HP for the player
        maxHealth = lily.GetComponent<Lily>().HP;
        maxCD = lily.GetComponent<TailController>().mAttackInterval;
        gameOverPanel.SetActive(isPaused);
        PausePanel.SetActive(isPaused);
        Bullet.SetTargetHero(lily.GetComponent<Lily>());
        GrowingBullet.SetTargetHero(lily.GetComponent<Lily>());
        TraceBullet.SetTargetHero(lily.GetComponent<Lily>());
        Time.timeScale = 1;
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
        UpdateLength();
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
        slider.value =  1 - lily.GetComponent<TailController>().GetAttackTimer() / maxCD;
    }
    public void UpdateLength()
    {
        if (!lily) return;
        int L = lily.GetComponent<TailController>().GetFollowedList().Count;
        length.text = "total length: " + L.ToString() + (L >= 18 ? " able" : " not able") + " to ultimate attack";
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

    public void Pause() {
        Time.timeScale = isPaused ? 0 : 1;
        PausePanel.SetActive(isPaused = !isPaused);
    }

    public void PauseTostart() {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        isPaused = false;
    }
}
