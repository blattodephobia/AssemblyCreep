namespace AssemblyCreep.Models
{
    public class SelectedDependentItemModel<TItem, TParent> : SelectedItemModel<TItem>
    {
        public TParent Parent { get; private set; }

        public SelectedDependentItemModel(TItem item, TParent parent = default(TParent)) :
            base(item)
        {
            Parent = parent;
        }
    }
}
