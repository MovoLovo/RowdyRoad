using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTouchController : MonoBehaviour
{
    RaycastHit hit;
    Touch touch;

    void Update()
    {
        if(Input.touchCount > 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
