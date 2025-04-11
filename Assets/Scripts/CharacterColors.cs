using UnityEngine;

public class CharacterColors : MonoBehaviour
{
    public BodyColorCustom Body;
    public BodyColorCustom Arms;
    public BodyColorCustom Legs;
    public MeshRenderer Horn;
    public void RecolorBody(Material NewColor)
    {
        Body.ChangeMaterial(0, NewColor);
        Arms.ChangeMaterial(0, NewColor);
        Legs.ChangeMaterial(0, NewColor);
    }
    public void RecolorHorn(Material NewColor)
    {
        Horn.material = NewColor;
    }
}
