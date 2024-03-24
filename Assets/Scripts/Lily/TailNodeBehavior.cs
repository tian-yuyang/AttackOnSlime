/*********
 * TailNodeBehavior.cs : 实现TailNode的行为，包括位置更新，碰撞检测等
 * 
 ********/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailNodeBehavior : MonoBehaviour
{
    //TODO(Hangyu) : 是否需要Editor映射？
    public static int SearchInterval = 5;


    private GameObject mLeader;
    private int mCurrentNodeIdx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //更新tail node位置
        int searchPosOnTrack = (mCurrentNodeIdx + 1) * SearchInterval;  //eg: (0 + 1) * 5 表示node0的SearchPos在Track上一直为5

        List<Vector3> track = mLeader.GetComponent<TailController>().GetTrack();
        transform.position = track[searchPosOnTrack];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO(Hangyu) : 目前只考虑了TailNode之间的碰撞，后续需要考虑其他物体的碰撞

        if (collision.gameObject.tag == "Tail")
        {
            if (!mLeader) return;

            List<int> triggerFlags = mLeader.GetComponent<TailController>().GetTriggerFlags();
            int collidedNodeIdx = collision.gameObject.GetComponent<TailNodeBehavior>().mCurrentNodeIdx;
            if(Math.Abs(collidedNodeIdx - mCurrentNodeIdx) > 1)
                triggerFlags[mCurrentNodeIdx] = Math.Min(collidedNodeIdx, mCurrentNodeIdx);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tail")
        {
            if (!mLeader) return;

            List<int> triggerFlags = mLeader.GetComponent<TailController>().GetTriggerFlags();
            triggerFlags[mCurrentNodeIdx] = 0;
        }
    }


    public void SetCurrentNodeIdx(int InSearchPos)
    {
        mCurrentNodeIdx = InSearchPos;
    }

    public int GetCurrentNodeIdx()
    {
        return mCurrentNodeIdx;
    }

    public void SetLeader(GameObject leader)
    {
        mLeader = leader;
    }

    public GameObject GetLeader()
    {
        return mLeader;
    }

}
