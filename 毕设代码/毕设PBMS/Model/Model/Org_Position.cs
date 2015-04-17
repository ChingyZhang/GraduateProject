using System;
using System.Collections.Generic;
using System.Text;

namespace MCSFramework.Model
{
    public class Org_Position
    {
        public Org_Position()
        { }
        #region Model
        private int _id;
        private string _name;
        private int _superid;
        private string _isheadoffice;
        private string _remark;
        private int? _department;
        private string _enabled;
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
        public int SuperID
        {
            set { _superid = value; }
            get { return _superid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsHeadOffice
        {
            set { _isheadoffice = value; }
            get { return _isheadoffice; }
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
        public int? Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        #endregion Model

    }
}
