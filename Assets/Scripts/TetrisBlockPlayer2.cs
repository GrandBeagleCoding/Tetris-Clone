using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisBlockPlayer2 : GameScript1 //Script erbt vom Gamescript1
{

    public Vector3 rotationPoint; // Vector f�r Rotation der Minos
    private float previousTime;
    public float fallTime = 0.8f; //Zeit die verstreicht bis ein Mino sich nach unten bewegt
    public TetrisBlockPlayer2 Tetrimino;


    //SoundClips
    public AudioClip moveSound;
    public AudioClip rotateSound;
    public AudioClip landSound;
  
    void PlayMoveAudio()
    {
        audioSource.PlayOneShot(moveSound, 1.0f);
    }

    void PlayRotateSound()
    {
        audioSource.PlayOneShot(rotateSound, 1.0f);
    }

    void PlayLandAudio()
    {
        audioSource.PlayOneShot(landSound, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Input       
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0); // ver�ndert Position um -1 auf der X-Achse
            if (!ValidMoveP2())
                transform.position -= new Vector3(-1, 0, 0);
                PlayMoveAudio();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0); // Ver�ndert Position um +1 auf der X-Achse
            if (!ValidMoveP2())
                transform.position -= new Vector3(1, 0, 0);
                PlayMoveAudio();
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            //Rotation der Bl�cke um 90 Grad
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!ValidMoveP2())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                PlayRotateSound();
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime))
        {
            if (Input.GetKey(KeyCode.S))
            {
                PlayMoveAudio();
            }
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMoveP2())
            {
                transform.position -= new Vector3(0, -1, 0);
                P2AddToGrid();        //F�gt den Block zum Vorhandenen Grid hinzu
                P2CheckForLines();    //�berpr�ft das Vorhandensein von vollen Reihen und f�hrt dies dann in Gamescript1 aus
                PlayLandAudio();

                enabled = false;
                FindObjectOfType<SpawnTetriminoes1>().NewTetiminoe_p2();
                if (P2CheckIsAboveGrid(Tetrimino))
                {
                    GameOver();
                }
            }
            previousTime = Time.time;

        }
        
    }

  
}
