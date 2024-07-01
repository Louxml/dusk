using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAnimotar : MonoBehaviour
{
    public GameObject objectToToggle1;
    public GameObject objectToToggle2;
    public GameObject objectToToggle3;
    public GameObject objectToToggle4;
    public GameObject objectToToggle5;
    public GameObject objectToToggle6;
    public GameObject objectToToggle7;
    public GameObject objectToToggle8;
    public GameObject objectToToggle9;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    // 通过调用这个方法来切换可见性
    public void SetVisibility(bool isVisible,int _index)
    {
        if (_index == 1)
        {
            objectToToggle1.GetComponent<Renderer>().enabled = isVisible;
            objectToToggle2.GetComponent<Renderer>().enabled = isVisible;
            objectToToggle3.GetComponent<Renderer>().enabled = isVisible;

        }
        if (_index == 2)
        {
            objectToToggle4.GetComponent<Renderer>().enabled = isVisible;
            objectToToggle5.GetComponent<Renderer>().enabled = isVisible;
            objectToToggle6.GetComponent<Renderer>().enabled = isVisible;

        }
        if (_index == 3)
        {
            objectToToggle7.GetComponent<Renderer>().enabled = isVisible;
            objectToToggle8.GetComponent<Renderer>().enabled = isVisible;
            objectToToggle9.GetComponent<Renderer>().enabled = isVisible;
        }
        

    }
}
