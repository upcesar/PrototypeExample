using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeExample.ConsoleApp.Shapes
{
    public class Tag
    {
        public Guid Id { get;  private set; }
        public string Notes { get; set; }

        public Tag() => Id = Guid.NewGuid();
        public Tag(string notes) : this() => this.Notes = notes;

        public override string ToString() => $"\n\tId: {Id}\n\tNotes: {Notes}\n";

    }
}
