using Cultura.Core;

namespace Cultura.Units
{
    public interface IDepositor
    {
        Inventory Inventory { get; set; }

        Construction.ResourceRepository TargetRepository { get; set; }

        void DepositResources();
    }
}