<%@ page title="Carreira - Opus" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Carreira.aspx.cs" inherits="Opus.View.Telas.Autonomo.Carreira" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Carreira</h2>
    </center>

    <br />

    <!-- ===================================== SERVIÇO ===================================== -->

    <h5>Opções de serviços para oferecer</h5>

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
        CssClass="btn cor-roxa btn-dark"
        OnClick="btnAdicionarServico_Click" />

    <br />
    <br />

    <asp:GridView
        ID="gvServicos"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped"
        OnRowDeleting="gvServicos_RowDeleting">

        <columns>

            <asp:BoundField
                DataField="NomeServico"
                HeaderText="Serviço" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Remover" />

        </columns>

    </asp:GridView>

    <br />
    <hr />
    <br />

    <!-- ===================================== REGIÃO ===================================== -->

    <h5>Opções de regiões para atender</h5>

    <br />

    <asp:DropDownList
        ID="ddlEstado"
        runat="server"
        CssClass="form-select"
        AutoPostBack="true"
        OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
    </asp:DropDownList>

    <br />

    <asp:DropDownList
        ID="ddlCidade"
        runat="server"
        CssClass="form-select"
        Enabled="false">
    </asp:DropDownList>

    <br />

    <asp:Button
        ID="btnSalvarCidade"
        runat="server"
        Text="Adicionar"
        CssClass="btn cor-roxa"
        OnClick="btnSalvarCidade_Click" />

    <br />

    <asp:GridView
        ID="gvRegiao"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped"
        OnRowDeleting="gvRegiao_RowDeleting">

        <columns>

            <asp:BoundField
                DataField="NomeEstado"
                HeaderText="Estado" />

            <asp:BoundField
                DataField="NomeCidade"
                HeaderText="Cidade" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Remover" />

        </columns>

    </asp:GridView>

</asp:Content>
