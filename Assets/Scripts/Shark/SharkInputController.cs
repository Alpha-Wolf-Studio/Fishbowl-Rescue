using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkInputController : MonoBehaviour
{
    [SerializeField] private SharkStats _stats;

    public Action OnSharkMove;
    public Action OnSharkPatrol;
    public Action OnSharkAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
