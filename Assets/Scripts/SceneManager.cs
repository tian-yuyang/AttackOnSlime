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

    public void SwitchGuide() {
        LoadSceneByName("Guide");
    }
    public void SwitchLevelSelect() {
        LoadSceneByName("LevelSelect");
    }
    public void BackToStart() {
        LoadSceneByName("GameStart");
    }
}
