namespace spiritual_center.Code
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add (TEntity entity);
        TEntity Update (TEntity entity);
        TEntity Delete (TEntity entity);
        IEnumerable<TEntity> Index();
        IEnumerable<TEntity> Find(Func<TEntity,bool> predicate);
        bool IsExist(Func<TEntity,bool> predicate);
    }
}
