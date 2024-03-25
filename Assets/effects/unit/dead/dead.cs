using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dead : MonoBehaviour
{
    public Animator anim; 
    public void dead_true()
    {
        anim .SetBool("dead", true);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        dead_true();
        Debug.Log("HIT");
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
