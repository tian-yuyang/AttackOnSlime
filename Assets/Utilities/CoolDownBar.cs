using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownBar : MonoBehaviour
{
    private float coolTime = 2.0f;
    private float timePassed = 0f;
    private bool isCoolDown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > coolTime)
        {
            isCoolDown = true;
        }
    }

    public void TriggerCoolDown()
    {
        isCoolDown = false;
        timePassed = 0f;
    }

    public bool ReadyForNext()
    {
        return isCoolDown;
    }
}
