using UnityEngine;
using UnityEngine.UI; // Required for UI components like Text and Slider
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text hpBar; // Assign in the inspector
    public TMP_Text durationTimeText = null; // Assign in the inspector
    public TMP_Text killCountText = null; // Assign in the inspector

    public Lily lily = null; // Assign in the inspector
    private float durationTime = 0f;
    private int killCount = 0;

    void Update()
    {
        // Example of how you might update the duration time every frame
        durationTime += Time.deltaTime;
        durationTimeText.text = $"Time: {durationTime:F2}s";

        // Assuming you have a method to call when an enemy is killed to update the kill count
        // UpdateKillCount(); // This should be called from wherever you handle killing enemies
        
        // Example of updating the HP bar
        // UpdateHP(50); // Let's assume the player's HP is 50 for this example
    }

    // Call this method when you want to update the player's HP
    public void UpdateHP(float hp)
    {
        hpBar.text = hp.ToString();
    }

    // Call this method to increment kill count when an enemy is killed
    public void IncrementKillCount()
    {
        killCount++;
        killCountText.text = $"Kills: {killCount}";
    }
}
