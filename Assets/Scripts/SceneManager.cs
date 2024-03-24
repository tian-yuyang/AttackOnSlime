using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void StartGame()
    {
        LoadSceneByName("GameStart");
    }

    public void SwitchGuide() {
        LoadSceneByName("Guide");
    }
    public void SwitchLevelSelect() {
        LoadSceneByName("LevelSelect");
    }
}
