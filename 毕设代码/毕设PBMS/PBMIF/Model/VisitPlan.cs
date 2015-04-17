using MCSFramework.Model.CM;
using MCSFramework.Model.VST;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.VST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCSFramework.Model;
using MCSFramework.BLL;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class VisitPlan
    {
        #region 公共属性
        ///<summary>
        ///ID
        ///</summary>
        public int ID = 0;

        ///<summary>
        ///线路ID
        ///</summary>
        public int Route = 0;
        public string RouteName = "";

        ///<summary>
        ///关联业代
        ///</summary>
        public int RelateStaff = 0;
        public string RelateStaffName = "";

        ///<summary>
        ///计划拜访日期
        ///</summary>
        public DateTime PlanVisitDate = new DateTime(1900, 1, 1);

        ///<summary>
        ///是否顺序拜访
        ///</summary>
        public string IsMustSequenceVisit = "N";

        ///<summary>
        ///备注
        ///</summary>
        public string Remark = "";

        ///<summary>
        ///审核标志
        ///</summary>
        public int ApproveFlag = 2;

        public List<VisitPlanDetail> Items = new List<VisitPlanDetail>();
        #endregion

        public VisitPlan() { }
        public VisitPlan(VST_VisitPlan m)
        {
            if (m != null) FillModel(m.ID);
        }

        public VisitPlan(int ID)
        {
            FillModel(ID);
        }

        private void FillModel(int ID)
        {
            VST_VisitPlanBLL bll = new VST_VisitPlanBLL(ID);
            if (bll.Model == null) return;

            VST_VisitPlan m = bll.Model;

            ID = m.ID;
            Route = m.Route;
            RelateStaff = m.RelateStaff;
            PlanVisitDate = m.PlanVisitDate;
            IsMustSequenceVisit = m.IsMustSequenceVisit;
            Remark = m.Remark;
            ApproveFlag = m.ApproveFlag;

            Items = new List<VisitPlanDetail>(bll.Items.Count);
            foreach (var item in bll.Items)
            {
                Items.Add(new VisitPlanDetail(item));
            }

            if (m.Route > 0)
            {
                VST_Route r = new VST_RouteBLL(m.Route).Model;
                if (r != null) RouteName = r.Name;

                Org_Staff s = new Org_StaffBLL(m.RelateStaff).Model;
                if (s != null) RelateStaffName = s.RealName;
            }
        }
    }

    [Serializable]
    public class VisitPlanDetail
    {
        ///<summary>
        ///ID
        ///</summary>
        public int ID = 0;

        ///<summary>
        ///拜访客户
        ///</summary>
        public int Client = 0;
        public string ClientName = "";

        ///<summary>
        ///拜访顺序
        ///</summary>
        public int VisitSequence = 0;

        ///<summary>
        ///是否拜访
        ///</summary>
        public int VisitedFlag = 2;

        ///<summary>
        ///实际拜访时间
        ///</summary>
        public DateTime VisitedTime = new DateTime(1900, 1, 1);

        ///<summary>
        ///备注
        ///</summary>
        public string Remark = "";

        public VisitPlanDetail() { }
        public VisitPlanDetail(VST_VisitPlan_Detail m)
        {
            ID = m.ID;
            Client = m.Client;
            VisitSequence = m.VisitSequence;
            VisitedFlag = m.VisitedFlag;
            VisitedTime = m.VisitedTime;
            Remark = m.Remark;

            if (m.Client > 0)
            {
                CM_Client c = new CM_ClientBLL(m.Client).Model;
                if (c != null) ClientName = c.FullName;
            }
        }

    }
}