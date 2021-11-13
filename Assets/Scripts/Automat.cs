using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automat : MonoBehaviour
{
    [SerializeField] private GameObject pref;
    private GameObject[,] cellurs;
    private bool[,] activeCellurs;
    private int count;

    void Start()
    {
        count = 150;
        cellurs = new GameObject[count + 2, count + 2];
        for (int i = 0; i < count + 2; i++)
            for (int j = 0; j < count + 2; j++)
            {
                cellurs[i, j] = Instantiate(pref, new Vector3(i, j, 0), pref.transform.rotation);
                int life = Random.Range(0,2);
                if (life == 1) cellurs[i, j].SetActive(true); 
                else cellurs[i, j].SetActive(false);
            }
        for (int i = 0; i < count + 2; i++)
        {
            cellurs[0, i].SetActive(false);
            cellurs[i, 0].SetActive(false);
            cellurs[i, count + 1].SetActive(false);
            cellurs[count + 1, i].SetActive(false);
        }
        activeCellurs = new bool[count + 2, count + 2];
        StartCoroutine(Life());
    }
    // Update is called once per frame
    IEnumerator Life()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f);

            for (int i = 1; i < count + 1; i++)
                for (int j = 1; j < count + 1; j++)
                    if (cellurs[i, j].activeSelf) activeCellurs[i, j] = true;
                    else activeCellurs[i, j] = false;

            for (int i = 1; i < count + 1; i++)
                for (int j = 1; j < count + 1; j++)
                    if (activeCellurs[i, j] == true)
                    {
                        if (WillDie(i, j)) cellurs[i, j].SetActive(false);
                    }
                    else
                    {
                        if (WillCellursBorn(i, j)) cellurs[i, j].SetActive(true);
                    }
        } 
    }
    

    private bool WillCellursBorn(int i, int j)
    {
        int count = HowManyLivesNear(i, j);
        if (count == 3) return true;
        else return false;
    }
   
    private bool WillDie(int i, int j)
    {
        int count = HowManyLivesNear(i, j);
        if (count == 3 ||
            count == 2) return false;
        else return true;
    }

    private int HowManyLivesNear(int i, int j)
    {
        int count = 0;
        if (activeCellurs[i - 1, j]) count++;
        if (activeCellurs[i + 1, j]) count++;
        if (activeCellurs[i, j - 1]) count++;
        if (activeCellurs[i, j + 1]) count++;
        if (activeCellurs[i + 1, j + 1]) count++;
        if (activeCellurs[i - 1, j + 1]) count++;
        if (activeCellurs[i - 1, j - 1]) count++;
        if (activeCellurs[i + 1, j - 1]) count++;

        return count;
    }
}
