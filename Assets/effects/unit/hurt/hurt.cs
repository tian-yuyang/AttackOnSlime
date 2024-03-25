using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurt : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    public void hurt_trigger() {
        anim.SetTrigger("hurt_trig");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hurt_trigger();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
