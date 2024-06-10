using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Generator_ : MonoBehaviour
{
    public  Transform TargetPrefab;
    public Transform Player;

    public static Target_Generator_ instance;
    private void Awake()
    {
        instance = this;
    }

}
