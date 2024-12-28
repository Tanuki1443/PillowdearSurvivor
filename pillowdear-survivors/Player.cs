using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400;

	private Vector2 ScreenSize;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size; 
	}

	public override void _Process(double delta)
	{
		Movement(delta);
		attack();
	}

	private void Movement(double delta)
	{
		var velocity = Vector2.Zero;
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D"); // Get the animated sprite for walking

		if (Input.IsActionPressed("right"))
		{
			velocity.X += 1;
		}
		
		if (Input.IsActionPressed("left"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("down"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("up"))
		{
			velocity.Y -= 1;
		}

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		
		if (velocity.X !=0){
			animatedSprite2D.Animation = "walk";
//			animatedSprite2D.FlipV = false;
//			animatedSprite2D.FlipH = velocity.X < 0;//flipping the character sprite for walking if needed
		}
		else if (velocity.Y != 0){
			animatedSprite2D.Animation = "up";
//			animatedSprite2D.FlipV = velocity.Y > 0; //flipping the character sprite for walking if needed
		}
		}
	public void attack(){
		var attackAnimation =  GetNode<AnimatedSprite2D>("Attack"); //get the animatedsprite for the chosen attack or some shit
		attackAnimation.Play("attack");
	}
}
