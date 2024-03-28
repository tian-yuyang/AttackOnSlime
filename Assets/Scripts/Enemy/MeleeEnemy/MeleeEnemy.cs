using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
	private bool isElite = false;

    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
        Init();
    }

    // Update is called once per frame
    protected override void Update()
    {
		base.Update();
        // alert mode
		if (GetTargetDistance() < alertDistance)// && coolDown.ReadyForNext())
		{
			Attack();
		}
    }

	public void SetElite()
	{
		isElite = true;
		SetLife(30);
	}
    
    void Init()
	{
		SetSpeed(3f);
		SetLife(50);
		SetAttack(1);
	}

	void Attack()
	{
		Vector3 p = transform.position;
		p += speed * Time.smoothDeltaTime * GetTargetDirection();
		transform.position = p;
		// coolDown.TriggerCoolDown();
	}

    protected override void OnCollisionStay2D(Collision2D objectName)
    {
		base.OnCollisionStay2D(objectName);

        if (objectName.gameObject.tag == "Player")
        {
	        targetHero.Damage(GetAttack());
        }
    }
}
