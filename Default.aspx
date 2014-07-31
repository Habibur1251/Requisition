<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="body" runat="server">

            <asp:Panel ID="Panel1" runat="server" Width="100%">

                <div id="content" class="span10">
                    <!-- content starts -->
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>

                    <div class="breadcrumb">

                        <asp:Label ID="reqdate" runat="server" Text="Req. Date"></asp:Label>

                        <asp:TextBox ID="date" runat="server" Enabled="False" Height="10px" Width="100px"></asp:TextBox>

                        <asp:Button ID="btnnewreq" runat="server" CssClass="label label-success" Text="New Req No" Width="100px" />

                        <asp:TextBox ID="deptmemo0" runat="server" Height="10px" Width="12%"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server" Text="Ref. Req. No"></asp:Label>
                        <asp:TextBox ID="txtReqNo" runat="server" Height="10px" Width="11%"></asp:TextBox>
                        <asp:TextBox ID="txtReqRefNo" runat="server" Height="10px" Width="12%"></asp:TextBox>

                        <br />
                        <asp:Label ID="Label5" runat="server" Text="Department"></asp:Label>

                        <asp:DropDownList ID="ddldept" runat="server" DataSourceID="SqlDataSource1" DataTextField="DEPARTMENT_NAME" DataValueField="DEPARTMENT_ID">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dtcon %>" SelectCommand="SELECT 0 as DEPARTMENT_ID, '[Select Department]' as DEPARTMENT_NAME 
 UNION select DEPARTMENT_ID,  DEPARTMENT_NAME as DEPARTMENT_NAME   FROM dbo.SYS_DEPARTMENT;

"></asp:SqlDataSource>

                        <asp:Label ID="Label9" runat="server" Text="Expected Date"></asp:Label>

                        <asp:TextBox ID="datepicker" runat="server" Width="130px"></asp:TextBox>
                        <br />
                        <asp:Label ID="Label10" runat="server" Text="Catagory"></asp:Label>
                        <asp:DropDownList ID="ddlcategory" runat="server" DataSourceID="SqlDataSource2" DataTextField="CAT_NAME" DataValueField="CAT_id" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:dtcon %>" SelectCommand="

