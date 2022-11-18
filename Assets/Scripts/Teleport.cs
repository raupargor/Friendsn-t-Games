using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{   
    public Vector3 posicionX;
    public bool quitaPuntos=true;
    public PlayerController pc;

     // Start is called before the first frame update
    void Start()
    {
     try{pc = GameObject.FindWithTag("PlayerController").GetComponent<PlayerController>();}catch{}
    
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if( collision.tag == "Player"){        
            Debug.Log("TP");
            collision.gameObject.GetComponent<Movement>().transform.position = posicionX;

        }
        if(quitaPuntos){
            pc.subtrackPoints(5);
        }
    }

}
