using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TailController))]
public class Lily : MonoBehaviour
{
    [Tooltip("Lily��ͼ")]
    public Sprite[] pic;//��ͼ
    SpriteRenderer sr;//��ͼ������
    // static public int sprite_num = 2;//��ͼ��
    Material mat;//������˸
    [Tooltip("ȫ���ܻ�����")]
    public Animator screenhit;//�ܻ�����
    public Animator restore;

    [Tooltip("�ƶ��ٶ�")]
    public float mSpeed = 5.0f;  //Lily�ƶ��ٶ�

    [Tooltip("����ֵ")]
    public float HP = 1000.0f;  //Lily����ֵ

    [Tooltip("�ܻ����޵�ʱ��")]
    public float mInvincibleTime = 1.0f;  //Lily�ܻ����޵�ʱ��

    public void LilyChangeSprite()// Lily ������ͼ
    {
        // if (n >= pic.Length && n < 0) { n = 0; }
        sr.sprite = pic[PlayerPrefs.GetInt("SkinNumber", 0)];
    }

    private bool mFaceToward = true;  //Lily���� ���� trueΪ�ң�falseΪ��
    private float mInvincibleTimer = 0.0f;  //�޵�ʱ���ʱ��
    private float maxHP;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        mat = GetComponent<SpriteRenderer>().material;
        LilyChangeSprite();
        maxHP = HP;
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
    }

    void LateUpdate()
    {
        if (HP <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float damage) //Lily�ܵ��˺�
    {
        if (mInvincibleTimer <= 0.0f)
        {
            HP -= damage;
            mInvincibleTimer = mInvincibleTime;
            GetComponent<OneShotAudioPlayer>().PlayHurt();
            screenhit.SetTrigger("herohurt");
            mat.EnableKeyword("FLICKER_ON");
            Invoke("Stop_flicker", mInvincibleTime);
        }
    }
    private void Stop_flicker()//ֹͣ��˸
    {
        mat.DisableKeyword("FLICKER_ON");
    }

    private void OnDestroy()
    {
        //if any animation is needed


        GetComponent<TailController>().ClearTail();
    }

    public void SetInvincibleTimer(float inTime)
    {
        mInvincibleTimer = inTime;
    }

    public void Heal(float inHealthMount)
    {
        HP += inHealthMount;
        HP = Math.Clamp(HP, 0, maxHP);
        restore.SetTrigger("herorestore");
    }
}
