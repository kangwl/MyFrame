using System;
using System.Collections.Generic;
using Demo.Model;
using Demo.Repository._Base;

namespace Demo.Repository.IRepository
{
    public interface ICommissionDetailRepository:IRepositoryBase
    {
        CommissionDetail GetOne(Guid id);
        bool Insert(CommissionDetail detail);
        bool UpdateAmt(Guid id, decimal amt);
        bool Delete(Guid id);
        List<CommissionDetail> GetPaged(Guid userID, int pageIndex, int pageSize);
    }
}