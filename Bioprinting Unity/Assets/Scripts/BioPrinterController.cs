using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BioPrinterController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private XRSocketInteractor bioInkSocket;
    [SerializeField] private XRSocketInteractor generalMaterialSocket;
    [SerializeField] private XRSocketInteractor ExtraMaterialSocket;

    public GameObject bioInk;
    private GameObject generalMaterial;
    private GameObject extraMaterial;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void print()
    {
        bioInk = bioInkSocket.gameObject ;
        generalMaterial = generalMaterialSocket.gameObject;
        extraMaterial = ExtraMaterialSocket.gameObject;  
        animator.SetTrigger("Print!");
    }
}
