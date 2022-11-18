using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    public bool damage=true;
    public bool attack=false;
    public bool shoot=false;
    private Memory memory;
    void Start()
    {
        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
    }

    // Update is called once per frame
    void Update()
    {   
        try{
            if(attack){
                Attack(true);
            }
            if(shoot){
                Shoot(true);
            }
            if(damage){
                Damage(true);
            }
            if(!attack){
                Attack(false);
            }
            if(!shoot){
                Shoot(false);
            }            
            if(!damage){
                Damage(false);
            }     
        }catch{}
        
    }

    private void Shoot(bool value){
        var player = PhotonView.Find(memory.photonID).gameObject;
        player.GetComponent<Movement>().canShoot=value;
    }
    private void Attack(bool value){
        var player = PhotonView.Find(memory.photonID).gameObject;
        player.GetComponent<Movement>().canAttackPlayer=value;
    }
    private void Damage(bool value){
        var player = PhotonView.Find(memory.photonID).gameObject;
        player.GetComponent<Movement>().canReceiveDamage=value;
    }
    public void addPoints(int points){ 
        Debug.Log("Added " + points + " points");
        
        for(int i = 0; i < points; i++) {
            memory.Points+=1;
        }
    }
    public void subtrackPoints(int points){ 
        Debug.Log("Subtracked " + points + " points");
        
        for(int i = 0; i < points; i++) {
            memory.Points-=1;
        }
    }
}
