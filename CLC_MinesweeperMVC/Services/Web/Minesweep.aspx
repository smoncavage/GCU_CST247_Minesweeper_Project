<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Minesweep.aspx.cs" Inherits="CLC_MinesweeperMVC.Services.Web.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Minesweeper</title>
</head>
<body>
    <form id="form2" runat="server">
        <div style="text-align:center">
            <asp:Panel runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Button id="boardGrid" runat="server" Font-Names="Stencil" onclick="Grid_Button_Click" width="100px" />
                                <!--<img src="~/Images/checkered_flag.bmp" />-->
                            
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>   
</body>
</html>
