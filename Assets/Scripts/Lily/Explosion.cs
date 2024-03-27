using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Tooltip("爆炸持续时间")]
    public float mLifeTime = 0.25f;
    [Tooltip("特效持续时间")]
    public float mShowTime = 1.25f;

    [Tooltip("爆炸攻击力")]
    public int mAttack = 1000;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mLifeTime -= Time.deltaTime;
        mShowTime -= Time.deltaTime;
        if (mShowTime <= 0.0f) Destroy(gameObject);
        GetComponent<SpriteRenderer>().material.SetFloat("_FadeAmount", 1.25f - mShowTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( mLifeTime >= 0 && (collision.gameObject.tag == "MeleeEnemy" || collision.gameObject.tag == "RemoteEnemy" || collision.gameObject.tag == "TowerEnemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Damage(mAttack);
        }
    }
}
