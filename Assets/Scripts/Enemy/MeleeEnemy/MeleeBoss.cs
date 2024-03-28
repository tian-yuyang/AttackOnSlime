using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBoss : Enemy
{
    public float rushSpeed = 30f;
    private int counter = 0;
    private const int STOP = 0;
    private const int MOVE = 1;
    private const int RUSH = 2;
    private Vector3 direction;
    private float distance = 0f;
    private int status = MOVE;

    static public UIManager uiManager;

    private GameObject rectangle = null;

    public CoolDownBar coolDown = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
    }

    static public void SetUI(UIManager newUI)
    {
        uiManager = newUI;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // be rushing to Lily
        if (status == RUSH)
        {
            Rush();
            return;
        }

        if (status == STOP)
        {
            return;
        }

        // alert mode
        if (GetTargetDistance() < alertDistance)
        {
            Attack();
            if (coolDown.ReadyForNext())
            {
                if (counter % 4 == 0)
                {
                    AdvanceAttack();
                }
                counter = (counter + 1) % 60;
                coolDown.TriggerCoolDown();
            }

        }
    }

    void Init()
    {
        status = MOVE;
        SetSpeed(5f);
        SetLife(50);
        SetAttack(1);
    }

    void Attack()
    {
        Vector3 p = transform.position;
        p += speed * Time.smoothDeltaTime * GetTargetDirection();
        transform.position = p;
    }

    void AdvanceAttack()
    {
        direction = GetTargetDirection();
        distance = GetTargetDistance();

        rectangle = new GameObject("Rectangle");
        rectangle.AddComponent<SpriteRenderer>().color = Color.red;
        SpriteRenderer spriteRenderer = rectangle.GetComponent<SpriteRenderer>();
        Sprite newSprite = Resources.Load<Sprite>("Squ");
        spriteRenderer.sprite = newSprite;

        Vector3 scale = new Vector3(distance, 1f, 1f);
        rectangle.transform.localScale = scale;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectangle.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 position = transform.position + direction * (distance / 2f);
        rectangle.transform.position = position;

        status = STOP;
        Invoke("ChangeToRush", 0.5f);
    }

    void ChangeToRush()
    {
        Destroy(rectangle.transform.gameObject);
        status = RUSH;
    }

    void Rush()
    {
        Debug.Log(distance);
        transform.position += rushSpeed * Time.smoothDeltaTime * direction;
        distance -= rushSpeed * Time.smoothDeltaTime;
        if (distance < 0)
        {
            status = MOVE;
        }
    }

    override public void Kill()
    {
        anim.SetBool("dead", true);
        Invoke("Destroy", 1.0f);
        uiManager.Win();
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