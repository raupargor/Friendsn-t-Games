using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesHeal : MonoBehaviour
{   
    private Transform movement;
    public GameObject particleHeal;
    private bool healIsPlaying=false;



    // Start is called before the first frame update
    void Start()
    {
        movement=transform.parent.transform.parent.Find("Animator-movement").transform;
    }

    // Update is called once per frame
    void Update()
    {               
    transform.position = Vector3.MoveTowards(transform.position, movement.transform.position + new Vector3(0f,0,-1), 10f);

    if(movement.GetComponent<Movement>().canHeal){
        Debug.Log("CAN HEAL");
        if(healIsPlaying==false){
            Debug.Log("HEAL instanciating");
            GameObject e1=Instantiate(particleHeal, transform.position, Quaternion.identity);
            e1.transform.parent = gameObject.transform;
        }
        healIsPlaying=true;
    }else { 
        healIsPlaying=false;
        try{ 
        GameObject child=gameObject.transform.GetChild(0).gameObject;
        Destroy(child);
        }
        catch{ }
    }            
    }
}