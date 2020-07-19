using UnityEngine;

public static class GlassFactory {

    public static Glass GenerateGlass() {
        // Controll the probability of poison glasses appearing
        // 0 = poison, 1 - 4 = regular glass, 1/5 chance of poison glass
        int _glassValue = Random.Range(0, 5);

        if (_glassValue == 0) {
            return new PoisonGlass(_glassValue);
        }
        else {
            return new RegularGlass(_glassValue);
        }
    }
}