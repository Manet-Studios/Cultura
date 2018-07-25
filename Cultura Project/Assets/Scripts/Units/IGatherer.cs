using Cultura.Core;

namespace Cultura.Units
{
    public interface IGatherer
    {
        Inventory Inventory { get; set; }
        Construction.ResourceDeposit TargetDeposit { get; set; }

        void GatherResources();
    }
}