﻿

using Client.Code.Main;

static class Program
{
    static void Main(string[] args)
    { 
        using var game = new GalaxiasClient();
        game.Run();
    }
} 

