using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    [SerializeField]
    Animator titleAnimator;

    [SerializeField]
    GameObject text1;

    [SerializeField]
    GameObject text2;

    [SerializeField]
    GameObject text3;

    [SerializeField]
    GameObject text4;

    int titleFlg = 0;

    // Update is called once per frame
    void Awake(){
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
    }

    void Update()
    {
        if(titleFlg == 0){
            // StartCoroutine("TitleAnimation");
            titleAnimator.Play("TitleAnimation");
            titleFlg = 1;
        }
    }

    private IEnumerator TitleAnimation() {
        // titleAnimator.Play("TitleAnimation");
        // yield return new WaitForSeconds (5.0f);

        text1.SetActive(true);
        AudioManager.Instance.PlaySE("don");
        yield return new WaitForSeconds (0.5f);
        text1.SetActive(false);
        text2.SetActive(true);
        AudioManager.Instance.PlaySE("don");
        yield return new WaitForSeconds (0.5f);
        text2.SetActive(false);
        text3.SetActive(true);
        AudioManager.Instance.PlaySE("don");
        yield return new WaitForSeconds (0.5f);
        text3.SetActive(false);
        text4.SetActive(true);
        AudioManager.Instance.PlaySE("dodon");
        yield return new WaitForSeconds (2.5f);
        
        SceneManager.LoadScene("Main");
    }

}
