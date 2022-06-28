using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{

    public class Bot_Moveing : MonoBehaviour
    {
        public GameObject[] botCars;

        void Start()
        {
            StartCoroutine(spawn());
        }
        IEnumerator spawn()
        {
            while (true)
            {
                Instantiate(botCars[Random.Range(0, botCars.Length)],
                   new Vector3(0, 0, 0), Quaternion.identity);
                yield return new WaitForSeconds(5f);
            }
        }
    }
}