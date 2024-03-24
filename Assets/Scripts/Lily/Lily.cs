using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;

public class Lily : MonoBehaviour
{
    public float mSpeed = 5.0f;  //Lily移动速度

    private float mAngle = 0.0f; //Lily面朝角度

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 faceToward = Vector2.zero;
        faceToward.x = Input.GetAxis("Vertical");
        faceToward.y = Input.GetAxis("Horizontal");

        float angle = Mathf.Atan2(faceToward.y,faceToward.x) * Mathf.Rad2Deg;
        mAngle = faceToward.x == 0 && faceToward.y == 0 ? mAngle : angle;
        
        transform.localRotation = Quaternion.Euler(0, 0, -mAngle);

        if (faceToward.x != 0 || faceToward.y != 0)
            transform.Translate(transform.up * mSpeed * Time.smoothDeltaTime, Space.World);
    }

}
