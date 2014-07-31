using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.ComponentModel;
using System.Web.SessionState;
using System.Collections;
using System.Diagnostics;

/// <summary>
/// Summary description for APPFUN
/// </summary>
public class APPFUN
{
    string connStr = SQLHelper.ConnectionString; // ConfigurationManager.ConnectionStrings["dtcon"].ConnectionString;
    //_connection = new SqlConnection(connStr);


    private SqlConnection con;

    public APPFUN()
    {
        con = new SqlConnection(SQLHelper.ConnectionString);
    }

    public void FillGrid(string selectQuery, GridView GridView1)
    {
        SqlCommand com = new SqlCommand(selectQuery, con);
        SqlDataAdapter adapter = new SqlDataAdapter(com);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);

        GridView1.DataSource = null;
        GridView1.DataBind();

        GridView1.DataSource = dataTable;
        GridView1.DataBind();
    }

    public void FillCombobox(DropDownList ComboBoxName, string sSQL)
    {
        try
        {
            SqlCommand com = new SqlCommand(sSQL, con);

            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            ComboBoxName.DataSource = dataTable;
            ComboBoxName.DataBind();
        }
        catch (Exception ex)
        {
            ComboBoxName.DataSource = null;
            ComboBoxName.DataBind();
        }

    }

    public static DataTable FillDataTablebyQuery(string select_statement)
    {
        SqlConnection _con = new SqlConnection(SQLHelper.ConnectionString);
        SqlDataAdapter ad = new SqlDataAdapter(select_statement, _con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        _con.Close();
        return dt;
    }


    public static DataSet FillDatasetByQuery(string select_statement)
    {
        try
        {
            SqlConnection _con = new SqlConnection(SQLHelper.ConnectionString);
            SqlDataAdapter ad = new SqlDataAdapter(select_statement, _con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            //_con.Close();
            return ds;
        }
        catch (Exception)
        {
            return null;
        }

    }

    public string SplitFunction(string changeWhat, string delimiter, string replaceWord)
    {
        string s = changeWhat;
        int index = s.IndexOf(delimiter);
        if (index != -1)
        {
            changeWhat = s.Substring(index + 1);
        }
        changeWhat = changeWhat.Replace(replaceWord, "");
        return changeWhat;
    }


    public static bool CheckForNumeric(char ch)
    {
        //allow only numbers and a decimal point and backspace key
        int keyInt = (int)ch;
        if ((keyInt < 48 || keyInt > 57) && keyInt != 46 && keyInt != 8)
            return false;
        else
            return true;
    }

    public static double DateDiff(string howtocompare, System.DateTime startDate, System.DateTime endDate)
    {
        double diff = 0;
        System.TimeSpan TS = new System.TimeSpan(endDate.Ticks - startDate.Ticks);

        switch (howtocompare.ToLower())
        {
            case "year":
                diff = Convert.ToDouble(TS.TotalDays / 365);
                break;
            case "month":
                diff = Convert.ToDouble((TS.TotalDays / 365) * 12);
                break;
            case "day":
                diff = Convert.ToDouble(TS.TotalDays);
                break;
            case "hour":
                diff = Convert.ToDouble(TS.TotalHours);
                break;
            case "minute":
                diff = Convert.ToDouble(TS.TotalMinutes);
                break;
            case "second":
                diff = Convert.ToDouble(TS.TotalSeconds);
                break;
        }

        return diff;
    }

    public static string GenerateIDCODE(int pCodeLength, string pFieldName, string pTableName, string pSQLCondition = "")
    {
        try
        {
            SQLHelper dcon = new SQLHelper();

            DataTable dtPFTran = new DataTable();
            SqlCommand cmd = dcon.CreateCommand("SP_GENERATE_CODE", true);
            cmd.Parameters.AddWithValue("@LENGTH", pCodeLength);
            cmd.Parameters.AddWithValue("@FIELDNAME", pFieldName);
            cmd.Parameters.AddWithValue("@TABLENAME", pTableName);
            cmd.Parameters.AddWithValue("@SQLCONDITION", pSQLCondition);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtPFTran);

            return dtPFTran.Rows[0][0].ToString();

        }

        catch (Exception ex)
        {
            return null;
        }
    }

    public static DataTable CreateTable(DataView obDataView)
    {
        if (null == obDataView)
        {
            throw new ArgumentNullException
            ("DataView", "Invalid DataView object specified");
        }

        DataTable obNewDt = obDataView.Table.Clone();
        int idx = 0;
        string[] strColNames = new string[obNewDt.Columns.Count];
        foreach (DataColumn col in obNewDt.Columns)
        {
            strColNames[idx++] = col.ColumnName;
        }

        IEnumerator viewEnumerator = obDataView.GetEnumerator();
        while (viewEnumerator.MoveNext())
        {
            DataRowView drv = (DataRowView)viewEnumerator.Current;
            DataRow dr = obNewDt.NewRow();
            try
            {
                foreach (string strName in strColNames)
                {
                    dr[strName] = drv[strName];
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            obNewDt.Rows.Add(dr);
        }


        return obNewDt;
    }

    public static DataTable GET_DATATABLE(string pSQL = "")
    {
        SQLHelper dcon = new SQLHelper();

        DataTable dtPFTran = new DataTable();
        SqlCommand cmd = dcon.CreateCommand("UTL_PROCUDURE_EXEC", true);
        cmd.Parameters.AddWithValue("@vQuery", pSQL);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtPFTran);
        return dtPFTran;
    }

    public void ClearControls(Control ctrl)
    {
        try
        {
            if (ctrl.Controls.Count > 0)
            {
                for (int i = 0; i < ctrl.Controls.Count; i++)
                {

                    ClearControls(ctrl.Controls[i]);
                }
            }
            switch (ctrl.GetType().ToString())
            {
                case "System.Web.UI.WebControls.TextBox":
                    TextBox txt = (TextBox)ctrl;
                    txt.Text = null;
                    break;
                case "System.Web.UI.WebControls.DropDownList":
                    DropDownList ddl = (DropDownList)ctrl;
                    ddl.SelectedIndex = 0;
                    break;
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }


    }

    public void FillSelectAny(Control ctrl)
    {
        switch (ctrl.GetType().ToString())
        {
            case "System.Web.UI.WebControls.DropDownList":
                DropDownList ddl = (DropDownList)ctrl;
                ddl.Items.Insert(0, "-- Select Any --");
                ddl.Items[0].Value = "0";
                break;
        }
    }

    public static DataTable GET_YEAR_LIST()
    {
        //IT WILL REPLACE BY STROCE PROC
        DataTable dt = new DataTable();
        dt.Columns.Add("MyYear");
        dt.Columns.Add("MyYearVal");

        DataRow rw1 = dt.NewRow();
        dt.Rows.Add(2013, "2013");
        dt.Rows.Add(2012, "2012");
        dt.Rows.Add(2011, "2011");
        dt.Rows.Add(2010, "2010");
        return dt;
    }

    public string getRecordByDescription(string tableName, string columnName, string columnID, int TranID = 0)
    {
        string Description = "";

        SQLHelper _sqlh = new SQLHelper();
        SqlDataReader dReader = _sqlh.FetchQuery("SELECT " + columnName + " as Description FROM " + tableName + " WHERE " + columnID + "=" + TranID);

        while (dReader.Read())
        {
            Description = dReader["Description"].ToString();
        }

        return Description;
    }

    public static void sendMail(string sFrom, string sTo, string sBcc, string sCC, string sSubject, string sBody)
    {
        try
        {
            MailMessage message = new MailMessage();        //Here we will create object of MailMessage class.

            message.From = new MailAddress("belayet.hossain@orion-group.net");   //new MailAddress(sFrom);          //Initilize From in mail address.

            message.To.Add(new MailAddress(sTo));  //Initilize To in mail address.


            //if (!string.IsNullOrEmpty(sBcc))                //Check whether sBcc is not empty.
            //{
            //    message.Bcc.Add(new MailAddress(sBcc));     //Add sBcc in mail address.
            //}

            //if (!string.IsNullOrEmpty(sCC))                 //Check whether sCC is not empty.
            //{
            //    message.CC.Add(new MailAddress(sCC));       //Add CC in mail address.
            //}

            message.Subject = sSubject;                    //Add subject in mail message.

            message.Body = sBody;                         //Add body in mail message.

            message.IsBodyHtml = true;

            message.Priority = MailPriority.High;        //Set priority of mail message.

            SmtpClient client = new SmtpClient();        //Create an object of Smtp client.

            client.Send(message);                       //Send message by using send() method.
        }
        catch (Exception ex)
        {

        }
    }
}