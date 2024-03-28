using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStone : MonoBehaviour
{
    private int heal = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Kill()
    {
        Destroy(transform.gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D objectName)
    {
        if (objectName.gameObject.tag == "Player")
        {
            //targetHero.Damage(-heal);
            Kill();
        }
    }
}
