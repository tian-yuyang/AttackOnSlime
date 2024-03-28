using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required for UI components like Text and Slider
using UnityEngine;

public class CDBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Image Back = null;
    public Image Front = null;
    public Image Frame = null;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Image getBack()
    {
        return Back;
    }
    public Image getFront()
    {
        return Front;
    }
    public Image getFrame()
    {
        return Frame;
    }
}
