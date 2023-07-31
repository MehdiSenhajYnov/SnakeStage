using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoStepVirage : MonoBehaviour
{
    [SerializeField] GameObject PartOne;
    [SerializeField] GameObject PartTwo;
    [SerializeField] float creationDelay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateOneThenTwo(int fisrtPart)
    {
        if (fisrtPart == 0)
        {
            StartCoroutine(CreateAngle(PartOne, PartTwo));
        } else if(fisrtPart == 1)
        {
            StartCoroutine(CreateAngle(PartTwo, PartOne));
        } else
        {
            Debug.Log("Create angle error, int must be 0 or 1!");
        }
    }

    IEnumerator CreateAngle(GameObject G1, GameObject G2)
    {
        G1.SetActive(true);
        yield return new WaitForSeconds(creationDelay);
        G2.SetActive(true);
    }

    public void DestroyOneThenTwo(int fisrtPart)
    {
        if (fisrtPart == 0)
        {
            StartCoroutine(DestroyAngle(PartOne, PartTwo));
        }
        else if (fisrtPart == 1)
        {
            StartCoroutine(DestroyAngle(PartTwo, PartOne));
        }
        else
        {
            Debug.Log("Destroy angle error, int must be 0 or 1!");
        }
    }

    IEnumerator DestroyAngle(GameObject G1, GameObject G2)
    {
        yield return new WaitForSeconds(0.2f);
        G1.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        G2.SetActive(false);
    }
}
