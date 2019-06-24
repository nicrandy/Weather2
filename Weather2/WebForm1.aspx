<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Weather2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Local Weather</title>
    <style type="text/css">
        body{
            font-family: Arial;
            font-size: 10pt;
        }
        table{
            border: 1px solid #ccc;
            border-collapse:collapse;
        }
        table th{
            background-color:#F7F7F7;
            color:#333;
            font-weight:bold;
        }
        table th, table td{
            padding: 5px;
            border: 1px solid #ccc;
        }
        table, table table td{
            border: 0px solid #ccc;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox ID="txtZip" runat="server" Text="Enter Zip Code" />
    <asp:Button Text="Get Weather" runat="server" OnClick="GetWeatherInfo" />
        <hr />
    <table id="tblWeather" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false">
        <tr>
            <th colspan="2">
                Weather Info
            </th>
            <th colspan="2">
                City:
                <asp:Label ID="lblCity" runat="server" />
            </th>
        </tr>
        <tr>
            <td rowspan="3">
                
            </td>
        </tr>
        <tr>
            <td>
                Temperature:
                <asp:Label ID="lblTemp" runat="server" />
            </td>
                        <td>
                Humidity:
                <asp:Label ID="lblHumid" runat="server" />
            </td>
        </tr>
        <tr>

                        <td>
                Wind Speed:
                <asp:Label ID="lblWind" runat="server" />
            </td>
                                    <td>
                Wind is from the
                <asp:Label ID="lblDir" runat="server" />
            </td>

        </tr>
        <tr>
        </tr>



    </table>
    </form>
</body>
</html>
