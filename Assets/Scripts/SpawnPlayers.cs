using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayers : MonoBehaviour
{
    // public GameObject playerPrefab;
    public CinemachineVirtualCamera cmcamPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    PhotonView view;
    Memory memory;
    private void Start(){
        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();

        Vector2 randomPosition = new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY));
        // GameObject p =PhotonNetwork.Instantiate(playerPrefab.name,randomPosition,Quaternion.identity);

        GameObject armature = PhotonNetwork.Instantiate("Armatures/A"+memory.Color, randomPosition, Quaternion.identity, 0);

        var cmcam=Instantiate(cmcamPrefab);
        cmcam.LookAt = armature.transform.GetChild(1).transform;
        cmcam.Follow = armature.transform.GetChild(1).transform;

        int parentViewID = armature.transform.GetChild(1).GetComponent<PhotonView>().ViewID;
        object[] myCustomInitData = new object[3];
        myCustomInitData[0] = parentViewID;
        GameObject hat = PhotonNetwork.Instantiate("Hats/H"+memory.Hat, randomPosition, Quaternion.identity, 0,myCustomInitData);
    }
}
