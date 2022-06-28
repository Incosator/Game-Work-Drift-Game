using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Drift_Score : MonoBehaviour
{

    private float score;

    [SerializeField] Text driftScore;

    Wheelskid skid;

    void Start()
    {
        skid = GetComponentInChildren<Wheelskid>();
        driftScore.text = score.ToString();
    }


    void Update()
    {
        if (skid.Drift)
        {
            score += 10 * Time.deltaTime; // Увеличение счетика дрифта
            driftScore.text = Mathf.Round(score * 100).ToString();
        }
    }
}
