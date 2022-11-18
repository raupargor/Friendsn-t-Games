using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Linq;
public class Ranking : MonoBehaviour
{
    public GameObject playerPrefab;
    public Memory memory;
    public PhotonView view;
    private float timer;
    private float timer2;

    private List<Vector2> puestosLibres=new List<Vector2>();
    Vector2 position1 = new Vector2(3.9f, 5f);
    Vector2 position2 = new Vector2(0f, 4f);
    Vector2 position3 = new Vector2(3f, 7.7f);
    // Vector2 position4 = new Vector2(4.5f, 2.42f);
    // Vector2 position5 = new Vector2(7.77f, 5.53f);
    // Vector2 position6 = new Vector2(10f, 6.55f);
    // public List<GameObject> peopleList = new List<GameObject>();
    private int oneTime=1;

    public Dictionary<int, int> dict = new Dictionary<int, int>();
    public Dictionary<int, int> sortedDict = new Dictionary<int, int>();
    public List<KeyValuePair<int,int>> keys = new List<KeyValuePair<int,int>>();
    void Start()
    {   
        puestosLibres.Add(position1);        puestosLibres.Add(position2);        puestosLibres.Add(position3);
        // puestosLibres.Add(position4);        puestosLibres.Add(position5);        puestosLibres.Add(position6);

        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
        // view= GetComponent<PhotonView>();
        // view.RPC("sharePoints", RpcTarget.All);
        // timer += Time.deltaTime;

        GameObject armature = PhotonNetwork.Instantiate("Armatures/A"+memory.Color, new Vector2(3f, 25f), Quaternion.identity, 0);

        int parentViewID = armature.transform.GetChild(1).GetComponent<PhotonView>().ViewID;
        armature.transform.GetChild(1).GetComponent<Movement>().puntos=memory.Points;
        armature.transform.GetChild(1).GetComponent<PhotonView>().Owner.NickName = memory.nickname;
        object[] myCustomInitData = new object[3];
        myCustomInitData[0] = parentViewID;
        GameObject hat = PhotonNetwork.Instantiate("Hats/H"+memory.Hat, position1, Quaternion.identity, 0,myCustomInitData);
    }

    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        try{
            if (timer <= 1f)
            {   
                timer=0;
                GameObject p = GameObject.Find("Animator-movement");
                p.GetComponentInChildren<Movement>().canShoot = false;
                p.GetComponentInChildren<Movement>().canMove = false;
                p.GetComponentInChildren<Movement>().canAttackPlayer = false;
                // Debug.Log(p.GetComponentInChildren<PhotonView>().Owner.NickName+": "+p.GetComponentInChildren<Movement>().puntos);
                
            }
        }catch{}
        if(oneTime==1 && timer2 >=8){ 
            oneTime=2;
            sharePoints();
        }
        if(oneTime==2){
            oneTime=3;
            keys = dict.OrderByDescending(x => x.Value).ToList();
            Debug.Log(keys[0]);
            Debug.Log(keys[1]);
            // =sortedDict.Keys.ToList();
        }
        if(oneTime==3){
            oneTime=4;
            orderPlayers();
        }

    }
    // [PunRPC]
    void sharePoints(){ //
        // foreach(GameObject x in GameObject.FindGameObjectsWithTag("Animator-movement")) {
        foreach(GameObject x in GameObject.FindObjectsOfType(typeof(GameObject))){
            if (x.name =="Animator-movement"){
                // Debug.Log(x.GetComponent<Movement>().view.Owner.NickName+": "+x.GetComponent<Movement>().puntos);
                dict.Add(x.GetComponent<PhotonView>().ViewID, x.GetComponent<Movement>().puntos);
                             
            }
            
        }
    }
    void orderPlayers(){ //
        foreach(GameObject x in GameObject.FindObjectsOfType(typeof(GameObject))){
            try{
            if (x.name =="Animator-movement"){
                if(x.GetComponent<PhotonView>().ViewID==keys[0].Key){ //
                    x.GetComponent<Movement>().transform.position = puestosLibres[0];      
                }                
                if(x.GetComponent<PhotonView>().ViewID==keys[1].Key){ //
                    x.GetComponent<Movement>().transform.position = puestosLibres[1];      
                }                
                if(x.GetComponent<PhotonView>().ViewID==keys[2].Key){ //
                    x.GetComponent<Movement>().transform.position = puestosLibres[2];      
                }
                
            }}catch{}
            
        }
    }
}
