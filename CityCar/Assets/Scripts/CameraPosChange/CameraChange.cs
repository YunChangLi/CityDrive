using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.transform.position += new Vector3(0, 0.1f, 0);
                Debug.Log("UP");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.transform.position -= new Vector3(0, 0.1f, 0);
                Debug.Log("Down");
            }
        }
    }
}
