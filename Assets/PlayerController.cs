using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int iniLevelBerapa, berikutnyaLevelBerapa;
    public int mauBerapaTelurJikaMenang, mauBerapaScoreDiLevelIni;
    public float movementSpeed, vertical, horizontal;
    public Text telurText, scoreText;
    public int telur, score, saveSound;
    public bool MovementBool = true, isRight;
    bool coolDownPeringatanUI;
    public GameObject menangUI, peringatanUI, keluarUI, sound;

    Vector2 tempatAwal;

    Rigidbody2D rb;
    Animator anim;

    public AudioClip suaraAmbilTelur, suaraHitTembok, suaraButton, suaraNextLevel;
    AudioSource aS;

    string walk_parameter = ("walk");
    string idle_parameter = ("idle");
    string dead_parameter = ("dead");

    Vector2 lookDirection = new Vector2(1, 0);
    // Start is called before the first frame update
    void Start()
    {
        aS = gameObject.GetComponent<AudioSource>();
        saveSound = PlayerPrefs.GetInt("saveSound");
        if (saveSound == 0)
        {
            sound.SetActive(true);

        }
        if (saveSound == 1)
        {
            sound.SetActive(false);
        }


        PlayerPrefs.SetInt("saveLevel", iniLevelBerapa);

        tempatAwal = transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        keluarGame();

        scoreText.text = "" + score;
        telurText.text = "Telur : " + telur;
    }
    private void FixedUpdate()
    {
        if (MovementBool)
        {
            Movement();
        }
        

    }
    void keluarGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            keluarUI.SetActive(true);
        }
    }
    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 position = rb.position;
        position.x = position.x + movementSpeed * horizontal * Time.deltaTime;
        position.y = position.y + movementSpeed * vertical * Time.deltaTime;
        rb.MovePosition(position);




        if (horizontal != 0)
        {
            anim.SetTrigger(walk_parameter);
        }
        else
        {
            anim.SetTrigger(idle_parameter);
        }

        if (horizontal > 0 && !isRight)
        {
            transform.eulerAngles = Vector2.up * 180;
            isRight = true;
        }
        else if (horizontal < 0 && isRight)
        {
            transform.eulerAngles = Vector2.zero;
            isRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Telur"))
        {
            telur++;
            aS.PlayOneShot(suaraAmbilTelur);
        }

        if (collision.CompareTag("Batu"))
        {
            MovementBool = false;
            anim.SetTrigger(dead_parameter);
            Invoke("RespawnKembali", 2);
            aS.PlayOneShot(suaraHitTembok);
        }
        if (collision.CompareTag("Tembok"))
        {
            MovementBool = false;
            anim.SetTrigger(dead_parameter);
            Invoke("RespawnKembali", 2);
            aS.PlayOneShot(suaraHitTembok);
        }

        if (collision.CompareTag("Pohon") && !coolDownPeringatanUI)
        {
            if(telur == mauBerapaTelurJikaMenang)
            {
                menangUI.SetActive(true);
                score = mauBerapaScoreDiLevelIni;
                aS.PlayOneShot(suaraNextLevel);
            }
            else
            {
                coolDownPeringatanUI = true;
                peringatanUI.SetActive(true);
                Invoke("FalsePeringatanUI", 3);
                print("Masih ada telur yang tertinggal");
            }
        }
    }
    public void NextLevel()
    {
        aS.PlayOneShot(suaraButton);
        SceneManager.LoadScene(berikutnyaLevelBerapa);
    }
    public void RestratLevel()
    {
        aS.PlayOneShot(suaraButton);
        SceneManager.LoadScene(iniLevelBerapa);
    }

    public void RespawnKembali()
    {
        float tempX = tempatAwal.x;
        float tempY = tempatAwal.y;
        transform.position = new Vector2(tempX, tempY);

        anim.SetTrigger(idle_parameter);
        MovementBool = true;
    }

    public void Iya()
    {
        aS.PlayOneShot(suaraButton);
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }
    public void Tidak()
    {
        aS.PlayOneShot(suaraButton);
        Time.timeScale = 1;
        keluarUI.SetActive(false);
    }

    void FalsePeringatanUI()
    {
        coolDownPeringatanUI = false;
        peringatanUI.SetActive(false);
    }




}
