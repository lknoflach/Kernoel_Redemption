using UnityEngine;

namespace Animations.EasterEgg
{
    public class TalkScript : MonoBehaviour
    {
        private GameObject _text;
        public bool isNearButton;
        private void Start()
        {
            isNearButton = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject;
            switch (target.tag)
            {
                case "Player":
                    isNearButton = true;
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.gameObject;
            switch (target.tag)
            {
                case "Player":
                    isNearButton = false;
                    break;
            }
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && isNearButton)
            {
       
            }
        }
    }
}
