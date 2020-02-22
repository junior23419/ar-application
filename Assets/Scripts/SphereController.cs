using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public SpheresManager spheresManager;
    [HideInInspector]public List<SphereParentController> targets;
    [HideInInspector] public int targetid;
    [HideInInspector] public int id=-1;
    public UnityEngine.UI.Text debugText;
    Vector3 originpos;
    float speed;
    private void Start()
    {
        originpos = transform.position;
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (spheresManager.activeSphereParents.Count <= 1)
        {
            //print(55555 + gameObject.name);
            SetPositionToOrigin();
            return;
        }
        

        if (targetid >= spheresManager.activeSphereParents.Count)
        {
            //print("hey");
            NextID();
        }

        debugText.text = spheresManager.activeSphereParents.Count.ToString()+id+" " +targetid;
        if (spheresManager.activeSphereParents[targetid] != null)
        {
            Vector3 direction = (spheresManager.activeSphereParents[targetid].transform.position - transform.position);
            direction.Normalize();
            
            transform.position += direction * Time.deltaTime * speed;
        }
            

        if (Vector3.Distance(transform.position, spheresManager.activeSphereParents[targetid].transform.position) < 0.1f)
        {
            NextID();
        }
    }

    public void SetID(int val)
    {
        id = val;
        targetid = val;
        NextID();
    }

    public void ResetTarget()
    {
        targetid = id;
        SetPositionToOrigin();
        NextID();
    }

    public void SetPositionToOrigin()
    {
        transform.localPosition = Vector3.zero;
    }

    void NextID()
    {
        targetid++;
        if (targetid >= spheresManager.activeSphereParents.Count)
            targetid = 0;
        if (spheresManager.activeSphereParents.Count > 1)
        {
            Debug.Log("speed");
            speed = Vector3.Distance(spheresManager.activeSphereParents[targetid].transform.position, transform.position) / 2f;
        }
    }
}
