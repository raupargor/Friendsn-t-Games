using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class puntosCarrera : MonoBehaviour
{
    public PlayerController pc;
    public string tipoCarrera = "x";
    // public Vector3 posicionMeta;
    public Vector3 posicionPlayer;
    private Memory memory;
    public bool finish = false;
    private bool done = false;
    private endGame _endGame;
    // Start is called before the first frame update
    void Start()
    {
        _endGame = GameObject.FindWithTag("endGame").GetComponent<endGame>();
        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
        try{pc = GameObject.FindWithTag("PlayerController").GetComponent<PlayerController>();}catch{}
    }

    // Update is called once per frame
    void Update()
    {
        if(finish && !done){
            var player = PhotonView.Find(memory.photonID).gameObject;
            posicionPlayer=player.GetComponent<Movement>().transform.position;

            if(tipoCarrera=="x"){ //
                pc.addPoints((int)posicionPlayer.x);
            }else if(tipoCarrera=="y"){ //
                pc.addPoints((int)(posicionPlayer.y+100)/2);
            }
            pc.addPoints(10);
            done=true;
        }
        if(done){
            _endGame.finished=true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if( collision.tag == "Player"){        
           finish=true;
        }
    
    }
}
