namespace PickPoint.Lib.Filters.Declarations
{
    public interface IDataFilter<in TData, in TFilter, in TOption, out TResult>
    { 
        TResult Apply(TData data, TFilter filter, TOption option);
    }
}