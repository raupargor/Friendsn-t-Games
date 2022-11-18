using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject x in GameObject.FindObjectsOfType(typeof(GameObject))){
            try{
            if (x.name =="Ambient"){
                x.GetComponent<Music>().StopMusic();
            }}catch{}
            
        }
        GameObject.Find("Ambiente1").GetComponent<Music>().PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
