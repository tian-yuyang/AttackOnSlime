using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    // Start is called before the first frame update

    private int SkinNumber = 0;
    private int[] posX = new int[] {-4,0,4}; 

    public void BackToStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameStart");
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            SkinNumber = (SkinNumber + 2) % 3;
            transform.position = new Vector3(posX[SkinNumber], transform.position.y, 0);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            SkinNumber = (SkinNumber + 1) % 3;
            transform.position = new Vector3(posX[SkinNumber], transform.position.y, 0);
        }
        if(Input.GetKeyDown(KeyCode.Return)) {
            PlayerPrefs.SetInt("SkinNumber", SkinNumber);
            BackToStart();
        }
    }
}
