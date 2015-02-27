using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Data;

namespace MCSFramework.BLL
{
    public class Org_PositionBLL : BaseSimpleBLL<Org_Position>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Org_Position_DAL";
        private Org_Position_DAL _dal;

        #region 构建BLL
        public Org_PositionBLL()
            : base(DALClassName)
        {
            _dal = (Org_Position_DAL)_DAL;
            _m = new Org_Position();    //实例化Model
        }

        public Org_PositionBLL(int id)
            : base(DALClassName)
        {
            _dal = (Org_Position_DAL)_DAL;
            FillModel(id);
        }

        public Org_PositionBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Org_Position_DAL)_DAL;
            FillModel(id, bycache);
        }

        #endregion

        public static IList<Org_Position> GetModelList(string condition)
        {
            Org_PositionBLL bll = new Org_PositionBLL();
            return bll._GetModelList(condition);
        }

        public string GetAllChildPosition()
        {
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Org_Position", "ID", "SuperID", _m.ID.ToString());
            string ids = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ids != "") ids += ",";
                ids += dt.Rows[i]["ID"];
            }

            return ids;

        }

        public static DataTable GetAllChildPositionList(int ID)
        {
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Org_Position", "ID", "SuperID", ID.ToString());
            return dt;

        }
        /// <summary>
        /// 获取所有职位
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllPostion()
        {
            ///可以引入缓存
            Org_Position_DAL dal = (Org_Position_DAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAllPosition();
        }
    }
}
