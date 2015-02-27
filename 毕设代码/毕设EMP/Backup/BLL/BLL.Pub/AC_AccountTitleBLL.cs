
// ===================================================================
// 文件： AC_AccountTitleDAL.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;
using System.Collections.Generic;
using System.Linq;

namespace MCSFramework.BLL.Pub
{
    /// <summary>
    ///AC_AccountTitleBLL业务逻辑BLL类
    /// </summary>
    public class AC_AccountTitleBLL : BaseSimpleBLL<AC_AccountTitle>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.AC_AccountTitleDAL";
        private AC_AccountTitleDAL _dal;

        #region 构造函数
        ///<summary>
        ///AC_AccountTitleBLL
        ///</summary>
        public AC_AccountTitleBLL()
            : base(DALClassName)
        {
            _dal = (AC_AccountTitleDAL)_DAL;
            _m = new AC_AccountTitle();
        }

        public AC_AccountTitleBLL(int id)
            : base(DALClassName)
        {
            _dal = (AC_AccountTitleDAL)_DAL;
            FillModel(id);
        }

        public AC_AccountTitleBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (AC_AccountTitleDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<AC_AccountTitle> GetModelList(string condition)
        {
            return new AC_AccountTitleBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取指定费用类型的会计科目
        /// </summary>
        /// <param name="FeeType"></param>
        /// <returns></returns>
        public static IList<AC_AccountTitle> GetListByFeeType(int FeeType)
        {
            IList<AC_AccountTitle> source = GetModelList("(ID IN (SELECT AccountTitle FROM AC_AccountTitleInFeeType WHERE FeeType=" + FeeType.ToString() + ") OR ID = 1) AND MCS_SYS.dbo.UF_Spilt2('MCS_Pub.dbo.AC_AccountTitle',ExtPropertys,'IsDisable')<>'Y'");

            IList<AC_AccountTitle> dest = new List<AC_AccountTitle>(source.Count);
            SortAccountTitle(source, dest, 0);
            return dest;
        }

        public static void SortAccountTitle(IList<AC_AccountTitle> source, IList<AC_AccountTitle> dest, int superid)
        {
            foreach (AC_AccountTitle item in source.Where(p => p.SuperID == superid))
            {
                item.Name = "——————————".Substring(0, item.Level) + item.Name;
                dest.Add(item);
                SortAccountTitle(source, dest, item.ID);
            }
        }

        public static DataTable GetAllChild(int AccountTitle)
        {
            AC_AccountTitleDAL dal = (AC_AccountTitleDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAllChild(AccountTitle);          
        }
    }
}