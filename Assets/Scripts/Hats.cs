using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class Hats : MonoBehaviour //,IPunObservable
{
    public SpriteRenderer spriteRenderer;
    public int newSprite;
    public List<Sprite> spriteArray;

    private PhotonView view;

    // void Awake()
    // {
    //     spriteArray.Add(Resources.Load<Sprite>("PATITO"));
    //     spriteArray.Add(Resources.Load<Sprite>("NAVIDAD"));
    //     spriteArray.Add(Resources.Load<Sprite>("SOMBRERO"));
    //     spriteArray.Add(Resources.Load<Sprite>("HUEVO"));
    //     spriteArray.Add(Resources.Load<Sprite>("HAMBURGUESA"));
    //     spriteArray.Add(Resources.Load<Sprite>("COKE"));
    //     spriteArray.Add(Resources.Load<Sprite>("CONO"));
    //     spriteArray.Add(Resources.Load<Sprite>("BARQUITO"));
    //     spriteArray.Add(Resources.Load<Sprite>("CUMPLEAÑOS"));
    //     spriteArray.Add(Resources.Load<Sprite>("CORONA"));
    // }

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        view = GetComponent<PhotonView>();
        // AddObservable();
    }

    void Update()
    {
        if (!view.IsMine)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.parent.Find("Animator-movement").transform.position+ new Vector3(0, 0.95f, -1),10f);
        if (transform.parent.Find("Animator-movement").GetComponent<SpriteRenderer>().flipX == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.position = transform.position + new Vector3(-0.2f, 0, 0);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            transform.position = transform.position + new Vector3(0.2f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.H) && transform.tag == "Player")
        {
            ChangeSprite(newSprite);
        }
    }

    public void ChangeSprite(int somb)
    {
        // if (view.IsMine){
            spriteRenderer.sprite = spriteArray.ElementAt(somb);
        // }
    }

    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    // 	if (stream.IsWriting)
    // 	{
    // 		stream.SendNext(spriteRenderer.sprite);
    // 	}
    // 	else
    // 	{
    // 		spriteRenderer =(SpriteRenderer.sprite) stream.ReceiveNext();
    // 	}
    // }
    // private void AddObservable()
    // {
    // 	if (!view.ObservedComponents.Contains(this))
    // 	{
    // 		view.ObservedComponents.Add(this);
    // 	}
    // }
}
