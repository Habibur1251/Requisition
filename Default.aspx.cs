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


public partial class _Default : System.Web.UI.Page
{
    DataTable GlobalDataTable = new DataTable();
    
    public static int GlobalIndex;
    public static int temp;

    protected void Page_Load(object sender, EventArgs e)
    {

        date.Text = DateTime.Now.ToString("dd-MM-yyyy");

        if (!IsPostBack)
        {
            BindSecondGrid();
        }
       
        
        //Check View state and create data table to show emply grid box
        if (ViewState["GetRecords"] == null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ITEM_CODE");
            dt.Columns.Add("ITEM_NAME");
            dt.Columns.Add("unit_desc");
            dt.Rows.Add();
            dt.AcceptChanges();

            dgitem.DataSource = dt;
            dgitem.DataBind();
        }
       
    }
   

    #region Check box select

    protected void chkSelect_CheckChanged(object sender, EventArgs e)
    {
        GetSelectedRows();
        BindSecondGrid();
    }

    protected void BindSecondGrid()
    {
        DataTable dt = new DataTable(); 
     
        dt = (DataTable)ViewState["GetRecords"];

        if (GlobalIndex != 0)
        {
            dgitem.DataSource = dt;

        }
        else
        {
            dgitem.DataSource = dt; 
        }
            dgitem.DataBind();

            if (IsPostBack)
            {
                fill_dgitem();
            }
    }
    private void GetSelectedRows()
    {
        DataTable dt;
        if (ViewState["GetRecords"] != null)
        {
            dt = (DataTable)ViewState["GetRecords"];
        }
        else
            dt = CreateTable();
     
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView3.Rows[i].Cells[0].FindControl("chkSelect");
            
            if (chk.Checked)
            {
                dt = AddGridRow(GridView3.Rows[i], dt,i);
                chk.Visible = false;
                GridView3.Rows[i].Cells[0].Visible = false;
                GridView3.Rows[i].Cells[1].Visible = false;
                GridView3.Rows[i].Cells[2].Visible = false;
                GridView3.Rows[i].Cells[3].Visible = false;
            }
            else
            {
                dt = RemoveRow(GridView3.Rows[i], dt);
                chk.Visible = true;
                GridView3.Rows[i].Cells[0].Visible = true;
                GridView3.Rows[i].Cells[1].Visible = true;
                GridView3.Rows[i].Cells[2].Visible = true;
                GridView3.Rows[i].Cells[3].Visible = true;
            }
        }
        
