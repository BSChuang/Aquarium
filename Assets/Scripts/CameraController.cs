using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float speed = 1;
    public int zoomSpeed = 1;

    // Update is called once per frame
    void FixedUpdate () {
        Vector3 move = Vector3.zero;
		if (Input.GetKey(KeyCode.LeftArrow)) {
            move += Vector3.left * speed;
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            move += Vector3.right * speed;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            move += Vector3.up * speed;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            move += Vector3.down * speed;
        }

        transform.position += move;


        
        if (Input.GetKey(KeyCode.PageUp) && GetComponent<Camera>().orthographicSize > 10) {
            GetComponent<Camera>().orthographicSize -= zoomSpeed;
        }

        if (Input.GetKey(KeyCode.PageDown) && GetComponent<Camera>().orthographicSize < 75) {
            GetComponent<Camera>().orthographicSize += zoomSpeed;
        }
    }
}
