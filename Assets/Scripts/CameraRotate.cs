using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float size;
    [SerializeField] private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(size/2, size/2, size/2);
        camera.transform.position = new Vector3(size/2, size/2, -size);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
