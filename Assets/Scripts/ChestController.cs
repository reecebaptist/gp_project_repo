using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestController : MonoBehaviour
{
    public TextMeshProUGUI treasureText;
    [SerializeField] private Animator anim;


    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && anim.GetBool("Switch") == false)
        {
            DoorController.treasureCount += 1;
            treasureText.text = DoorController.treasureCount.ToString();
            anim.SetBool("Switch", true);
        }
    }
}
