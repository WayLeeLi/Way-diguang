﻿using System;
namespace DTcms.Model
{
    /// <summary>
    /// 积分记录日志
    /// </summary>
    [Serializable]
    public partial class point_log
    {
        public point_log()
        { }
        #region Model
        private int _id;
        private int _user_id;
        private string _user_name;
        private int _value = 0;
        private string _remark;
        private DateTime _add_time = DateTime.Now;
        private int _type;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 增减积分
        /// </summary>
        public int value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 时间
        /// </summary>
        public int type
        {
            set { _type = value; }
            get { return _type; }
        }

        #endregion Model

    }
}