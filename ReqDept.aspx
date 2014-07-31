<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReqDept.aspx.cs" Inherits="ReqDept" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
            $("#add").click(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ReqDept.aspx/ADDNEWROW",
                   // data: "{'ItemName':'" + document.getElementById('txtSearch2').value + "'}",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });
            });
        });
        function SearchText() {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ReqDept.aspx/GetAutoCompleteData",
                        data: "{'ItemName':'" + document.getElementById('txtSearch2').value + "'}",
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
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div id="flip" style="border: inset; border-color: aliceblue;">Add New Item</div>
        <%--<button id="flip">Animate height</button>--%>
        <div id="panel">
            <%--<asp:Button ID="btnAddNew1" runat="server" CausesValidation="false" Text="Add New" OnClick="btnAddNew_Click" />--%>
            <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%" ScrollBars="Vertical" GroupingText="Search Item">
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            <br />
                            <asp:GridView ID="GridView1" runat="server"
                                AutoGenerateColumns="False" Font-Names="Verdana"
                                AllowPaging="false" ShowFooter="True" PageSize="10"
                                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px">
                                <AlternatingRowStyle BackColor="#FFD4BA" />
                                <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                <HeaderStyle Height="10px" BackColor="#FF9E66" Font-Size="15px" BorderColor="#CCCCCC"
                                    BorderStyle="Solid" BorderWidth="1px" />
                                <RowStyle Height="20px" Font-Size="13px" BorderColor="#CCCCCC" BorderStyle="Solid"
                                    BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="itemId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemIdSearch" runat="server" Text='<%#Eval("ItemId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemNameSearch" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description" HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescriptionSearch" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitSearch" runat="server" Text='<%#Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantitySearch" runat="server" Text="0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqDescription" ValidationGroup="ValgrpCust" ControlToValidate="txtQuantitySearch" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AddNote" HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAddNoteSearch" runat="server" Text=""></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="What to do">
                                        <ItemTemplate>
                                            <span onclick="return confirm('Are you sure want to delete?')">
                                                <asp:Button ID="btnInsertRecord" runat="server" Text=" Add " ValidationGroup="ValgrpCust" CommandName="Insert" />
                                            </span>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="Panel2" runat="server" Width="100%" Height="20%" ScrollBars="Vertical" GroupingText="Requisition Items">
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" AllowPaging="false"
                            AutoGenerateColumns="False"
                            BorderWidth="1px" Font-Names="Verdana" PageSize="10" ShowFooter="True" DataKeyNames="ItemID"
                            BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="4">
                            <AlternatingRowStyle BackColor="#FFD4BA" />
                            <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                            <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                            <HeaderStyle BackColor="#FF9E66" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" Font-Size="15px" Height="10px" />
                            <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                Font-Size="13px" Height="20px" />
                            <Columns>
                                <asp:TemplateField HeaderText="itemId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemId" runat="server" Text='<%#Eval("itemId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="20px" HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="20px" HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="20px" HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Unit") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqQuantity" ValidationGroup="ValgrpCust" ControlToValidate="txtQuantity" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <HeaderStyle Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Width="20px" HeaderText="AddNote">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAddNote" runat="server" Text=""></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="15%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="What to do">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" />
                                        <span onclick="return confirm('Are you sure want to delete?')">
                                            <asp:LinkButton ID="btnDelete" Text="Delete" runat="server" CommandName="Delete" />
                                        </span>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="Update" />
                                        <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="15%"></HeaderStyle>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </asp:Panel>

       <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                    <ContentTemplate>  

        <asp:GridView runat="server" ID="grditem" OnRowCommand="grditem_RowCommand" OnRowDataBound="grditem_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                       <div class="demo">
                            <div class="ui-widget">
                                <input type="text" id="txtSearch2" class="autosuggest" />
                                <%--<asp:TextBox ID="txtSearch2" runat="server" AutoPostBack="true"></asp:TextBox>--%>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Button" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input id="bd" type="text" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input id="cd" type="text" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <input id="add"  type="button" value="ADD" />
                        <%--<asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />--%>
                        
                    </ItemTemplate>
                    <FooterTemplate>
                        
                    </FooterTemplate>
                </asp:TemplateField>
          <asp:ButtonField CommandName="ADD" Text="ADD" />
                  </Columns>
          
        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                        <br />
                        </ContentTemplate>
           </asp:UpdatePanel>
    </form>
</body>


</html>
