using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;


public enum GamePhase
{
    playerDraw, playerAction, enemyDraw, enemyAction, gameStart
}
public class Battlemanager1 : MonoSingleton<Battlemanager1>
{
    public static Battlemanager1 Instance;

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

    public UnityEvent phaseChangeEvent = new UnityEvent();

    public int[] SummonCountMax = new int[2];
    private int[] SummonCounter = new int[2];

    private GameObject waitingMonster;
    private int waitingPlayer;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
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
        DrawCard(0, 1);
        DrawCard(1, 1);

        currentphase = GamePhase.playerDraw;

        SummonCounter = SummonCountMax;
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
    public void OnPlayerDraw()
    {
        if(currentphase == GamePhase.playerDraw)
        {
            DrawCard(0, 1);
            currentphase = GamePhase.playerAction;
            phaseChangeEvent.Invoke();
        }
        
    }
    public void OnEnemyDraw()
    {
        if (currentphase == GamePhase.enemyDraw) 
        { 
            DrawCard(1, 1); 
            currentphase = GamePhase.enemyAction;
            phaseChangeEvent.Invoke();
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
            Card.GetComponent<BattleCard>().playerID  = _player;
            drawDeck.RemoveAt(0);
        }
    }
    public void OnclickTurnEnd()
    {
        TurnEnd();
    }
    public void TurnEnd()
    {
        if (currentphase == GamePhase.playerAction)
        {
            currentphase = GamePhase.enemyDraw;
            phaseChangeEvent.Invoke();
        }
        else if (currentphase == GamePhase.enemyAction)
        {
            currentphase = GamePhase.playerDraw;
            phaseChangeEvent.Invoke();
        }
    }
    public void SummonRequest(int _player,GameObject _monster)
    {
        GameObject[] blocks;
        bool hasEmptyBlock = false;
        if (_player == 0)
        {
            blocks = playerBlocks;
        }
        else
        {
            blocks = enemyBlocks;
        }
        if (SummonCounter[_player] > 0)
        {
            foreach (var block in blocks)
            {
                if (block.GetComponent<Block>().card == null)
                {
                    block.GetComponent<Block>().SummonBlock.SetActive(true);
                    hasEmptyBlock = true;
                }
            }
        }
        if (hasEmptyBlock)
        {
            waitingMonster = _monster;
            waitingPlayer = _player;
        }
    }
    public void SummonConfirm(Transform _block)
    {
        Summon(waitingPlayer, waitingMonster, _block);
    }
    public void Summon(int _player, GameObject _monster, Transform _block)
    {
        _monster.transform.SetParent(_block);
        _monster.transform.localPosition = Vector3.zero;
        _monster.GetComponent<BattleCard>().state = BattleCardState.inBlock;
        _block.GetComponent<Block>().card = _monster;
        SummonCounter[_player]--;
    }
}
