using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    public float ForceMove;
    public float ForceJump;
    private Vector2 move;

    [Header("Check Ground")] 
    public Vector3 checkFloorPos;
    public float checkFloorRadio;
    public LayerMask checkFloorMask;
    public bool isGround;

    private Rigidbody2D theRigid;
    
    [Header("FlipPlayer")]
    [SerializeField] private KeyCode _horizonL = default;
    [SerializeField] private KeyCode _horizonR = default;

    [Header("Angles")] 
    [SerializeField] private float pos180 = default;
    [SerializeField] private float neg180 = default;
    void Start()
    {
        theRigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        isGround = Physics2D.OverlapCircle(transform.position + checkFloorPos, checkFloorRadio, checkFloorMask);
        theRigid.AddForce(move * ForceMove);
        //Flipeando(move.x);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            theRigid.AddForce(Vector2.up * ForceJump, ForceMode2D.Impulse);
        }

        /*if (Input.GetKey(KeyCode.Space))
        {
            
        }*/
        //Functions for Animators goes here.

        if (Input.GetKeyDown(_horizonL))
        {
            transform.Rotate(0f, pos180, 0f);
        }
        if (Input.GetKeyDown(_horizonR))
        {
            transform.Rotate(0f,neg180,0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + checkFloorPos, checkFloorRadio);
    }

    private void Flip(float _dirX)
    {
        Vector3 localScale = transform.localScale;
        if (_dirX > 0)
        {
            localScale.x = 1f;
        }
        else if (_dirX < 0f)
        {
            localScale.x = -1f;
        }
        transform.localScale = localScale;
    }
}
