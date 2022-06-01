using System;
using System.Collections;
using System.Collections.Generic;
using FPSController;
using LightingSystem;
using UnityEngine;
using VHS;

public class EnemyController : MonoBehaviour
{
    public enum SphereStates
    {
        Idle,
        Spotted,
        FollowLight,
        FollowPlayer,
        Attack
    }

    private SphereStates _sphereState = SphereStates.Idle;
    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;
    public float speed = 0.001f;
    public bool isPlayerDetected;
    public bool isPointed;
    private bool isLightDetected;
    public float anger ;
    private Vector3 originalPosition;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        originalPosition = transform.position;
        // gameObject.GetComponent<Renderer>().material.color = Color.clear;
    }

    private void FixedUpdate()
    {
        if (_sphereState == SphereStates.Idle)
        {
            Idle();
        }
        
    }

    private void Update()
    {
        if (_sphereState == SphereStates.Idle)
        {
            if (isPlayerDetected || isPointed)
            {
                _sphereState = SphereStates.Spotted;
                animator.SetBool("IsPlayerDetected",isPlayerDetected);
            }
            LookForPlayer();
            
        }

        if (_sphereState == SphereStates.Spotted)
        {
            originalPosition = transform.position;
            // gameObject.GetComponent<Renderer>().material.color = Color.red;
            if (anger<=0 && (isPlayerDetected && isPointed) )
            {
                _sphereState = SphereStates.Idle;
            }
            if (anger>80)
            {
                Shake();
            } 
            if (anger>100)
            {
                _sphereState = SphereStates.FollowPlayer;
            }
        }

        if (_sphereState==SphereStates.FollowPlayer)
        {
            FollowPlayer();
        }
        
    }


    private void Idle()
    {
        var Offset = 0.03f * Mathf.Sin (Time.fixedTime * 1);
        transform.position = originalPosition + new Vector3 (0, Offset, 0);
        
    }

    private void Shake()
    {
        var Offset = 0.008f * Mathf.Cos(Time.fixedTime * anger);
        transform.position = originalPosition + new Vector3 (Offset, 0, 0);
        // gameObject.transform.position = new Vector3(1 + Mathf.Sin(anger) / 20,  transform.position.y, transform.position.z);
    }

    private void LookForPlayer()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = FirstPersonController.Instance.transform.position - enemyPosition;
        toPlayer.y = 0;
        if (toPlayer.magnitude <= detectionRadius)
        {
            if (Vector3.Dot(toPlayer.normalized, transform.forward) > Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                isPlayerDetected = true;
                anger += 10;
                Debug.Log("Player has been detected!");
            }
        }
        else
        {
            isPlayerDetected = false;
            animator.SetBool("IsPlayerDetected",isPlayerDetected);
        }
    }

    private void FollowPlayer()
    {
        if (isPlayerDetected)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                FirstPersonController.Instance.transform.position,
                (speed*Time.deltaTime) / 20);
        }
    }

    // private void CheckForLight()
    // {
    // }
    //
    // private void OnTriggerStay(Collider other)
    // {
    //     
    //     if (other.GetComponent<LightExtended>())
    //     {
    //         Debug.Log("LOL");
    //         other.GetComponent<LightExtended>().LightTypeP = LightExtended.LightType.Flickering;
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.GetComponent<LightExtended>())
    //     {
    //         Debug.Log("LOL EXIT");
    //         other.GetComponent<LightExtended>().isOn= false;
    //     }
    // }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0.8f, 0, 0, 0.4f);
        UnityEditor.Handles.color = c;

        Vector3 rotatedForward = Quaternion.Euler(
            0,
            -detectionAngle * 0.5f,
            0) * transform.forward;
        UnityEditor.Handles.DrawSolidDisc(gameObject.transform.position,gameObject.transform.up,2);
        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            rotatedForward,
            detectionAngle,
            detectionRadius);

    }
#endif
}
