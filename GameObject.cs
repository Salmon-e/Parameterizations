using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Parameterizations
{
    class GameObject
    {
        public Vector2 position;
        public MovementChain movement;
        Game1 game;
        public GameObject(Vector2 position, MovementChain movement, Game1 game)
        {
            this.game = game;
            this.position = position;
            this.movement = movement;
        }
        public GameObject(Vector2 position, Movement movement, Game1 game)
        {
            this.game = game;
            this.position = position;
            this.movement = new MovementChain();
            this.movement.AddMovement(movement);
        }
        public void Tick()
        {
            movement.Tick();
            position = movement.GetPosition();
        }
    }
}
