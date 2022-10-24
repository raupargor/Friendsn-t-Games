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

    // private ExitGames.Client.Photon.Hashtable myCustomProperties= new ExitGames.Client.Photon.Hashtable();
    // void Awake()
    // {
    //     colores.Add(new Color32(255, 0, 215, 255));
    //     colores.Add(new Color32(36, 0, 255, 255));
    //     colores.Add(new Color32(50, 210, 0, 255));
    //     colores.Add(new Color32(0, 0, 0, 255));
    //     colores.Add(new Color32(255, 28, 0, 255));
    //     colores.Add(new Color32(0, 221, 255, 255));
    //     colores.Add(new Color32(255, 214, 0, 255));
    //     colores.Add(new Color32(255, 118, 0, 255));
    //     colores.Add(new Color32(255, 255, 255, 255));
    //     colores.Add(new Color32(164, 0, 255, 255));
    //     colores.Add(new Color32(108, 0, 255, 255));
    //     colores.Add(new Color32(0, 136, 255, 255));
    //     colores.Add(new Color32(0, 255, 166, 255));
    //     memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
    // }
    public void ColorChosen()
    {
        if (allowedColors.Contains(int.Parse(colorInput.text)))
        {
            Debug.Log(PhotonNetwork.PlayerList);

            memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
            memory.Color = colorInput.text;

            PhotonNetwork.LoadLevel("ChooseHat");
            // SceneManager.LoadScene("ChooseHat");
        }
        else
        {
            Debug.Log("El color ya está escogido");
        }
    }
}
