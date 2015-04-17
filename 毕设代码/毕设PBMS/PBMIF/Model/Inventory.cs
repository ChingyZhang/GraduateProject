using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCSFramework.Model.PBM;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class Inventory
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public int Product = 0;

        /// <summary>
        /// 批号
        /// </summary>
        public string LotNumber = "";

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity = 0;

        /// <summary>
        /// 库存价格
        /// </summary>
        public decimal CostPrice = 0;

        /// <summary>
        /// 默认销售价
        /// </summary>
        public decimal Price = 0;

        /// <summary>
        ///商品种类
        /// <summary>
        public string CategoryName = "";

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName = "";

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName = "";

        /// <summary>
        /// 整件包装
        /// </summary>
        public string PackingName_T = "";

        /// <summary>
        /// 零散包装
        /// </summary>
        public string PackingName_P = "";

        /// <summary>
        /// 整件数量
        /// </summary>
        public int Quantity_T = 0;

        /// <summary>
        /// 零散数量
        /// </summary>
        public int Quantity_P = 0;

        /// <summary>
        /// 批零系数
        /// </summary>
        public int ConvertFactor = 0;

        public Inventory() { }

        public Inventory(INV_Inventory m)
        {
            Product = m.Product;
            Quantity = m.Quantity;
            LotNumber = m.LotNumber;
            CostPrice = m.Price;

            PDT_Product p = new PDT_ProductBLL(Product).Model;
            if (p == null) return;

            ProductName = p.ShortName;

            PDT_Brand b = new PDT_BrandBLL(p.Brand).Model;
            if (b != null) BrandName = b.Name;

            if (p.Category > 0)
            {
                CategoryName = PDT_CategoryBLL.GetFullCategoryName(p.Category);
            }

            if (p.TrafficPackaging > 0) PackingName_T = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
            if (p.Packaging > 0) PackingName_P = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();

            if (PackingName_T == "") PackingName_T = p.TrafficPackaging.ToString();

            if (p.ConvertFactor == 0) p.ConvertFactor = 1;

            ConvertFactor = p.ConvertFactor;
            Quantity_T = Quantity / p.ConvertFactor;
            Quantity_P = Quantity % p.ConvertFactor;
        }
    }
}
