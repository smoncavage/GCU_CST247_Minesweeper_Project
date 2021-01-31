<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Minesweep.aspx.cs" Inherits="CLC_MinesweeperMVC.Services.Web.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Minesweeper</title>
</head>
<body>
    <form id="form2" runat="server" style="text-align:center">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">

        </asp:ScriptManager>
        <div style="text-align:center">
            <asp:UpdatePanel id="ButtonPanel" runat="server" BorderStyle="solid"  style="text-align:center" Width="300px" Height="300px" UpdateMode="Always">
                <ContentTemplate>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"> </asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>   
</body>
</html>
