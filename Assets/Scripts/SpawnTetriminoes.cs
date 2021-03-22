using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetriminoes : MonoBehaviour
{

    [SerializeField] private GameObject[] Tetriminoes;

    // Start is called before the first frame update
    void Start()
    {
        NewTetiminoe();
    }

    public void NewTetiminoe()
    {

        Instantiate(Tetriminoes[Random.Range(0, Tetriminoes.Length)], transform.position, Quaternion.identity);

    }

}
