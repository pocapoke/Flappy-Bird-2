using Godot;
using System;

public partial class GetScore : Area2D
{
    public void OnBodyExit(Node2D body)
    {
        if (body.GetClass() != "RigidBody2D") return;

        Bird player = (Bird)body;

        if (player != null && !player.crashed)
        {
            GameManager.Instance.Point();
        }
    }
}
