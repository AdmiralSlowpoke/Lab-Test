using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject plane;
    private DrawLineScript drawLineScript;
    void Start()
    {
        drawLineScript = Camera.main.gameObject.GetComponent<DrawLineScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                drawLineScript.RemoveLine();
            }
        }
    }
}
