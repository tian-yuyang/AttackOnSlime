using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ABC
public class Enemy : MonoBehaviour
{
    private int life = 10;
    public float speed;
    public float alertDistance = 70f;
	public Vector3 originPosition;

    // not implemented about hero
    public Hero targetHero;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // transform.position = originPosition;
		originPosition = transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (life <= 0)
        {
            Kill();
        }
		if (GetTargetDistance() > alertDistance && !IsReturned())
		{
			ReturnToOrigin();
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

    public void SetTargetHero(Hero newTargetHero)
    {
        targetHero = newTargetHero;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

	public float GetOriginDistance()
	{
		Vector3 v = transform.position - originPosition;
		v.z = 0;
		return v.magnitude;
	}

	public Vector3 GetOriginDirection()
    {
        Vector3 v = originPosition - transform.position;
		v.z = 0;
		return v.normalized;
    }

    public float GetTargetDistance()
    {
        Vector3 v = transform.position - targetHero.transform.position;
		v.z = 0;
		return v.magnitude;
    }

    public Vector3 GetTargetDirection()
    {
        Vector3 v = targetHero.transform.position - transform.position;
		v.z = 0;
		return v.normalized;
    }

	bool IsReturned()
	{
		if (GetOriginDistance() < 1f)
		{
			return true;
		}
		return false;
	}

	void ReturnToOrigin()
	{
		Vector3 p = transform.position;
		p += speed * Time.smoothDeltaTime * GetOriginDirection();
		transform.position = p;
	}
    
    protected virtual void OnTriggerEnter2D(Collider2D objectName)
    {
        if (objectName.gameObject.name == "Map")
        {
            ;
        }
    }
}
