using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] pages;

    public SceneSwitcher mSceneSwitcher = null;

    public void Awake()
    {
        Debug.Assert(mSceneSwitcher != null, "SceneSwitcher is not set in UIManager");
    }

    public void SwitchPage(int pageIndex)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == pageIndex);
        }
    }
}
