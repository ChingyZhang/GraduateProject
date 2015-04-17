using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.IFStrategy;
using MCSFramework.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace MCSFramework.SQLDAL
{
    public abstract class BaseComplexDAL<H, D> : BaseSimpleDAL<H>, IComplexDAL<H, D>
    {
        private int _headid = 0;

        /// <summary>
        /// Head Table's ID
        /// </summary>
        public int HeadID
        {
            get { return _headid; }
            set { _headid = value; }
        }

        #region ISimpleDAL<T> 成员
        /// <summary>
        /// 获取实例化后的Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new virtual H GetModel(int id)
        {
            HeadID = id;

            return base.GetModel(id);
        }

        /// <summary>
        /// 删除指定ID的记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new virtual int Delete(int id)
        {
            HeadID = id;
            DeleteDetail();     //先删除所有明细

            return base.Delete(id);
        }
        #endregion

        #region IComplexDAL<H,D> 成员
        /// <summary>
        /// 向数据库中增加主从表记录
        /// </summary>
        /// <param name="h">主表Model</param>
        /// <param name="d">从表IList(Model)</param>
        /// <returns></returns>
        public virtual int Add(H h, IList<D> items)
        {
            HeadID = Add(h);

            if (HeadID < 0) return HeadID;
            if (items != null && items.Count > 0) AddDetail(items);
            return HeadID;
        }

        /// <summary>
        /// 获取主从表的明细项目
        /// </summary>
        /// <returns>从表明细项目的Model列表</returns>
        public virtual IList<D> GetDetail()
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.Int,4,HeadID)};
            SqlDataReader dr = null;

            SQLDatabase.RunProc(_ProcePrefix + "_GetDetail", parameters, out dr);

            return FillDetailModelList(dr);
        }

        /// <summary>
        /// 根据条件获取主从表的明细项目
        /// </summary>
        /// <param name="condition"></param>
        /// <returns>从表明细项目的Model列表</returns>
        public virtual IList<D> GetDetail(string condition)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.Int,4,HeadID),
				    SQLDatabase.MakeInParam("@Condition", SqlDbType.VarChar, condition.Length, condition)};
            SqlDataReader dr = null;

            SQLDatabase.RunProc(_ProcePrefix + "_GetDetailByCondition", parameters, out dr);

            return FillDetailModelList(dr);
        }

        /// <summary>
        /// 向从表增加明细项目
        /// </summary>
        /// <param name="items">从表IList(Model)</param>
        /// <returns></returns>
        public virtual int AddDetail(IList<D> items)
        {
            int ret = 0;
            foreach (D d in items)
            {
                if (AddDetail(d) < 0) return -1;
            }

            return ret;
        }

        /// <summary>
        /// 新增明细项目
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public abstract int AddDetail(D d);

        /// <summary>
        /// 更新从表的明细项目
        /// </summary>
        /// <param name="items">从表IList(Model)</param>
        /// <returns></returns>
        public virtual int UpdateDetail(IList<D> items)
        {
            int ret = 0;
            foreach (D d in items)
            {
                if (UpdateDetail(d) < 0) return -1;
            }

            return ret;
        }

        /// <summary>
        /// 更新指定明细项目
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public abstract int UpdateDetail(D d);

        /// <summary>
        /// 删除指定明细项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int DeleteDetail(int detailid)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@DetailID", SqlDbType.Int,4,detailid)				};
            return SQLDatabase.RunProc(_ProcePrefix + "_DeleteDetail", parameters);
        }

        /// <summary>
        /// 删除所有明细项目
        /// </summary>
        /// <returns></returns>
        public virtual int DeleteDetail()
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.Int,4,HeadID)				};
            return SQLDatabase.RunProc(_ProcePrefix + "_ClearDetail", parameters);
        }

        /// <summary>
        /// 根据明细记录的ID，获取明细Model
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public virtual D GetDetailModel(int detailid)
        {
            SqlDataReader dr = null;
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@DetailID", SqlDbType.Int,4,detailid)				};
            SQLDatabase.RunProc(_ProcePrefix + "_GetDetailModel", parameters, out dr);

            D m = default(D);

            if (dr.Read())
            {
                m = FillDetailModel(dr);
            }
            dr.Close();

            return m;
        }
        #endregion

        /// <summary>
        /// 根据IDataReader来填充明细项目的Model列表集
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public IList<D> FillDetailModelList(IDataReader dr)
        {
            IList<D> list = new List<D>();
            while (dr.Read())
            {
                list.Add(FillDetailModel(dr));
            }
            dr.Close();

            return list;
        }

        /// <summary>
        /// 根据IDataReader的当前值来填充明细项目Model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected abstract D FillDetailModel(IDataReader dr);
    }
}
