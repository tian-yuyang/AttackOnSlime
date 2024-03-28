using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : Enemy
{
	public CoolDownBar coolDown = null;
	public TowerBoss targetBoss = null;
	private int coolCount = 0;
	public int coolDownInterval = 10;

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
		if (GetTargetDistance() < alertDistance && coolDown.ReadyForNext())
		{
			if (isElite)
			{
				coolCount += 1;
				if (coolCount % coolDownInterval == 0)
				{
					GenerateEnemy();
				}
				else
				{
					coolDown.TriggerCoolDown();
				}
			}
			else
			{
				Attack();
			}
		}
    }
    
    void Init()
	{
		SetSpeed(0f);
		SetLife(10);
		Bullet.SetTargetHero(targetHero);
		TraceBullet.SetTargetHero(targetHero);
		Bullet.SetAttack(1);
		TraceBullet.SetAttack(1);
	}

	void Attack()
	{
		GameObject newBullet = Instantiate(Resources.Load("Prefabs/Enemy/Bullet") as GameObject);
		newBullet.transform.position = transform.position;
		coolDown.TriggerCoolDown();
	}

	public void GenerateEnemy()
	{
		GameObject newEnemy = Instantiate(Resources.Load("Prefabs/Enemy/MeleeEnemy") as GameObject);
		newEnemy.transform.position = transform.position;
		coolDown.TriggerCoolDown();
	}

	public void SetElite(TowerBoss newBoss)
	{
		isElite = true;
		targetBoss = newBoss;
	}

	protected override void OnCollisionEnter2D(Collision2D objectName)
    {
		base.OnCollisionEnter2D(objectName);
    }
}
