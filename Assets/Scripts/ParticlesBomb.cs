using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesBomb : MonoBehaviour
{   
    private Transform movement;
    public GameObject particleBomb;
    private bool bombIsPlaying=false;



    // Start is called before the first frame update
    void Start()
    {
        movement=transform.parent.transform.parent.Find("Animator-movement").transform;
    }

    // Update is called once per frame
    void Update()
    {  
    try{           
    transform.position = Vector3.MoveTowards(transform.position, movement.transform.position + new Vector3(0f,0,-1), 10f);

    if(movement.GetComponent<Movement>().canBomb){
        Debug.Log("CAN BOMB");
        if(bombIsPlaying==false){
            Debug.Log("BOMB instanciating");
            GameObject e1=Instantiate(particleBomb, transform.position, Quaternion.identity);
            e1.transform.parent = gameObject.transform;
        }
        bombIsPlaying=true;
    }else { 
        bombIsPlaying=false;
        try{ 
        GameObject child=gameObject.transform.GetChild(0).gameObject;
        Destroy(child);
        }
        catch{ }
    }  }catch{}          
    }
}