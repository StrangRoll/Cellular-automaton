using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automat : MonoBehaviour
{
    [SerializeField] private GameObject pref;
    private GameObject[,,] cellurs;
    private bool[,,] activeCellurs;
    public int count;

    void Start()
    {
        cellurs = new GameObject[count + 2, count + 2, count + 2];
        for (int i = 0; i < count + 2; i++)
            for (int j = 0; j < count + 2; j++)
                for (int g = 0; g < count + 2; g++)
                {
                    cellurs[i, j, g] = Instantiate(pref, new Vector3(i, j, g), pref.transform.rotation);
                    int life = Random.Range(0, 2);
                    if (life == 1) cellurs[i, j, g].SetActive(true);
                    else cellurs[i, j, g].SetActive(false);
                }

        for (int i = 0; i < count + 2; i++)
            for (int j = 0; j < count + 2; j++)
            {
                cellurs[i, j, 0].SetActive(false);
                cellurs[i, j, count + 1].SetActive(false);

                cellurs[i, 0, j].SetActive(false);
                cellurs[i, count + 1, j].SetActive(false);

                cellurs[0, i, j].SetActive(false);
                cellurs[count + 1, i, j].SetActive(false);

            }

        activeCellurs = new bool[count + 2, count + 2, count + 2];
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
                    for (int g = 1; g < count + 1; g++)
                        if (cellurs[i, j, g].activeSelf) activeCellurs[i, j, g] = true;
                        else activeCellurs[i, j, g] = false;

            for (int i = 1; i < count + 1; i++)
                for (int j = 1; j < count + 1; j++)
                    for (int g = 1; g < count + 1; g++)
                        if (activeCellurs[i, j, g] == true)
                        {
                            if (WillDie(i, j, g)) cellurs[i, j, g].SetActive(false);
                        }
                        else
                        {
                            if (WillCellursBorn(i, j, g)) cellurs[i, j, g].SetActive(true);
                        }
        } 
    }
    

    private bool WillCellursBorn(int i, int j, int g)
    {
        int count = HowManyLivesNear(i, j, g);
        if (count == 9 || count == 8 || count == 7) return true;
        else return false;
    }
   
    private bool WillDie(int i, int j, int g)
    {
        int count = HowManyLivesNear(i, j, g);
        if (count >= 4 &&
            count <= 9) return false;
        else return true;
    }

    private int HowManyLivesNear(int i, int j, int g)
    {
        int count = 0;
        if (activeCellurs[i - 1, j, g]) count++;
        if (activeCellurs[i + 1, j, g]) count++;
        if (activeCellurs[i, j - 1, g]) count++;
        if (activeCellurs[i, j + 1, g]) count++;
        if (activeCellurs[i + 1, j + 1, g]) count++;
        if (activeCellurs[i - 1, j + 1, g]) count++;
        if (activeCellurs[i - 1, j - 1, g]) count++;
        if (activeCellurs[i + 1, j - 1, g]) count++;

        if (activeCellurs[i - 1, j, g + 1]) count++;
        if (activeCellurs[i + 1, j, g + 1]) count++;
        if (activeCellurs[i, j - 1, g + 1]) count++;
        if (activeCellurs[i, j + 1, g + 1]) count++;
        if (activeCellurs[i + 1, j + 1, g + 1]) count++;
        if (activeCellurs[i - 1, j + 1, g + 1]) count++;
        if (activeCellurs[i - 1, j - 1, g + 1]) count++;
        if (activeCellurs[i + 1, j - 1, g + 1]) count++;
        if (activeCellurs[i, j, g + 1]) count++;

        if (activeCellurs[i - 1, j, g - 1]) count++;
        if (activeCellurs[i + 1, j, g - 1]) count++;
        if (activeCellurs[i, j - 1, g - 1]) count++;
        if (activeCellurs[i, j + 1, g - 1]) count++;
        if (activeCellurs[i + 1, j + 1, g - 1]) count++;
        if (activeCellurs[i - 1, j + 1, g - 1]) count++;
        if (activeCellurs[i - 1, j - 1, g - 1]) count++;
        if (activeCellurs[i + 1, j - 1, g - 1]) count++;
        if (activeCellurs[i, j, g - 1]) count++;


        return count;
    }
}
