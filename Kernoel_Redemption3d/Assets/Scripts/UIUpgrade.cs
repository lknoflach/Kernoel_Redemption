using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player; 
    CharacterMovement characterMovement;
    
    void Start()
    {
         characterMovement = player.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgradeSpeed(){
        characterMovement.speed = characterMovement.speed + 1f;
    }
}
