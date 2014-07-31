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
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.IO;


public partial class Grid_Search : System.Web.UI.Page
{
    public static int index;
    protected void Page_Load(object sender, EventArgs e)
    {
        date.Text = DateTime.Now.ToString("dd-MM-yyyy");
        if (!IsPostBack)
        {

            if (ViewState["GetRecords"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemName");
                dt.Columns.Add("ITEM_ID");
                dt.Columns.Add("UNIT_DESC");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Note");
                dt.Columns.Add("Spec");
                dt.Columns.Add("UNIT_ID");

                
                dt.Rows.Add();
                dt.AcceptChanges();
                ViewState["GetRecords"] = dt;
            }

        }
    }

   
    [WebMethod(EnableSession = true)]

    public static List<string> GetAutoCompleteData(string ItemName)
    {
        String lang = Convert.ToString(HttpContext.Current.Session["cat"]);  
        List<string> result = new List<string>();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dtcon"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("select  ITEM_NAME + ' :::' + convert(varchar,ITEM_ID) as ITEM_NAME from INV_SYS_ITEM_MST where CAT_ID = @CAT_ID and ITEM_NAME LIKE '%'+@SearchText+'%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", ItemName);
                cmd.Parameters.AddWithValue("@CAT_ID", lang);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["ITEM_NAME"].ToString());
                }

