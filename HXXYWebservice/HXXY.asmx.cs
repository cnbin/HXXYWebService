using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace HXXYWebservice
{
    /// <summary>
    /// HXXY 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class HXXY : System.Web.Services.WebService
    {
        #region 账号登录
        [WebMethod(Description = "账号登录")]
        public int GetLogin(string i)
        {
            //默认返回0,表示验证失败
            int returnValue = 0;
            //连接SQL数据库
            System.Data.SqlClient.SqlConnection SqlCnn = new System.Data.SqlClient.SqlConnection("Data Source=192.168.20.5;Initial Catalog=RjtSchool;User ID=sa;Password=jsb@2015;");
            //打开数据库连接
            SqlCnn.Open();
            //加入SQL语句，实现数据库功能
            System.Data.SqlClient.SqlDataAdapter SqlDa = new System.Data.SqlClient.SqlDataAdapter("select * from dbo.hx_tblSchool", SqlCnn);
            //创建缓存
            DataSet DS = new DataSet();
            //将SQL语句放入缓存
            SqlDa.Fill(DS);
            //获取第一张表
            DataTable dt = DS.Tables[0];
            //获取第一行

            //比较数据值
            if (i == (string)dt.Rows[0][20])
            {
                //验证成功返回1
                returnValue = 1;
            }

            //释放资源
            SqlDa.Dispose();
            //关闭数据库
            SqlCnn.Close();
            return returnValue;

        }
        #endregion

        #region 项目首页
        [WebMethod(Description = "项目首页")]
        public String[] GetFirstPage()
        {
            String[] s = new String[11];
            //连接SQL数据库
            System.Data.SqlClient.SqlConnection SqlCnn = new System.Data.SqlClient.SqlConnection("Data Source=JOHN;Initial Catalog=webservice;User ID=sa;Password=12345678;");
            //打开数据库连接
            SqlCnn.Open();
            //加入SQL语句，实现数据库功能
            System.Data.SqlClient.SqlDataAdapter SqlDa = new System.Data.SqlClient.SqlDataAdapter("select * from dbo.shouye", SqlCnn);
            //创建缓存
            DataSet DS = new DataSet();
            //将SQL语句放入缓存
            SqlDa.Fill(DS);
            //获取第一张表
            DataTable dt = DS.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    s[i] = (String)dt.Rows[i][j];

                }
            }
            //释放资源
            SqlDa.Dispose();
            //关闭数据库
            SqlCnn.Close();
            return s;

        }
        #endregion

        #region 插入数据
        [WebMethod]
        public bool insertInfo(String text)
        {
            //连接SQL数据库
            System.Data.SqlClient.SqlConnection SqlCnn = new System.Data.SqlClient.SqlConnection("Data Source=JOHN;Initial Catalog=webservice;User ID=sa;Password=12345678;");
            //打开数据库连接
            SqlCnn.Open();
            string sqlstr = "insert into dbo.note (value) values('" + text + "')";
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = SqlCnn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sqlstr;
            //执行 
            cmd.ExecuteNonQuery();
            //关闭数据库
            SqlCnn.Close();
            return true;
        }
        #endregion

        #region 家庭作业
        [WebMethod(Description = "家庭作业")]
        [XmlInclude(typeof(HomeWorkClass))]
        public IList GetHomework()
        {
            //连接SQL数据库
            System.Data.SqlClient.SqlConnection SqlCnn = new System.Data.SqlClient.SqlConnection("Data Source=JOHN;Initial Catalog=webservice;User ID=sa;Password=12345678;");
            //打开数据库连接
            SqlCnn.Open();
            //加入SQL语句，实现数据库功能
            System.Data.SqlClient.SqlDataAdapter SqlDa = new System.Data.SqlClient.SqlDataAdapter("select * from dbo.homework", SqlCnn);
            //创建缓存
            DataSet DS = new DataSet();
            //将SQL语句放入缓存
            SqlDa.Fill(DS);
            //获取第一张表
            DataTable dt = DS.Tables[0];
            IList result = new ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int title = (int)dt.Rows[i][0];
                string content =(string)dt.Rows[i][1] ;
                DateTime time = (DateTime)dt.Rows[i][2];
               
                result.Add(new HomeWorkClass(title, content, time.ToString("yyyy/MM/dd hh:mm:ss")));
            }
            return result;
        }
      
        public class HomeWorkClass
        {
            private int title;
            private string content;
            private string time;

            public HomeWorkClass()
            {

            }
            public HomeWorkClass(int title, string content, string time)
            {
                this.title = title;
                this.content = content;
                this.time = time;
            }

            public int Title
            {
                get { return title; }
                set { title = value; }
            }
            public string Content
            {
                get { return content; }
                set { content = value; }
            }
            public string Time
            {
                get { return time; }
                set { time = value; }
            }
        }
        #endregion

        #region 平安校园
        [WebMethod(Description = "平安校园列表")]
        [XmlInclude(typeof(ReadCardRecordClass))]
        public IList GetCardRecordList()
        {
            //连接SQL数据库
            SqlConnection SqlCnn = new SqlConnection("Data Source=192.168.20.5;Initial Catalog=RjtSchool;User ID=sa;Password=jsb@2015;");
            //打开数据库连接
            SqlCnn.Open();

            //加入SQL语句，实现数据库功能
            SqlDataAdapter SqlCardRecord = new SqlDataAdapter("select * from dbo.hx_tblCardRecord", SqlCnn);
            //创建缓存
            DataSet DSCardRecord = new DataSet();
            //将SQL语句放入缓存
            SqlCardRecord.Fill(DSCardRecord);
            //获取第一张表
            DataTable dt = DSCardRecord.Tables[0];

            SqlDataAdapter SqlGrade = new SqlDataAdapter("select * from dbo.hx_tblGrade", SqlCnn);
            DataSet DSGrade = new DataSet();
            //将SQL语句放入缓存
            SqlGrade.Fill(DSGrade);
            //获取第一张表
            DataTable dtGrade = DSGrade.Tables[0];

            SqlDataAdapter SqlClass = new SqlDataAdapter("select * from dbo.hx_tblClasses", SqlCnn);
            DataSet DSClass = new DataSet();
            //将SQL语句放入缓存
            SqlClass.Fill(DSClass);
            //获取第一张表
            DataTable dtClass = DSClass.Tables[0];

            IList result = new ArrayList();
           
            for (int i = 0; i < dt.Rows.Count; i++)

            {
                int ID = (int)dt.Rows[i][0];
                DateTime CreateDate = (DateTime)dt.Rows[i][1];
                bool IsAvailable = (bool)dt.Rows[i][2];
                string StudentName = (string)dt.Rows[i][4];
                string  Grade = (string)dt.Rows[i][6];
                int intGrade = Convert.ToInt32(Grade);
                string Classes = (string)dt.Rows[i][7];
                int intClasses = Convert.ToInt32(Classes);
                string GradeName = null;
                string ClassesName = null;
                string InOut = (string)dt.Rows[i][11];

                for (int j = 0; j < dtGrade.Rows.Count; j++)
                {
                    if (intGrade == (int)dtGrade.Rows[j][0])
                    {
                        GradeName = (string)dtGrade.Rows[j][6];
                    }
                }

                for (int j = 0; j < dtClass.Rows.Count; j++)
                {
                    if (intClasses == (int)dtClass.Rows[j][0])
                    {
                        ClassesName = (string)dtClass.Rows[j][7];
                    }
                }
        
                if (IsAvailable)
                {
                    result.Add(new ReadCardRecordClass(CreateDate, StudentName,
                           GradeName, ClassesName, InOut));
                }
            }
            return result;
        }
        public class ReadCardRecordClass
        {
            public DateTime CreateDate;
            public string StudentName;
            public string GradeName;
            public string ClassesName;
            public string InOut;

            public ReadCardRecordClass()
            {

            }
            public ReadCardRecordClass(DateTime CreateDate, string StudentName, string GradeName,
                string ClassesName, string InOut)
            {
                this.CreateDate = CreateDate;
                this.StudentName = StudentName;
                this.GradeName = GradeName;
                this.ClassesName = ClassesName;
                this.InOut = InOut;
             
            }
        }

        #endregion
    }
    
}
