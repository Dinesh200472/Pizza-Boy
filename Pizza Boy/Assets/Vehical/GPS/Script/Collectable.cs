using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public  Transform TargetPrefab;
    public Transform Player;

    public static Collectable instance;
    private void Awake()
    {
        instance = this;
    }

}
