using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hats : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public List<Sprite> spriteArray;

    void Start()
    {
        spriteArray.Add(Resources.Load<Sprite>("BARQUITO"));
        spriteArray.Add(Resources.Load<Sprite>("COKE"));
        spriteArray.Add(Resources.Load<Sprite>("CONO"));
        spriteArray.Add(Resources.Load<Sprite>("CORONA"));
        spriteArray.Add(Resources.Load<Sprite>("CUMPLEAÑOS"));
        spriteArray.Add(Resources.Load<Sprite>("HAMBURGUESA"));
        spriteArray.Add(Resources.Load<Sprite>("HUEVO"));
        spriteArray.Add(Resources.Load<Sprite>("NAVIDAD"));
        spriteArray.Add(Resources.Load<Sprite>("PATITO"));

    }

    void Update()
    {        
        transform.position = Vector3.MoveTowards(transform.position, transform.parent.Find("Animator-movement").transform.position + new Vector3(0,0.95f,-1), 10f);
        if(transform.parent.Find("Animator-movement").GetComponent<SpriteRenderer>().flipX==true){
            gameObject.GetComponent<SpriteRenderer>().flipX=false;
            transform.position=transform.position+ new Vector3(-0.2f,0,0);
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().flipX=true;
            transform.position=transform.position+ new Vector3(0.2f,0,0);

        }
        if(Input.GetKeyDown(KeyCode.H) )
        {
            ChangeSprite(newSprite);
        }
    }

    void ChangeSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite; 
    }
}
