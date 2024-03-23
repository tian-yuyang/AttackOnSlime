using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float bulletSpeed = 40f;
	static public Hero targetHero;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 v = GetTargetDirection();
        v.z = 0;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, v);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

	private void Move()
	{
		transform.position += transform.up * (bulletSpeed * Time.smoothDeltaTime);
	}

	void Kill()
    {
        Destroy(transform.gameObject);
    }

	static public void SetTargetHero(Hero newTargetHero)
	{
		targetHero = newTargetHero;
	}

	public Vector3 GetTargetDirection()
    {
        return (targetHero.transform.position - transform.position).normalized;
    }

	void OnTriggerEnter2D(Collider2D objectName)
    {
        if (objectName.gameObject.name == "Map")
        {
            Kill();
        }
		if (objectName.gameObject.name == "Hero")
        {
            Kill();
			// targetHero.Damage();
        }
    }
}
