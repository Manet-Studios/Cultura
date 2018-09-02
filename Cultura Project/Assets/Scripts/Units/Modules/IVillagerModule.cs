namespace Cultura.Units.Modules
{
    public interface IVillagerModule
    {
        int ID { get; }

        void Initialize(VillagerBase villagerBase);
    }

    public enum ModuleID
    {
        Gatherer,
        Builder,
        Worker
    }
}