using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    [Serializable]
    public class Dictionary_Data
    {
        public Dictionary_Data()
        { }

        #region Model
        private Guid _id = Guid.NewGuid();
        private string _code = "";
        private string _name = "";
        private int _type = 0;
        private string _enabled = "Y";
        private string _description = "";
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private string _InsertUser = "";
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private string _UpdateUser = "";

        private string _typename = "";
        private string _tablename = "";

        /// <summary>
        /// 
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
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
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime InsertTime
        {
            set { _inserttime = value; }
            get { return _inserttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InsertUser
        {
            set { _InsertUser = value; }
            get { return _InsertUser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateUser
        {
            set { _UpdateUser = value; }
            get { return _UpdateUser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }

        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        #endregion Model

        public override string ToString()
        {
            return _name;
        }
    }

    public class Dictionary_Type
    {
        public Dictionary_Type()
        { }

        #region Model
        private int _id;
        private string _name;
        private string _tablename;
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
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        #endregion Model

        public override string ToString()
        {
            return _name;
        }
    }
}
