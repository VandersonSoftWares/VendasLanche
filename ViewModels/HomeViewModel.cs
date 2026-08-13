using MVCLanche.Models;

namespace MVCLanche.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; } = Enumerable.Empty<Lanche>();
    }
}