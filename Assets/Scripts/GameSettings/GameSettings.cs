using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Tooltip("Tail Node间隔(单位为frame)")]
    public int TailNodeInterval = 3;

    [Tooltip("First Tail Node相对于起始位置的偏移")]
    public int FirstTailNodeOffset = 2;

    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 30; //锁定三十帧
        TailNodeBehavior.FirstSearchPosOffset = FirstTailNodeOffset;
        TailNodeBehavior.SearchInterval = TailNodeInterval;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
