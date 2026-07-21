<%@ Page Title="Carreira - Opus" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Carreira.aspx.cs" Inherits="Opus.View.Telas.Autonomo.Carreira" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h2>Carreira</h2>
    </center>

    <br />

    <h4>Opções de serviços para oferecer</h4>

    <br />
    <asp:DropDownList
        ID="ddlServico"
        runat="server"
        CssClass="form-select">
    </asp:DropDownList>

    <br />

    <asp:Button
        ID="btnAdicionarServico"
        runat="server"
        Text="Adicionar Serviço"
        CssClass="btn cor-roxa"
        OnClick="btnAdicionarServico_Click" />

    <br />
    <hr />
    <br />

    <asp:GridView
        ID="gvServicos"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped"
        OnRowDeleting="gvServicos_RowDeleting">

        <Columns>

            <asp:BoundField
                DataField="NomeServico"
                HeaderText="Serviço" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Excluir" />

        </Columns>

    </asp:GridView>

    <br />
    <br />

    <h4>Opções de serviços para oferecer</h4>

    <br />
    <asp:DropDownList
        ID="ddlRegiao"
        runat="server"
        CssClass="form-select">
    </asp:DropDownList>

    <br />

    <asp:Button
        ID="btnCadastrarRegiao"
        runat="server"
        Text="Adicionar Região"
        CssClass="btn cor-roxa"
        OnClick="btnAdicionarRegiao_Click" />

    <br />
    <hr />
    <br />

    <asp:GridView
        ID="gvRegiao"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped"
        OnRowDeleting="gvRegiao_RowDeleting">

        <Columns>

            <asp:BoundField
                DataField="NomeRegiao"
                HeaderText="Região" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Excluir" />

        </Columns>

    </asp:GridView>

</asp:Content>