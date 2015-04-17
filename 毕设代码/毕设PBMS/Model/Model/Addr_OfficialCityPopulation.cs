// ===================================================================
// 文件： Addr_OfficialCityPopulation.cs
// 项目名称：
// 创建时间：2010/12/17
// 作者:	   MeiChis
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    /// <summary>
    ///Addr_OfficialCityPopulation数据实体类
    /// </summary>
    [Serializable]
    public class Addr_OfficialCityPopulation : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _totalpopulation = 0;
        private int _manpopulation = 0;
        private int _femalepopulation = 0;
        private int _f1 = 0;
        private int _f2 = 0;
        private int _f3 = 0;
        private int _f4 = 0;
        private int _f5 = 0;
        private int _f6 = 0;
        private int _f7 = 0;
        private int _f8 = 0;
        private int _f9 = 0;
        private int _f10 = 0;
        private int _f11 = 0;
        private int _f12 = 0;
        private int _f13 = 0;
        private int _f14 = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Addr_OfficialCityPopulation()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Addr_OfficialCityPopulation(int id, int totalpopulation, int manpopulation, int femalepopulation, int f1, int f2, int f3, int f4, int f5, int f6, int f7, int f8, int f9, int f10, int f11, int f12, int f13, int f14)
        {
            _id = id;
            _totalpopulation = totalpopulation;
            _manpopulation = manpopulation;
            _femalepopulation = femalepopulation;
            _f1 = f1;
            _f2 = f2;
            _f3 = f3;
            _f4 = f4;
            _f5 = f5;
            _f6 = f6;
            _f7 = f7;
            _f8 = f8;
            _f9 = f9;
            _f10 = f10;
            _f11 = f11;
            _f12 = f12;
            _f13 = f13;
            _f14 = f14;

        }
        #endregion

        #region 公共属性
        ///<summary>
        ///ID
        ///</summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        ///<summary>
        ///TotalPopulation
        ///</summary>
        public int TotalPopulation
        {
            get { return _totalpopulation; }
            set { _totalpopulation = value; }
        }

        ///<summary>
        ///ManPopulation
        ///</summary>
        public int ManPopulation
        {
            get { return _manpopulation; }
            set { _manpopulation = value; }
        }

        ///<summary>
        ///FemalePopulation
        ///</summary>
        public int FemalePopulation
        {
            get { return _femalepopulation; }
            set { _femalepopulation = value; }
        }

        ///<summary>
        ///家庭户户数
        ///</summary>
        public int F1
        {
            get { return _f1; }
            set { _f1 = value; }
        }

        ///<summary>
        ///家庭户总人口
        ///</summary>
        public int F2
        {
            get { return _f2; }
            set { _f2 = value; }
        }

        ///<summary>
        ///家庭男人口
        ///</summary>
        public int F3
        {
            get { return _f3; }
            set { _f3 = value; }
        }

        ///<summary>
        ///家庭女人口
        ///</summary>
        public int F4
        {
            get { return _f4; }
            set { _f4 = value; }
        }

        ///<summary>
        ///0-14岁小计
        ///</summary>
        public int F5
        {
            get { return _f5; }
            set { _f5 = value; }
        }

        ///<summary>
        ///0-14男小计
        ///</summary>
        public int F6
        {
            get { return _f6; }
            set { _f6 = value; }
        }

        ///<summary>
        ///0-14女小计
        ///</summary>
        public int F7
        {
            get { return _f7; }
            set { _f7 = value; }
        }

        ///<summary>
        ///15-64岁小计
        ///</summary>
        public int F8
        {
            get { return _f8; }
            set { _f8 = value; }
        }

        ///<summary>
        ///15-64岁男小计
        ///</summary>
        public int F9
        {
            get { return _f9; }
            set { _f9 = value; }
        }

        ///<summary>
        ///15-64女小计
        ///</summary>
        public int F10
        {
            get { return _f10; }
            set { _f10 = value; }
        }

        ///<summary>
        ///65岁及以上
        ///</summary>
        public int F11
        {
            get { return _f11; }
            set { _f11 = value; }
        }

        ///<summary>
        ///65岁男小计
        ///</summary>
        public int F12
        {
            get { return _f12; }
            set { _f12 = value; }
        }

        ///<summary>
        ///65岁女小计
        ///</summary>
        public int F13
        {
            get { return _f13; }
            set { _f13 = value; }
        }

        ///<summary>
        ///户口在本地 
        ///</summary>
        public int F14
        {
            get { return _f14; }
            set { _f14 = value; }
        }


        /// <summary>
        /// 扩展属性集合
        /// </summary>
        public NameValueCollection ExtPropertys
        {
            get { return _extpropertys; }
            set { _extpropertys = value; }
        }
        #endregion

        public string ModelName
        {
            get { return "Addr_OfficialCityPopulation"; }
        }
        #region 索引器访问
        public string this[string FieldName]
        {
            get
            {
                switch (FieldName)
                {
                    case "ID":
                        return _id.ToString();
                    case "TotalPopulation":
                        return _totalpopulation.ToString();
                    case "ManPopulation":
                        return _manpopulation.ToString();
                    case "FemalePopulation":
                        return _femalepopulation.ToString();
                    case "F1":
                        return _f1.ToString();
                    case "F2":
                        return _f2.ToString();
                    case "F3":
                        return _f3.ToString();
                    case "F4":
                        return _f4.ToString();
                    case "F5":
                        return _f5.ToString();
                    case "F6":
                        return _f6.ToString();
                    case "F7":
                        return _f7.ToString();
                    case "F8":
                        return _f8.ToString();
                    case "F9":
                        return _f9.ToString();
                    case "F10":
                        return _f10.ToString();
                    case "F11":
                        return _f11.ToString();
                    case "F12":
                        return _f12.ToString();
                    case "F13":
                        return _f13.ToString();
                    case "F14":
                        return _f14.ToString();
                    default:
                        if (_extpropertys == null)
                            return "";
                        else
                            return _extpropertys[FieldName];
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        int.TryParse(value, out _id);
                        break;
                    case "TotalPopulation":
                        int.TryParse(value, out _totalpopulation);
                        break;
                    case "ManPopulation":
                        int.TryParse(value, out _manpopulation);
                        break;
                    case "FemalePopulation":
                        int.TryParse(value, out _femalepopulation);
                        break;
                    case "F1":
                        int.TryParse(value, out _f1);
                        break;
                    case "F2":
                        int.TryParse(value, out _f2);
                        break;
                    case "F3":
                        int.TryParse(value, out _f3);
                        break;
                    case "F4":
                        int.TryParse(value, out _f4);
                        break;
                    case "F5":
                        int.TryParse(value, out _f5);
                        break;
                    case "F6":
                        int.TryParse(value, out _f6);
                        break;
                    case "F7":
                        int.TryParse(value, out _f7);
                        break;
                    case "F8":
                        int.TryParse(value, out _f8);
                        break;
                    case "F9":
                        int.TryParse(value, out _f9);
                        break;
                    case "F10":
                        int.TryParse(value, out _f10);
                        break;
                    case "F11":
                        int.TryParse(value, out _f11);
                        break;
                    case "F12":
                        int.TryParse(value, out _f12);
                        break;
                    case "F13":
                        int.TryParse(value, out _f13);
                        break;
                    case "F14":
                        int.TryParse(value, out _f14);
                        break;
                    default:
                        if (_extpropertys == null)
                            _extpropertys = new NameValueCollection();
                        if (_extpropertys[FieldName] == null)
                            _extpropertys.Add(FieldName, value);
                        else
                            _extpropertys[FieldName] = value;
                        break;
                }
            }
        }
        #endregion
    }
}
