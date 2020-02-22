using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player; 
    CharacterMovement characterMovement;
    public float totalSpeed = 12f;
    CloningScript cloningScript;

    [Header("Unity Stuff")] public Image speedBar;
   
    void Start()
    {
        characterMovement = player.GetComponent<CharacterMovement>();
        cloningScript = player.GetComponent<CloningScript>();
        if (speedBar) speedBar.fillAmount = ((float) characterMovement.speed) / ((float) totalSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgradeSpeed(){
        if(characterMovement.speed < totalSpeed && cloningScript.low_grade_kernoel >= 3){
            cloningScript.low_grade_kernoel = cloningScript.low_grade_kernoel - 3;
            characterMovement.speed = characterMovement.speed + 1f;
             if (speedBar) speedBar.fillAmount = ((float) characterMovement.speed) / ((float) totalSpeed);

        }
    }
}
