using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSquare : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 scale = transform.localScale;
        scale.x = Camera.main.aspect * scale.y;
        transform.localScale = scale;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
