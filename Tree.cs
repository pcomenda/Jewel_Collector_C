
public class Tree : Obstacle, Rechargeable
{
    public Tree() : base("$$ "){}


    public void Recharge(Robot r)
    {
        r.energy = r.energy + 3;
        
    }
    
}
