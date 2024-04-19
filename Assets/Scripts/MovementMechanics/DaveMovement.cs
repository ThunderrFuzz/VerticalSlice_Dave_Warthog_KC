using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveMovement : BasePlayerMovement
{
    [Header("DaveMovement")]
    public float daveMove;
       
    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = base.baseSpeed + daveMove;
    }

    // Update is called once per frame
    void Update()
    {
        base.HandleInput();
        base.MoveCharacter();
        base.ApplyGravity();
    }
}
