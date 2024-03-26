using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ABC
public class Enemy : MonoBehaviour
{
    private int life = 10;
    private int attack = 1;
	private int damage = 0;
    public float speed;
    public float alertDistance = 70f;
	public Vector3 originPosition;

    public Lily targetHero;

	// anitmation field
	public Animator anim;
	public HPController hpcontrol;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // transform.position = originPosition;
		originPosition = transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
		// life control and HP display
		hpcontrol.HP = (float)damage / (float)life;
		Debug.Log(hpcontrol.HP);
        if (life <= damage)
        {
            Kill();
        }

		// automatically go back
		if (GetTargetDistance() > alertDistance && !IsReturned())
		{
			ReturnToOrigin();
		}
    }

    public void Damage(int newDamage)
    {
		anim.SetTrigger("hurt_trig");
        damage += newDamage;
    }

	public void Heal(int newHeal)
    {
        damage -= newHeal;
		if (damage < 0)
		{
			damage = 0;
		}
    }

    public void Kill()
    {
		anim .SetBool("dead", true);
		Invoke("Destroy", 1.0f);
    }

    public void Destroy()
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

    public void SetTargetHero(Lily newTargetHero)
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

	public void SetAttack(int newAttack)
	{
		attack = newAttack;
	}
	
	public int GetAttack()
	{
		return attack;
	}

    protected virtual void OnCollisionEnter2D(Collision2D objectName)
    {
        if (objectName.gameObject.tag == "Map")
        {
            ;
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D objectName)
    {
        if (objectName.gameObject.tag == "Map")
        {
            ;
        }
    }
}
