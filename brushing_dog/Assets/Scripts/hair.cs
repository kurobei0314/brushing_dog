using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hair : MonoBehaviour
{
    [SerializeField]
    GameObject bottom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveHair();
    }

    void MoveHair(){

        Vector3 position = transform.position;

        if (position.y < bottom.transform.position.y){
            Destroy(gameObject);
        }
        else{
            position.y -= 0.05f;
            transform.position = new Vector3(position.x, position.y, 1);
        }
    }
}
