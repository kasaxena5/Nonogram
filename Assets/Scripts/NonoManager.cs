using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NonoManager : MonoBehaviour
{
    [SerializeField]
    private NonoTile nonoTilePrefab;

    private TMP_Text[] horizontalNonoText;
    private TMP_Text[] verticalNonoText;

    [SerializeField]
    private int size = 10;

    [SerializeField]
    private Transform canvas;

    private NonoTile[,] nonogram;

    private void InitializeNonogram()
    {
        nonogram = new NonoTile[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                nonogram[i, j] = Instantiate(nonoTilePrefab);
                nonogram[i, j].i = i;
                nonogram[i, j].j = j;
                nonogram[i, j].transform.SetParent(canvas, false);
                nonogram[i, j].GetComponent<RectTransform>().localPosition = new Vector3(20 * i - 100, 20 * j - 100, 0);
                
                int x = i;
                int y = j;
                nonogram[i, j].GetComponent<Button>().onClick.AddListener(() => { OnClick(x, y); });
            }
        }
    }

    private void DestroyNonogram()
    {
        if (nonogram == null)
            return;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (nonogram[i, j] != null)
                {
                    Destroy(nonogram[i, j].gameObject);
                }
            }
        }
    }

    void Awake()
    {
        InitializeNonogram();
    }

    public void ResetGame()
    {
        DestroyNonogram();
        InitializeNonogram();
    }

    public void OnClick(int i, int j)
    {
        Debug.Log("i: " + i + ", j: " + j);
        nonogram[i, j].SetState(NonoTile.NonoState.Cross);
    }

}
