using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp : MonoBehaviour
{
    public HPcontrol hpcontrol;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hpcontrol.HP < 0.8) hpcontrol.HP += 0.2f;
        else hpcontrol.HP = 0;//reset to full hp
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
