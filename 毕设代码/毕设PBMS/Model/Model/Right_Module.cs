using System;
using System.Collections.Generic;
using System.Text;

namespace MCSFramework.Model
{
    public class Right_Module
    {
        public Right_Module()
        { }
        #region Model
        private int _id = 0;
        private string _name = "";
        private string _remark = "";
        private int _superid = 0;
        private int? _sortvalue = 0;
        private string _enableflag = "Y";
        private string _ico = "";
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SuperID
        {
            set { _superid = value; }
            get { return _superid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SortValue
        {
            set { _sortvalue = value; }
            get { return _sortvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EnableFlag
        {
            set { _enableflag = value; }
            get { return _enableflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Ico
        {
            set { _ico = value; }
            get { return _ico; }
        }
        #endregion Model

    }
}
