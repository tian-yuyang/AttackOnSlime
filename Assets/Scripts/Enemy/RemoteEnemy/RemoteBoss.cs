/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBoss : Enemy
{

    public int numBullets = 5;
    public float angle = 45f;
    public CoolDownBar coolDown = null;

    private int counter = 0;

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
            if (counter % 4 == 0)
            {
                AdvanceAttack();
            }
            else
            {
                Attack();
            }
            counter = (counter + 1) % 60;
        }
    }

    void Init()
    {
        SetSpeed(1.5f);
        SetLife(10);
        Bullet.SetTargetHero(targetHero);
        TraceBullet.SetTargetHero(targetHero);
        GrowingBullet.SetTargetHero(targetHero);
        Bullet.SetAttack(1);
        TraceBullet.SetAttack(1);
        GrowingBullet.SetAttack(1);
    }

    void AdvanceAttack()
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
    
    void Attack()
    {
        GameObject newBullet = null;
        newBullet = Instantiate(Resources.Load("Prefabs/Enemy/Bullet") as GameObject);
        newBullet.transform.localPosition = transform.localPosition;
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
}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBoss : Enemy
{

    public int numBullets = 5;
    public float angle = 45f;
    public CoolDownBar coolDown = null;
    static public UIManager uiManager;

    private int counter = 0;

    // Start is called before the first frame update

    static public void SetUI(UIManager newUI)
    {
        uiManager = newUI;
    }
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
            if (counter % 4 == 0)
            {
                AdvanceAttack();
            }
            else
            {
                Attack();
            }
            counter = (counter + 1) % 60;
        }
    }

    void Init()
    {
        SetSpeed(1.5f);
        SetLife(10);
        Bullet.SetTargetHero(targetHero);
        TraceBullet.SetTargetHero(targetHero);
        GrowingBullet.SetTargetHero(targetHero);
        Bullet.SetAttack(1);
        TraceBullet.SetAttack(1);
        GrowingBullet.SetAttack(1);
    }

    void AdvanceAttack()
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

    override public void Kill()
    {
        anim.SetBool("dead", true);
        Invoke("Destroy", 1.0f);
        uiManager.Win();
    }
    void Attack()
    {
        GameObject newBullet = null;
        newBullet = Instantiate(Resources.Load("Prefabs/Enemy/Bullet") as GameObject);
        newBullet.transform.localPosition = transform.localPosition;
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