using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetriminoes : MonoBehaviour
{

    [SerializeField] private GameObject[] Tetriminoe;

    // Start is called before the first frame update
    void Start()
    {
        NewTetiminoe();
    }

    public void NewTetiminoe()
    {

        Instantiate(Tetriminoe[Random.Range(0, Tetriminoe.Length)], transform.position, Quaternion.identity);

    }

}
