/*
Classe responsável por caracterizar a joia Azul. A mesma possui, além 
de uma pontuação específica, a capacidade de dar energia ao jogador
*/
public class JewelBlue : Jewel, Rechargeable
{
    public JewelBlue() : base("JB ", 10){}

    public void Recharge(Robot r)
    {
        r.energy = r.energy + 5;
    }
}
