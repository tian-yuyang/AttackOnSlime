using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceBullet : MonoBehaviour
{
    private float bulletSpeed = 40f;
    private int life = 400;
    static private Lily targetHero;

	static public int attack = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        life--;
        if (life <= 0)
        {
            Kill();
        }
    }

    private void Move()
    {
        // rotate to hero and move forward
        Vector3 v = GetTargetDirection();
        v.z = 0;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, v);
        transform.position += transform.up * bulletSpeed * Time.smoothDeltaTime;
    }

    void Kill()
    {
        Destroy(transform.gameObject);
    }
    
    public Vector3 GetTargetDirection()
    {
        return (targetHero.transform.position - transform.position).normalized;
    }

    static public void SetTargetHero(Lily newTargetHero)
    {
        targetHero = newTargetHero;
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