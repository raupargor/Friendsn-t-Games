using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class showNickname : MonoBehaviour
{
    public string textValue;
    public Text textElement;

    public PhotonView view;

    void Start()
    {
        textValue = view.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        textElement.text = textValue ;
    }
}
