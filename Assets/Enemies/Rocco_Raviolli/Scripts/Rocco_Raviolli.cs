﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocco_Raviolli : MonoBehaviour
{
    private Transform playerPos;

    private float agroRange;
    private float distanceToFloor;

    [SerializeField] float moveSpeed;
    [SerializeField] float underRange;

    private Vector2 OrigPosition;

    private Rigidbody2D rb2d;
    private bool InRange;
    private bool IsFalling;
    private bool IsGrounded;
    private bool Reset;
    [SerializeField] BoxCollider2D collision;
    [SerializeField] BoxCollider2D trigger;
    private CircleCollider2D bottom;
    private ContactFilter2D contacts;
    private List<RaycastHit2D> hitResults = new List<RaycastHit2D>(16);

    // Start is called before the first frame update
    void Start()
    {
        //Sets rigidbody
        rb2d = GetComponent<Rigidbody2D>();
        bottom = GetComponent<CircleCollider2D>();

        //Gets original position to return to after stomp
        OrigPosition = transform.position;
        //Gets height of object using RayCast
        distanceToFloor = OrigPosition[1];
        //agrorange is now just slightly to the sides of the stomping enemy
        agroRange = distanceToFloor + 1;

        InRange = false;
        IsFalling = false;
        IsGrounded = false;
        Reset = false;

        contacts.useTriggers = false;
        contacts.SetLayerMask(LayerMask.GetMask("ground"));
        contacts.useLayerMask = true;
    }

    void Update(){
        // if(playerPos)
        //     Debug.Log(playerPos.position);

        //check to see if player is under rocco
        if(CheckIfPlayerUnder()){
            IsFalling = true;
        }
    }
    void FixedUpdate()
    {
        //Checks Distance to player
        // float distToPlayer = Vector2.Distance(transform.position, player.position);
        

        // if(distToPlayer < agroRange)
        // {
        //     //Stomp on Player
        //     stomp();
        // }
        // else
        // {
        //     unstomp();
        // }
        Vector3 oldPos = rb2d.position;

        // if(newPos == oldPos && IsFalling){
        //     IsGrounded = true;
        //     Debug.Log("Hit ground");
        // }
        
        if(IsFalling && !IsGrounded && !Reset){
            Vector2 down = new Vector2(rb2d.position.x, rb2d.position.y - moveSpeed * Time.deltaTime);
            int hit = Physics2D.Raycast(bottom.transform.position, Vector2.down, contacts, hitResults, .1f);
            if(hitResults.Count > 0)
                Debug.DrawLine(rb2d.position, hitResults[0].point, Color.black);
                Debug.Log("Hit ground");
            rb2d.MovePosition(down);
        }
        Vector3 newPos = rb2d.position;

    }

    // use enter trigger to check if rocco should be preped
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        if(player){
            Debug.Log("Player enter trigger");
            playerPos = player.gameObject.GetComponent<Transform>();
            if(playerPos)
                Debug.Log("Got player transform");
            InRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        if(player){
            playerPos = null;
            Debug.Log("Player exit trigger");
            InRange = false;
        }
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     Grid ground = other.gameObject.GetComponent<Tile>();
    //     if()
    // }

    private bool CheckIfPlayerUnder(){
        if(playerPos){
            if(playerPos.position.x > rb2d.position.x - underRange && playerPos.position.x < rb2d.position.x + underRange){
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    void stomp() {
        //if the player is withing 1 unit to the left and right of the enemy
        // if (transform.position.y > player.position.y)
        // {
        //     if (transform.position.y >= OrigPosition.y)
        //     {
        //         if (transform.position.x < player.position.x + 1)
        //         {
        //             if (transform.position.x > player.position.x - 1)
        //             {

        //                 //stomp code here
        //                 rb2d.velocity = new Vector2(0, -moveSpeed);
        //             }
        //         }
        //     }
        // }
    }

    void unstomp() {
        if(transform.position.y < OrigPosition.y)
        {
            rb2d.velocity = new Vector2(0, 1);
        }
        else { rb2d.velocity = new Vector2(0, 0); };
    }
}
