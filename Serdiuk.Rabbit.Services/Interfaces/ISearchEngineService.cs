namespace Serdiuk.Rabbit.Services.Interfaces
{
    public interface ISearchEngineService<TIndex>
    {
        Task<bool> IndexAsync(TIndex index);
        Task<bool> UpdateIndexAsync(TIndex index);
        Task<bool> DeleteIndexAsync(TIndex index);
        Task<List<TIndex>> SearchAsync(string key);
    }
}
