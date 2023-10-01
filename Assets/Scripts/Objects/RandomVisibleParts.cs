using System.Collections.Generic;
using UnityEngine;

public class RandomVisibleParts : MonoBehaviour
{
    [SerializeField] private List<GameObject> listParts = new List<GameObject>();
    [SerializeField] private int minParts = 3;
    [SerializeField] private int maxParts = 6;

    private void Start ()
    {
        if (listParts.Count > maxParts)
            maxParts = listParts.Count;

        for (int i = 0; i < listParts.Count; i++)
            listParts[i].SetActive(false);

        int random = Random.Range(minParts, maxParts);

        for (int i = 0; i < random; i++)
        {
            FindPart().SetActive(true);
        }

        listParts[2].transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        listParts[3].transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        listParts[4].transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
    }

    GameObject FindPart ()
    {
        int counter = 50;
        int random = 0;
        bool wasFound = false;
        while (!wasFound)
        {
            counter--;
            if (counter < 0)
            {
                Debug.Log("Error");
                wasFound = true;
            }
            random = Random.Range(0, listParts.Count);
            if (!listParts[random].activeSelf)
                wasFound = true;
        }

        return listParts[random];
    }
}