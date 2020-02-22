using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheresManager : MonoBehaviour
{
    int sphCount = 0;
    public List<GameObject> sphereParents;
    public List<SphereParentController> activeSphereParents;
    Color[] colors;
    public UnityEngine.UI.Text debugText;
    int lastCount = 0;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = activeSphereParents.Count.ToString();
        if (lastCount != activeSphereParents.Count && activeSphereParents.Count>1)
        {
            foreach(SphereParentController parent in activeSphereParents)
            {
                parent.transform.GetChild(0).GetComponent<SphereController>().ResetTarget();
            }
            lastCount = activeSphereParents.Count;
        }
        if(activeSphereParents.Count ==1)
            activeSphereParents[0].transform.GetChild(0).GetComponent<SphereController>().SetPositionToOrigin();
    }

    public Color GetAppropriateColor(int id) // call when detect new image
    {
        long r, g, b;
        if(colors ==null)
            colors = new Color[sphereParents.Count];
        bool colorUsable ;
        do
        {
            r = Random.Range(0, 256);
            g = Random.Range(0, 256);
            b = Random.Range(0, 256);
            colorUsable = true;
            for (int i = 0; i < colors.Length; i++)
            {
                if (i != id && colors[i] != null)
                {
                    Vector3 colorA = new Vector3(r, g, b);
                    Vector3 colorB = new Vector3(Mathf.Round(colors[i].r * 255), Mathf.Round(colors[i].g * 255), Mathf.Round(colors[i].b * 255));
                    float distance = Vector3.Distance(colorA, colorB);

                    if(distance < 200f)
                    {
                        colorUsable = false;
                        break;
                    }

                }
            }
        } while (!colorUsable);

        colors[id] = new Color(r / 255f, g / 255f, b / 255f);
        return colors[id];
    }

    public int GetId()
    {
        int reval = sphCount;
        sphCount++;
        return reval;
    }

}
