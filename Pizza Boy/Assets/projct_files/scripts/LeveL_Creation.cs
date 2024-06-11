using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create_LeveL",menuName ="Level")]
public class LeveL_Creation :ScriptableObject
{
    public int no_of_target;
    public GameObject[] target = new GameObject[0];
  
    public int[] pizza = new int[0];
    public int Cash;
    public float timer;
}
