using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BattleCardState
{
    inHand,inBlock
}
public class BattleCard : MonoBehaviour,IPointerDownHandler
{
    public int playerID;
    public BattleCardState state = BattleCardState.inHand;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<CardDisplay>().card is MonsterCard)
        {
            if (state == BattleCardState.inHand)
            {
                Battlemanager1.Instance.SummonRequest(playerID, gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
