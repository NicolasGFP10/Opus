<%@ Page Title="Bem-vindo ao Opus!" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Opus.View.Usuario.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br /> 
    <center>
        <img src="../../Img/Central.png" alt="introdução ao Opus" width="50%"/></center>
    <center>
        <asp:Button ID="Button1" CssClass="btn btn-dark cor-roxa" Width="200px" runat="server" Text="Começar" OnClick="Button1_Click" /></center>
</asp:Content>
