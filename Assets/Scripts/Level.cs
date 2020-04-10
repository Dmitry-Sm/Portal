using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private Finish finish;

    private void Start() {
        finish.finishEvent += OnFinish;
    }
    
    public void OnFinish() {
        SceneManager.LoadScene("Main menu");
    }
}
