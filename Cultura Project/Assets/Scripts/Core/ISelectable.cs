namespace Cultura.Units
{
    public interface ISelectable
    {
        bool Selected { get; set; }

        void OnSelect();

        void OnDeselect();
    }
}