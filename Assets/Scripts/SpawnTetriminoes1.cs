using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetriminoes1 : MonoBehaviour
{

    [SerializeField] private GameObject[] Tetriminoes_p2;

    // Start is called before the first frame update
    void Start()
    {
        NewTetiminoe_p2();
    }

    public void NewTetiminoe_p2()
    {

        Instantiate(Tetriminoes_p2[Random.Range(0, Tetriminoes_p2.Length)], transform.position, Quaternion.identity);

    }
}
