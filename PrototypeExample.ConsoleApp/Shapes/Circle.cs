using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeExample.ConsoleApp.Shapes
{
    public class Circle : Shape
    {
        public int Radius { get; private set; }

        public Circle() : base() => this.SetRadius(100);

        public Circle(int radius, int x, int y)
        {
            this.SetRadius(radius);
            this.Move(x, y);
        }

        public void SetRadius(int radius) => this.Radius = radius;

        protected override void PreserveAttributes(Shape shapeToClone)
        {
            base.PreserveAttributes(shapeToClone);
            
            var clone = shapeToClone as Circle;
            clone.Radius = this.Radius;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(base.ToString());            
            return sb.AppendLine($" - Radius: {this.Radius}")
                     .ToString();
        }
    }
}
