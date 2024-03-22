using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float bulletSpeed = 40f;
	private int targetHero;
    // Start is called before the first frame update
    void Start()
    {
        
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

	public void SetTargetHero(int newTargetHero)
	{
		targetHero = newTargetHero;
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
