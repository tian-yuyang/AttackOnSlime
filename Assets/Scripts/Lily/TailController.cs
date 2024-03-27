using AllIn1SpriteShader;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;

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
    private float mAttackTimer = 0.0f; //攻击计时器

    //Fix bug: 用于松弛的环判定
    private List<TriggerCircle> mCircleList = null;
    private float mRingRemainTimer = 0.0f;

    [Tooltip("环延长判定时间")]
    public float mRingRemainInterval = 0.05f;
    //

    [Tooltip("最大尾巴长度")]
    public int mMaxTailLength = 30; //最大尾巴长度

    [Tooltip("生成间隔")]
    public float mFollowedGenerateInterval = 3.0f; //TailNode生成间隔

    [Tooltip("攻击间隔")]
    public float mAttackInterval = 0.5f; //攻击间隔

    [Tooltip("攻击惩罚倍率")]
    public float mAttackPenaltyRatio = 1.5f; //攻击惩罚

    [Tooltip("Tail Node间隔")]
    public float TailNodeInterval = 0.71f;

    [Tooltip("First Tail Node相对于起始位置的偏移")]
    public float FirstTailNodeOffset = 1.064f;

    // Start is called before the first frame update
    void Start()
    {
        mTrack = new List<Vector3>();
        mFollowedList = new List<GameObject>();
        mCircleList = new List<TriggerCircle>();
        mTriggerFlags = Enumerable.Repeat(0, mMaxTailLength).ToList();

        mFollowedGenerateTimer = mFollowedGenerateInterval;

        TailNodeBehavior.FirstSearchPosOffset = (int)MathF.Round(FirstTailNodeOffset * Application.targetFrameRate / GetComponent<Lily>().mSpeed);
        TailNodeBehavior.SearchInterval = (int)MathF.Round(TailNodeInterval * Application.targetFrameRate / GetComponent<Lily>().mSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        mTrack.Insert(0, transform.position);
        if (mTrack.Count() > mMaxTailLength * TailNodeBehavior.SearchInterval + TailNodeBehavior.FirstSearchPosOffset + 10 /*magic number: ensure bug-free*/)
        {
            mTrack.RemoveAt(mTrack.Count() - 1);
        }

        GenerateNewTailNode();

        List<TriggerCircle> list = ExtractCircleOnTrack();
        LooseRingRemainJudge(list);

        TestColor(mCircleList);

        {
            Attack(); //普攻
            DeleteNodeAndReshapeTrack(mCircleList); //技能1
            ReTrace(); //终结技
        }
    }

    private void LooseRingRemainJudge(List<TriggerCircle> list)
    {
        if (mCircleList.Count() == 0 || mCircleList.Count() < list.Count())
        {
            mCircleList = list;
            mRingRemainTimer = mRingRemainInterval;
        }
        else if (mCircleList.Count() >= list.Count())
        {
            if (mRingRemainTimer > 0.0f)
            {
                mRingRemainTimer -= Time.deltaTime;
                return;
            }
            mCircleList = list;
            mRingRemainTimer = mRingRemainInterval;
        }

    }

    private void GenerateNewTailNode()
    {
        if (mFollowedGenerateTimer > 0.0f) //生成TailNode的间隔
        {
            mFollowedGenerateTimer -= Time.deltaTime;
            return;
        }

        int currentIdx = mFollowedList.Count();
        int searchPos = currentIdx * TailNodeBehavior.SearchInterval + TailNodeBehavior.FirstSearchPosOffset;

        if (currentIdx >= mMaxTailLength || searchPos >= mTrack.Count()) //如果当前位置超出了轨迹的范围，则不生成TailNode
        {
            return;
        }

        Vector3 generatedPos = mTrack[searchPos];

        GameObject tailElement = Instantiate(Resources.Load("Prefabs/Tail"), generatedPos, Quaternion.identity) as GameObject;
        tailElement.GetComponent<TailNodeBehavior>().SetLeader(gameObject);
        tailElement.GetComponent<TailNodeBehavior>().SetCurrentNodeIdx(currentIdx);

        mFollowedList.Add(tailElement);

        mFollowedGenerateTimer = mFollowedGenerateInterval;
    }

    private List<TriggerCircle> ExtractCircleOnTrack()
    {
        List<TriggerCircle> list = new List<TriggerCircle>();

        List<int> triggerFlagTypeList = new List<int>(); //统计triggerFlags中包含几种类型的Flag，即存在几组相交
        List<int> flagMinPosList = new List<int>(); //统计flag的最小出现位置
        List<int> flagMaxPosList = new List<int>(); //统计flag的最大出现位置

        for (int i = 0; i < mFollowedList.Count(); i++)
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

        for (int i = 0; i < triggerFlagTypeList.Count(); i++)
        {
            bool isCircle = true;
            TriggerCircle currentCircle = new TriggerCircle() { mMinPos = flagMinPosList[i], mMaxPos = flagMaxPosList[i] };
            {
                for (int j = 0; j < list.Count(); j++)
                {
                    if (currentCircle.mMinPos < list[j].mMaxPos && currentCircle.mMaxPos > list[j].mMaxPos) //交叉情况
                    {
                        list[j].mMaxPos = currentCircle.mMaxPos;
                        isCircle = false;
                        break;
                    }

                    if (currentCircle.mMaxPos < list[j].mMaxPos)
                    {
                        isCircle = false;
                        break;
                    }
                }
            }
            isCircle &= (currentCircle.mMinPos != currentCircle.mMaxPos);
            if (isCircle) list.Add(currentCircle);
        }

        return list;
    }

    private void TestColor(List<TriggerCircle> list)
    {
        for (int i = 0; i < mFollowedList.Count(); i++)
        {
            mFollowedList[i].GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0.0f);
            mFollowedList[i].GetComponent<SpriteRenderer>().material.color = Color.white;
            for (int j = 0; j < list.Count(); j++)
            {
                if (i >= list[j].mMinPos && i <= list[j].mMaxPos)
                {
                    mFollowedList[i].GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 1.0f);
                    mFollowedList[i].GetComponent<SpriteRenderer>().material.color = Color.red;
                }
            }

        }
    }

    private void Attack()
    {
        if (mAttackTimer > 0.0f) //生成TailNode的间隔
        {
            mAttackTimer -= Time.deltaTime;
            return;
        }

        if (!Input.GetKey(KeyCode.J))
            return;

        bool isAttackSuccess = false;
        for (int i = 0; i < mFollowedList.Count(); i++)
        {
            bool currentSuccess = mFollowedList[i].GetComponent<TailNodeBehavior>().Attack();
            isAttackSuccess |= currentSuccess;
            if (currentSuccess) mFollowedList[i].GetComponent<TailNodeBehavior>().SetAttackEffectTimer();
        }

        if (isAttackSuccess)
        {
            mAttackTimer = mAttackInterval;
            mFollowedGenerateTimer = Math.Min(mFollowedGenerateInterval * mAttackPenaltyRatio, 4.8f);
        }
    }

    private void DeleteNodeAndReshapeTrack(List<TriggerCircle> list)
    {
        if (!Input.GetKeyDown(KeyCode.K))
            return;

        for (int i = 0; i < list.Count(); i++)
        {
            int prevSearchPos = (list[i].mMinPos - 1) * TailNodeBehavior.SearchInterval + TailNodeBehavior.FirstSearchPosOffset;
            if (list[i].mMinPos == 0) prevSearchPos = 0;
            int postSearchPos = Math.Min((list[i].mMaxPos + 1) * TailNodeBehavior.SearchInterval + TailNodeBehavior.FirstSearchPosOffset, mTrack.Count() - 1);
            Vector3 prevPos = mTrack[prevSearchPos];
            Vector3 postPos = mTrack[postSearchPos];
            List<Vector3> insertPos = new List<Vector3>();
            int lerpIter = TailNodeBehavior.SearchInterval;
            if(prevSearchPos == 0) lerpIter = TailNodeBehavior.FirstSearchPosOffset;
            for (int j = 1; j < lerpIter; j++)
            {
                float linearRatio = (float)j / lerpIter;  // x在0~1之间均匀分布

                float lerpRatio = MathF.Sqrt(linearRatio);  // 使用平方根函数进行插值
                //float lerpRatio = 0.5f * (1.0f - MathF.Cos(linearRatio * MathF.PI));  // 使用正弦函数进行插值，使得两端插值点密，中间插值点稀疏

                insertPos.Add(Vector3.Lerp(prevPos, postPos, lerpRatio));
            }

            mTrack.RemoveRange(prevSearchPos + 1, postSearchPos - prevSearchPos - 1);
            mTrack.InsertRange(prevSearchPos + 1, insertPos);

            List<Vector2> ringNodePos = new List<Vector2>();
            Vector3 averagePos = new Vector3(0.0f, 0.0f, 0.0f);
            for (int j = list[i].mMinPos; j <= Math.Min(list[i].mMaxPos, mFollowedList.Count() - 1); j++)
            {
                ringNodePos.Add(new Vector2(mFollowedList[j].transform.position.x, mFollowedList[j].transform.position.y));
                averagePos += mFollowedList[j].transform.position;
                Destroy(mFollowedList[j]);
            }
            GameObject ring = Instantiate(Resources.Load("Prefabs/Ring"), Vector3.zero, Quaternion.identity) as GameObject;
            ring.GetComponent<RingBehavior>().SetColliderPoints(ringNodePos);

            mFollowedList.RemoveRange(list[i].mMinPos, list[i].mMaxPos - list[i].mMinPos + 1);

            for (int j = i + 1; j < list.Count(); j++)
            {
                list[j].mMinPos -= (list[i].mMaxPos - list[i].mMinPos + 1);
                list[j].mMaxPos -= (list[i].mMaxPos - list[i].mMinPos + 1);
            }

            for (int j = list[i].mMinPos; j < mFollowedList.Count(); j++)
            {
                int pos = mFollowedList[j].GetComponent<TailNodeBehavior>().GetCurrentNodeIdx();
                mFollowedList[j].GetComponent<TailNodeBehavior>().SetCurrentNodeIdx(pos - (list[i].mMaxPos - list[i].mMinPos + 1));
            }
        }

        for (int i = 0; i < mTriggerFlags.Count(); i++)
        {
            mTriggerFlags[i] = 0;
        }
    }

    //结合两个特殊技能的描述：1. 回溯轨迹 2. 大范围引爆
    private void ReTrace()
    {
        if (mFollowedList.Count() <= 18)
            return;
        else
        {
            for (int i = 0; i < mFollowedList.Count(); i++)
            {
                mFollowedList[i].GetComponent<SpriteRenderer>().material.color = Color.blue;
            }
        }

        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        for (int i = 0; i < mTriggerFlags.Count(); i++)
        {
            mTriggerFlags[i] = 0;
        }

        transform.position = mTrack[(mFollowedList[mFollowedList.Count() - 1].GetComponent<TailNodeBehavior>().GetCurrentNodeIdx()) * TailNodeBehavior.SearchInterval + TailNodeBehavior.SearchInterval];
        Instantiate(Resources.Load("Prefabs/Explosion"), transform.position, Quaternion.identity);

        ClearTail();
    }

    public void ClearTail()
    {
        mTrack.Clear();
        for (int i = 0; i < mFollowedList.Count(); i++)
        {
            Destroy(mFollowedList[i]);
        }

        mFollowedList.Clear();
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

    public float GetAttackTimer()
    {
        return mAttackTimer;
    }
}
