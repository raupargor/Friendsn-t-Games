using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PuntosUI : MonoBehaviour
{
    public Text textElement;
    private Memory memory;
    void Start()
    {
        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();

    }

    // Update is called once per frame
    void Update()
    {
            textElement.text =memory.Points.ToString();
    }
}


