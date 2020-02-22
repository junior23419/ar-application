using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereParentController : MonoBehaviour
{
    public SpheresManager spheresManager;
    [HideInInspector]private int id = -1;
    Material material;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            material.color = spheresManager.GetAppropriateColor(id);

    }

    private void OnEnable()
    {
        //Debug.Log("enable"+id);

        spheresManager.activeSphereParents.Add(this);
        if(id==-1)
        {
            id = spheresManager.GetId();
            gameObject.transform.GetChild(0).GetComponent<SphereController>().SetID(id);
        }

        material = gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        material.color = spheresManager.GetAppropriateColor(id);
    }

    private void OnDisable()
    {
        gameObject.transform.GetChild(0).GetComponent<SphereController>().SetPositionToOrigin();
        spheresManager.activeSphereParents.Remove(this);
    }
}
