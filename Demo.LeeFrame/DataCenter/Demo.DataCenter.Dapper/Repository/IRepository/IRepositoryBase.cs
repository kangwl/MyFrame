namespace Demo.DataCenter.Dapper.Repository.IRepository
{
    public interface IRepositoryBase<in T> where T:class 
    {
        string TableName { get; }
        bool Insert(T t);
    }
}