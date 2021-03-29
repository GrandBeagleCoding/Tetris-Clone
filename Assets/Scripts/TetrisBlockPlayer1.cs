using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TetrisBlockPlayer1 : GameScript1 //Script erbt vom Gamescript1
{

    public Vector3 rotationPoint; // Vector für Rotation der Minos
    private float previousTime;
    public float fallTime = 0.8f; //Zeit die verstreicht bis ein Mino sich nach unten bewegt

    //SoundClips
    public AudioClip moveSound;
    public AudioClip rotateSound;
    public AudioClip landSound;

    
        
    // Update is called once per frame
    void Update()
    {
        Gameplay();

    }
   
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

    public void Gameplay()
    {
        //Input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0); // verändert Position um -1 auf der X-Achse
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
                PlayMoveAudio();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0); // Verändert Position um +1 auf der X-Achse
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
                PlayMoveAudio();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Rotation der Blöcke um 90 Grad
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                PlayRotateSound();
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {

            transform.position += new Vector3(0, -1, 0);

            if (Input.GetKey(KeyCode.DownArrow))
            {
                PlayMoveAudio();
            }
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();        //Fügt den Block zum Vorhandenen Grid hinzu
                CheckForLines();    //Überprüft das Vorhandensein von vollen Reihen und führt dies dann in Gamescript1 aus
                PlayLandAudio();

                //if(CheckIsAboveGrid(this))
                //{
                //    GameOver();
                //}

                enabled = false;
                FindObjectOfType<SpawnTetriminoes>().NewTetiminoe();

            }
            previousTime = Time.time;


        }

    }
           
}
