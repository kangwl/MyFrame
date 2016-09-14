using System;
using System.Collections.Generic;
using Demo.Common.DB;
using Demo.Common.DB.Operate;
using Demo.Model;
using Demo.Repository.IRepository;
using Demo.Repository._Base;

namespace Demo.Repository
{
    public class CommissionDetailRepository : BaseRepository, ICommissionDetailRepository
    {
        public string TableName { get { return "CommissionDetail"; } }

        public bool Insert(CommissionDetail detail)
        {
            InsertEntity<CommissionDetail> insertEntity = new InsertEntity<CommissionDetail>(TableName, detail,
                new List<string>()
                {
                    nameof(detail.ID),
                    nameof(detail.Amt),
                    nameof(detail.UserID),
                    nameof(detail.CreateTime)
                });
            return base.InsertBase(insertEntity);
        }

        public bool UpdateAmt(Guid id,decimal amt)
        {
            CommissionDetail detail = new CommissionDetail
            {
                ID = id,
                Amt = amt
            };
            List<WhereItem> whereItems=new List<WhereItem>()
            {
                new WhereItem(nameof(detail.ID),"=")
            };
            UpdateEntity<CommissionDetail> updateEntity = new UpdateEntity<CommissionDetail>(TableName,
                new List<string>() {nameof(detail.Amt)}, whereItems, detail);

            return base.UpdateBase(updateEntity);
        }

        public bool Delete(Guid id)
        {
            CommissionDetail detail = new CommissionDetail
            {
                ID = id
            };
            List<WhereItem> whereItems = new List<WhereItem>()
            {
                new WhereItem(nameof(detail.ID),"=")
            };

            DeleteEntity<CommissionDetail> deleteEntity = new DeleteEntity<CommissionDetail>(TableName, whereItems,
                detail);

            return base.DeleteBase(deleteEntity);
        }

        public CommissionDetail GetOne(Guid id)
        {
            CommissionDetail detail = new CommissionDetail {ID = id};
            
            List<WhereItem> whereItems = new List<WhereItem>()
            {
                new WhereItem(nameof(detail.ID), "=")
            };
            SelectEntity<CommissionDetail> selectEntity = new SelectEntity<CommissionDetail>(TableName,
                new List<string>(), whereItems, detail);

            return base.GetOneBase(selectEntity);
        }

        public List<CommissionDetail> GetPaged(Guid userID, int pageIndex, int pageSize)
        {
            CommissionDetail detail = new CommissionDetail() {UserID = userID};
            var orderItem = new OrderByItem(nameof(detail.CreateTime), false);
            PagedEntity<CommissionDetail> pagedEntity = new PagedEntity<CommissionDetail>(TableName, orderItem,
                pageIndex, pageSize)
            {
                TEntity = detail,
                WhereItems = new List<WhereItem>() {new WhereItem(nameof(detail.UserID), "=")}
            };
            return base.GetPagedBase(pagedEntity);
        }

    }
}