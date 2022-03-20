using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Camera _cam;

    void Start() {
        _cam = FindObjectOfType<Camera>();
    }

    void Update() {
        var pos = _cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
