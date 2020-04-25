using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nswag_liquid_slim.ViewModels.Lookup
{
    public class GeographicAreaSelectionViewModel
    {
        public string AreaName { get; set; } = "";

        public string State { get; set; } = "";

        public override int GetHashCode() =>
            $"{AreaName},{State}".GetHashCode();

        public override bool Equals(object? obj)
        {
            var otherObj = obj as GeographicAreaSelectionViewModel;

            return otherObj?.GetHashCode() == GetHashCode();
        }
    }
}
