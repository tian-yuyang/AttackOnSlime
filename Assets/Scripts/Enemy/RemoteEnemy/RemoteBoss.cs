using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBoss : Enemy
{

    public int numBullets = 5;
    public float angle = 45f;
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
        SetSpeed(1.5f);
        SetLife(10);
        Bullet.SetTargetHero(targetHero);
        TraceBullet.SetTargetHero(targetHero);
        Bullet.SetAttack(1);
        TraceBullet.SetAttack(1);
    }

    void Attack()
    {
        GameObject newBullet = null;
        for (float i = -angle; i < angle; i += angle * 2 / (float)numBullets)
        {	
            newBullet = Instantiate(Resources.Load("Prefabs/Enemy/GrowingBullet") as GameObject);
            newBullet.transform.localPosition = transform.localPosition;
            Vector3 v = GetTargetDirection();
            v.z = 0;
            newBullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, v);
            newBullet.transform.Rotate(Vector3.forward, i);
        }
        coolDown.TriggerCoolDown();
    }

    public void SetAngle(float newAngle)
    {
        angle = newAngle;
    }

    public void SetNumBullets(int newNum)
    {
        numBullets = newNum;
    }

    protected override void OnCollisionEnter2D(Collision2D objectName)
    {
        base.OnCollisionEnter2D(objectName);
        if (objectName.gameObject.name == "Map")
        {
            ;
        }
    }
}