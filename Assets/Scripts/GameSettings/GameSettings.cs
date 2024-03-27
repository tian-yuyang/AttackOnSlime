using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = (int)(1.0f / Time.smoothDeltaTime); //������ʮ֡
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
