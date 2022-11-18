using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ContadorUI : MonoBehaviour
{
    public string textValue;
    public Text textElement;
    private endGame endGame;

    void Start()
    {
        endGame=GameObject.FindWithTag("endGame").GetComponent<endGame>();

    }

    // Update is called once per frame
    void Update()
    {
        if(endGame.timeToFinish>=5){
            textElement.text = (endGame.timeToFinish-5f).ToString("0");
        }else{ 
            textElement.text =(string)"0";
        }

    }
}
