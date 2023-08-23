using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorScript : MonoBehaviour
{
    [SerializeField] private Animator doctorAnimator;
    
    private void Start()
    {
        doctorAnimator = GetComponent<Animator>();
    }

    public void doctorWave()
    {
        doctorAnimator.SetTrigger("Wave");
    }
}
