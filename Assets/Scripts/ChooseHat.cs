using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ChooseHat : MonoBehaviour
{
    public List<int> allowedHats = new List<int>();
    public List<Sprite> spriteArray;
    public Text hatInput;
    Memory memory;

    void Awake()
    {
        allowedHats.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
        spriteArray.Add(Resources.Load<Sprite>("PATITO"));
        spriteArray.Add(Resources.Load<Sprite>("NAVIDAD"));
        spriteArray.Add(Resources.Load<Sprite>("SOMBRERO"));
        spriteArray.Add(Resources.Load<Sprite>("HUEVO"));
        spriteArray.Add(Resources.Load<Sprite>("HAMBURGUESA"));
        spriteArray.Add(Resources.Load<Sprite>("COKE"));
        spriteArray.Add(Resources.Load<Sprite>("CONO"));
        spriteArray.Add(Resources.Load<Sprite>("BARQUITO"));
        spriteArray.Add(Resources.Load<Sprite>("CUMPLEAÑOS"));
        spriteArray.Add(Resources.Load<Sprite>("CORONA"));
    }

    public void HatChosen()
    {
        if (allowedHats.Contains(int.Parse(hatInput.text)))
        {
            // PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "playerHat", int.Parse(hatInput.text) } });
            // if(PhotonNetwork.IsMasterClient){
            // PhotonNetwork.AutomaticallySyncScene=true;
            // }        
            memory.Hat = hatInput.text;
            // Debug.Log("El sombrero es"+memory.Hat);
            PhotonNetwork.LoadLevel("Loading");

            // SceneManager.LoadScene("Sala");
        }
        else
        {
            Debug.Log("El sombrero ya está escogido");
        }


        // Debug.Log("Sombrero: "+PhotonNetwork.LocalPlayer.CustomProperties["playerHat"]);
    }
}
