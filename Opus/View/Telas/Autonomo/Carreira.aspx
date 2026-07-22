<%@ Page Title="Carreira - Opus" Language="C#" MasterPageFile="~/View/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Carreira.aspx.cs"
    Inherits="Opus.View.Telas.Autonomo.Carreira" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Minha Carreira</h2>
        <p class="text-muted">
            Escolha os serviços que você oferece e as cidades onde trabalha.
        </p>
    </center>

    <hr />

    <!-- ========================================================= -->
    <!-- SERVIÇOS -->
    <!-- ========================================================= -->

    <div class="card mb-4">

        <div class="card-header">
            <strong>Serviços oferecidos</strong>
        </div>

        <div class="card-body">

            <div class="row">

                <div class="col-md-9">

                    <asp:DropDownList
                        ID="ddlServico"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>

                </div>

                <div class="col-md-3">

                    <asp:Button
                        ID="btnAdicionarServico"
                        runat="server"
                        Text="Adicionar"
                        CssClass="btn cor-roxa w-100"
                        OnClick="btnAdicionarServico_Click" />

                </div>

            </div>

            <br />

            <asp:GridView
                ID="gvServicos"
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="ID"
                CssClass="table table-striped table-hover table-bordered"
                OnRowDeleting="gvServicos_RowDeleting">

                <Columns>

                    <asp:BoundField
                        DataField="NomeServico"
                        HeaderText="Serviço" />

                    <asp:CommandField
                        ShowDeleteButton="True"
                        DeleteText="Remover" />

                </Columns>

            </asp:GridView>

        </div>

    </div>

    <!-- ========================================================= -->
    <!-- CIDADES -->
    <!-- ========================================================= -->

    <div class="card">

        <div class="card-header">
            <strong>Regiões de atendimento</strong>
        </div>

        <div class="card-body">

            <div class="row">

                <div class="col-md-5">

                    <asp:DropDownList
                        ID="ddlEstado"
                        runat="server"
                        CssClass="form-select"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                    </asp:DropDownList>

                </div>

                <div class="col-md-5">

                    <asp:DropDownList
                        ID="ddlCidade"
                        runat="server"
                        CssClass="form-select"
                        Enabled="false">
                    </asp:DropDownList>

                </div>

                <div class="col-md-2">

                    <asp:Button
                        ID="btnSalvarCidade"
                        runat="server"
                        Text="Adicionar"
                        CssClass="btn cor-roxa w-100"
                        OnClick="btnSalvarCidade_Click" />

                </div>

            </div>

            <br />

            <asp:GridView
                ID="gvRegiao"
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="ID"
                CssClass="table table-striped table-hover table-bordered"
                OnRowDeleting="gvRegiao_RowDeleting">

                <Columns>

                    <asp:BoundField
                        DataField="NomeEstado"
                        HeaderText="Estado" />

                    <asp:BoundField
                        DataField="NomeCidade"
                        HeaderText="Cidade" />

                    <asp:CommandField
                        ShowDeleteButton="True"
                        DeleteText="Remover" />

                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>
