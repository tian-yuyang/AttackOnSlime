using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = (int)(1.0f / Time.smoothDeltaTime); //Ëø¶¨ÈýÊ®Ö¡
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
