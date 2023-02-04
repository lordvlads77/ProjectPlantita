using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    public float ForceMove;
    public float ForceJump;
    public Vector2 move;
    [SerializeField] private float maxSpeed = default;

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

    private void Awake()
    {
    }

    void Start()
    {
        theRigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        isGround = Physics2D.OverlapCircle(transform.position + checkFloorPos, checkFloorRadio, checkFloorMask);
        theRigid.AddForce(move * ForceMove);
        Flip(move.x);
        
        if (ForceMove >= maxSpeed)
        {
            ForceMove = maxSpeed;
            Debug.Log(ForceMove);
        }
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
        if (Input.GetKeyUp(KeyCode.A))
        {
            StartCoroutine(desacceleration());
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            StartCoroutine(desacceleration());
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

    IEnumerator acceleration()
    {
        yield return new WaitForSeconds(0.5f);
        ForceMove = maxSpeed;
        Debug.Log("Max Speed");
        yield break;
    }

    IEnumerator desacceleration()
    {
        yield return new WaitForSeconds(0.3f);
        ForceMove = 3;
        yield return new WaitForSeconds(0.5f);
        ForceMove = maxSpeed;
        yield break;
    }
}
