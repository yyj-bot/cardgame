using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum GamePhase
{
    playerDraw, playerAction, enemyDraw, enemyAction, gameStart
}
public class Battlemanager1 : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerData enemyData;

    public List<Card> playerDesklist = new List<Card>();
    public List<Card> enemyDesklist = new List<Card>();

    public GameObject cardPrefab;

    public Transform playerHand;
    public Transform enemyHand;

    public GameObject[] playerBlocks;
    public GameObject[] enemyBlocks;

    public GameObject playericon;
    public GameObject enemyicon;

    public GamePhase currentphase = GamePhase.gameStart;
    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStart()
    {
        ReadDeck();
        ShuffleDeck(0);
        ShuffleDeck(1);
        DrawCard(0, 2);
        DrawCard(1, 2);
    }
    public void ReadDeck()
    {
        for (int i = 0; i < playerData.playerDeck.Length; i++)
        {
            if (playerData.playerDeck[i] != 0)
            {
                int count = playerData.playerDeck[i];
                for (int j = 0; j < count; j++)
                {
                    playerDesklist.Add(playerData.CardStore.CopyCard(i));
                }
            }
        }
        for (int i = 0; i < enemyData.playerDeck.Length; i++)
        {
            if (enemyData.playerDeck[i] != 0)
            {
                int count = enemyData.playerDeck[i];
                for (int j = 0; j < count; j++)
                {
                    enemyDesklist.Add(enemyData.CardStore.CopyCard(i));
                }
            }
        }
    }
    public void ShuffleDeck(int _player)
    {
        List<Card> shuffleDeck = new List<Card>();
        if (_player == 0)
        {
            shuffleDeck = playerDesklist;
        }
        else if (_player == 1)
        {
            shuffleDeck = enemyDesklist;
        }
        for (int i = 0; i < shuffleDeck.Count; i++)
        {
            int rad = Random.Range(0, shuffleDeck.Count);
            Card temp = shuffleDeck[i];
            shuffleDeck[i] = shuffleDeck[rad];
            shuffleDeck[rad] = temp;
        }
    }
    public void DrawCard(int _player, int _count)
    {
        List<Card> drawDeck = new List<Card>();
        Transform hand = transform;
        if (_player == 0)
        {
            drawDeck = playerDesklist;
            hand = playerHand;
        }
        else if (_player == 1)
        {
            drawDeck = enemyDesklist;
            hand = enemyHand;
        }
        for (int i = 0; i < _count; i++)
        {
            GameObject Card = Instantiate(cardPrefab, hand);
            Card.GetComponent<CardDisplay>().card = drawDeck[0];
            drawDeck.RemoveAt(0);
        }
    }
    public void TurnEnd()
    {
        if (currentphase == GamePhase.playerAction)
        {
            currentphase = GamePhase.enemyDraw;
        }
        else if (currentphase == GamePhase.enemyAction)
        {
            currentphase = GamePhase.playerDraw;
        }
    }
}
