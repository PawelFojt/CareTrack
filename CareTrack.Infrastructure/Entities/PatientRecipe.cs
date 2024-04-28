using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareTrack.Infrastructure.Entities;
public class PatientRecipe
{
    public int RecipeId { get; set; }
    public List<Recipe> Recipes { get; set; } = [];

    public int PatientId { get; set; }
    public Patient Patient { get; set; } = new();
}
