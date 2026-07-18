<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Trabalho.aspx.cs" Inherits="Opus.View.Telas.Moderador.Trabalho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Cadastrar Serviços</h2>
    </center>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Serviço</label>
        <asp:TextBox ID="tbxServico" type="text" class="form-control" runat="server"></asp:TextBox>
    </div>
    <center>
        <asp:Button runat="server" ID="btnEnviar" type="button" OnClick="EnviarServico" Width="200px" class="btn cor-roxa btn-dark" Text="Enviar" />
    </center>
    <br />

    <asp:GridView
        ID="gvServico"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        OnRowDeleting="btnExcluir">

        <Columns>

            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Nome" HeaderText="Serviço" />

            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>

                    <asp:Button
                        ID="btnExcluir"
                        runat="server"
                        Text="Excluir"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="Delete"
                        OnClientClick="return confirm('Deseja excluir este serviço?');" />

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
</asp:Content>
