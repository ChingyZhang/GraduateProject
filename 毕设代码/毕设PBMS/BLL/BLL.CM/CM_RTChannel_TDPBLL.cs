
// ===================================================================
// 文件： CM_RTChannel_TDPDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.SQLDAL.CM;

namespace MCSFramework.BLL.CM
{
    /// <summary>
    ///CM_RTChannel_TDPBLL业务逻辑BLL类
    /// </summary>
    public class CM_RTChannel_TDPBLL : BaseSimpleBLL<CM_RTChannel_TDP>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_RTChannel_TDPDAL";
        private CM_RTChannel_TDPDAL _dal;

        #region 构造函数
        ///<summary>
        ///CM_RTChannel_TDPBLL
        ///</summary>
        public CM_RTChannel_TDPBLL()
            : base(DALClassName)
        {
            _dal = (CM_RTChannel_TDPDAL)_DAL;
            _m = new CM_RTChannel_TDP();
        }

        public CM_RTChannel_TDPBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_RTChannel_TDPDAL)_DAL;
            FillModel(id);
        }

        public CM_RTChannel_TDPBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_RTChannel_TDPDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CM_RTChannel_TDP> GetModelList(string condition)
        {
            return new CM_RTChannel_TDPBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 根据供应商获取其自营渠道
        /// </summary>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static IList<CM_RTChannel_TDP> GetRTChannel_ByTDP(int TDP)
        {
            return GetModelList("OwnerClient=" + TDP.ToString());
        }
        
    }
}