        ViewState["GetRecords"] = dt;
    }
    private DataTable AddGridRow(GridViewRow gvRow, DataTable dt, int index)
    {
        DataRow[] dr = dt.Select("ITEM_ID = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length <= 0)
        {
            //monitor update or new data add on dgitem

            if (GlobalIndex != 0) 
            {
                int i = GlobalIndex + 1;
                string s = dt.Rows[GlobalIndex]["index"].ToString();
                int temp = Convert.ToInt32(s);
                CheckBox chk = (CheckBox)GridView3.Rows[temp].Cells[0].FindControl("chkSelect");
                dt.Rows[GlobalIndex]["ITEM_ID"] = gvRow.Cells[1].Text;
                dt.Rows[GlobalIndex]["ITEM_CODE"] = gvRow.Cells[2].Text;
                dt.Rows[GlobalIndex]["ITEM_NAME"] = gvRow.Cells[3].Text;
                dt.Rows[GlobalIndex]["index"] = index.ToString();
              
                    if (chk.Checked)
                    {
                        chk.Checked = false;
                        GetSelectedRows();
                        BindSecondGrid();
                    }
            }
            else
            {
                dt.Rows.Add();
                int rowscount = dt.Rows.Count - 1;
                dt.Rows[rowscount]["ITEM_ID"] = gvRow.Cells[1].Text;
                dt.Rows[rowscount]["ITEM_CODE"] = gvRow.Cells[2].Text;
                dt.Rows[rowscount]["ITEM_NAME"] = gvRow.Cells[3].Text;
                dt.Rows[rowscount]["index"] = index.ToString();
                
            }
            dt.AcceptChanges();
           
        }
        return dt;
    }
    private DataTable RemoveRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("ITEM_ID = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length > 0)
        {
            dt.Rows.Remove(dr[0]);
            dt.AcceptChanges();
        }
        return dt;
    }
    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();
        dt.Rows.Add();
        dt.Columns.Add("ITEM_ID");
        dt.Columns.Add("ITEM_CODE");
        dt.Columns.Add("ITEM_NAME");
        dt.Columns.Add("UNIT_DESC");
        dt.Columns.Add("index");
      
        
        dt.AcceptChanges();
       
    
        return dt;
    }

    #endregion

    #region Gridview3 search

    public string HighlightText(string InputTxt)
    {
        string Search_Str = txtsearch.Text;
        // Setup the regular expression and add the Or operator.
        Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
        // Highlight keywords by calling the
        //delegate each time a keyword is found.
        return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
    }
    public string ReplaceKeyWords(Match m)
    {
        return ("<span class=highlight>" + m.Value + "</span>");
    }
    private string SearchString = "";
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        //  Set the value of the SearchString so it gets
        
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        SearchString = txtsearch.Text;
    }

    #endregion

    protected void dgitem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Add new row to dgitem grid 
        if (e.CommandName.Equals("trigger_id"))
        {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#currentdetail').modal('show').draggable();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            
                string index = e.CommandArgument.ToString();
                GlobalIndex = Convert.ToInt32(index);
           
                temp_dgitem_values_add();
            //    XML_Reader("asd","dasd","dasdas");
        }

        ///Remove dgitem grid row

        else if (e.CommandName.Equals("remove"))
        {

            DataTable dt;
            if (ViewState["GetRecords"] != null)
            {
                dt = (DataTable)ViewState["GetRecords"];
                GlobalDataTable = (DataTable)ViewState["GetRecords"];
            }


            string index = e.CommandArgument.ToString();
            GlobalIndex = Convert.ToInt32(index);

            if (GlobalIndex != 0)
            {
                /* module to uncheck the checked box of gridview3 */
                string s = GlobalDataTable.Rows[GlobalIndex]["index"].ToString();
                temp = Convert.ToInt32(s);
                CheckBox chk = (CheckBox)GridView3.Rows[temp].Cells[0].FindControl("chkSelect");
                if (chk.Checked)
                {
                    chk.Checked = false;
                    UpdatePanel4.Update();
                    GetSelectedRows();
                    BindSecondGrid();
                }
            }
        }
    }

    protected void temp_dgitem_values_add()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ITEM_Budget_Quantity");
        dt.Columns.Add("ITEM_Required_Quantity");
        dt.Columns.Add("RowIndex");

        for (int i = 0; i < dgitem.Rows.Count; i++)
        {
            dt.Rows.Add();
            dt.Rows[i][0] = ((TextBox)dgitem.Rows[i].Cells[2].FindControl("ITEM_Budget_Quantity")).Text;
            dt.Rows[i][1] = ((TextBox)dgitem.Rows[i].Cells[5].FindControl("ITEM_Required_Quantity")).Text;
            dt.Rows[i][2] = i;
        }
        ViewState["dgitemtable"] = dt;
    }

    protected void fill_dgitem()
    {
        DataTable dt2 = new DataTable();
        dt2 = (DataTable)ViewState["dgitemtable"];
        
        if (ViewState["dgitemtable"]!=null && dt2.Rows.Count > dgitem.Rows.Count)
        {
            dt2.Rows[GlobalIndex].Delete();
        }

        if (ViewState["dgitemtable"] != null && dt2.Rows.Count<=dgitem.Rows.Count)
        {
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                ((TextBox)dgitem.Rows[i].Cells[2].FindControl("ITEM_Budget_Quantity")).Text = dt2.Rows[i][0].ToString();
                ((TextBox)dgitem.Rows[i].Cells[5].FindControl("ITEM_Required_Quantity")).Text = dt2.Rows[i][1].ToString();
            }
        }
    }
    protected void ITEM_Budget_Quantity_TextChanged(object sender, EventArgs e)
    {
        temp_dgitem_values_add();
        //if (IsPostBack)
        //{
        //    TextBox txt = ((TextBox)dgitem.Rows[0].Cells[2].FindControl("ITEM_Budget_Quantity"));
        //    txt.Focus();
        //}
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        APPFUN _app = new APPFUN();
        string requisitionId = "";// Convert.ToInt32(_app.GenerateIDCODE(4, "DEPT_REQ_NO", "INV_TRANS_DEPT_REQ_MST")).ToString();

        if (!string.IsNullOrEmpty(txtReqRefNo.Text) && txtReqNo.Text != string.Empty)
        {
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_INV_TRANS_DEPT_REQ_MST_Insert";
                cmd.Connection = conn;

                //Insert to DocumentInf
                cmd.Parameters.Add("@WhatToDo", "Insert");
                cmd.Parameters.Add("@REQ_REF_MEMO", SqlDbType.VarChar).Value = "14-0007";
                cmd.Parameters.Add("@REQ_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yyyy");

                cmd.Parameters.Add("@DEPARTMENT_ID", SqlDbType.Int).Value = ddldept.SelectedValue.ToString();
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
                cmd.Parameters.Add("@old_code", SqlDbType.Int).Value = null;
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
                DataTable table = ViewState["GetRecords"] as DataTable;

                string itemID, unitID, reqQty, procQty, specification, referenceNo;

                foreach (DataRow row in table.Rows)
                {
                    if (row.ItemArray[1].ToString() != string.Empty || row.ItemArray[1].ToString() != null)
                    {
                        itemID = row.ItemArray[1] as string;
                        unitID = row.ItemArray[2] as string;
                        reqQty = row.ItemArray[3] as string;
                        procQty = row.ItemArray[3] as string;
                        specification = row.ItemArray[3] as string;
                        referenceNo = row.ItemArray[3] as string;

                        XmlElement IS = xmldoc.CreateElement("RequisitionItems"); //InvestmentSignatory
                        doc.AppendChild(IS);
                        StringBuilder titleID = new StringBuilder("DT100");
                        StringBuilder titleName = new StringBuilder("OOP Concepts and .NET Part ");
                        IS.SetAttribute("DEPT_REQ_NO", requisitionId);
                        IS.SetAttribute("ITEM_ID", itemID);
                        IS.SetAttribute("MAX_UNIT_ID", unitID);
                        IS.SetAttribute("REQUIRED_QTY", reqQty);
                        IS.SetAttribute("PROCURE_QTY", procQty);
                        IS.SetAttribute("SPECIFICATION", specification);
                        IS.SetAttribute("REFERENCE_NO", referenceNo);
                    }
                }
                cmd.Parameters.AddWithValue("@doc", xmldoc.OuterXml);   //Use for item Details


                //command.Parameters.Add("@DEPT_REQ_NO", SqlDbType.Int).Value = null;
                //command.Parameters.Add("@ITEM_ID", SqlDbType.Int).Value = null;
                //command.Parameters.Add("@MAX_UNIT_ID", SqlDbType.Int).Value = null;
                //command.Parameters.Add("@REQUIRED_QTY", SqlDbType.Int).Value = null;
                //command.Parameters.Add("@CUR_STOCK", SqlDbType.Int).Value = null;
                //command.Parameters.Add("@PROCURE_QTY", SqlDbType.Int).Value = null;
                //command.Parameters.Add("@PRE_REQ_ITEM", SqlDbType.VarChar).Value = null;
                //command.Parameters.Add("@SPECIFICATION", SqlDbType.VarChar).Value = null;
                //command.Parameters.Add("@REFERENCE_NO", SqlDbType.VarChar).Value = null;


                cmd.Parameters.Add("@pIntErrDescOut", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    int rec = cmd.ExecuteNonQuery();
                    int retVal = (int)cmd.Parameters["@pIntErrDescOut"].Value;

                    if (!string.IsNullOrEmpty(Request.QueryString["RequisitionNo"]))
                    {

                    }
                    else
                    {
                        //txtReqRefNo.Text = txtBillRefNo.Text.Trim();
                        lblStatus.Text = "Save Successfully! Your Tracking No.: " + txtReqRefNo.Text.Trim() + ", Please Write the Tracking No. Top of Envelop";
                        string Msg = "<script>alert('Submit Successfully! Your Tracking No. is : " + txtReqRefNo.Text.Trim() + ",\\n\\ Please Write the Tracking No. Top of Envelop');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Save Alert", Msg, false);
                    }

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