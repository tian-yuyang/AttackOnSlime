using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : MonoBehaviour
{
    public float mHeroSpeed = 20f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Now spawn an egg when space bar is hit
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        transform.position = p;
    }
}
