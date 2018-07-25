namespace Cultura.Core
{
    public interface ISelectable
    {
        bool Selected { get; set; }

        void OnSelect();

        void OnDeselect();
    }
}