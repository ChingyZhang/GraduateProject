using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI.Model
{
    /// <summary>
    /// TDP标准价表
    /// </summary>
    [Serializable]
    public class TDPStandardPrice
    {
        /// <summary>
        /// 价表ID
        /// </summary>
        public int ID = 0;

        /// <summary>
        /// 价表名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 价表适用区域
        /// </summary>
        public int FitSalesArea = 0;
        public string FitSalesAreaName = "";

        /// <summary>
        /// 价表适用渠道
        /// </summary>
        public int FitRTChannel = 0;
        public string FitRTChannelName = "";

        /// <summary>
        /// 价表备注
        /// </summary>
        public string Remark = "";

        public List<TDPStandardPriceDetail> Items = new List<TDPStandardPriceDetail>();

        public TDPStandardPrice() { }

        public TDPStandardPrice(PDT_StandardPrice m)
        {
            if (m != null) FillModel(m.ID);
        }

        public TDPStandardPrice(int PriceID) 
        {
            FillModel(PriceID);
        }

        private void FillModel(int PriceID)
        {
            PDT_StandardPriceBLL bll = new PDT_StandardPriceBLL(PriceID);
            if (bll.Model == null) return;

            ID = PriceID;
            Name = bll.Model.Name;
            FitSalesArea = bll.Model.FitSalesArea;
            FitRTChannel = bll.Model.FitRTChannel;
            Remark = bll.Model.Remark;

            #region 获取价表明细
            Items = new List<TDPStandardPriceDetail>(bll.Items.Count);
            foreach (PDT_StandardPrice_Detail item in bll.Items)
            {
                Items.Add(new TDPStandardPriceDetail(item));
            }
            #endregion
            
            #region 获取渠道名称
            if (FitRTChannel > 0)
            {
                CM_RTChannel_TDP channel = new CM_RTChannel_TDPBLL(FitRTChannel).Model;
                if (channel != null) FitRTChannelName = channel.Name;
            }
            #endregion

            #region 获取区域名称
            if (FitSalesArea > 0)
            {
                CM_RTSalesArea_TDP area = new CM_RTSalesArea_TDPBLL(FitSalesArea).Model;
                if (area != null) FitSalesAreaName = area.Name;
            }
            #endregion
            
        }


        [Serializable]
        public class TDPStandardPriceDetail
        {
            /// <summary>
            /// 产品ID
            /// </summary>
            public int Product = 0;
            /// <summary>
            /// 产品名称
            /// </summary>
            public string ProductName = "";

            /// <summary>
            /// 默认销售价
            /// </summary>
            public decimal Price = 0;

            /// <summary>
            /// 备注
            /// </summary>
            public string Remark = "";

            public TDPStandardPriceDetail() { }

            public TDPStandardPriceDetail(PDT_StandardPrice_Detail m)
            {
                Product = m.Product;
                Price = m.Price;
                Remark = m.Remark;

                PDT_Product p = new PDT_ProductBLL(m.Product).Model;
                if (p != null)
                {
                    ProductName = p.ShortName == "" ? p.FullName : p.ShortName;
                }
            }
        }
    }


}