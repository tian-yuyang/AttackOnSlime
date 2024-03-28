/*********
 * TailNodeBehavior.cs : 实现TailNode的行为，包括位置更新，碰撞检测等
 * 
 ********/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class TailNodeBehavior : MonoBehaviour
{
    public static int SearchInterval;
    public static int FirstSearchPosOffset;

    [Tooltip("材质")]
    public Material[] mat;
    [Tooltip("贴图")]
    public Sprite[] pic;//贴图
    //TODO(Hangyu) : 根Enemy保持一致，暂定为int型
    [Tooltip("普通攻击攻击力")]
    public int mAttack = 5;
    [Tooltip("攻击特效持续时间")]
    public float mAttackEffectTime = 0.15f;

    private GameObject mLeader;
    private int mCurrentNodeIdx;

    private GameObject mCollidedObject = null;
    private SpriteRenderer sr = null;

    private float mAttackEffectTimer = 0.0f;

    public void TailChangeSprite()// 更换贴图及材质
    {
        // if (n >= pic.Length && n < 0) { n = 0; }
        sr.sprite = pic[PlayerPrefs.GetInt("SkinNumber", 0)];
        sr.material = mat[PlayerPrefs.GetInt("SkinNumber", 0)];
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        TailChangeSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mLeader) return;
        if (mLeader.GetComponent<TailController>().GetRetraceState()) return;

        //更新tail node位置
        int searchPosOnTrack = mCurrentNodeIdx * SearchInterval + FirstSearchPosOffset;  //eg: (0 + 1) * 5 表示node0的SearchPos在Track上一直为5

        List<Vector3> track = mLeader.GetComponent<TailController>().GetTrack();
        transform.position = track[searchPosOnTrack];

        OperateAttackEffect();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO(Hangyu) : 目前只考虑了TailNode之间的碰撞，后续需要考虑其他物体的碰撞
        if (collision.gameObject.tag == "Tail")
        {
            if (!mLeader) return;

            List<int> triggerFlags = mLeader.GetComponent<TailController>().GetTriggerFlags();
            int collidedNodeIdx = collision.gameObject.GetComponent<TailNodeBehavior>().mCurrentNodeIdx;
            if (Math.Abs(collidedNodeIdx - mCurrentNodeIdx) > 1)
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
        if (collision.gameObject.tag == "MeleeEnemy" || collision.gameObject.tag == "RemoteEnemy" || collision.gameObject.tag == "Bullet")
        {
            mCollidedObject = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MeleeEnemy" || collision.gameObject.tag == "RemoteEnemy" || collision.gameObject.tag == "Bullet") // 普攻对炮台无效
        {
            mCollidedObject = collision.gameObject;
        }
    }

    private void OperateAttackEffect()
    {
        if (mAttackEffectTimer > 0.0f)
        {
            switch (PlayerPrefs.GetInt("SkinNumber", 0))
            {
                case 0:
                    GetComponent<SpriteRenderer>().material.SetFloat("_ShineGlow", 0.6f);
                    float currentWidth = GetComponent<SpriteRenderer>().material.GetFloat("_ShineWidth");
                    float speed = 0.5f / mAttackEffectTime;
                    currentWidth += speed * Time.deltaTime;
                    GetComponent<SpriteRenderer>().material.SetFloat("_ShineWidth", currentWidth);
                    break;
                case 1:
                    GetComponent<SpriteRenderer>().material.SetFloat("_FishEyeUvAmount", 0.37f);
                    break;
                case 2:
                    GetComponent<SpriteRenderer>().material.SetFloat("_ZoomUvAmount", 1.8f);
                    break;
            }
            mAttackEffectTimer -= Time.deltaTime;
        }
        else
        {
            switch (PlayerPrefs.GetInt("SkinNumber", 0))
            {
                case 0:
                    GetComponent<SpriteRenderer>().material.SetFloat("_ShineGlow", 0.0f);
                    GetComponent<SpriteRenderer>().material.SetFloat("_ShineWidth", 0.05f);
                    break;
                case 1:
                    GetComponent<SpriteRenderer>().material.SetFloat("_FishEyeUvAmount", 0.0f);
                    break;
                case 2:
                    GetComponent<SpriteRenderer>().material.SetFloat("_ZoomUvAmount", 1.0f);
                    break;
            }
        }
    }


    public bool Attack()
    {
        if (!mCollidedObject) return false;

        if (mCollidedObject.gameObject.tag == "Bullet")
        {
            Bullet bullet = mCollidedObject.GetComponent<Bullet>();
            if (!bullet)
            {
                TraceBullet traceBullet = mCollidedObject.GetComponent<TraceBullet>();
                if (!traceBullet)
                    return false;
                traceBullet.Kill();
            }
            else
            {
                bullet.Kill();
            }
        }

        else
        {
            Enemy enemy = mCollidedObject.GetComponent<Enemy>();
            if (!enemy) return false;
            enemy.Damage(mAttack);
        }
        return true;
    }
    
    public void SetHueShift(float hue)
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_HsvShift", hue);
    }

    public void SetSaturation(float saturation)
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_HsvSaturation", saturation);
    }

    public void SetBrightness(float brightness)
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_HsvBright", brightness);
    }

    public void SetAttackEffectTimer()
    {
        mAttackEffectTimer = mAttackEffectTime;
    }

    public void SetCurrentNodeIdx(int InNodeIdx)
    {
        mCurrentNodeIdx = InNodeIdx;
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
