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
        ID="btnAdicionar"
        runat="server"
        Text="Adicionar Serviço"
        CssClass="btn cor-roxa"
        OnClick="btnAdicionar_Click" />

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
    


</asp:Content>