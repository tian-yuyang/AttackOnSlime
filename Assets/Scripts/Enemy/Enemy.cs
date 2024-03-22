using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ABC
public class Enemy : MonoBehaviour
{
    private int life;
    public float speed;
    public float alertDistance = 10f;

    // not implemented about hero
    public int targetHero;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (life <= 0)
        {
            Kill();
        }
    }

    public void Damage(int damage)
    {
        life -= damage;
    }

    public void Kill()
    {
        Destroy(transform.gameObject);
    }

    public void SetLife(int newLife)
    {
        life = newLife;
    }
    
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetTargetHero(int newTargetHero)
    {
        targetHero = newTargetHero;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public float GetTargetDistance()
    {
        return 0f;
        // return (transform.position - targetHero.transform.position).magnitude;
    }

    public Vector3 GetTargetDirection()
    {
        return new Vector3(1,1,1);
        // return (targetHero.transform.position - transform.position).Normalize();
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D objectName)
    {
        if (objectName.gameObject.name == "Map")
        {
            ;
        }
    }
}
