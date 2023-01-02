using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HomeController : MonoBehaviour
{
    public AudioClip suaraButton;
    AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        aS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        aS.PlayOneShot(suaraButton);
        SceneManager.LoadScene(4);
    }
    public void QuitGame()
    {
        aS.PlayOneShot(suaraButton);
        Application.Quit();
    }
}
