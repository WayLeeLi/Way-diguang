using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// �û���Ϣ
    /// </summary>
    public partial class users
    {
        private readonly DAL.users dal = new DAL.users();
        public users()
        { }
        #region  Method
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// ����û����Ƿ����
        /// </summary>
        public bool Exists(string user_name, int group_id, int bz)
        {
            return dal.Exists(user_name, group_id, bz);
        }

        /// <summary>
        /// ����û����Ƿ����
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }
        /// <summary>
        /// ���Email�Ƿ���ڲ������Ñ�ID
        /// </summary>
        public int GetIDByExistsEmail(string email)
        {
            return dal.GetIDByExistsEmail(email);
        }
        /// <summary>
        /// ���ͬһIPע����(Сʱ)���Ƿ����
        /// </summary>
        public bool Exists(string reg_ip, int regctrl)
        {
            return dal.Exists(reg_ip, regctrl);
        }

        /// <summary>
        /// ���Email�Ƿ����
        /// </summary>
        public bool ExistsEmail(string email)
        {
            return dal.ExistsEmail(email);
        }

        /// <summary>
        /// ����һ������û���
        /// </summary>
        public string GetRandomName(int length)
        {
            string temp = Utils.Number(length, true);
            if (Exists(temp, 1))
            {
                return GetRandomName(length);
            }
            return temp;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(DTcms.Model.users model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(DTcms.Model.users model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }
        /// <summary>
        /// ���¸���״̬
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int UpFee(int UserID, int value)
        {
            return dal.UpFee(UserID, value);
        }
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public DTcms.Model.users GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// �����û������뷵��һ��ʵ��
        /// </summary>
        public Model.users GetModel(string user_name, string password, int emaillogin, string where = null)
        {
            return dal.GetModel(user_name, password, emaillogin, where);
        }

        /// <summary>
        /// �����û�������һ��ʵ��
        /// </summary>
        public Model.users GetModel(string user_name)
        {
            return dal.GetModel(user_name);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #region ���
        public DataTable list_pagesWhere(int page, int numPerPage, string whereStr, string orderStr)
        {
            return dal.list_pagesWhere(page, numPerPage, whereStr, orderStr);
        }
        #endregion

        #region �@ȡ����
        public int GetTatalNum(string strWhere)
        {
            return dal.GetTatalNum(strWhere);
        }
        #endregion

        #region ��չ����===================================
        /// <summary>
        /// �û�����
        /// </summary>
        public bool Upgrade(int id)
        {
            if (!Exists(id))
            {
                return false;
            }
            Model.users model = GetModel(id);
            Model.user_groups groupModel = new user_groups().GetUpgrade(model.group_id, model.exp);
            if (groupModel == null)
            {
                return false;
            }
            int result = UpdateField(id, "group_id=" + groupModel.id);
            if (result > 0)
            {
                //���ӻ���
                if (groupModel.point > 0)
                {
                    new BLL.point_log().Add(model.id, model.user_name, groupModel.point, "�����@�÷e��");
                }
                //���ӽ��
                if (groupModel.amount > 0)
                {
                    new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.SysGive.ToString(), groupModel.amount, "����ٛ�ͽ��~", 1);
                }
            }
            return true;
        }
        #endregion

        #region �����c��
        public int UpPoint(int Uid, int point)
        {
            return dal.UpPoint(Uid, point);
        }
        #endregion

        #region �����û��������c��
        public int UpPoint(string UName, int point)
        {
            return dal.UpPoint(UName, point);
        }
        #endregion

        #region �����c��
        public int UpJianPoint(int Uid, int point)
        {
            return dal.UpJianPoint(Uid, point);
        }
        #endregion

        #region ���ݶ����ż����c��
        public int UpJianPoint(string order_no, int point)
        {
            return dal.UpJianPoint(order_no, point);
        }
        #endregion

        #region �����Ñ��������Ñ���Ϣ
        public DataTable GetUser_Info(string UserName)
        {
            return dal.GetUser_Info(UserName);
        }
        #endregion

        #region �ж��Ƿ���VIP
        public bool isVip(int ID)
        {
            return dal.isVip(ID);
        }
        #endregion

        #region �涨ʱ��δ�������Ϊ��ͨ��Ա
        public int UpUserSetCommon(int UID)
        {
            return dal.UpUserSetCommon(UID);
        }
        #endregion

        #endregion  Method
    }
}