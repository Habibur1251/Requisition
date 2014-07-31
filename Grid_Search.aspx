<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Grid_Search.aspx.cs" Inherits="Grid_Search" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <%--<asp:LinkButton ID="lnkedit" CommandName="Edit" runat="server" CausesValidation="true" Text="<i class=icon-edit icon-white></i>Edit"></asp:LinkButton>
                                    |--%>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.7.2.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#flip").click(function () {
                $("#panel").slideToggle("slow");
            });
        });
    </script>
    <style>
        #panel {
            padding: 5px;
            text-align: center;
            background-color: #e5eecc;
            border: solid 1px #c3c3c3;
        }

        #panel {
            padding: 5px;
            display: none;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"
        type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            SearchText();

        });
        function SearchText() {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Grid_Search.aspx/GetAutoCompleteData",
                        data: "{'ItemName':'" + document.getElementById('<%=txtSearch.ClientID%>').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                }
            });
        }
        

    </script>
    <script type="text/javascript">
        var ddl = document.getElementById("<%=ddlcategory.ClientID%>");
        var SelVal = ddl.options[ddl.selectedIndex].text;
        alert(SelVal); //SelVal is the selected Value
    </script>

    <script type="text/javascript">
        function removeElement() {
        document.getElementById("<%= txtSearch.ClientID %>").value = "";
        document.getElementById("<%= txtquantity.ClientID %>").value = "";
        document.getElementById("<%= txtbudget.ClientID %>").value = "";
    }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Top Div--->
    <div class="box span12">
        <%--<asp:LinkButton ID="lnkedit" CommandName="Edit" runat="server" CausesValidation="true" Text="<i class=icon-edit icon-white></i>Edit"></asp:LinkButton>
                                    |--%>
        <div class="box-content">
            <table class="table table-striped table-bordered bootstrap-datatable datatable">
                <tbody>
                    <asp:Button ID="NewReq" CssClass="btn-success btn-round" runat="server" Text="New Requisition" OnClick="NewReq_Click" Width="120px" />
                    Company Name: <asp:DropDownList ID="ddlCompany" AutoPostBack="true"  runat="server" DataSourceID="SqlDataSource3" DataTextField="COMPANY_NAME" DataValueField="COMPANY_ID">
                    </asp:DropDownList>

                    <tr>
                        <td class="center">
                            <span class="label label-success">Requisition Date</span>
                        </td>
                        <td>

                            <asp:TextBox ID="date" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <span class="label label-success">Ref. Req. No</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReqRefNo" runat="server"></asp:TextBox>
                        </td>

                        <td>
                            <span class="label label-success">Expected Date</span>
                        </td>
                        <td>
                            <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="label label-success">Category</span>
                        </td>
                        <td>
                            <asp:DropDownList AutoPostBack="true" ID="ddlcategory" runat="server" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" DataSourceID="SqlDataSource1" DataTextField="CAT_NAME" DataValueField="CAT_ID"></asp:DropDownList>

                        </td>
                        <td>
                            <span class="label label-success">Department</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddldepartment" runat="server" DataSourceID="SqlDataSource2" DataTextField="DEPARTMENT_NAME" DataValueField="DEPARTMENT_ID"></asp:DropDownList>

                        </td>
                        <td>
                            <span class="label label-success">Req. Type</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlreqcat" runat="server">
                                <asp:ListItem Value="0">Purchase Requisition</asp:ListItem>
                                <asp:ListItem Value="1">Store Requisition</asp:ListItem>
                                <asp:ListItem Value="2">Monthly Requisition</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <!-- End Top Div--->

    <div class="box span12">
        <div class="box-header well">
            <input id="flip" type="button" value="Add Item" class="btn btn-minimize btn-round" />
        </div>
        <div id="panel">

            <asp:TextBox ViewStateMode="Disabled" runat="server" ID="txtSearch" placeholder="Find Item Name" CssClass="autosuggest" AutoCompleteType="None"></asp:TextBox>
            <asp:TextBox ID="txtquantity" placeholder="Quantity" Visible="false" runat="server" AutoCompleteType="None"></asp:TextBox>
            <asp:TextBox ID="txtbudget" placeholder="Budget" runat="server" Visible="false" AutoCompleteType="None"></asp:TextBox>
            <%--<asp:LinkButton ID="lnkedit" CommandName="Edit" runat="server" CausesValidation="true" Text="<i class=icon-edit icon-white></i>Edit"></asp:LinkButton>
                                    |--%>
            <asp:Button ID="btnAdd" class="btn-primary btn-round" runat="server" Text="ADD"  OnClick="btnAdd_Click" Height="31px" Width="96px" />
            <input type="button" name="add" id="addnew" value="Clear" class=" btn-round" onclick="removeElement()" style="Height:31px; Width:96px" />

        </div>
    </div>

    <div class="box span12">
        <div class="box-header well">
            <h2>List</h2>
        </div>
        <div class="box-content">
            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView Width="100%" ID="grdDetails" 
                        CssClass="box-content table table-bordered" runat="server" 
                        AutoGenerateColumns="False" OnRowCommand="Gridview1_RowCommand" 
                        DataKeyNames="ITEM_ID" onrowdeleting="grdDetails_RowDeleting" >
                        <EmptyDataTemplate>
                            <asp:Label ID="emp" runat="server">No data available</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>

                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemID" runat="server" Text='<%# Bind("ITEM_ID") %>'></asp:Label>
                                    <asp:Label ToolTip='<%# Bind("ITEM_ID") %>' ID="lblitemname" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblIUnitID" runat="server" Visible="true" Text='<%# Bind("UNIT_ID") %>'></asp:Label>
                                    <asp:Label ID="lblunit" runat="server" Text='<%# Bind("UNIT_DESC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-CssClass="center" HeaderText="Quantity">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAddQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtquantity" placeholder="Enter Quantity" runat="server" OnTextChanged="txtquantity_TextChanged" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredQuantity" runat="server" ControlToValidate="txtquantity"
                                        Display="Dynamic" ValidationGroup="ValgrpCust" ErrorMessage="Enter the Quantity!">*</asp:RequiredFieldValidator>

                                    <asp:CompareValidator ID="CompareQuantity" runat="server" ControlToValidate="txtquantity"
                                        ErrorMessage="Number Only" Operator="DataTypeCheck" Type="Integer" SetFocusOnError="true"></asp:CompareValidator>
                                </ItemTemplate>
                                <HeaderStyle CssClass="center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Specifications">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAddSpecification" runat="server" Text='<%# Bind("Spec") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtspec" OnTextChanged="txtquantity_TextChanged" placeholder="Enter Specification" runat="server" Text='<%# Bind("Spec") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-CssClass="center" HeaderText="Add Note">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAddNote" runat="server" Text='<%# Bind("Note") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNote" OnTextChanged="txtquantity_TextChanged" AutoPostBack="true" placeholder="Enter Budget" runat="server" Text='<%# Bind("Note") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle CssClass="center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-CssClass="center" ShowHeader="false">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="lnkedit" CommandName="Edit" runat="server" CausesValidation="true" Text="<i class=icon-edit icon-white></i>Edit"></asp:LinkButton>
                                    |--%>
                                    <asp:LinkButton ID="LinkButton1" CommandName="Delete" runat="server" CausesValidation="true" Text="<i class=icon-trash icon-white></i>Delete" ForeColor="Red"></asp:LinkButton>

                                </ItemTemplate>
                                <%-- <EditItemTemplate>
                                    <asp:LinkButton ID="lnkup" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                    | 
                                    <asp:LinkButton ID="lnkcancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>--%>
                                <HeaderStyle CssClass="center" />
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    <div>
                        <table>
                            <tr>
                                <td style="width: 900px">
                                    <span class="label label-success">Remarks:</span>
                                    <br />
                                    <br />

                                    <asp:TextBox ID="txtremarks" TextMode="MultiLine" runat="server" Width="95%"></asp:TextBox>
                                    <br />
                                    <asp:Button ID="btnSave" runat="server" Text="Save" 
                                        CssClass="btn-success btn-round" onclick="btnSave_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                        CssClass="btn-danger btn-round" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" 
                                        CssClass="btn-info btn-round" />
                                    <asp:Button ID="Button5" runat="server" Text="Close" CssClass="btn-round" />
                                </td>
                                <td>
                                    <label style="width: 150px">Attach Proper Document(If any)</label>
                                    <div class="controls">
                                        <input class="input-file uniform_on" id="fileInput" type="file">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                </ContentTemplate>

                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnADD" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dtcon %>" SelectCommand="SELECT 0 as CAT_ID , '[Select Category]' as CAT_NAME 
 UNION select CAT_ID ,  CAT_NAME as CAT_NAME FROM dbo.INV_SYS_ITEM_CATEGORY;"></asp:SqlDataSource>



    <asp:Label ID="lblStatus" runat="server"></asp:Label>



    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:dtcon %>" SelectCommand="SELECT 0 as DEPARTMENT_ID, '[Select Department]' as DEPARTMENT_NAME 
 UNION select DEPARTMENT_ID,  DEPARTMENT_NAME as DEPARTMENT_NAME   FROM dbo.SYS_DEPARTMENT;"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:dtcon %>" SelectCommand="SELECT 0 as COMPANY_ID, '[Select Department]' as COMPANY_NAME 
 UNION select DISTINCT [COMPANY_ID],[COMPANY_NAME] FROM [SYSTEM_CONFIG].[dbo].[DATABASE_MODULE] ORDER BY [COMPANY_ID];"></asp:SqlDataSource>
</asp:Content>
