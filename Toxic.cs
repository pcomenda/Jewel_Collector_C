public class Toxic : ItemMap, Radioactive
{
    public Toxic() : base("!! "){}

    public void LoseEnergy(Robot r)
    {
        r.energy = r.energy - 10;
    }
}
