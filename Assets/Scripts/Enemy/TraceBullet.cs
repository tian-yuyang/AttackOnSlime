using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceBullet : MonoBehaviour
{
    private float bulletSpeed = 40f;
    private int life = 400;
    static private Lily targetHero;
    
    // anitmation field
    public Animator anim; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        life--;
        if (life <= 0)
        {
            Kill();
        }
    }

    private void Move()
    {
        // rotate to hero and move forward
        Vector3 v = GetTargetDirection();
        v.z = 0;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, v);
        transform.position += transform.up * bulletSpeed * Time.smoothDeltaTime;
    }

    void Kill()
    {
        anim .SetBool("dead", true);
        Destroy(transform.gameObject);
    }
    
    public Vector3 GetTargetDirection()
    {
        return (targetHero.transform.position - transform.position).normalized;
    }

    static public void SetTargetHero(Lily newTargetHero)
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