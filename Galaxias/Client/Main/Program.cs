using Galaxias.Client.Main;
static class Program
{
    static void Main(string[] args)
    { 
        using var game = new Galaxias.Client.Main.GalaxiasClient();
        game.Run();
    }
} 