                return result;
            }
        }

    }

    public DataTable ADD_Row(string ItemName, string ITEM_ID, string unit_desc, string quantity, string note, string spec,string UnitID, int index)
    {
        DataTable dt = new DataTable();

        if (ViewState["GetRecords"] != null)
        {
            dt = (DataTable)ViewState["GetRecords"];

            //for (int i = 0; i < dt.Rows.Count-1;i++ )
            //{
            //    if ((dt.Rows[i][1].ToString() != ITEM_ID))
            //    {
                    dt.Rows[index - 1][0] = ItemName;
                    dt.Rows[index - 1][5] = spec;
                    dt.Rows[index - 1][1] = ITEM_ID;
                    dt.Rows[index - 1][2] = unit_desc;
                    dt.Rows[index - 1][3] = quantity;
                    dt.Rows[index - 1][4] = note;
                    dt.Rows[index - 1][6] = UnitID;
            //    }
            //}
            
            
           
            dt.Rows.Add();
            dt.AcceptChanges();
            ViewState["GetRecords"] = dt;
        }

        return dt;
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        String item = txtSearch.Text;
        String quantity = txtquantity.Text;
        String note = txtbudget.Text;

        if (!string.IsNullOrEmpty(item));
        DataTable dtItem = this.LoadItemInformation(item);

        
        String ITEM_ID = dtItem.Rows[0][0].ToString();
        String Item_name = dtItem.Rows[0][1].ToString();
        String UnitID = dtItem.Rows[0][2].ToString();
        String unit_desc = dtItem.Rows[0][3].ToString();
        String Spec = dtItem.Rows[0][4].ToString();
        

        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["GetRecords"];

        int rowcount = dt.Rows.Count;

        dt = ADD_Row(Item_name, ITEM_ID, unit_desc, quantity, note, Spec,UnitID, rowcount);
        txtSearch.Text = "";
        index = dt.Rows.Count;
        grdDetails.DataSource = dt;
        grdDetails.DataBind();
        grdDetails.Rows[index - 1].Visible = false; //Hides the final row
    }

    protected void NewReq_Click(object sender, EventArgs e)
    {
        Response.Redirect("Grid_Search.aspx");
    }

    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        String ss = ddlcategory.SelectedValue;
        Session["cat"] = ss;
        ddlcategory.Enabled = false;
    }

    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)    {

        if (e.CommandName.Equals("Delete"))
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex; 
   
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["GetRecords"];
            dt.Rows.RemoveAt(RowIndex);
            ViewState["GetRecords"] = dt;
            grdDetails.DataSource = dt;
            grdDetails.DataBind();
            index = dt.Rows.Count;
            grdDetails.Rows[index - 1].Visible = false;
  
        }
    }



    private DataTable LoadItemInformation(string itemName)
    {
        APPFUN _app = new APPFUN();
        string[] lines = itemName.ToString().Split(new string[] { ":::" }, StringSplitOptions.None);
        string vSQL = @"SELECT TOP 1000 I.ITEM_ID ,I.ITEM_NAME,I.MAX_UNIT_ID AS UNIT_ID,UNIT_DESC,I.ITEM_SPEC FROM [IMS_PM].[dbo].[INV_SYS_ITEM_MST] I
                        INNER JOIN INV_SYS_UNIT AS U ON U.UNIT_ID=I.MAX_UNIT_ID
                        WHERE I.ITEM_ID=" + Convert.ToInt32(lines[1].ToString());

        DataTable dtUnit = APPFUN.FillDataTablebyQuery(vSQL);

        return dtUnit;
    }

    
    protected void txtquantity_TextChanged(object sender, EventArgs e)
    {
       DataTable dt = new DataTable();
       dt = (DataTable)ViewState["GetRecords"];
       for(int i=0; i < dt.Rows.Count;i++)
       {

           dt.Rows[i][3] = ((TextBox)grdDetails.Rows[i].Cells[2].FindControl("txtquantity")).Text;
           dt.Rows[i][4] = ((TextBox)grdDetails.Rows[i].Cells[3].FindControl("txtNote")).Text;
           dt.Rows[i][5] = ((TextBox)grdDetails.Rows[i].Cells[4].FindControl("txtspec")).Text;
       }
    }

    protected void grdDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        APPFUN _app = new APPFUN();
        string requisitionId = "123";// Convert.ToInt32(_app.GenerateIDCODE(4, "DEPT_REQ_NO", "INV_TRANS_DEPT_REQ_MST")).ToString();

        if (!string.IsNullOrEmpty(txtReqRefNo.Text) && txtReqRefNo.Text != string.Empty)
        {
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_DEPT_REQ";
                cmd.Connection = conn;

                //Insert to DocumentInf
                cmd.Parameters.Add("@WhatToDo", "Insert");
                cmd.Parameters.Add("@REQ_REF_MEMO", SqlDbType.VarChar).Value = "14-0007";
                cmd.Parameters.Add("@REQ_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yyyy");

                cmd.Parameters.Add("@DEPARTMENT_ID", SqlDbType.Int).Value = ddldepartment.SelectedValue.ToString();
                cmd.Parameters.Add("@CAT_ID", SqlDbType.Int).Value = ddlcategory.SelectedValue.ToString();
                cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = null;
                cmd.Parameters.Add("@INV_TYPE_ID", SqlDbType.Int).Value = null;
                cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = null;
                cmd.Parameters.Add("@REQUIRED_DATE", SqlDbType.DateTime).Value = null;
                cmd.Parameters.Add("@URGENT_NOTE", SqlDbType.VarChar).Value = null;
                cmd.Parameters.Add("@IS_URGENT ", SqlDbType.Int).Value = null;
                cmd.Parameters.Add("@REMARKS", SqlDbType.VarChar).Value = null;
                cmd.Parameters.Add("@ENTRY_DATE", SqlDbType.DateTime).Value = null;
                cmd.Parameters.Add("@ENTRY_BY", SqlDbType.Int).Value = null;
                // command.Parameters.Add(" @UPDATE_DATE",SqlDbType.DateTime).Value = null;
                //  command.Parameters.Add(" @UPDATE_BY" , SqlDbType.VarChar).Value = null;
                // command.Parameters.Add(" @CHECK_BY ", SqlDbType.Int).Value = null;
                //  command.Parameters.Add(" @CHECK_DATE",SqlDbType.DateTime).Value = null;
                //command.Parameters.Add(" @APPROVE_BY ", SqlDbType.Int).Value = null;
                //  command.Parameters.Add(" @APPROVE_DATE",SqlDbType.DateTime).Value = null;
                cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = null;
                cmd.Parameters.Add("@IS_MONTHLY_SUMMERY", SqlDbType.Int).Value = null;
            //    cmd.Parameters.Add("@old_code", SqlDbType.Int).Value = null;
                cmd.Parameters.Add("@DEP_CHECK_BY", SqlDbType.Int).Value = null;
                cmd.Parameters.Add("@DEP_CHECK_DATE", SqlDbType.DateTime).Value = null;
                cmd.Parameters.Add("@DEP_APPROVE_BY", SqlDbType.Int).Value = null;
                cmd.Parameters.Add("@DEP_APPROVE_DATE", SqlDbType.DateTime).Value = null;
                cmd.Parameters.Add("@SURVEY_LINE", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@SectionRoleID", SqlDbType.Int).Value = null;

                //Department Requisition Details
                XmlDocument xmldoc = new XmlDocument();
                XmlElement doc = xmldoc.CreateElement("doc");
                xmldoc.AppendChild(doc);

                //Save to DocumentItems
                //DataTable table = ViewState["GetRecords"] as DataTable;

                //string itemID, unitID, reqQty, procQty, specification, referenceNo;

                //foreach (DataRow row in table.Rows)
                foreach (GridViewRow row in grdDetails.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label itemID = (Label)row.FindControl("lblItemID");
                        Label unitID = (Label)row.FindControl("lblIUnitID");
                        TextBox reqQty = (TextBox)row.FindControl("txtquantity");
                     //   TextBox procQty = (TextBox)row.FindControl("txtprocQty");
                        TextBox specification = (TextBox)row.FindControl("txtspec");
                        TextBox referenceNo = (TextBox)row.FindControl("txtNote");
                        XmlElement IS = xmldoc.CreateElement("RequisitionItems"); //InvestmentSignatory
                        doc.AppendChild(IS);
                        StringBuilder titleID = new StringBuilder("DT100");
                        StringBuilder titleName = new StringBuilder("OOP Concepts and .NET Part ");
                        IS.SetAttribute("DEPT_REQ_NO", requisitionId);
                        IS.SetAttribute("ITEM_ID", itemID.Text);
                        IS.SetAttribute("MAX_UNIT_ID", unitID.Text);
                        IS.SetAttribute("REQUIRED_QTY", reqQty.Text);
                      //  IS.SetAttribute("PROCURE_QTY", procQty.Text);
                        IS.SetAttribute("SPECIFICATION", specification.Text);
                        IS.SetAttribute("REFERENCE_NO", referenceNo.Text);
                    }
                }
                cmd.Parameters.AddWithValue("@doc", xmldoc.OuterXml);   //Use for item Details

               // cmd.Parameters.Add("@pIntErrDescOut", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    int rec = cmd.ExecuteNonQuery();
                   // int retVal = (int)cmd.Parameters["@pIntErrDescOut"].Value;

                    //if (!string.IsNullOrEmpty(Request.QueryString["RequisitionNo"]))
                    //{

                    //}
                    //else
                    //{
                    //    //txtReqRefNo.Text = txtBillRefNo.Text.Trim();
                    //    lblStatus.Text = "Save Successfully! Your Tracking No.: " + txtReqRefNo.Text.Trim() + ", Please Write the Tracking No. Top of Envelop";
                    //    string Msg = "<script>alert('Submit Successfully! Your Tracking No. is : " + txtReqRefNo.Text.Trim() + ",\\n\\ Please Write the Tracking No. Top of Envelop');</script>";
                    //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Save Alert", Msg, false);
                    //}

                }

                catch (Exception ex)
                {
                    lblStatus.Text = ex.Message;
                }

                finally
                {
                    conn.Close();
                    cmd.Dispose();
                    conn.Dispose();
                }
            }
        }
    }
}