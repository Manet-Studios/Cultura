namespace Cultura.Construction.Modules
{
    public interface IBuildingModule
    {
        int ID { get; }

        void OnBuild(BuildingBase buildingBase);

        void OnDemolish();
    }
}