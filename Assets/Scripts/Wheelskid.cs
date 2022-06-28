using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheelskid : MonoBehaviour
{
    [SerializeField] float intensityModifier = 1.5f;

    Skidmarks skidMarksContorller;
    PlayerCar playerCar;
    ParticleSystem ps;

    private int lastSkidId = -1; // How long will be skidmark

    public bool Drift { get; set; }

    void Start()
    {
        skidMarksContorller = FindObjectOfType<Skidmarks>();
        playerCar = GetComponentInParent<PlayerCar>();
        ps = GetComponent<ParticleSystem>();
        Drift = false;
    }

    void LateUpdate()
    {
        float intensity = playerCar.SidesSlipAmount;
        //if car move to fast
        if (intensity < 0)
            intensity = -intensity;
        // if car rotate particles and skidmarks playing. Skidmarks ++
        if (intensity > 0.2f)
        {
            lastSkidId = skidMarksContorller.AddSkidMark(transform.position, transform.up, intensity * intensityModifier, lastSkidId);
            if (ps != null && !ps.isPlaying)
                ps.Play();
            Drift = true;
        }
        else
        {
            lastSkidId = -1;
            if (ps != null && ps.isPlaying)
                ps.Stop();
            Drift = false;
        }
    }
}
