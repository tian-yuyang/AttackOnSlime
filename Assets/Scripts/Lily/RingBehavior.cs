using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
public class RingBehavior : MonoBehaviour
{
    [Tooltip("环持续时间")]
    public float mLifeTime = 1.0f;

    [Tooltip("环攻击伤害")]
    public int mAttack = 1000;
    
    private List<Vector2> mPoints = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        polygonCollider.SetPath(0, mPoints);
        polygonCollider.offset = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        mLifeTime -= Time.deltaTime;
        if(mLifeTime <= 0.0f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MeleeEnemy" || collision.gameObject.tag == "RemoteEnemy" || collision.gameObject.tag == "TowerEnemy")
        {
            collision.gameObject.GetComponent<Enemy>().Damage(mAttack);
        }
    }

    public void SetColliderPoints(List<Vector2> points)
    {
        mPoints = points;
    }

    public List<Vector2> GetColliderPoints()
    {
        return mPoints;
    }
}
