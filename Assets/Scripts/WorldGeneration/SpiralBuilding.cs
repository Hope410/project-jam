using UnityEngine;

[RequireComponent(typeof(ChildrenMeshesCombinator))]
public class SpiralBuilding : MonoBehaviour {
    public Transform Block;

    private ChildrenMeshesCombinator _combinator => GetComponent<ChildrenMeshesCombinator>();

    private void Awake()
    {
      _combinator.Combine();
    }

    private void Generate()
    {
    }
}