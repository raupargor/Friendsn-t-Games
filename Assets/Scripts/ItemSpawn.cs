using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    float timer1;
    float timer2=3f;
    float timer3=5f;
    float timer4=2f;
    public GameObject DashPrefab;
    public GameObject AttackPrefab;
    public GameObject HealthPrefab;
    public GameObject BombPrefab;

    public float rangoxA=30f;
    public float rangoxB=-10f;
    public float rangoyA=2f;
    public float rangoyB=-2f;

 void Update(){   
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;
        timer3 += Time.deltaTime;
        timer4 += Time.deltaTime;





        if(timer1 >= 19f){
            timer1=9;
            float x =Random.Range(rangoxA, rangoxB);
            float y =Random.Range(rangoyA, rangoyB);
            Vector3 position= new Vector3(x,y,0);
            Quaternion rotation= new Quaternion();
            Instantiate(DashPrefab, position, rotation);
        }
        if(timer2 >= 20f){
            timer2=9;
            float x =Random.Range(rangoxA, rangoxB);
            float y =Random.Range(rangoyA, rangoyB);
            Vector3 position= new Vector3(x,y,0);
            Quaternion rotation= new Quaternion();
            Instantiate(AttackPrefab, position, rotation);
        }
        if(timer3 >= 19f){
            timer3=9;
            float x =Random.Range(rangoxA, rangoxB);
            float y =Random.Range(rangoyA, rangoyB);
            Vector3 position= new Vector3(x,y,0);
            Quaternion rotation= new Quaternion();
            Instantiate(HealthPrefab, position, rotation);
        }
        if(timer4 >=27f){
            timer4=9;
            float x =Random.Range(rangoxA, rangoxB);
            float y =Random.Range(rangoyA, rangoyB);
            Vector3 position= new Vector3(x,y,0);
            Quaternion rotation= new Quaternion();
            Instantiate(BombPrefab, position, rotation);
        }
    
    }    
}
