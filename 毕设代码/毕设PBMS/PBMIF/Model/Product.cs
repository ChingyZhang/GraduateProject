using System;
using System.Linq;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Model;
using MCSFramework.Model.Pub;
using System.Collections.Generic;
using MCSFramework.Common;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class Product
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ID = 0;

        /// <summary>
        /// 产品全称
        /// </summary>
        public string FullName = "";

        /// <summary>
        /// 产品简称
        /// </summary>
        public string ShortName = "";

        /// <summary>
        /// 产品编码(平台级编码)
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 零售商品条形码
        /// </summary>
        public string BarCode = "";

        /// <summary>
        /// 整件商品条形码
        /// </summary>
        public string BoxBarCode = "";

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName = "";

        /// <summary>
        /// 系列名称
        /// </summary>
        public string ClassifyName = "";

        /// <summary>
        ///商品种类
        /// <summary>
        public int Category = 0;
        public string CategoryName = "";

        /// <summary>
        /// 段位
        /// </summary>
        public int Grade = 0;
        /// <summary>
        /// 段位名称
        /// </summary>
        public string GradeName = "";

        /// <summary>
        /// 整件包装名称
        /// </summary>        
        public string TrafficPackagingName = "";

        /// <summary>
        /// 零售包装名称
        /// </summary>
        public string PackagingName = "";

        /// <summary>
        /// 包装系数
        /// </summary>
        public int ConvertFactor = 1;

        /// <summary>
        /// TDP自编码
        /// </summary>
        public string TDPCode = "";

        /// <summary>
        /// 默认采购价
        /// </summary>
        public decimal BuyPrice = 0;

        /// <summary>
        /// 默认销售价
        /// </summary>
        public decimal SalesPrice = 0;

        /// <summary>
        /// 生产厂商
        /// </summary>
        public string FactoryName = "";

        /// <summary>
        /// 厂家商品码
        /// </summary>
        public string FactoryCode = "";

        /// <summary>
        /// 规格
        /// </summary>
        public string Spec = "";

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark = "";

        /// <summary>
        /// 首要图片
        /// </summary>
        public Guid ImageGUID = Guid.Empty;

        public List<Attachment> Atts = new List<Attachment>();
        public Product() { }

        public Product(int ProductID, int TDP)
        {
            PDT_Product m = new PDT_ProductBLL(ProductID).Model;
            if (m != null) FillModel(m, TDP);
        }

        public Product(PDT_Product m, int TDP)
        {
            if (m != null) FillModel(m, TDP);
        }

        private void FillModel(PDT_Product m, int TDP)
        {
            ID = m.ID;
            FullName = m.FullName;
            ShortName = m.ShortName;
            Code = m.Code;
            BarCode = m.BarCode;
            BoxBarCode = m.BoxBarCode;
            ConvertFactor = m.ConvertFactor;
            Spec = m.Spec;
            Remark = m.Remark;
            Grade = m.Grade;
            Category = m.Category;
            FactoryCode = m.FactoryCode;
            FactoryName = m.FactoryName;

            #region 获取商品的TDP扩展管理信息
            if (TDP != 0)
            {
                IList<PDT_ProductExtInfo> exts = PDT_ProductExtInfoBLL.GetModelList("Supplier=" + TDP.ToString() + " AND Product=" + m.ID.ToString());
                if (exts.Count > 0)
                {
                    TDPCode = exts[0].Code;
                    if (exts[0].Category != 0) Category = exts[0].Category;
                    BuyPrice = exts[0].BuyPrice;
                    SalesPrice = exts[0].SalesPrice;
                }
            }
            #endregion

            #region 获取品牌、系列、类别名称
            if (m.Brand > 0)
            {
                PDT_Brand brand = new PDT_BrandBLL(m.Brand).Model;
                if (brand != null) BrandName = brand.Name;
            }
            if (m.Classify > 0)
            {
                PDT_Classify classify = new PDT_ClassifyBLL(m.Classify).Model;
                if (classify != null) ClassifyName = classify.Name;
            }
            if (Category > 0)
            {
                CategoryName = PDT_CategoryBLL.GetFullCategoryName(Category);
            }
            #endregion

            #region 获取字典表名称
            try
            {
                if (m.Grade > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("PDT_Grade")[m.Grade.ToString()];
                    if (dic != null) GradeName = dic.Name;
                }

                if (m.TrafficPackaging > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("PDT_Packaging")[m.TrafficPackaging.ToString()];
                    if (dic != null) TrafficPackagingName = dic.Name;
                }
                if (m.Packaging > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("PDT_Packaging")[m.Packaging.ToString()];
                    if (dic != null) PackagingName = dic.Name;
                }
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("MCSFramework.WSI.Product", err);
            }
            #endregion

            #region 获取首要图片
            string condition = " RelateType=11 AND RelateID=" + m.ID.ToString() + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',1)='Y'";
            IList<ATMT_Attachment> lists = ATMT_AttachmentBLL.GetModelList(condition);
            if (lists.Count > 0 && ATMT_AttachmentBLL.IsImage(lists[0].ExtName))
            {
                ImageGUID = lists[0].GUID;
            }
            #endregion

            #region 获取附件明细
            Atts = new List<Attachment>();
            IList<ATMT_Attachment> atts = ATMT_AttachmentBLL.GetAttachmentList(11, m.ID, new DateTime(1900, 1, 1), new DateTime(2100, 1, 1));
            foreach (ATMT_Attachment item in atts.OrderBy(p => p.Name))
            {
                Atts.Add(new Attachment(item));
            }
            #endregion
        }
    }
}
