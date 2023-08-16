using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.DebugUI;

public class BioPrinterController : MonoBehaviour
{
    [Serializable]
    private class craftingInfo
    {
        public GameObject BioInk; //crafting info
        public GameObject GeneralMaterial;
        public GameObject ExtraMaterial;
        public GameObject Output;
    } 

    [SerializeField] private Animator animator;
    [SerializeField] private XRSocketInteractor bioInkSocket;
    [SerializeField] private XRSocketInteractor generalMaterialSocket;
    [SerializeField] private XRSocketInteractor ExtraMaterialSocket;
    [SerializeField] private GameObject bioInkPlateform;
    [SerializeField] private GameObject generalMaterialPlateform;
    [SerializeField] private GameObject ExtraMaterialPlateform;
    [SerializeField] private craftingInfo[] crafting;
    [SerializeField] private float displayalpha = 0.25f;

    private GameObject bioInk; //selected by socket
    private GameObject generalMaterial;
    private GameObject extraMaterial;
    private GameObject BioInkDisplay;
    private GameObject GeneralMaterialDisplay;
    private GameObject ExtraMaterialDisplay;
    private bool isPrinting = false;
    private int index = 0;
    private void Start()
    {
        isPrinting = false;
        animator = GetComponent<Animator>();
    }
    public void print() //called by print button
    {
        if(bioInkSocket.selectTarget.gameObject != null)
        {
            bioInk = bioInkSocket.selectTarget.gameObject;
        }
        else
        {
            bioInk = null;
        }
        
        if(generalMaterialSocket.selectTarget.gameObject != null)
        {
            generalMaterial = generalMaterialSocket.selectTarget.gameObject;
        }
        else
        {
            generalMaterial = null;
        }

        if(ExtraMaterialSocket.selectTarget.gameObject != null)
        {
            extraMaterial = ExtraMaterialSocket.selectTarget.gameObject;
        }
        else
        {
            extraMaterial = null;
        }
        if (isPrinting == false)
        {
            for (int i = 0; i < crafting.Length; i++)
            {
                if (crafting[i].BioInk.name == bioInk.name && crafting[i].GeneralMaterial.name == generalMaterial.name && crafting[i].ExtraMaterial.name == extraMaterial.name)
                {
                    index = i;
                    Destroy(bioInk);
                    Destroy(generalMaterial);
                    Destroy(extraMaterial);
                    isPrinting = true;
                    animator.SetTrigger("Print!");
                    break;
                }
            }
        }
    }

    public void printingDone() //called through animation
    {
        isPrinting = false;
        Instantiate(crafting[index].Output, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), crafting[index].Output.transform.rotation);
        Debug.Log("printing done");
    }


    public void showCrafting(int index)
    {
        if(BioInkDisplay != null)
        {
            Destroy(BioInkDisplay);
            Destroy(GeneralMaterialDisplay);
            Destroy(ExtraMaterialDisplay);
        }
        BioInkDisplay = Instantiate(crafting[index].BioInk, new Vector3(bioInkPlateform.transform.position.x, bioInkPlateform.transform.position.y + .5f, bioInkPlateform.transform.position.z), crafting[index].Output.transform.rotation);
        Destroy(BioInkDisplay.GetComponent<XRGrabInteractable>());
        Destroy(BioInkDisplay.GetComponent<Rigidbody>());
        Color BioInkDisplayColor = BioInkDisplay.GetComponent<Renderer>().material.color;
        BioInkDisplay.GetComponent<Renderer>().material.color = new Color(BioInkDisplayColor.r, BioInkDisplayColor.g, BioInkDisplayColor.b, displayalpha);

        GeneralMaterialDisplay = Instantiate(crafting[index].GeneralMaterial, new Vector3(generalMaterialPlateform.transform.position.x, generalMaterialPlateform.transform.position.y + .5f, generalMaterialPlateform.transform.position.z), crafting[index].Output.transform.rotation);
        Destroy(GeneralMaterialDisplay.GetComponent<XRGrabInteractable>());
        Destroy(GeneralMaterialDisplay.GetComponent<Rigidbody>());
        Color GeneralMaterialDisplayColor = GeneralMaterialDisplay.GetComponent<Renderer>().material.color;
        GeneralMaterialDisplay.GetComponent<Renderer>().material.color = new Color(GeneralMaterialDisplayColor.r, GeneralMaterialDisplayColor.g, GeneralMaterialDisplayColor.b, displayalpha);

        ExtraMaterialDisplay = Instantiate(crafting[index].ExtraMaterial, new Vector3(ExtraMaterialPlateform.transform.position.x, ExtraMaterialPlateform.transform.position.y + .5f, ExtraMaterialPlateform.transform.position.z), crafting[index].Output.transform.rotation);
        Destroy(ExtraMaterialDisplay.GetComponent<XRGrabInteractable>());
        Destroy(ExtraMaterialDisplay.GetComponent<Rigidbody>());
        Color ExtraMaterialDisplayColor = ExtraMaterialDisplay.GetComponent<Renderer>().material.color;
        ExtraMaterialDisplay.GetComponent<Renderer>().material.color = new Color(ExtraMaterialDisplayColor.r, ExtraMaterialDisplayColor.g, ExtraMaterialDisplayColor.b, displayalpha);
    }
}
