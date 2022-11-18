using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopAmbiente1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Ambiente1").GetComponent<Music>().StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
