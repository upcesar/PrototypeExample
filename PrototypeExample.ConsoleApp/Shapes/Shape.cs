using PrototypeExample.ConsoleApp.Prototype;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrototypeExample.ConsoleApp.Shapes
{
    public abstract class Shape : Prototype<Shape>, IShape
    {
        public string Name => this.GetType().Name;
        public Color Color { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Tag Tag { get; set; }
        
        public string Notes;

        public Shape()
        {
            this.Move(0, 0);
            Color = Color.Transparent;
        }

        public void Move(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void FillColor(Color color) => this.Color = color;

        protected override void PreserveAttributes(Shape clone)
        {
            clone.Tag = new Tag(this.Tag.Notes);
            clone.Color = this.Color;
            clone.X = this.X;
            clone.Y = this.Y;
            clone.Notes = string.Concat(this.Notes);
        }

        public override string ToString()
        {
            var sb = new StringBuilder($"\nShape - {this.GetType().Name}\n");            
            return sb.AppendLine($" - X     : {this.X}")
                     .AppendLine($" - Y     : {this.Y}")
                     .AppendLine($" - Color : {this.Color.Name}")
                     .ToString();
        }
    }
}
