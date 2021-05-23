using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject switchTxt;
    
    private void Start()
    {
        switchTxt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && SwitchController.switchOn == true)
        {
            SceneManager.LoadScene(sceneName);
        }

        else if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(waiter());
            
        }
    }

    IEnumerator waiter()
    {
        switchTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        switchTxt.gameObject.SetActive(false);
    }
}
