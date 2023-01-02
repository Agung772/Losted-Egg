using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int saveLevel, saveSound;
    bool coolDownPeringatanUI;
    public GameObject peringatanUI, gameOptionsUI, sound, muteButton, unmuteButton;

    public AudioClip suaraButton;
    AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        aS = gameObject.GetComponent<AudioSource>();

        saveLevel = PlayerPrefs.GetInt("saveLevel");
        saveSound = PlayerPrefs.GetInt("saveSound");

        if (saveSound == 0)
        {
            sound.SetActive(true);
            unmuteButton.SetActive(false);
            muteButton.SetActive(true);
        }
        if (saveSound == 1)
        {
            sound.SetActive(false);
            unmuteButton.SetActive(true);
            muteButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        aS.PlayOneShot(suaraButton);
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        aS.PlayOneShot(suaraButton);
        if (saveLevel >= 1)
        {
            SceneManager.LoadScene(saveLevel);
        }
        else if(!coolDownPeringatanUI)
        {
            Invoke("FalsePeringatanUI", 3);
            coolDownPeringatanUI = true;
            peringatanUI.SetActive(true);
        }
        
    }

    public void ResetGame()
    {
        aS.PlayOneShot(suaraButton);
        PlayerPrefs.DeleteAll();
        saveLevel = PlayerPrefs.GetInt("saveLevel");
    }

    public void GameOptions()
    {
        aS.PlayOneShot(suaraButton);
        gameOptionsUI.SetActive(true);
    }

    public void Mute()
    {
        aS.PlayOneShot(suaraButton);
        unmuteButton.SetActive(true);
        muteButton.SetActive(false);
        PlayerPrefs.SetInt("saveSound", 1);
        saveSound = PlayerPrefs.GetInt("saveSound");
        if(saveSound == 1)
        {
            sound.SetActive(false);
        }
    }

    public void Unmute()
    {
        aS.PlayOneShot(suaraButton);
        unmuteButton.SetActive(false);
        muteButton.SetActive(true);
        PlayerPrefs.SetInt("saveSound", 0);
        saveSound = PlayerPrefs.GetInt("saveSound");
        if (saveSound == 0)
        {
            sound.SetActive(true);
        }
    }

    public void Back()
    {
        aS.PlayOneShot(suaraButton);
        gameOptionsUI.SetActive(false);
    }

    public void Home()
    {
        aS.PlayOneShot(suaraButton);
        SceneManager.LoadScene("HomeScene");
    }

    public void StartGame()
    {
        aS.PlayOneShot(suaraButton);
        SceneManager.LoadScene("4");
    }

    void FalsePeringatanUI()
    {
        coolDownPeringatanUI = false;
        peringatanUI.SetActive(false);
    }
}
