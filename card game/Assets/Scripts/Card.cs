public class Card
{
    public int id;
    public string cardName;
    public Card(int id, string cardName)
     {
       this.id = id;
       this.cardName = cardName;
      }
}
public class MonsterCard : Card
{
    public int attack;
    public int healthPoint;
    public int healthMax;
    public MonsterCard(int id,string cardName,int attack,  int healthMax):base(id,cardName)
     {
            this.attack = attack;
            this.healthPoint = healthMax; ;
            this.healthMax = healthMax;
     }
}

public class SpellCard : Card 
{
    public string effect;
    public SpellCard(string effect,int id ,string cardName):base(id,cardName) 
    {
        this.effect = effect;
    }
}
