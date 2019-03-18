using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorController : MonoBehaviour
{
    Renderer r;
    Color color;

    private bool wTestBool;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        color = r.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        r.material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        color = Color.Lerp(color, Color.white, .1f);
    }
}
