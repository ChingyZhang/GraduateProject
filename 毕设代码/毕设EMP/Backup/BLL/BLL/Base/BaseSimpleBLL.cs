using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.Common;
using MCSFramework.IFStrategy;
using MCSFramework.SQLDAL;
using System.Web.Caching;

namespace MCSFramework.BLL
{
    public abstract class BaseSimpleBLL<T> : ISimpleBll<T>
    {
        protected T _m = default(T);
        private ISimpleDAL<T> _d;

        private int keytype = 1;    //主键类型 1:int 2:guid 3:string

        private int _id;
        private Guid _guid;
        private string _code;

        protected ISimpleDAL<T> _DAL
        {
            get { return _d; }
        }

        public BaseSimpleBLL(string fullclassname)
        {
            _d = (ISimpleDAL<T>)DataAccess.CreateObject(fullclassname);
        }

        public BaseSimpleBLL(string fullclassname, bool dalbycache)
        {
            _d = (ISimpleDAL<T>)DataAccess.CreateObject(fullclassname, dalbycache);
        }

        protected void FillModel(int id)
        {
            FillModel(id, false);
        }

        /// <summary>
        /// 根据bycache值决定是否从缓存中获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bycache"></param>
        protected void FillModel(int id, bool bycache)
        {
            keytype = 1;
            _id = id;

            if (_d == null)
                throw new Exception("数据库访问对象DAL的ClassName不能为空，请赋值！");

            if (bycache)
            {
                string bllname = this.ToString();
                if (bllname.LastIndexOf('.') >= 0)
                {
                    bllname = bllname.Substring(bllname.LastIndexOf('.') + 1);
                }
                string CacheKey = "Model-" + bllname + "-" + id.ToString();
                _m = (T)DataCache.GetCache(CacheKey);
                if (_m == null)
                {
                    try
                    {
                        _m = _d.GetModel(id);
                        if (_m != null)
                        {
                            int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                            DataCache.SetCache(CacheKey, _m, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                _m = _d.GetModel(id);
            }
        }


        protected void FillModel(Guid id)
        {
            FillModel(id, false);
        }

        /// <summary>
        /// 根据bycache值决定是否从缓存中获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bycache"></param>
        protected void FillModel(Guid id, bool bycache)
        {
            keytype = 2;
            _guid = id;

            if (_d == null)
                throw new Exception("数据库访问对象DAL的ClassName不能为空，请赋值！");

            if (bycache)
            {
                string bllname = this.ToString();
                if (bllname.LastIndexOf('.') >= 0)
                {
                    bllname = bllname.Substring(bllname.LastIndexOf('.') + 1);
                }
                string CacheKey = "Model-" + bllname + "-" + id.ToString();
                _m = (T)DataCache.GetCache(CacheKey);
                if (_m == null)
                {
                    try
                    {
                        _m = _d.GetModel(id);
                        if (_m != null)
                        {
                            int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                            DataCache.SetCache(CacheKey, _m, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                _m = _d.GetModel(id);
            }
        }

        protected void FillModel(string code)
        {
            FillModel(code, false);
        }
        /// <summary>
        /// 根据bycache值决定是否从缓存中获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bycache"></param>
        protected void FillModel(string code, bool bycache)
        {
            keytype = 3;
            _code = code;

            if (_d == null)
                throw new Exception("数据库访问对象DAL的ClassName不能为空，请赋值！");

            if (bycache)
            {
                string bllname = this.ToString();
                if (bllname.LastIndexOf('.') >= 0)
                {
                    bllname = bllname.Substring(bllname.LastIndexOf('.') + 1);
                }
                string CacheKey = "Model-" + bllname + "-" + code.ToString();
                _m = (T)DataCache.GetCache(CacheKey);
                if (_m == null)
                {
                    try
                    {
                        _m = _d.GetModel(code);
                        if (_m != null)
                        {
                            int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                            DataCache.SetCache(CacheKey, _m, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                _m = _d.GetModel(code);
            }
        }

        #region ISimpleBll<T> 成员

        public T Model
        {
            get
            { return _m; }
            set
            { _m = value; }
        }

        /// <summary>
        /// 将Model信息新增到数据库中
        /// </summary>
        /// <returns></returns>
        public virtual int Add()
        {
            return _d.Add(_m);
        }

        /// <summary>
        /// 将Model信息更新到数据库中
        /// </summary>
        /// <returns></returns>
        public virtual int Update()
        {
            return _d.Update(_m);
        }

        /// <summary>
        /// 删除指定ID的记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete()
        {
            if (keytype == 1)
                return _d.Delete(_id);
            else if (keytype == 2)
                return _d.Delete(_guid);
            else
                throw new System.Exception("删除时，关键字不能为字符串");
        }

        public virtual int Delete(int id)
        {
            return _d.Delete(id);
        }

        public virtual int Delete(Guid id)
        {
            return _d.Delete(id);
        }

        /// <summary>
        /// 判断指定ID的记录是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Exists(int id)
        {
            return _d.Exists(id);
        }

        public virtual bool Exists(Guid id)
        {
            return _d.Exists(id);
        }


        #endregion
        
        /// <summary>
        /// 根据自定义SQL条件获取Model列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual IList<T> _GetModelList(string condition)
        {
            return _d.GetModelList(condition);
        }
    }
}
