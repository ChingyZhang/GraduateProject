using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    [Serializable]
    public class Right_Assign:IModel
    {
        public Right_Assign()
        { }

        #region Model
        private int _id = 0;
        private int _module = 0;
        private int _action = 0;
        private int _based_on = 0;
        private int _position = 0;
        private string _rolename = "";
        private string _username = "";

        private string _modulename;
        private string _actionname;
        private string _actioncode;
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
        public int Module
        {
            set { _module = value; }
            get { return _module; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Action
        {
            set { _action = value; }
            get { return _action; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Based_On
        {
            set { _based_on = value; }
            get { return _based_on; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Position
        {
            set { _position = value; }
            get { return _position; }
        }

        public string RoleName
        {
            get { return _rolename; }
            set { _rolename = value; }
        }

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        //public string ModuleName
        //{
        //    get { return _modulename; }
        //    set { _modulename = value; }
        //}
        //public string ActionCode
        //{
        //    get { return _actioncode; }
        //    set { _actioncode = value; }
        //}
        //public string ActionName
        //{
        //    get { return _actionname; }
        //    set { _actionname = value; }
        //}
        #endregion Model


        #region IModel 成员

        public string ModelName
        {
            get { return "Right_Assign"; }
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
                    case "Module":
                        return _module.ToString();
                    case "Action":
                        return _action.ToString();
                    case "Based_On":
                        return _based_on.ToString();
                    case "Position":
                        return _position.ToString();
                    case "RoleName":
                        return _rolename;
                    case "UserName":
                        return _username;
                    default:
                        return "";
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        int.TryParse(value, out _id);
                        break;
                    case "Module":
                        int.TryParse(value, out _module);
                        break;
                    case "Action":
                        int.TryParse(value, out _action);
                        break;
                    case "Based_On":
                        int.TryParse(value, out _based_on);
                        break;
                    case "Position":
                        int.TryParse(value, out _position);
                        break;
                    case "RoleName":
                        _rolename = value;
                        break;
                    case "UserName":
                        _username = value;
                        break;

                }
            }
        }
        #endregion

        #endregion
    }
}
