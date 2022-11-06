using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemController : MonoBehaviour
{   
    // public GameObject BombPrefab;
    private void OnTriggerEnter2D(Collider2D collision){
        if( collision.tag == "Player"){//collision.tag =="Enemy" ||
            if(gameObject.tag=="HealthItem"){
                collision.gameObject.GetComponent<Movement>().canHeal=true;
                collision.gameObject.GetComponent<Movement>().canDash=false;
                collision.gameObject.GetComponent<Movement>().canBomb=false;
                collision.gameObject.GetComponent<Movement>().canAttack=false;
            }
            if(gameObject.tag=="DashItem"){
                collision.gameObject.GetComponent<Movement>().canHeal=false;
                collision.gameObject.GetComponent<Movement>().canDash=true;
                collision.gameObject.GetComponent<Movement>().canBomb=false;
                collision.gameObject.GetComponent<Movement>().canAttack=false;
            }
            if(gameObject.tag=="BombItem"){
                collision.gameObject.GetComponent<Movement>().canHeal=false;
                collision.gameObject.GetComponent<Movement>().canDash=false;
                collision.gameObject.GetComponent<Movement>().canBomb=true;
                collision.gameObject.GetComponent<Movement>().canAttack=false;
            }
            if(gameObject.tag=="AttackItem"){
                collision.gameObject.GetComponent<Movement>().canHeal=false;
                collision.gameObject.GetComponent<Movement>().canDash=false;
                collision.gameObject.GetComponent<Movement>().canBomb=false;
                collision.gameObject.GetComponent<Movement>().canAttack=true;
            }
            Destroy(gameObject.transform.parent.gameObject);
        }
        
    }


}
