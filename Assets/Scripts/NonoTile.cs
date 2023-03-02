using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonoTile : MonoBehaviour
{
    [SerializeField] Sprite crossSprite;

    public enum NonoState
    {
        Unrevealed,
        Fill,
        Cross
    }

    [SerializeField]
    private NonoState state = NonoState.Unrevealed;

    public void SetState(NonoState newState)
    {
        state = newState;
        this.GetComponent<Image>().color = state switch
        {
            NonoState.Unrevealed => Color.white,
            NonoState.Fill => Color.black,
            NonoState.Cross => Color.black,
            _ => Color.white,
        };

        if(state == NonoState.Cross)
            this.GetComponent<Image>().sprite = crossSprite;
    }

    public NonoState GetState()
    {
        return state;
    }

    public int i;
    public int j;
}
