﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerPlatformerController player;
    private Vector3 playerStart;
    private Checkpoint lastCheckpoint;
    
    private void OnEnable() {
        PlayerPlatformerController.Killed += Respawn;
    }

    private void OnDisable() {
        PlayerPlatformerController.Killed -= Respawn;
    }
        void Start()
    {
        player = FindObjectOfType<PlayerPlatformerController>();
        if(player){
            // Debug.Log("FOund player");
            playerStart = player.GetRBPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn(int health){
        Debug.Log("Respawn");
        player.ResetGrapple();
        Rigidbody2D playerRB = player.GetPlayerRB();
        playerRB.position = lastCheckpoint.GetPlayerPosition();
        player.SetCurrenthealth(health);
        playerRB.velocity = Vector3.zero;
    }
    public void setLastCheckpoint(Checkpoint cp){
        lastCheckpoint = cp;
    }
}
