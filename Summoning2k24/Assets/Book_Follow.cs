using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_Follow : MonoBehaviour
{
    public GameObject Target; 
    
    void Start()
    {
     
    }
    void Update()
    {
       transform.position = Vector2.MoveTowards(transform.position , Target.transform.position , 5 * Time.deltaTime);
       
     
   
    }
}