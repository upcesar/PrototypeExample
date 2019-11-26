using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeExample.ConsoleApp.Shapes
{
    public class Rectangle : Shape
    {
        public int Width { get; private set; }
        public int Height { get; private set; }


        public Rectangle() => this.SetDimension(100, 100);
        public Rectangle(int x, int y, int width, int height)
        {
            this.Move(x, y);
            this.SetDimension(width, height);
        }

        public void SetDimension(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        protected override void PreserveAttributes(Shape shapeToClone)
        {
            base.PreserveAttributes(shapeToClone);
            var clone = shapeToClone as Rectangle;
            clone.Width = this.Width;
            clone.Height = this.Height;
        }
    }
}
