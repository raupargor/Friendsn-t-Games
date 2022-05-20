using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDash : MonoBehaviour
{   
    private Transform movement;
    public GameObject particleDash;
    private bool dashIsPlaying=false;



    // Start is called before the first frame update
    void Start()
    {
        movement=transform.parent.transform.parent.Find("Animator-movement").transform;
    }

    // Update is called once per frame
    void Update()
    {               
    transform.position = Vector3.MoveTowards(transform.position, movement.transform.position + new Vector3(0f,0,-1), 10f);

    if(movement.GetComponent<Movement>().canDash){
        Debug.Log("CAN DASH");
        if(dashIsPlaying==false){
            Debug.Log("DASH instanciating");
            GameObject e1=Instantiate(particleDash, transform.position, Quaternion.identity);
            e1.transform.parent = gameObject.transform;
        }
        dashIsPlaying=true;
    }else { 
        dashIsPlaying=false;
        try{ 
        GameObject child=gameObject.transform.GetChild(0).gameObject;
        Destroy(child);
        }
        catch{ }
    }            
    }
}