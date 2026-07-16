<%@ page title="Bem-vindo ao Opus!" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Default.aspx.cs" inherits="Opus.View.Usuario.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <img src="../../Img/Central.png" alt="introdução ao Opus" style="display: block; height: 70%; margin: auto; position: center; top: 0; bottom: 0; left: 0; right: 0; padding: 70px;" />

    <center><asp:Button ID="Button1" CssClass="btn btn-dark cor-roxa" Width="200px" runat="server" Text="Começar" OnClick="Button1_Click"/></center>
</asp:Content>
