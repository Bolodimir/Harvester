using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandButton : RaycastTarget
{
    [SerializeField] GridView view;
    [SerializeField] GridModel model;
    [SerializeField] ExpandButton[] Neighbours;
    [SerializeField] Transform FirstBorderPoint;
    [SerializeField] Transform SecondBorderPoint;

    [SerializeField] Resource ExpandPrice;
    [SerializeField] Vector2 ExpandDirection;


    public override void OnPressed()
    {
        if (Stats.Instance.Check(ExpandPrice))
        {
            Stats.Instance.Withdraw(ExpandPrice);
            foreach (ExpandButton eb in Neighbours)
            {
                eb.OnNeighborExpanded(ExpandDirection);
            }
            model.ExpandGrid(ExpandDirection);
            MoveButton(ExpandDirection);
            MoveBorderPoints(ExpandDirection);
        }        
    }

    public void OnNeighborExpanded(Vector2 Expansion)
    {
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

    private void MoveBorderPoints(Vector2 MoveDirection)
    {
        Vector3 shift = new Vector3
        (
            MoveDirection.x * view.GetSize(),
            0,
            MoveDirection.y * view.GetSize()
        );

        if (MoveDirection.x > 0 || MoveDirection.y > 0) SecondBorderPoint.position += shift;
        if (MoveDirection.x < 0 || MoveDirection.y < 0) FirstBorderPoint.position += shift;
    }
}