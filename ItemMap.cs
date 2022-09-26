/*
Classe abstrata responsável por definir as características gerais 
dos objetos que estarão presentes no mapa.
Cada objeto tem o seu símbolo e atribuições específicas
*/

public abstract class ItemMap
{
    private string Symbol;

    public ItemMap(string Symbol)
    {
        this.Symbol = Symbol;
    }

    public sealed override string ToString()
    {
        return Symbol;
    }


}
