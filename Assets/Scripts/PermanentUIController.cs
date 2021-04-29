using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PermanentUIController : MonoBehaviour
{
    //Player Stats
    public int cherries = 0;
    public int health = 3;
    public TextMeshProUGUI cherryText;
    public TextMeshProUGUI healthAmount;

    public static PermanentUIController perm;

    private void Start()
    {

        DontDestroyOnLoad(gameObject);

        //Singleton
        if(!perm)
        {
            perm = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        cherries = 0;
        cherryText.text = cherries.ToString();
        health = 3;
        healthAmount.text = health.ToString();
    }

    public void Terminate() 
    {
            Destroy(gameObject);
    } 

}
