
public class Robot : ItemMap
{
    public Map map {get; private set;}
    private int x, y;
    private List<Jewel> Bag = new List<Jewel>();
    public int energy {get; set;}

    public Robot(Map map, int x=0, int y=0, int energy=5) : base("ME "){
        this.map = map;
        this.x = x;
        this.y = y;
        this.energy = energy;

        this.map.Insert(this, x, y);
    }


    public void MoveNorth(){
        
        try
        {
            map.Update(this.x, this.y, this.x-1, this.y);
            this.x--;
            this.energy--;

            Radioactive? Closerradio = map.GetRadioactive(this.x, this.y);
            Closerradio?.LoseEnergy(this);
        }
        catch (Exception e)
        {

        }
    }

    public void MoveSouth(){
        
        try
        {
            map.Update(this.x, this.y, this.x+1, this.y);
            this.x++;
            this.energy--;

            Radioactive? Closerradio = map.GetRadioactive(this.x, this.y);
            Closerradio?.LoseEnergy(this);
        }
        catch(OccupiedPositionException e)
        {
            Console.WriteLine($"Position {this.x+1}, {this.y} is occupied");
        }
        catch(OutOfMapException e)
        {
            Console.WriteLine($"Position {this.x+1}, {this.y} is out of map");
        }
        catch(Exception e)
        {
            Console.WriteLine($"Position is prohibit");
        }
    }

    public void MoveEast(){
        
        try
        {
            map.Update(this.x, this.y, this.x, this.y+1);
            this.y++;
            this.energy--;

            Radioactive? Closerradio = map.GetRadioactive(this.x, this.y);
            Closerradio?.LoseEnergy(this);
        }
        catch (Exception e)
        {

        }
    }

    public void MoveWest(){
        
        try
        {
            map.Update(this.x, this.y, this.x, this.y-1);
            this.y--;
            this.energy--;

            Radioactive? Closerradio = map.GetRadioactive(this.x, this.y);
            Closerradio?.LoseEnergy(this);
        }
        catch (Exception e)
        {

        }
    }

/*
    public void GetSick()
    {
        Radioactive? Closeradio = map.GetRadioactive(this.x, this.y);

        Closeradio?.LoseEnergy(this);
    }
*/


    public void Get()
    {
        //List<Jewel> NearJewels = map.GetJewels(this.x, this.y);

        Rechargeable? RechargeEnergy = map.GetRechargeable(this.x, this.y);

        RechargeEnergy?.Recharge(this);

        List<Jewel> NearJewels = map.GetJewels(this.x, this.y);

        foreach(Jewel j in NearJewels)
            Bag.Add(j);
        
    }

    private (int, int) GetBagInfo()
    {
        int Points = 0;

        foreach(Jewel j in this.Bag)
            Points += j.Points;
        
        return (this.Bag.Count, Points);
    }

    public void Print()
    {
        map.Print();

        (int ItensBag, int TotalPoints) = this.GetBagInfo();
        Console.WriteLine($"Itens Bag: {ItensBag} - Total Points: {TotalPoints} - Energy: {this.energy}");
    }

    public bool HasEnergy()
    {
        return this.energy > 0;
    }



}
