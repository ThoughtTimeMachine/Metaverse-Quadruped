using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    // Start is called before the first frame update
        public static CoroutineHelper Instance;

        private void Awake()
        {
            // Ensure there is only one instance of the coroutine runner object
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }