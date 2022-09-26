/*
Classe que traz as características herdadas da classe ItemMap que 
tem como objetivo caracterizar as Joias de formar geral, as mesmas 
que serão futuramente especificadas.
As joias trazem uma pontuação específica cada e essa herança será 
herdada daqui
*/
public class Jewel : ItemMap
{
    public int Points {get; private set;}

    public Jewel(string Symbol, int Points) : base(Symbol)
    {
        this.Points = Points;
    }
}
