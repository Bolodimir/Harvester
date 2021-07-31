using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandButton : RaycastTarget
{
    [SerializeField] GridView view;
    [SerializeField] GridModel model;
    [SerializeField] ExpandButton[] Neighbours;

    [SerializeField] Resource ExpandPrice;
    [SerializeField] Vector2 ExpandDirection;


    public override void OnPressed()
    {
        if (Stats.Instance.Check(ExpandPrice))
        {
            foreach(ExpandButton eb in Neighbours)
            {
                eb.OnNeighborExpanded(ExpandDirection);
            }
            model.ExpandGrid(ExpandDirection);
            MoveButton(ExpandDirection);
        }        
    }
    public void OnNeighborExpanded(Vector2 Expansion)
    {
        print(Expansion);
        transform.localScale = transform.localScale + new Vector3
        (
            Mathf.Abs(Expansion.x) * view.GetSize(),
            0,
            Mathf.Abs(Expansion.y) * view.GetSize()
        );

        MoveButton(Expansion / 2);
    }
    private void MoveButton(Vector2 MoveDirection)
    {
        Vector3 shift = new Vector3
        (
            MoveDirection.x * view.GetSize(),
            0,
            MoveDirection.y * view.GetSize()
        );
        transform.position = transform.position + shift;
    }

}
