using System.Collections.Generic;

namespace Pronia.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Plant> Plants { get; set; }
    }
}
