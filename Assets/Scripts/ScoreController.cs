using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = PermanentUIController.perm.cherries.ToString();
        PermanentUIController.perm.Reset();
        PermanentUIController.perm.Terminate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
