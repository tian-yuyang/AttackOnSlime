using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    public GameObject HP_Obj = null;
    public float HP = 0f;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = HP_Obj.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("_ClipUvRight", HP);// 0 is full and 1 means die
    }
}
