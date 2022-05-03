using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{   


    // public GameObject BombPrefab;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag =="Enemy" || collision.tag == "Player"){
            if(gameObject.tag=="HealthItem"){
                collision.gameObject.GetComponent<Movement>().canHeal=true;
                collision.gameObject.GetComponent<Movement>().canDash=false;
                collision.gameObject.GetComponent<Movement>().canBomb=false;
                collision.gameObject.GetComponent<Movement>().canAttack=false;
                Debug.Log("HealthItem");
            }
            if(gameObject.tag=="DashItem"){
                collision.gameObject.GetComponent<Movement>().canHeal=false;
                collision.gameObject.GetComponent<Movement>().canDash=true;
                collision.gameObject.GetComponent<Movement>().canBomb=false;
                collision.gameObject.GetComponent<Movement>().canAttack=false;
                Debug.Log("DashItem");
            }
            if(gameObject.tag=="BombItem"){
                collision.gameObject.GetComponent<Movement>().canHeal=false;
                collision.gameObject.GetComponent<Movement>().canDash=false;
                collision.gameObject.GetComponent<Movement>().canBomb=true;
                collision.gameObject.GetComponent<Movement>().canAttack=false;
                Debug.Log("BombItem");
            }
            if(gameObject.tag=="AttackItem"){
                collision.gameObject.GetComponent<Movement>().canHeal=false;
                collision.gameObject.GetComponent<Movement>().canDash=false;
                collision.gameObject.GetComponent<Movement>().canBomb=false;
                collision.gameObject.GetComponent<Movement>().canAttack=true;
                Debug.Log("AttackItem");
            }
            Destroy(gameObject.transform.parent.gameObject);
            // Debug.Log(collision);
        }
        
    }


}
