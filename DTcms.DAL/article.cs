using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTcms.Common;
using DTcms.DBUtility;

namespace DTcms.DAL
{
    /// <summary>
    /// ����ģ��
    /// </summary>
    public partial class article
    {
        public article()
        { }
        #region  Method
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_article");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from dt_article");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>


        /// <summary>
        /// ɾ��һ�����ݣ����ӱ������������
        /// </summary>
        public bool Delete(int id)
        {
            //ȡ�����MODEL
            List<Model.article_albums> albumsList = new article_albums().GetList(id);
            //ȡ�ø���MODEL
            List<Model.download_attach> attachList = new download_attach().GetList(id);

            List<CommandInfo> sqllist = new List<CommandInfo>();
            //ɾ������ģ������
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_article_news ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //ɾ������ģ������
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from dt_article_download ");
            strSql2.Append(" where id=@id ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters2[0].Value = id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //ɾ����Ʒģ������
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from dt_article_goods ");
            strSql3.Append(" where id=@id ");
            SqlParameter[] parameters3 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters3[0].Value = id;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //ɾ������ģ������
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from dt_article_content ");
            strSql4.Append(" where id=@id ");
            SqlParameter[] parameters4 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters4[0].Value = id;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd);

            //ɾ�����Ͳ�
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete from dt_article_diggs ");
            strSql5.Append(" where id=@id ");
            SqlParameter[] parameters5 = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters5[0].Value = id;
            cmd = new CommandInfo(strSql5.ToString(), parameters5);
            sqllist.Add(cmd);

            //ɾ����Ʒ�۸�
            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("delete from dt_goods_group_price ");
            strSql6.Append(" where article_id=@article_id ");
            SqlParameter[] parameters6 = {
                    new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters6[0].Value = id;
            cmd = new CommandInfo(strSql6.ToString(), parameters6);
            sqllist.Add(cmd);

            //ɾ�����صĸ���
            StringBuilder strSql7 = new StringBuilder();
            strSql7.Append("delete from dt_download_attach ");
            strSql7.Append(" where article_id=@article_id ");
            SqlParameter[] parameters7 = {
                    new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters7[0].Value = id;
            cmd = new CommandInfo(strSql7.ToString(), parameters7);
            sqllist.Add(cmd);

            //ɾ��ͼƬ���
            StringBuilder strSql8 = new StringBuilder();
            strSql8.Append("delete from dt_article_albums ");
            strSql8.Append(" where article_id=@article_id ");
            SqlParameter[] parameters8 = {
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters8[0].Value = id;
            cmd = new CommandInfo(strSql8.ToString(), parameters8);
            sqllist.Add(cmd);

            //ɾ����չ����
            StringBuilder strSql9 = new StringBuilder();
            strSql9.Append("delete from dt_attribute_value ");
            strSql9.Append(" where article_id=@article_id ");
            SqlParameter[] parameters9 = {
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters9[0].Value = id;
            cmd = new CommandInfo(strSql9.ToString(), parameters9);
            sqllist.Add(cmd);

            //ɾ������
            StringBuilder strSql10 = new StringBuilder();
            strSql10.Append("delete from dt_article_comment ");
            strSql10.Append(" where article_id=@article_id ");
            SqlParameter[] parameters10 = {
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters10[0].Value = id;
            cmd = new CommandInfo(strSql10.ToString(), parameters10);
            sqllist.Add(cmd);

            //ɾ��������Ϣ
            StringBuilder strSql11 = new StringBuilder();
            strSql11.Append("delete from dt_article ");
            strSql11.Append(" where id=@id ");
            SqlParameter[] parameters11 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters11[0].Value = id;
            cmd = new CommandInfo(strSql11.ToString(), parameters11);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                new article_albums().DeleteFile(albumsList); //ɾ��ͼƬ
                new download_attach().DeleteFile(attachList); //ɾ������
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion  Method

        #region ǰ̨����
        /// <summary>
        ///  ���� and
        /// </summary>
        /// <param name="where"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public DataTable GetTableByWhere(string where, int? Top = null)
        {
            string strtop = "";
            if (Top > 0)
            {
                strtop = " top " + Top;
            }
            string sql = string.Format("select {0} * from dt_article where 1=1 {1} and Status=1 order by sort_id asc,add_time desc", strtop, where);
            var table = DbHelperSQL.Query(sql).Tables[0];
            return table;
        }

        /// <summary>
        /// ��ȡ��Ҫ��ʾ��ҳ����Ʒ,ͨ��Postid
        /// </summary>
        /// <param name="Postid"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public DataTable GetLIstByPostid(string Postid, int Top)
        {
            string sql = string.Format("select top {0} * from dt_article where Postid={1} and Status=1 order by sort_id asc,add_time desc", Top, Postid);
            var table = DbHelperSQL.Query(sql).Tables[0];
            return table;
        }

        /// <summary>
        ///ǰ̨��ҳ�õ� ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetPageindexList(string WherePostid, int pageSize, string strWhere = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM view_article_goods where Status=1 and Type=1"); ;
            if (!string.IsNullOrEmpty(WherePostid))
            {
                strSql.AppendFormat(" and Postid={0} ", WherePostid);

            }
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" " + strWhere);
            }
            //recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            int recordCount = 0;
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, 1, strSql.ToString(), "  sort_id asc"));
        }
        /// <summary>
        ///ǰ̨��ҳ�õ� ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetPageJPList(string WherePostid, int pageSize, string strWhere = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM view_article_goods where Status=1 "); ;
            if (!string.IsNullOrEmpty(WherePostid))
            {
                strSql.AppendFormat(" and Postid={0} ", WherePostid);

            }
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" " + strWhere);
            }
            //recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            int recordCount = 0;
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, 1, strSql.ToString(), " add_time desc"));
        }
        /// <summary>
        ///ǰ̨��ҳ�õ� ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetPageHelpList(string WherePostid, int pageSize, string strWhere = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM view_article_goods where 1=1"); ;
            if (!string.IsNullOrEmpty(WherePostid))
            {
                strSql.AppendFormat(" and Postid={0} ", WherePostid);

            }
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" " + strWhere);
            }
            //recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            int recordCount = 0;
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, 1, strSql.ToString(), " sort_id asc"));
        }
        #endregion


    }
}