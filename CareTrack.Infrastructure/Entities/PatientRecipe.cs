using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareTrack.Infrastructure.Entities;
public class PatientRecipe
{
    public int RecipeId { get; set; }
    public int PatientId { get; set; }
    public virtual Recipe? Recipe { get; set; }
    public virtual Patient? Patient { get; set; }
}
