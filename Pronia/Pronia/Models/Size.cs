using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Size
    {
        public int Id { get; set; }
        
        [StringLength(maximumLength:10,ErrorMessage ="You can write max 10 character")]
        public string Name { get; set; }

        public List<Plant> Plants { get; set; }
    }
}
