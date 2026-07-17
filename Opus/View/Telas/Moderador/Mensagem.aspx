<%@ Page Title="Mensagens - Opus" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Mensagem.aspx.cs" Inherits="Opus.View.Telas.Moderador.Mensagem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="gvMensagens"
    runat="server"
    AutoGenerateColumns="false"
    CssClass="table table-striped table-bordered"
    DataKeyNames="Id"
    OnRowDeleting="gvMensagens_RowDeleting">

    <Columns>

        <asp:BoundField DataField="Id" HeaderText="Código" />

        <asp:BoundField DataField="Email"
            HeaderText="E-mail" />

        <asp:BoundField DataField="Texto"
            HeaderText="Mensagem" />

        <asp:BoundField DataField="DataEnvio"
            HeaderText="Data"
            DataFormatString="{0:dd/MM/yyyy}" />

        <asp:TemplateField HeaderText="Ação">
    <ItemTemplate>

        <asp:Button ID="btnExcluir"
            runat="server"
            Text="Excluir"
            CssClass="btn btn-danger btn-sm"
            CommandName="Delete"
            OnClientClick="return confirm('Deseja realmente excluir esta mensagem?');" />

    </ItemTemplate>
</asp:TemplateField>

    </Columns>

</asp:GridView>

</asp:Content>
