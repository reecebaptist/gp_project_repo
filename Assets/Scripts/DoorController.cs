using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DoorController : MonoBehaviour
{
    public TextMeshProUGUI treasureText;
    public static int treasureCount = 0;

    [SerializeField] private string sceneName;
    [SerializeField] private TextMeshProUGUI switchTxt;

    // Start is called before the first frame update
    private void Start()
    {
        treasureText.text = treasureCount.ToString();
        switchTxt.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && treasureCount == 4)
        {
            Destroy(gameObject);
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
