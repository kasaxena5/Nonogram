using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NonoManager : MonoBehaviour
{
    [SerializeField]
    private NonoTile nonoTilePrefab;

    [SerializeField]
    private NonoText nonoTextPrefab;

    [SerializeField]
    private Scrollbar cursorScrollbar;

    [SerializeField]
    private int size = 10;

    [SerializeField]
    private Transform canvas;

    private NonoText[] horizontalNonoText;
    private NonoText[] verticalNonoText;
    private NonoTile[,] nonogram;

    private bool[,] nonogame;

    private bool cursorType = true;

    private void InitializeNonoTile()
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
                nonogram[i, j].GetComponent<RectTransform>().localPosition = new Vector3(20 * j - 100, 20 * i - 100, 0);

                int x = i;
                int y = j;
                nonogram[i, j].GetComponent<Button>().onClick.AddListener(() => { OnClick(x, y); });
            }
        }
    }

    private void InitializeNonoGame()
    {
        nonogame = new bool[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (Random.Range(0f, 1f) < 0.6f)
                    nonogame[i, j] = true;
                else
                    nonogame[i, j] = false;
            }
        }

        for(int i = 0; i < size; i++)
        {
            string text = "";
            int cnt = 0;
            for(int j = 0; j < size; j++)
            {
                if(nonogame[i, j])
                {
                    cnt++;
                }
                else
                {
                    if(cnt > 0)
                    {
                        text += (cnt.ToString() + ' ');
                        cnt = 0;
                    }
                }
            }
            if(cnt > 0)
            {
                text += (cnt.ToString() + ' ');
            }
            horizontalNonoText[i].SetText(text);
        }

        for (int j = 0; j < size; j++)
        {
            string text = "";
            int cnt = 0;
            for (int i = size - 1; i >= 0; i--)
            {
                if (nonogame[i, j])
                {
                    cnt++;
                }
                else
                {
                    if (cnt > 0)
                    {
                        text += (cnt.ToString() + '\n');
                        cnt = 0;
                    }
                }
            }
            if(cnt > 0)
            {
                text += (cnt.ToString() + '\n');
            }
            verticalNonoText[j].SetText(text);
        }

    }

    private void InitializeNonoText()
    {
        horizontalNonoText = new NonoText[size];
        verticalNonoText = new NonoText[size];
        for (int i = 0; i < size; i++)
        {
            verticalNonoText[i] = Instantiate(nonoTextPrefab);
            horizontalNonoText[i] = Instantiate(nonoTextPrefab);

            verticalNonoText[i].transform.SetParent(canvas, false);
            horizontalNonoText[i].transform.SetParent(canvas, false);

            verticalNonoText[i].i = i;
            horizontalNonoText[i].i = i;

            verticalNonoText[i].GetComponent<RectTransform>().localPosition = new Vector3(20 * i - 100, 150, 0);
            horizontalNonoText[i].GetComponent<RectTransform>().localPosition = new Vector3(-150, 20 * i - 100, 0);

        }
    }

    private void InitializeNonogram()
    {
        InitializeNonoText();
        InitializeNonoTile();
        InitializeNonoGame();
    }

    private void DestroyNonoTile()
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
        nonogram = null;
    }

    private void DestroyNonoGame()
    {
        nonogame = null;
    }

    private void DestroyNonoText()
    {
        if (horizontalNonoText == null && verticalNonoText == null)
            return;
        for (int i = 0; i < size; i++)
        {
            if(horizontalNonoText[i] != null)
               Destroy(horizontalNonoText[i].gameObject);
            if (verticalNonoText[i] != null)
                Destroy(verticalNonoText[i].gameObject);
        }
        horizontalNonoText = null;
        verticalNonoText = null;
    }

    private void DestroyNonogram()
    {
        DestroyNonoTile();
        DestroyNonoText();
        DestroyNonoGame();
        
    }

    
    void Awake()
    {
        cursorScrollbar.onValueChanged.AddListener((float val) => ToggleCursor(val));
        InitializeNonogram();
    }

    public void ResetGame()
    {
        DestroyNonogram();
        InitializeNonogram();
    }

    public void ToggleCursor(float value)
    {
        Debug.Log(value);
        if(value > 0.5f)
            cursorType = false;
        else
            cursorType = true;
    }

    public void OnClick(int i, int j)
    {
        Debug.Log("i: " + i + ", j: " + j);
        if(cursorType)
        {
            if(nonogame[i, j])
                nonogram[i, j].SetState(NonoTile.NonoState.Fill);
            else
                nonogram[i, j].SetState(NonoTile.NonoState.Error);
        }
        else
        {
            if(nonogame[i, j])
                nonogram[i, j].SetState(NonoTile.NonoState.Error);
            else
                nonogram[i, j].SetState(NonoTile.NonoState.Cross);
        }
    }

}
