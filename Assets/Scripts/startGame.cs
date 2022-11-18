using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class startGame : MonoBehaviour
{
    public string Titulo;
    public Text textElementTitulo;
    public string Descripcion;
    public Text textElementDescripcion;
    public float timeToStart=10f;
    private bool started = false;
    public GameObject spawnPlayers;
    private endGame _endGame;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public bool startDash;
    void Start()
    {
        textElementTitulo.text = Titulo;
        textElementDescripcion.text = Descripcion;
    }
    // Update is called once per frame
    void Update()
    {
        timeToStart -= Time.deltaTime;
        if (timeToStart<=0 && !started){
            started = true;
            object[] myCustomInitData = new object[5];
            myCustomInitData[0] = minX;
            myCustomInitData[1] = minY;
            myCustomInitData[2] = maxX;
            myCustomInitData[3] = maxY;
            myCustomInitData[4] = startDash;
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(spawnPlayers.name, new Vector3(0,0,0), Quaternion.identity,0, myCustomInitData);
            }
        }
        if(started){ 
        _endGame = GameObject.FindWithTag("endGame").GetComponent<endGame>();
        _endGame.playersNow = 100;
        _endGame.started = true;
        Destroy(gameObject);
        }
    }
}
