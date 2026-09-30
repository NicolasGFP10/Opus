<%@ Page Title="Avaliações - Opus" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Avaliacao.aspx.cs" Inherits="Opus.View.Telas.Usuario.Avaliacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Avaliação</h2>
    </center>

    <div class="d-grid gap-2 d-md-flex justify-content-md-end" style="padding-top: 20px; padding-right: 30px;">

        <asp:Label ID="lblTexto" runat="server" Text="Gerar código de Avaliação" Visible="false"></asp:Label>

        <asp:Label ID="lblCodigo" runat="server" Text="" CssClass="fonte-vermelha" Visible="false"></asp:Label>

        <asp:Button ID="btnGerar" runat="server" Text="Gerar" CssClass="btn cor-roxa btn-dark" Visible="false" OnClick="btnGerar_Click" />

    </div>

</asp:Content>
