using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesAttack : MonoBehaviour
{   
    private Transform movement;
    public GameObject particleAttack;
    private bool attackIsPlaying=false;



    // Start is called before the first frame update
    void Start()
    {
        movement=transform.parent.transform.parent.Find("Animator-movement").transform;
    }

    // Update is called once per frame
    void Update()
    {               
    transform.position = Vector3.MoveTowards(transform.position, movement.transform.position + new Vector3(0f,0,-1), 10f);

    if(movement.GetComponent<Movement>().canAttack){
        Debug.Log("CAN ATTACK");
        if(attackIsPlaying==false){
            Debug.Log("ATTACKING instanciating");
            GameObject e1=Instantiate(particleAttack, transform.position, Quaternion.identity);
            e1.transform.parent = gameObject.transform;
        }
        attackIsPlaying=true;
    }else { 
        attackIsPlaying=false;
        try{ 
        GameObject child=gameObject.transform.GetChild(0).gameObject;
        Destroy(child);
        }
        catch{ }
    }            
    }
}
