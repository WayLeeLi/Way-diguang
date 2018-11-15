﻿using System;
namespace DTcms.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    public partial class users
    {
        public users()
        { }
        #region Model
        private int _id;
        private int _group_id = 0;
        private string _user_name;
        private string _password;
        private string _email = "";
        private string _nick_name = "";
        private string _avatar = "";
        private string _sex = "保密";
        private DateTime? _birthday;
        private string _telphone = "";
        private string _mobile = "";
        private string _qq = "";
        private string _address = "";
        private string _safe_question = "";
        private string _safe_answer = "";
        private decimal _amount = 0M;
        private int _point = 0;
        private int _exp = 0;
        private int _isDonePoints = 0;
        private int _isHirePoints = 0;
        private int _is_lock = 0;
        private DateTime _reg_time = DateTime.Now;
        private string _reg_ip;
        private int _isVip;
        private int _isad = 0;
        private int _ismac = 0;
        private DateTime _endtime;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户组别
        /// </summary>
        public int group_id
        {
            set { _group_id = value; }
            get { return _group_id; }
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
        /// 用户密码
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nick_name
        {
            set { _nick_name = value; }
            get { return _nick_name; }
        }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string avatar
        {
            set { _avatar = value; }
            get { return _avatar; }
        }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string telphone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// QQ号码
        /// </summary>
        public string qq
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 安全问题
        /// </summary>
        public string safe_question
        {
            set { _safe_question = value; }
            get { return _safe_question; }
        }
        /// <summary>
        /// 问题答案
        /// </summary>
        public string safe_answer
        {
            set { _safe_answer = value; }
            get { return _safe_answer; }
        }
        /// <summary>
        /// 预存款
        /// </summary>
        public decimal amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 用户积分
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 经验值
        /// </summary>
        public int exp
        {
            set { _exp = value; }
            get { return _exp; }
        }
        /// <summary>
        /// 经验值
        /// </summary>
        public int isDonePoints
        {
            set { _isDonePoints = value; }
            get { return _isDonePoints; }
        }
        /// <summary>
        /// 经验值
        /// </summary>
        public int isHirePoints
        {
            set { _isHirePoints = value; }
            get { return _isHirePoints; }
        }
        /// <summary>
        /// 用户状态,0正常,1待验证,2待审核,3锁定
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// 是否是Vip 1是,0不是
        /// </summary>
        public int isVip
        {
            set { _isVip = value; }
            get { return _isVip; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime reg_time
        {
            set { _reg_time = value; }
            get { return _reg_time; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime endtime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 注册IP
        /// </summary>
        public string reg_ip
        {
            set { _reg_ip = value; }
            get { return _reg_ip; }
        }
        /// <summary>
        /// 介紹租賃廣告
        /// </summary>
        public int isAd
        {
            set { _isad = value; }
            get { return _isad; }
        }
        /// <summary>
        /// 介紹精品廠商
        /// </summary>
        public int isMac
        {
            set { _ismac = value; }
            get { return _ismac; }
        }
        public string dianming { get; set; }
        public string dianmiaoshu { get; set; }
        public string congye { get; set; }
        public string gongsi { get; set; }
        public string fuwuquyu { get; set; }
        public string shuxishequ { get; set; }
        public string fuwutechang { get; set; }
        public string jingli { get; set; }
        public string zhengshu { get; set; }
        public string note { get; set; }
        #endregion Model
    }
}