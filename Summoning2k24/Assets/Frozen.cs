using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : MonoBehaviour
{
    public GameObject ThawObject;
    public bool thaw_on;

    [Header("SerializeFields")]
    [SerializeField] private Transform radiusCheck;
    [SerializeField] private LayerMask fireLayer;


    void Update()
    {
        if (IsHeated())
            ThawObject.SetActive(thaw_on);
        else ThawObject.SetActive(!thaw_on);
    }

    bool IsHeated()
    {
        return Physics2D.OverlapCircle(radiusCheck.position, 1.9f, fireLayer);
    }
}
