using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MCSFramework.IFStrategy;
using MCSFramework.SQLDAL;

namespace MCSFramework.BLL
{
    public abstract class BaseComplexBLL<H, D> : BaseSimpleBLL<H>, IComplexBill<H, D>
    {
        private IComplexDAL<H, D> _d;

        private IList<D> _items;

        protected new IComplexDAL<H, D> _DAL
        {
            get
            { return _d; }
        }

        public BaseComplexBLL(string dalclassname)
            : base(dalclassname,false)
        {
            _d = (IComplexDAL<H, D>)base._DAL;
            _d.HeadID = 0;
        }

        public new virtual int Add()
        {
            return _d.Add(_m, _items);
        }

        #region IComplexBill<H,D> 成员

        public IList<D> Items
        {
            get
            {
                if (_items == null) _items = _d.GetDetail();
                return _items;
            }
            set { _items = value; }
        }

        public IList<D> GetDetail(string condition)
        {
            return _d.GetDetail(condition);
        }

        public int AddDetail()
        {
            return _d.AddDetail(_items);
        }

        public int AddDetail(D d)
        {
            return _d.AddDetail(d);
        }

        public int UpdateDetail()
        {
            return _d.UpdateDetail(_items);
        }

        public int UpdateDetail(D d)
        {
            return _d.UpdateDetail(d);
        }

        public int DeleteDetail(int detailid)
        {
            return _d.DeleteDetail(detailid);
        }

        public int DeleteDetail()
        {
            return _d.DeleteDetail();
        }

        public D GetDetailModel( int detailid)
        {
            return _d.GetDetailModel(detailid);
        }
        #endregion
    }
}