SELECT 0 as CAT_ID , '[Select Category]' as CAT_NAME 
 UNION select CAT_ID ,  CAT_NAME as CAT_NAME FROM dbo.INV_SYS_ITEM_CATEGORY;"></asp:SqlDataSource>
                        &nbsp;
                <asp:CheckBox ID="CheckBox1" runat="server" Height="20px" Text="Is PO / Cash" Width="120px" />
                        <asp:CheckBox ID="CheckBox2" runat="server" Height="20px" Text="Monthly Req." Width="120px" />
                    </div>
                    <div class="row-fluid sortable">
                        <div class="box span12">
                            <div class="box-header well">
                                <h2>Combined All</h2>
                                <div class="box-icon">
                                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                                </div>
                            </div>
                            <div class="box-content">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                    <ContentTemplate>
                                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                        <div id="dgitem_div">
                                            <asp:GridView CssClass="table table-bordered table-striped table-condensed" ID="dgitem" runat="server"
                                                AllowPaging="false" AutoGenerateColumns="False" OnRowCommand="dgitem_RowCommand">
                                                <EmptyDataTemplate>                                                  
                                                    <asp:Label ID="emp" runat="server">No data available</asp:Label>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField Visible="false" HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label Width="1%" ID="ITEM_CODE" runat="server" Text='<%#Eval("ITEM_CODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label CssClass="text" Width="75%" ID="ITEM_NAME" Text='<%#Eval("ITEM_NAME") %>' runat="server" ToolTip='<%# "Item code:"+ Eval("ITEM_CODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label Width="75%" ID="ITEM_UNIT" runat="server" Text='<%# Eval("unit_desc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B/Q">
                                                        <ItemTemplate>
                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="ITEM_Budget_Quantity_TextChanged" EnableViewState="true" Width="75%" ID="ITEM_Budget_Quantity" runat="server" ToolTip="Budget Quantity"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C/Q">
                                                        <ItemTemplate>
                                                            <asp:TextBox Visible="false" EnableViewState="true" Width="75%" ToolTip="Consume Quantity" ID="ITEM_Consume_Quantity" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock">
                                                        <ItemTemplate>
                                                            <asp:TextBox Visible="false" Width="75%" ID="ITEM_Stock" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R/Q">
                                                        <ItemTemplate>
                                                            <asp:TextBox Visible="true" AutoPostBack="true" OnTextChanged="ITEM_Budget_Quantity_TextChanged" ToolTip="Required Quantity" Width="75%" ID="ITEM_Required_Quantity" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PR Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox Visible="false" Width="75%" ID="ITEM_PR_QUANTITY" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notes">
                                                        <ItemTemplate>
                                                            <asp:TextBox Visible="false" TextMode="MultiLine" Width="90%" ID="ITEM_NOTES" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:ButtonField CommandName="remove"
                                                        ControlStyle-CssClass="btn" ButtonType="Button"
                                                        Text="Remove" />

                                                    <asp:ButtonField CommandName="trigger_id"
                                                        ControlStyle-CssClass="btn" ButtonType="Button"
                                                        Text="Attach" />
                                                </Columns>
                                                <PagerStyle CssClass="pagination" />
                                            </asp:GridView>
                                            <asp:Label Text="Remarks" runat="server" ID="label1"></asp:Label>
                                            <hr />
                                            <asp:TextBox Width="98%" TextMode="MultiLine" ID="txtremarks" runat="server"></asp:TextBox>
                                            <br />
                                            <br />
                                            <asp:Button CssClass="btn" ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                            <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                                        </div>
                                        <br />
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <img src="img/ajax-loaders/loadingAnimation.gif" class="modal modal-backdrop" alt="Loading.. Please wait!" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <!-- Pop Up panel Start-->
                                <div id="currentdetail" class="modal hide fade"
                                    tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                                    aria-hidden="true">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal"
                                            aria-hidden="true">
                                            ×</button>
                                        <h3 id="myModalLabel">Detailed View</h3>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Panel ScrollBars="Vertical" ID="pnlPopup" runat="server" CssClass="modalPopup" Height="200px">
                                            <%--<asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnSearch_Click" />--%>

                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">

                                                <ContentTemplate>

                                                    <fieldset>
                                                        <div>
                                                            <asp:Label Visible="false" ID="search" Text="Search" runat="server"></asp:Label>

                                                            <asp:TextBox Visible="false" AutoPostBack="true" ID="txtsearch" runat="server" OnTextChanged="txtsearch_TextChanged">

                                                            </asp:TextBox>
                                                        </div>
                                                        <asp:GridView CssClass="table table-bordered table-striped table-condensed" ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="ITEM_ID" DataSourceID="SqlDataSource5" AllowSorting="True" AllowPaging="false">
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="emp" runat="server">No data available</asp:Label>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <%--      <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select" OnClick="LinkButton1_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="ITEM_ID" HeaderText="ITEM ID" ReadOnly="True" SortExpression="ITEM_ID" />
                                                                <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM CODE" ReadOnly="True" SortExpression="ITEM_CODE" />
                                                                <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM NAME" SortExpression="ITEM_NAME" />

                                                            </Columns>
                                                            <%--<PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" />--%>
                                                        </asp:GridView>
                                                    </fieldset>
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <img src="img/ajax-loaders/loadingAnimation.gif" class="modal" alt="Loading.. Please wait!" />
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlcategory" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:dtcon %>" SelectCommand="SELECT [ITEM_ID], [ITEM_CODE], [ITEM_NAME], [CAT_ID],[UNIT_DESC] FROM [INV_SYS_ITEM_MST],INV_SYS_UNIT WHERE (INV_SYS_ITEM_MST.MAX_UNIT_ID = INV_SYS_UNIT.UNIT_ID and [CAT_ID] = @CAT_ID)" FilterExpression="ITEM_NAME LIKE '%{0}%'">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="ddlcategory" DefaultValue="0" Name="CAT_ID" PropertyName="SelectedValue" Type="Int32" />
                                                </SelectParameters>
                                                <FilterParameters>
                                                    <asp:ControlParameter Name="ITEM_NAME" ControlID="txtSearch" PropertyName="Text" />
                                                </FilterParameters>
                                            </asp:SqlDataSource>

                                        </asp:Panel>
                                        <div class="modal-footer">
                                            <button class="btn btn-info" data-dismiss="modal"
                                                aria-hidden="true">
                                                Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!--/span-->

                    </div>

                </div>
            </asp:Panel>
            <!---div1--->

</asp:Content>
