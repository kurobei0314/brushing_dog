using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brush : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBrush();
    }

    void MoveBrush(){

        Vector3 position = transform.position;
        position = camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y, 1);
    }
}
