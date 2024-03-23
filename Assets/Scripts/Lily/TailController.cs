using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TailController : MonoBehaviour
{
    private class TriggerCircle //用于记录轨迹中的环形
    {
        public int mMinPos;
        public int mMaxPos;
    };

    private List<Vector3> mTrack = null; //记录主角的轨迹, TODO(Hangyu) : 现在Hard code记录了framerate * 4的轨迹(4s)，后续需要根据实际情况调整
    private List<GameObject> mFollowedList = null; //记录跟随主角的尾巴
    private List<int> mTriggerFlags = null; //记录每个TailNode的触发状态
    private float mFollowedGenerateTimer = 0.0f; //TailNode生成计时器

    public float mFollowedGeneratenterval = 3.0f; //TailNode生成间隔

    // Start is called before the first frame update
    void Start()
    {
        mTrack = new List<Vector3>();
        mFollowedList = new List<GameObject>();
        mTriggerFlags = Enumerable.Repeat(0, Application.targetFrameRate * 4 / TailNodeBehavior.SearchInterval).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        mTrack.Insert(0, transform.position);
        if (mTrack.Count > Application.targetFrameRate * 4)
        {
            mTrack.RemoveAt(mTrack.Count - 1);
        }

        GenerateNewTailNode();

        List<TriggerCircle> list = ExtractCircleOnTrack();
        TestColor(list);
    }

    private void GenerateNewTailNode()
    {
        if (mFollowedGenerateTimer > 0.0f) //生成TailNode的间隔
        {
            mFollowedGenerateTimer -= Time.deltaTime;
            return;
        }

        int currentIdx = mFollowedList.Count;
        int searchPos = (currentIdx + 1) * TailNodeBehavior.SearchInterval;

        if (searchPos >= mTrack.Count) //如果当前位置超出了轨迹的范围，则不生成TailNode
        {
            return;
        }

        Vector3 generatedPos = mTrack[searchPos];

        GameObject tailElement = Instantiate(Resources.Load("Prefabs/Tail"), generatedPos, Quaternion.identity) as GameObject;
        tailElement.GetComponent<TailNodeBehavior>().SetLeader(gameObject);
        tailElement.GetComponent<TailNodeBehavior>().SetCurrentNodeIdx(currentIdx);

        mFollowedList.Add(tailElement);

        mFollowedGenerateTimer = mFollowedGeneratenterval;
    }

    private List<TriggerCircle> ExtractCircleOnTrack()
    {
        List<TriggerCircle> list = new List<TriggerCircle>();

        List<int> triggerFlagTypeList = new List<int>(); //统计triggerFlags中包含几种类型的Flag，即存在几组相交
        List<int> flagMinPosList = new List<int>(); //统计flag的最小出现位置
        List<int> flagMaxPosList = new List<int>(); //统计flag的最大出现位置

        for (int i = 0; i < mFollowedList.Count; i++)
        {
            int flag = mTriggerFlags[i];
            if (flag == 0) continue;

            bool isContained = triggerFlagTypeList.Contains(flag);
            if (!isContained)
            {
                triggerFlagTypeList.Add(flag);
                flagMinPosList.Add(i);
                flagMaxPosList.Add(i);
            }
            else
            {
                flagMaxPosList[triggerFlagTypeList.IndexOf(flag)] = i;
            }
        }

        for (int i = 0; i < triggerFlagTypeList.Count; i++)
        {
            bool isCircle = true;
            TriggerCircle currentCircle = new TriggerCircle() { mMinPos = flagMinPosList[i], mMaxPos = flagMaxPosList[i] };
            for (int j = 0; j < i; j++)
            {
                if (currentCircle.mMaxPos < flagMaxPosList[j]) //Min段不用检测
                {
                    isCircle = false;
                    break;
                }
            }
            if (isCircle) list.Add(currentCircle);
        }

        return list;
    }

    private void TestColor(List<TriggerCircle> list)
    {
        for (int i = 0; i < mFollowedList.Count; i++)
        {
            mFollowedList[i].GetComponent<SpriteRenderer>().color = Color.white;
            for (int j = 0; j < list.Count; j++)
            {
                if (i >= list[j].mMinPos && i <= list[j].mMaxPos)
                    mFollowedList[i].GetComponent<SpriteRenderer>().color = Color.green;
            }

        }
    }

    public List<Vector3> GetTrack()
    {
        return mTrack;
    }

    public List<GameObject> GetFollowedList()
    {
        return mFollowedList;
    }

    public List<int> GetTriggerFlags()
    {
        return mTriggerFlags;
    }

}
