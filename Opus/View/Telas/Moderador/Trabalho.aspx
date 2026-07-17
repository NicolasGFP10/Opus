<%@ page title="" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Trabalho.aspx.cs" inherits="Opus.View.Telas.Moderador.Trabalho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        OnRowDeleting="btnExcluir">

        <columns>

            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Nome" HeaderText="Serviço" />

            <asp:TemplateField HeaderText="Ações">
                <itemtemplate>

                    <asp:Button
                        ID="btnExcluir"
                        runat="server"
                        Text="Excluir"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="Delete"
                        OnClientClick="return confirm('Deseja excluir este serviço?');" />

                </itemtemplate>
            </asp:TemplateField>

        </columns>

    </asp:GridView>
</asp:Content>
