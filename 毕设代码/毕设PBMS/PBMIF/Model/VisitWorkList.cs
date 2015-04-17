using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCSFramework.BLL.VST;
using MCSFramework.Model.VST;
using MCSFramework.Model;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

namespace MCSFramework.WSI.Model
{
    /// <summary>
    /// 拜访工作列表
    /// </summary>
    [Serializable]
    public class VisitWork
    {
        /// <summary>
        /// 拜访工作ID
        /// </summary>
        public int ID = 0;

        /// <summary>
        /// 工作人
        /// </summary>
        public int RelateStaff = 0;
        public string RelateStaffName = "";

        /// <summary>
        /// 关联路线
        /// </summary>
        public int Route = 0;
        public string RouteName = "";

        /// <summary>
        /// 拜访客户
        /// </summary>
        public int Client = 0;
        public string ClientName = "";

        /// <summary>
        /// 拜访模板
        /// </summary>
        public int VisitTemplate = 0;
        public string VisitTemplateName = "";

        /// <summary>
        /// 工作类别
        /// </summary>
        public int WorkingClassify = 0;
        public string WorkingClassifyName = "";

        /// <summary>
        /// 工作是否完成
        /// </summary>
        public bool IsComplete = false;

        /// <summary>
        /// 开始工作时间
        /// </summary>
        public DateTime BeginTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 结束工作时间
        /// </summary>
        public DateTime EndTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 拜访计划
        /// </summary>
        public int PlanID = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark = "";

        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime InsertTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 详细工作项目
        /// </summary>
        public List<VisitWorkItem> Items = null;

        public VisitWork() { }
        public VisitWork(int WorkListID)
        {
            if (WorkListID > 0) FillModel(WorkListID);
        }
        public VisitWork(VST_WorkList m)
        {
            if (m != null) FillModel(m.ID);
        }

        private void FillModel(int WorkListID)
        {
            VST_WorkListBLL bll = new VST_WorkListBLL(WorkListID);
            if (bll.Model == null) return;

            ID = bll.Model.ID;
            RelateStaff = bll.Model.RelateStaff;
            Route = bll.Model.Route;
            Client = bll.Model.Client;
            VisitTemplate = bll.Model.Template;
            WorkingClassify = bll.Model.WorkingClassify;
            IsComplete = bll.Model.IsComplete.ToUpper() == "Y";
            BeginTime = bll.Model.BeginTime;
            EndTime = bll.Model.EndTime;
            PlanID = bll.Model.PlanID;
            Remark = bll.Model.Remark;
            InsertTime = bll.Model.InsertTime;

            //详细工作项目
            Items = new List<VisitWorkItem>(bll.Items.Count);
            foreach (VST_WorkItem item in bll.Items)
            {
                Items.Add(new VisitWorkItem(item));
            }

            #region ID转名称
            try
            {
                if (RelateStaff != 0)
                {
                    Org_Staff s = new Org_StaffBLL(RelateStaff).Model;
                    if (s != null) RelateStaffName = s.RealName;
                }

                if (Route != 0)
                {
                    VST_Route r = new VST_RouteBLL(Route).Model;
                    if (r != null) RouteName = r.Name;
                }

                if (Client != 0)
                {
                    CM_Client c = new CM_ClientBLL(Client).Model;
                    if (c != null) ClientName = c.FullName;
                }

                if (VisitTemplate != 0)
                {
                    VST_VisitTemplate t = new VST_VisitTemplateBLL(VisitTemplate).Model;
                    if (t != null) VisitTemplateName = t.Name;
                }

                if (WorkingClassify != 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("VST_WorkingClassify")[WorkingClassify.ToString()];
                    if (dic != null) WorkingClassifyName = dic.Name;
                }
            }
            catch { }
            #endregion


        }

        [Serializable]
        public class VisitWorkItem
        {
            /// <summary>
            /// 拜访工作项目ID
            /// </summary>
            public int WorkItemID = 0;

            /// <summary>
            /// 拜访环节编码
            /// </summary>
            public string ProcessCode = "";

            /// <summary>
            /// 拜访环节
            /// </summary>
            public VisitProcess Process = null;

            /// <summary>
            /// 工作时间
            /// </summary>
            public DateTime WorkTime = new DateTime(1900, 1, 1);

            /// <summary>
            /// 上传时间
            /// </summary>
            public DateTime InsertTime = new DateTime(1900, 1, 1);

            /// <summary>
            /// 备注
            /// </summary>
            public string Remark = "";

            /// <summary>
            /// 详细扩展参数
            /// </summary>
            public Hashtable ExtParams = new Hashtable();

            public VisitWorkItem() { }
            public VisitWorkItem(VST_WorkItem m)
            {
                WorkItemID = m.ID;
                
                if (m.Process != 0)
                {
                    Process = new VisitProcess(m.Process);
                    ProcessCode = Process.Code;
                }

                WorkTime = m.WorkTime;
                InsertTime = m.InsertTime;
                Remark = m.Remark;

                ExtParams = new Hashtable();

                switch (Process.Code)
                {
                    #region 进店详细属性
                    case "JD":
                        {
                            VST_WorkItem_JD jd = VST_WorkItem_JDBLL.GetDetailByJobID(m.ID);

                            //进出店类型
                            ExtParams.Add("JobType", jd.JobType);   //1:进店 2:离店

                            //进出店方式
                            ExtParams.Add("JudgeMode", jd.JudgeMode);   //1：查询 2：扫码 3：NFC

                            //经度
                            ExtParams.Add("Longitude", jd.Longitude);

                            //纬度
                            ExtParams.Add("Latitude", jd.Latitude);

                            try
                            {
                                //进出店类型字典名
                                if (jd.JobType > 0)
                                {
                                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("VST_VisitJDType")[jd.JobType.ToString()];
                                    if (dic != null) ExtParams.Add("JobTypeName", dic.Name);
                                }

                                //进出店方式字典名
                                if (jd.JudgeMode > 0)
                                {
                                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("VST_VisitJudgeMode")[jd.JudgeMode.ToString()];
                                    if (dic != null) ExtParams.Add("JudgeModeName", dic.Name);
                                }
                            }
                            catch { }
                        }
                        break;
                    #endregion
                }


            }

        }
    }
}