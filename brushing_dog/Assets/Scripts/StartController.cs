using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    [SerializeField]
    GameObject text1,text2,text3;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("StartAnimation");
    }
    private IEnumerator StartAnimation(){
        
        yield return new WaitForSeconds (1.0f);
        SceneManager.LoadScene("MainScene");
    }
}
