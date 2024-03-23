using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : Enemy
{
	public CoolDownBar coolDown = null;
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
		if (GetTargetDistance() < alertDistance && coolDown.ReadyForNext())
		{
			Attack();
		}
    }
    
    void Init()
	{
		SetSpeed(0f);
		SetLife(10);
		Bullet.SetTargetHero(targetHero);
		TraceBullet.SetTargetHero(targetHero);
	}

	void Attack()
	{
		GameObject newBullet = Instantiate(Resources.Load("Prefabs/Bullet") as GameObject);
		newBullet.transform.localPosition = transform.localPosition;
		coolDown.TriggerCoolDown();
	}

	protected override void OnTriggerEnter2D(Collider2D objectName)
    {
		base.OnTriggerEnter2D(objectName);
        if (objectName.gameObject.name == "Map")
        {
            ;
        }
    }
}
