using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // Start is called before the first frame update
  
    private void find_enemy (){
        GameObject m;
        m = GameObject.Find("enemy");
        if (m == null) { m = GameObject.Find("enemy(Clone)"); }
        if (m == null) {
            GameObject e = Instantiate(Resources.Load("Prefabs/enemy") as GameObject);
            e.transform.localPosition = transform.localPosition;
            e.transform.up = transform.up;
            Debug.Log("new enemy.");
        }
        
    }
    void Start()
    {
        InvokeRepeating("find_enemy", 1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
