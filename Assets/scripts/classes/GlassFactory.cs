using UnityEngine;

public static class GlassFactory {
    static Sprite _deathdrink = Resources.Load<Sprite>("deathdrink");
    static Sprite[] _drinks = { 
        Resources.Load<Sprite>("drink"), 
        Resources.Load<Sprite>("drink2"), 
        Resources.Load<Sprite>("drink3") 
        };
        
    public static Glass GenerateGlass() {
        Glass newGlass;
        int _rand = Random.Range(0, 5);

        if (_rand == 0) {
            newGlass = new PoisonGlass(_rand);
            newGlass.glassObject.GetComponent<SpriteRenderer>().sprite = _deathdrink;
        }
        else {
            newGlass = new RegularGlass(_rand);
            newGlass.glassObject.GetComponent<SpriteRenderer>().sprite = _drinks[_rand % _drinks.Length];
        }

        return newGlass;
    }
}