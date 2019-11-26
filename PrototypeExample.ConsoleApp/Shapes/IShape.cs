using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrototypeExample.ConsoleApp.Shapes
{
    public interface IShape
    {
        Color Color { get; }
        int X { get; }
        int Y { get; }

        void FillColor(Color color);
        void Move(int x, int y);
    }
}
