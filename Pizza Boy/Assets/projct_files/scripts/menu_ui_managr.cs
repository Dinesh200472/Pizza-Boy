using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_ui_managr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(CycleController.isfinished)
        {
            Destroy(gameObject,1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
