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
        LoadSceneByName("test");
    }
    public void SwitchToLevelSelect() {
        LoadSceneByName("LevelSelect");
    }
    public void BackToStart() {
        LoadSceneByName("GameStart");
    }
    public void SwitchToLevel(int x) {
        LoadSceneByName("Level" + x.ToString());
    }
}
