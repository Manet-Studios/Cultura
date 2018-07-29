using Cultura.Construction;

namespace Cultura.Units
{
    public interface IBuilder
    {
        BuildingBlueprint Blueprint { get; set; }

        void Build();
    }
}