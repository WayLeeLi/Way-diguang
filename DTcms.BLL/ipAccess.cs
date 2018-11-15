using System;
using System.Data;
using System.Collections.Generic;

namespace DTcms.BLL
{
    /// <summary>
    /// ֧����ʽ
    /// </summary>
    public partial class ipAccess
    {
        private readonly DAL.ipAccess dal = new DAL.ipAccess();
        public ipAccess()
        { }
        #region  Method
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string IP_Address)
        {
            return dal.Exists(IP_Address);
        }


        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.ipAccess model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ���ȫ������
        /// </summary>
        public int GetAllCount()
        {
            return dal.GetAllCount();
        }

        /// <summary>
        /// ��õ�������
        /// </summary>
        public int GetTodayCount()
        {
            return dal.GetTodayCount();
        }

        #endregion  Method
    }
}