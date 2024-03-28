using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBullet : MonoBehaviour
{
    static public float bulletSpeed = 5f;
    private float targetScale = 2f;
    private float growingSpeed = 1.01f;

    static public Lily targetHero;

    static public int attack = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Expand();
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * (bulletSpeed * Time.smoothDeltaTime);
    }

    private void Expand()
    {
        Vector3 currentScale = transform.localScale * growingSpeed;
        if (transform.localScale.x < targetScale)
        {
            transform.localScale = currentScale;
        }
    }

    public void Kill()
    {
        Destroy(transform.gameObject);
    }

    static public void SetTargetHero(Lily newTargetHero)
    {
        targetHero = newTargetHero;
    }

    public Vector3 GetTargetDirection()
    {
        Vector3 v = targetHero.transform.position - transform.position;
        v.z = 0;
        return v.normalized;
    }

    static public void SetAttack(int newAttack)
    {
        attack = newAttack;
    }

    void OnTriggerEnter2D(Collider2D objectName)
    {
        if (objectName.gameObject.tag == "Map")
        {
            Kill();
        }
        if (objectName.gameObject.tag == "Player")
        {
            targetHero.Damage(attack);
            Kill();
        }
    }
}