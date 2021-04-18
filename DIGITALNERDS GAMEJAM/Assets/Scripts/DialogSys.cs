using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogSys : MonoBehaviour
{
    public List<string> texts = new List<string>();
    public TextMeshProUGUI textBox;
    
    public bool isDialogEnabled;
    
    [SerializeField]
    private int dialogIndex = 0;

    void Start(){
        if(SceneManager.GetActiveScene().buildIndex == 1){
            isDialogEnabled = true;
        }
    }
    void Update(){
        if(isDialogEnabled){
            EnableDialog();//only visual
            textBox.text = texts[dialogIndex];
        }else{
            DisableDialog();//only visual
            dialogIndex = 0;
        }

        if(Input.GetButtonDown("Fire1")){
            if(dialogIndex < texts.Count-1){
                dialogIndex++;
            }else{
                DisableDialog();
                dialogIndex =0;
            }
            
        }
    }

    void EnableDialog(){
        isDialogEnabled = true;
        textBox.gameObject.SetActive(true);
    }

    void DisableDialog(){
        isDialogEnabled = false;
        textBox.gameObject.SetActive(false);
    }
}
