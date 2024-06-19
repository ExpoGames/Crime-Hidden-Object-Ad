using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectTwoDots.HexaGraph
{
    public class HexaTutorialHandController : MonoBehaviour
    {
        public GameObject fturhand;
        public GameObject clickedhand, initialHand;

        private void Update()
        {
            fturhand.transform.position = Input.mousePosition;

            if(Input.GetMouseButtonDown(0))
            {
                clickedhand.SetActive(true);
                initialHand.SetActive(false);
            }

            if (Input.GetMouseButtonUp(0))
            {
                clickedhand.SetActive(false);
                initialHand.SetActive(true);
            }

        }
    }
}


