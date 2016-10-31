namespace AssemblyCreep.Models
{
    public class SelectedDependentItemViewModel<TItem, TParent> : SelectedItemViewModel<TItem>
    {
        public TParent Parent { get; private set; }

        public SelectedDependentItemViewModel(TItem item, TParent parent = default(TParent)) :
            base(item)
        {
            Parent = parent;
        }
    }
}
