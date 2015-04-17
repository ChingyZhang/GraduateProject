using MCSFramework.BLL.VST;
using MCSFramework.Model.VST;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI.Model
{
    /// <summary>
    /// 拜访模板
    /// </summary>
    [Serializable]
    public class VisitTemplate
    {
        /// <summary>
        /// 拜访模板ID
        /// </summary>            
        public int ID = 0;

        /// <summary>
        /// 模板编码
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 是否必须关联线路
        /// </summary>
        public bool IsMustRelateRoute = false;

        /// <summary>
        /// 是否允许临时拜访
        /// </summary>
        public bool CanTempVisit = false;

        /// <summary>
        /// 是否强制顺序拜访
        /// </summary>
        public bool IsMustSequenceCall = false;

        /// <summary>
        /// 是否允许重复拜访
        /// </summary>
        public bool CanRepetitionCall = false;

        /// <summary>
        /// 是否必须关联车辆
        /// </summary>
        public bool IsMustRelateVehicel = false;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark = "";

        /// <summary>
        /// 拜访模板环节明细
        /// </summary>
        public List<VisitTemplateDetail> Items = new List<VisitTemplateDetail>();

        public VisitTemplate() { }
        public VisitTemplate(VST_VisitTemplate m)
        {
            if (m != null) FillModel(m.ID);
        }
        public VisitTemplate(int TemplateID)
        {
            if (TemplateID != 0) FillModel(TemplateID);
        }

        private void FillModel(int TemplateID)
        {
            VST_VisitTemplateBLL bll = new VST_VisitTemplateBLL(TemplateID);
            if (bll.Model == null) return;

            ID = bll.Model.ID;
            Code = bll.Model.Code;
            Name = bll.Model.Name;
            IsMustRelateRoute = bll.Model.IsMustRelateRoute.ToUpper() == "Y";
            CanTempVisit = bll.Model.CanTempVisit.ToUpper() == "Y";
            IsMustSequenceCall = bll.Model.IsMustSequenceCall.ToUpper() == "Y";
            CanRepetitionCall = bll.Model.CanRepetitionCall.ToUpper() == "Y";
            IsMustRelateVehicel = bll.Model["IsMustRelateVehicel"].ToUpper() == "Y";

            Remark = bll.Model.Remark;

            Items = new List<VisitTemplateDetail>(bll.Items.Count);
            foreach (VST_VisitTemplateDetail item in bll.Items.OrderBy(p => p.SortID))
            {
                Items.Add(new VisitTemplateDetail(item));
            }
        }

        /// <summary>
        /// 拜访模板环节子表
        /// </summary>
        [Serializable]
        public class VisitTemplateDetail
        {
            /// <summary>
            /// 拜访环节
            /// </summary>
            public VisitProcess Process = null;

            /// <summary>
            /// 顺序号
            /// </summary>
            public int SortID = 0;

            /// <summary>
            /// 是否可以跳过
            /// </summary>
            public bool CanSkip = false;

            /// <summary>
            /// 备注
            /// </summary>
            public string Remark = "";

            public VisitTemplateDetail() { }
            public VisitTemplateDetail(VST_VisitTemplateDetail m)
            {
                Process = new VisitProcess(m.ProcessID);
                SortID = m.SortID;
                CanSkip = m.CanSkip.ToUpper() == "Y";
                Remark = m.Remark;
            }
        }
    }

    /// <summary>
    /// 拜访步骤
    /// </summary>
    [Serializable]
    public class VisitProcess
    {
        /// <summary>
        /// 步骤编码
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 是否必须关联门店
        /// </summary>
        public bool IsMustRelateClient = false;

        /// <summary>
        /// 是否可直接调用
        /// </summary>
        public bool CanDirectCall = false;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark = "";

        /// <summary>
        /// 扩展参数
        /// </summary>
        public Hashtable ExtParams = new Hashtable();

        public VisitProcess() { }
        public VisitProcess(int ProcessID)
        {
            VST_Process m = new VST_ProcessBLL(ProcessID).Model;
            if (m != null) FillModel(m);
        }
        public VisitProcess(string ProcessCode)
        {
            VST_Process m = new VST_ProcessBLL(ProcessCode).Model;
            if (m != null) FillModel(m);
        }
        public VisitProcess(VST_Process m)
        {
            if (m != null) FillModel(m);
        }

        private void FillModel(VST_Process m)
        {
            if (m == null) return;

            Code = m.Code;
            Name = m.Name;
            IsMustRelateClient = m.IsMustRelateClient.ToUpper() == "Y";
            CanDirectCall = m.CanDirectCall.ToUpper() == "Y";
            Remark = m.Remark;

            //获取指定步骤编码开头的扩展字段属性,扩展字段名以步骤编码开头
            ExtParams = new Hashtable();
            foreach (string key in m.ExtPropertys.Keys)
            {
                if (key.ToUpper().StartsWith(Code.ToUpper()))
                {
                    ExtParams.Add(key, m.ExtPropertys[key]);
                }
            }
        }
    }

}