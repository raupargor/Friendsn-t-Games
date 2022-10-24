using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera cmcamPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    PhotonView view;

    private void Start(){
        Vector2 randomPosition = new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY));
        GameObject p =PhotonNetwork.Instantiate(playerPrefab.name,randomPosition,Quaternion.identity);
        var cmcam=Instantiate(cmcamPrefab);
        cmcam.LookAt = p.transform.GetChild(1).transform;
        cmcam.Follow = p.transform.GetChild(1).transform;

    }
}
