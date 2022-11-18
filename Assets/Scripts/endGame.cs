using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class endGame : MonoBehaviour
{
    public float timeToFinish=105f;
    public float timeToResume=5f;
    public float timerCheckNJugadores=0f;
    public string siguienteNivel="";
    public bool finished=false;
    public bool started=false;
    private Memory memory;
    public Text resumen;
    public int playersNow;
    // private int playersStart;
    private PlayerController pc;
    private int i = 1;
    public int ganador;
    void Start()
    {
        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
        // playersStart = PhotonNetwork.PlayerList.Length;
        try{pc = GameObject.FindWithTag("PlayerController").GetComponent<PlayerController>();}catch{}
        // GameObject.FindGameObjectWithTag("SeAcaboAudio").GetComponent<Music>().PlayMusic();

    }

    void Update()
    {
        resumen.text=(string)"";
        timerCheckNJugadores+= Time.deltaTime;
        //Temporizador
        if(timeToFinish>0){
        timeToFinish-= Time.deltaTime;
            if(timeToFinish<0){
                timeToFinish=0;
            }
        }
        //Condiciones de terminacion
        if(timeToFinish<=5 && !finished){
            finished=true;   
        }
        if(timerCheckNJugadores>3.5f){
            timerCheckNJugadores=0f;
            Debug.Log("Jugadores que hay ahora mismo jugando: " + playersNow);
            playersNow = GameObject.FindGameObjectsWithTag("Nickname").Length;
        }
        
        if(started && playersNow<=1 && !finished ){
            
            try{ganador=GameObject.Find("Animator-movement").GetComponent<PhotonView>().ViewID;
            Debug.Log("Ganador ID: "+ ganador);
            Debug.Log("memory ID: "+ memory.photonID);
            if(ganador==memory.photonID){
                pc.addPoints(70);
            }else{ pc.addPoints(10);}
            }catch{}
            finished=true;  
        }

        //Terminar
        if (finished){
            timeToResume-= Time.deltaTime;
            resumen.text=(string)"Se acabo!";
            GameObject.FindGameObjectWithTag("SeAcaboAudio").GetComponent<Music>().PlayMusic();

            stopPlayers();
            
            if(i==1 && PhotonNetwork.IsMasterClient && timeToResume<=0){
                i=0;
                PhotonNetwork.LoadLevel(siguienteNivel);         
            } 
        }
        
    } 
    private void stopPlayers(){
        try{
        var player = PhotonView.Find(memory.photonID).gameObject;
        player.GetComponent<Movement>().canMove=false;
        }
        catch{}
    }

}
