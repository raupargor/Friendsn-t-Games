using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;

// using Hashtable =ExitGames.Client.Photon.Hashtable;

public class ChooseColor : MonoBehaviour
{
    public List<int> allowedColors = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public Text colorInput;
    public List<Color> colores = new List<Color>();
    Memory memory;

    public void ColorChosen()
    {
        if (allowedColors.Contains(int.Parse(colorInput.text)))
        {
            Debug.Log(PhotonNetwork.PlayerList);

            memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
            memory.Color = colorInput.text;

            PhotonNetwork.LoadLevel("ChooseHat");
        }
        else
        {
            Debug.Log("El color ya está escogido");
        }
    }
}
