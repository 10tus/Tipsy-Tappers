using UnityEngine;

public static class GlassFactory {
    public static Glass GenerateGlass() {
        int _rand = new Random().Next(0, 5);

        if (_rand == 0) 
            return new PoisonGlass(); 
        else 
            return new RegularGlass(_rand);
    }
}