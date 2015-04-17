using MCSFramework.BLL.VST;
using MCSFramework.Model.VST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI.Model
{
    /// <summary>
    /// 拜访线路
    /// </summary>
    [Serializable]
    public class VisitRoute
    {
        /// <summary>
        /// 线路ID
        /// </summary>
        public int ID = 0;

        /// <summary>
        /// 线路编码
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 线路名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 拜访周期(7/14/21)
        /// </summary>
        public int VisitCycle = 0;

        /// <summary>
        /// 拜访日(1~31)
        /// </summary>
        public int VisitDay = 0;

        /// <summary>
        /// 是否顺序拜访
        /// </summary>
        public bool IsMustSequenceVisit = false;

        public VisitRoute() { }

        public VisitRoute(VST_Route m)
        {
            if (m != null) FillModel(m);
        }

        public VisitRoute(int RouteID)
        {
            if (RouteID > 0)
            {
                VST_Route r = new VST_RouteBLL(RouteID).Model;
                if (r != null) FillModel(r);
            }
        }

        private void FillModel(VST_Route m)
        {
            if (m == null) return;
            ID = m.ID;
            Code = m.Code;
            Name = m.Name;
            VisitCycle = m.VisitCycle;
            VisitDay = m.VisitDay;
            IsMustSequenceVisit = m.IsMustSequenceVisit.ToUpper() == "Y";
        }
    }
}