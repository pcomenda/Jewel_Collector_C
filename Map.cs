
public class Map
{
    private ItemMap[,] Matriz;
    public int h {get; private set;}
    public int w {get; private set;}

    public Map(int w=10, int h=10, int level=1)
    {
        this.w = w <= 30 ? w : 30;
        this.h = h <= 30 ? h : 30;

        Matriz = new ItemMap[w, h];
        
        for(int i=0; i < Matriz.GetLength(0); i++){
            for(int j=0; j < Matriz.GetLength(1); j++){
                Matriz[i, j] = new Empty();
            }
        }

        if (level == 1) GenerateFixed();
        else GenerateRandom(level);
    }


    public void Insert(ItemMap Item, int x, int y)
    {
        Matriz[x, y] = Item;
    }
    

    public void Update(int x_old, int y_old, int x, int y)
    {
        if(x < 0 || y < 0 || x > this.w-1 || y > this.h-1){
            throw new OutOfMapException();
        }
        if (IsAllowed(x, y)){
            Matriz[x, y] = Matriz[x_old, y_old];
            Matriz[x_old, y_old] = new Empty();
        }
        else if(IsToxic(x, y)){
            Matriz[x, y] = Matriz[x_old, y_old];
            Matriz[x_old, y_old] = new Empty();
            
        }
        else{
            throw new OccupiedPositionException();
        }
    }

    public List<Jewel> GetJewels(int x, int y)
    {
        List<Jewel> NearJewels = new List<Jewel>();

        int[,] Coords = GenerateCoord(x, y);

        for(int i=0; i < Coords.GetLength(0); i++){
            Jewel? jewel = GetJewel(Coords[i, 0], Coords[i, 1]);

            if(jewel is not null) NearJewels.Add(jewel);
        }

        return NearJewels;
    }


    private void GenerateFixed()
    {
        this.Insert(new JewelRed(), 1, 9);
        this.Insert(new JewelRed(), 8, 8);
        this.Insert(new JewelGreen(), 9, 1);
        this.Insert(new JewelGreen(), 7, 6);
        this.Insert(new JewelBlue(), 3, 4);
        this.Insert(new JewelBlue(), 2, 1);

        this.Insert(new Water(), 5, 0);
        this.Insert(new Water(), 5, 1);
        this.Insert(new Water(), 5, 2);
        this.Insert(new Water(), 5, 3);
        this.Insert(new Water(), 5, 4);
        this.Insert(new Water(), 5, 5);
        this.Insert(new Water(), 5, 6);
        this.Insert(new Tree(), 5, 9);
        this.Insert(new Tree(), 3, 9);
        this.Insert(new Tree(), 8, 3);
        this.Insert(new Tree(), 2, 5);
        this.Insert(new Tree(), 1, 4);
    }

    public void GenerateRandom(int amount)
    {
        Random r = new Random(1);
        
        for(int x = 0; x < 3 + amount; x++){
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new JewelBlue(), xRandom, yRandom);
        }
        for(int x = 0; x < 3 + amount; x++){
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new JewelGreen(), xRandom, yRandom);
        }
        for(int x = 0; x < 3 + amount; x++){
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new JewelRed(), xRandom, yRandom);
        }
        for(int x = 0; x < 5 + amount; x++){
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new Tree(), xRandom, yRandom); 
        }
        for(int x = 0; x < 7 + 2*amount; x++){
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new Water(), xRandom, yRandom);
        }
        for(int x = 0; x < 2 + amount; x++){
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new Toxic(), xRandom, yRandom);
        }
        
    }

    private Jewel? GetJewel(int x, int y)
    {
        if(Matriz[x, y] is Jewel jewel){
            Matriz[x, y] = new Empty();
            return jewel;
        }

        return null;
    }

    public Radioactive? GetRadioactive(int x, int y)
    {
        int[,] Coords = GenerateCoord(x, y);

        for(int i = 0; i < Coords.GetLength(0); i++){
            if(Matriz[Coords[i, 0], Coords[i, 1]] is Radioactive r) return r;
        }
        return null;
    }

    public Rechargeable? GetRechargeable(int x, int y)
    {
        int[,] Coords = GenerateCoord(x, y);

        for(int i = 0; i < Coords.GetLength(0); i++){
            if(Matriz[Coords[i, 0], Coords[i, 1]] is Rechargeable r) return r;
        }
        return null;
    }

    private int[,] GenerateCoord(int x, int y)
    {
        int[,] Coords = new int[4, 2] {
            {x, y+1 < w-1 ? y+1 : w-1}, 
            {x, y-1 > 0 ? y-1 : 0},
            {x+1 < h-1 ? x+1 : h-1, y},
            {x-1 > 0 ? x-1 : 0, y}
        };
        return Coords;
    }

    private bool IsAllowed(int x, int y){
        return Matriz[x, y] is Empty;
    }

    private bool IsToxic(int x, int y){
        return Matriz[x, y] is Toxic;
    }

    public void Print()
    {
        for(int i = 0; i < Matriz.GetLength(0); i++){
            for(int j = 0; j < Matriz.GetLength(1); j++){
                Console.Write(Matriz[i, j]);
            }
            Console.Write("\n");
        }
    }

    public bool IsDone()
    {
        for(int i = 0; i < Matriz.GetLength(0); i++){
            for(int j = 0; j < Matriz.GetLength(1); j++){
                if(Matriz[i, j] is Jewel) 
                    return false;
            }
        }

        return true;
    }

}
