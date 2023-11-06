using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace EasyUI.Dialogs{
    
    public class Dialog{
        public string message;
    }

    public class PopUp : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] Text messageUIText;
        [SerializeField] Button closeUIButton;

         Dialog dialog = new Dialog();

        public static PopUp Instance;

         void Awake() {
            Instance = this;
            closeUIButton.onClick.RemoveAllListeners();
            closeUIButton.onClick.AddListener(Hide);
        }
        
        public PopUp SetMessage(string m){
            dialog.message = m ;
            return Instance;
        }

        public void Show(){
            messageUIText.text = dialog.message;
            Debug.Log(canvas);
            canvas.SetActive(true);
            Debug.Log("canvas activated");
            
        }

        public void Hide(){

            canvas.SetActive(false);
            
            dialog = new Dialog();
            
        }

    
    }
}
