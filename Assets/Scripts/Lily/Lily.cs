using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lily : MonoBehaviour
{
    public float mSpeed = 5.0f;  //Lily移动速度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO(Hangyu) : 更复杂的移动方式
        transform.Translate(Input.GetAxis("Vertical") * transform.up * mSpeed * Time.smoothDeltaTime, Space.World);
        transform.Translate(Input.GetAxis("Horizontal") * transform.right * mSpeed * Time.smoothDeltaTime, Space.World);
    }
}
