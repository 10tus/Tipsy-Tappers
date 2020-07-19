using UnityEngine;

public static class GlassFactory {

    public static Glass GenerateGlass() {
        int _rand = Random.Range(0, 4);

        if (_rand == 0) {
            return new PoisonGlass(_rand);
        }
        else {
            return new RegularGlass(_rand);
        }
    }
}