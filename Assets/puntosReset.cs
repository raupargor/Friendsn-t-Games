using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puntosReset : MonoBehaviour
{
    float timer;
    bool uno=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        try{
            timer+=Time.deltaTime;
            if (timer>=10 && uno){
            uno=false;
            foreach(GameObject x in GameObject.FindObjectsOfType(typeof(GameObject))){
            if (x.name =="Animator-movement"){
                x.GetComponent<Movement>().puntos=0;
                             
            }
            }
        
            
        }
        }catch{}
    }   
}
