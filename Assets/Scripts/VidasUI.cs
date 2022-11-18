using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class VidasUI : MonoBehaviour
{   
    public int vidaID;
    public Image img;
    public GameObject movement;
    public Memory memory;
    private Movement player;
    private startGame startGame;
     void Start () {
        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
        startGame=GameObject.FindWithTag("startGame").GetComponent<startGame>();
        // movement=transform.parent.parent.parent.parent.parent.Find("Animator-movement").transform.gameObject;
    }
 
    void Update () {
        if(startGame.timeToStart<=0f){
            try{ 
                player = PhotonView.Find(memory.photonID).GetComponent<Movement>();
                // Debug.Log(player.vidas);
                if (player.vidas >= vidaID) {
                    img.enabled = true;
                    }
                    else {
                    img.enabled = false;
                }
            }
            catch{}
        }
    }
    
}

