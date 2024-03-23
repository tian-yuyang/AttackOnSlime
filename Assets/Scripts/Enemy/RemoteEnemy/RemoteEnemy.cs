using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteEnemy : Enemy
{
	private float escapeDistance = 5f;
	private bool isElite = false;
	private CoolDownBar coolDown = new CoolDownBar();

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
		
		// escape mode
		if (GetTargetDistance() < escapeDistance)
		{
			Escape();
		}
    }

	public void SetElite()
	{
		isElite = true;
		SetLife(30); 
	}
    
    void Init()
	{
		SetSpeed(10f);
		SetLife(10);
	}

	void Attack()
	{
		GameObject newBullet = null;
		if (isElite) 
		{ 
			newBullet = Instantiate(Resources.Load("Prefabs/TraceBullet") as GameObject);
		}
		else
		{
			newBullet = Instantiate(Resources.Load("Prefabs/Bullet") as GameObject);
		}
		
		newBullet.transform.localPosition = transform.localPosition;
		newBullet.transform.rotation = transform.rotation;
		coolDown.TriggerCoolDown();
	}

	void Escape()
	{
		Vector3 p = transform.position;
		p += -speed * Time.smoothDeltaTime * GetTargetDirection();
		transform.position = p;
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
