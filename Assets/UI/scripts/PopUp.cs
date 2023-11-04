using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace EasyUI.Dialogs{
    public class Dialog{
        public string title;
        public string message;
    }

   

    public class PopUp : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] Text titleUIText;
        [SerializeField] Text messageUIText;
        [SerializeField] Button closeUIButton;

         Dialog dialog = new Dialog();

        public static PopUp Instance;

         void Awake() {
            Instance = this;
            Debug.Log("pop up awake");
            closeUIButton.onClick.RemoveAllListeners();
            closeUIButton.onClick.AddListener(Hide);
        }
        public PopUp SetTitle(string t){
            dialog.title = t ;
            Debug.Log(t);
            return Instance;
        }
        public PopUp SetMessage(string m){
            dialog.message = m ;
            return Instance;
        }

        public void Show(){
            titleUIText.text = dialog.title;
            messageUIText.text = dialog.message;

            canvas.SetActive(true);
            
        }

        public void Hide(){

            canvas.SetActive(false);
            
            dialog = new Dialog();
            
        }

    
    }
}
