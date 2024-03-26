using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(TailController))]
public class Lily : MonoBehaviour
{
    [Tooltip("Lily贴图")]
    public Sprite[] pic;//贴图
    SpriteRenderer sr;//贴图父对象
    static public int sprite_num = 2;//贴图号
    Material mat;//调整闪烁
    [Tooltip("全屏受击动画")]
    public Animator screenhit;//受击动画

    [Tooltip("移动速度")]
    public float mSpeed = 5.0f;  //Lily移动速度

    [Tooltip("生命值")]
    public float HP = 1000.0f;  //Lily生命值

    [Tooltip("受击后无敌时间")]
    public float mInvincibleTime = 1.0f;  //Lily受击后无敌时间

    public void LilyChangeSprite(int n)// Lily 更换贴图
    {
        if (n >= pic.Length && n < 0) { n = 0; }
        sr.sprite = pic[sprite_num];
    }

    private bool mFaceToward = true;  //Lily朝向 —— true为右，false为左
    private float mInvincibleTimer = 0.0f;  //无敌时间计时器


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVec = Vector2.zero;
        moveVec.y = Input.GetAxis("Vertical");
        moveVec.x = Input.GetAxis("Horizontal");
        if (moveVec.x > 0)
        {
            mFaceToward = true;
        }
        else if (moveVec.x < 0)
        {
            mFaceToward = false;
        }

        GetComponent<SpriteRenderer>().flipX = !mFaceToward;

        transform.Translate(moveVec.y * Vector3.up * mSpeed * Time.smoothDeltaTime, Space.World);
        transform.Translate(moveVec.x * Vector3.right * mSpeed * Time.smoothDeltaTime, Space.World);

        if (mInvincibleTimer > 0.0f) mInvincibleTimer -= Time.deltaTime;

        // if (HP <= 0.0f)
        // {
        //     Destroy(gameObject);
        // }
    }

    void LateUpdate()
    {
        if (HP <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float damage) //Lily受到伤害
    {
        if (mInvincibleTimer <= 0.0f)
        {
            HP -= damage;
            mInvincibleTimer = mInvincibleTime;
            screenhit.SetTrigger("herohurt");
            mat.EnableKeyword("FLICKER_ON");
            Invoke("Stop_flicker", mInvincibleTime);
        }
    }
    private void Stop_flicker()//停止闪烁
    {
        mat.DisableKeyword("FLICKER_ON");
    }

    private void OnDestroy()
    {
        //if any animation is needed


        GetComponent<TailController>().ClearTail();
    }
}
