using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class WaitInRoom : MonoBehaviour
{
    public GameObject playerPrefab;
    public PhotonView view;
    public Text roomName;
    private float timer;

    // public int hat;
    // public int color;
    Memory memory;

    // [PunRPC]
    // void addParent(GameObject armature,GameObject hat)
    // {
    //     hat.transform.parent=armature.transform;
    // }
    void Start()
    {   
        // foreach(GameObject c in GameObject.FindGameObjectsWithTag("animatorMovement")){
        //     Debug.Log("dentro");
        //     Debug.Log("aaaa"+c.GetComponent<SpriteRenderer>().color.ToString());
        //     if(c.GetComponent<SpriteRenderer>().color== new Color32(255,0,215,255)){
        //                 SceneManager.LoadScene("ChooseColor");
        //             }
        // }
        // Debug.Log("bbbbb"+GameObject.FindWithTag("Player").ToString());

        Vector2 position1 = new Vector2(-1.15f, 6f);
        Vector2 position2 = new Vector2(0.23f, 3.41f);
        Vector2 position3 = new Vector2(2.5f, 2.42f);
        Vector2 position4 = new Vector2(4.5f, 2.42f);
        Vector2 position5 = new Vector2(7.77f, 5.53f);
        Vector2 position6 = new Vector2(10f, 6.55f);
        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
        timer += Time.deltaTime;

        
        // view= PhotonView.Get(this);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        // playerPrefab.transform.GetChild(1).GetComponent<SpriteRenderer>().color =memory.Color;
        // playerPrefab.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite= memory.Hat;
        if (PhotonNetwork.CurrentRoom.PlayerCount==1)
        {
            GameObject armature = PhotonNetwork.Instantiate("Armatures/A"+memory.Color, position1, Quaternion.identity, 0);

            int parentViewID = armature.GetComponentInChildren<PhotonView>().ViewID;
            object[] myCustomInitData = new object[3];
            Debug.Log(parentViewID);
            myCustomInitData[0] = parentViewID;
            GameObject hat = PhotonNetwork.Instantiate("Hats/H"+memory.Hat, position1, Quaternion.identity, 0,myCustomInitData);
            // view = armature.GetComponent<PhotonView>();
            // view.RPC("addParent", RpcTarget.All,armature,hat);
            // hat.transform.parent=armature.transform;

            // pr.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite= memory.Hat;

        }
        else
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount==2){
            GameObject armature = PhotonNetwork.Instantiate("Armatures/A"+memory.Color, position2, Quaternion.identity, 0);

            int parentViewID = armature.GetComponentInChildren<PhotonView>().ViewID;
            object[] myCustomInitData = new object[3];
            Debug.Log(parentViewID);
            myCustomInitData[0] = parentViewID;
            GameObject hat = PhotonNetwork.Instantiate("Hats/H"+memory.Hat, position2, Quaternion.identity, 0,myCustomInitData);

            }
            if(PhotonNetwork.CurrentRoom.PlayerCount==3){
                 PhotonNetwork.Instantiate(playerPrefab.name, position3, Quaternion.identity, 0);
            }
            if(PhotonNetwork.CurrentRoom.PlayerCount==4){
                 PhotonNetwork.Instantiate(playerPrefab.name, position4, Quaternion.identity, 0);
            }
            if(PhotonNetwork.CurrentRoom.PlayerCount==5){
                 PhotonNetwork.Instantiate(playerPrefab.name, position5, Quaternion.identity, 0);
            }
            if(PhotonNetwork.CurrentRoom.PlayerCount==6){
                 PhotonNetwork.Instantiate(playerPrefab.name, position6, Quaternion.identity, 0);
            }
            
        }

    }

    // Update is called once per frame

    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= 15f)
        {
            GameObject p = GameObject.Find("Animator-movement");
            if(p.GetComponentInChildren<Movement>().canMove){
                p.GetComponentInChildren<Movement>().canMove = false;
            }
            
        }
    }

    public void Empezar(){ 
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.LoadLevel("ZonaPruebas");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
    
    
    }

    // void OnPhotonSerializeView()
    //  {
    //       GameObject  p = GameObject.Find("/Armature(Clone)");
    //       if(timer < 5f){
    //          p.GetComponentInChildren<Movement>().ChangeColor(memory.Color);
    //          p.GetComponentInChildren<Hats>().ChangeSprite(memory.Hat);
    //       }


    //    p.transform.GetComponentInChildren<Movement>().canMove=false;
    // }
}
