using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service.Extend
{
    public class ServiceBase<TModel> : ServiceBaseSimple<TModel> where TModel : class
    {
        public bool Exist(Func<TModel, bool> funcWhere)
        {
            using (var context = GetContext())
            {
                return Data.Any(funcWhere);
            }
        }

        public TModel GetOne(Func<TModel, bool> funcWhere)
        {
            using (var context = GetContext())
            {
                var one = Data.First(funcWhere);
                return one;
            }
        }

        public List<TModel> GetTop<TKey>(Func<TModel, bool> funcWhere, Func<TModel, TKey> funcOrderBy, int count)
        {
            using (var context = GetContext())
            {
                var list = Data.Where(funcWhere).OrderBy(funcOrderBy).Take(count);
                return list.ToList();
            }
        }

        public List<TModel> GetPaged<TKey>(Func<TModel, bool> funcWhere, Func<TModel, TKey> funcOrderBy, int pageIndex,
            int pageSize, bool asc = true)
        {
            using (var context = GetContext())
            {
                int skip = (pageIndex - 1)*pageSize;
                var paged = Data.Where(funcWhere);
                if (asc)
                {
                    paged = paged.OrderBy(funcOrderBy);
                }
                else
                {
                    paged = paged.OrderByDescending(funcOrderBy);
                }

                paged = paged.Skip(skip).Take(pageSize);

                return paged.ToList();
            }
        }

        public bool Insert(TModel t)
        {
            using (var context = GetContext())
            {
                Data.Add(t);
                int ret = context.SaveChanges();
                return ret > 0;
            }
        }

        public bool Delete(TModel t)
        {
            using (var context = GetContext())
            {
                Data.Remove(t);
                int ret = context.SaveChanges();
                return ret > 0;
            }
        }

        public bool DeleteBatch(List<TModel> tList)
        {
            using (var context = GetContext())
            {
                tList.ForEach(one =>
                {
                    Data.Remove(one);
                });
                int ret = context.SaveChanges();
                return ret > 0;
            }
        }

        public bool Update(TModel t)
        {
            using (var context = GetContext())
            {
                context.Entry(t).State = EntityState.Modified;
                int ret = context.SaveChanges();
                return ret > 0;
            }
        }

        public bool UpdateBatch(List<TModel> tList)
        {
            using (var context = GetContext())
            {
                tList.ForEach(one =>
                {
                    context.Entry(one).State = EntityState.Modified;
                });

                int ret = context.SaveChanges();
                return ret > 0;
            }
        }


    }
}
