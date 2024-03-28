using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStone : MonoBehaviour
{
    private int heal = 3;
 
    static public Lily targetHero;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    static public void SetTargetHero(Lily newTargetHero)
    {
        targetHero = newTargetHero;
    }
    
    public void Kill()
    {
        Destroy(transform.gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D objectName)
    {
        if (objectName.gameObject.tag == "Player")
        {
            targetHero.Damage(-heal);
            Kill();
        }
    }
}
