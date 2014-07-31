using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Web.Services;

public partial class ReqDept : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

       // for (int i = 0; i < 10; i++ )
        if (!IsPostBack)
        {
            dt.Rows.Add();
            dt.AcceptChanges();

            grditem.DataSource = dt;
            grditem.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string vCondition = " AND isnull(ITEM_STATUS_ID,1) = 1";
        string vQuery = "SELECT TOP 10 I.ITEM_ID ItemId,I.ITEM_CODE code, I.ITEM_NAME ItemName,'' as Description,U.UNIT_DESC Unit,I.MAX_UNIT_ID,0 as Quantity FROM INV_SYS_ITEM_MST I,INV_SYS_UNIT U WHERE I.MAX_UNIT_ID=U.UNIT_ID AND I.COMPANY_ID =1 AND I.ITEM_CODE + ' '+ I.ITEM_NAME  Like '%" + txtSearch.Text.Trim() + "%'" + vCondition + " order by I.ITEM_NAME";

        APPFUN _APP = new APPFUN();

        _APP.FillGrid(vQuery, GridView1);
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {

    }

    [WebMethod]

    public static List<string> GetAutoCompleteData(string ItemName)
   {
       List<string> result = new List<string>();
       using (SqlConnection con = new SqlConnection("Data Source=IRONHIDE\\SQLEXPRESS;Initial Catalog=IMS_PM;Integrated Security=True"))
        {
            using (SqlCommand cmd = new SqlCommand("select  ITEM_NAME + ' :::' + convert(varchar,ITEM_ID) as ITEM_NAME from INV_SYS_ITEM_MST where ITEM_NAME LIKE '%'+@SearchText+'%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", ItemName);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["ITEM_NAME"].ToString());
                }
                
                return result;
            }
        }

    }
    //[WebMethod]

    //public void ADDNEWROW()
    //{
    //    DataTable dt = new DataTable();
    //    /// for (int i = 0; i < 10; i++ )
    //    dt.Rows.Add();
    //    dt.AcceptChanges();

    //    grditem.DataSource = dt;
    //    grditem.DataBind();


    //}

    public class MyItem
    {
        public string ItemName { get; set; }
        public int ItemId { get; set; }
    }

    protected void grditem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        /// for (int i = 0; i < 10; i++ )
        dt.Rows.Add();
        dt.AcceptChanges();

        grditem.DataSource = dt;
        grditem.DataBind();

    }
    protected void grditem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ADD"))
        {
            
        }
    }
}