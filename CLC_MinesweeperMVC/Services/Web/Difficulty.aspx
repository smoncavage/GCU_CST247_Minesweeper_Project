<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Difficulty.aspx.cs" Inherits="CLC_MinesweeperMVC.Services.Web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Difficulty</title>
</head>
<body>
    <form id="form1" runat="server" style="text-align:center">
        <h2 style="text-align:center; font-family:Stencil" >Please Select Difficulty</h2>

        <div style="margin-left: auto; margin-right:auto; Width:150px; Height:100px">
        <asp:panel id="panel1" runat="server" BorderStyle="solid"  style="text-align:center" >
            <asp:RadioButton ID="RadioButton1" runat="server" Font-Names="Stencil" Text="Easy" GroupName="difficulty" />
            <br />
            <asp:RadioButton ID="RadioButton2" runat="server" Font-Names="Stencil" Text="Moderate" GroupName="difficulty" />
            <br />
            <asp:RadioButton ID="RadioButton3" runat="server" Font-Names="Stencil" Text="Difficult" GroupName="difficulty" />
            <br />
            <asp:Button id="DifficultySubmit" runat="server" Font-Names="Stencil" Text="Submit" onclick="Button1_Click" width="100px" />
        </asp:panel>
        </div>
    </form>
</body>
</html>
