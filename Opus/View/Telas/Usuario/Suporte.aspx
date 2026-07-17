<%@ Page Title="Suporte - Opus" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Suporte.aspx.cs" Inherits="Opus.View.Telas.Usuario.Suporte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h2>Suporte</h2>
    </center>
    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">E-mail</label>
        <asp:Label runat="server" ID="lblEmail" CssClass="form-label" Text=""></asp:Label>
        <asp:TextBox ID="tbxEmail" type="email" class="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Mensagem</label>
        <asp:TextBox ID="tbxMensagem" type="text" class="form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
    </div>
    <center><asp:Button runat="server" ID="btnEnviar" type="button" OnClick="EnviarMensagem" Width="200px" class="btn cor-roxa btn-dark" Text="Enviar"/></center><br />
</asp:Content>