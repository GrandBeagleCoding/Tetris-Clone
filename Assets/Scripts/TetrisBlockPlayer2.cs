using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisBlockPlayer2 : GameScript2 //Script erbt vom Gamescript2
{

    public Vector3 rotationPoint; // Vector für Rotation der Minos
    private float previousTime;
    public float fallTime = 0.8f; //Zeit die verstreicht bis ein Mino sich nach unten bewegt

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
            transform.position += new Vector3(-1, 0, 0); // verändert Position um -1 auf der X-Achse
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
                PlayMoveAudio();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0); // Verändert Position um +1 auf der X-Achse
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
                PlayMoveAudio();
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            //Rotation der Blöcke um 90 Grad
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!ValidMove())
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
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();        //Fügt den Block zum Vorhandenen Grid hinzu
                CheckForLines();    //Überprüft das Vorhandensein von vollen Reihen und führt dies dann in Gamescript1 aus
                PlayLandAudio();

                this.enabled = false;
                FindObjectOfType<SpawnTetriminoes1>().NewTetiminoe_p2();

            }
            previousTime = Time.time;

        }
        
    }

  
}
