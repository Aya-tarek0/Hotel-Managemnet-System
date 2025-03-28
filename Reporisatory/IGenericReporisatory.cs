namespace mvcproj.Reporisatory
{
    public interface IGenericReporisatory<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
        void Save();
    }
}
