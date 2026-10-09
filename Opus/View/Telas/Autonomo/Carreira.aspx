<%@ page title="Carreira - Opus"
    language="C#"
    masterpagefile="~/View/Site.Master"
    autoeventwireup="true"
    codebehind="Carreira.aspx.cs"
    inherits="Opus.View.Telas.Autonomo.Carreira" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <center>
        <h2>Carreira</h2>
    </center>

    <br />

    <!-- ===================================================== -->
    <!-- SERVIÇOS -->
    <!-- ===================================================== -->

    <h5>Serviços oferecidos</h5>

    <br />

    <div class="mb-3">

        <label class="form-label">
            Serviço
        </label>

        <asp:DropDownList
            ID="ddlServico"
            runat="server"
            CssClass="form-select">
        </asp:DropDownList>

    </div>

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
        CssClass="table table-striped table-bordered"
        OnRowDeleting="gvServicos_RowDeleting">

        <columns>

            <asp:BoundField
                DataField="Nome"
                HeaderText="Serviço" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Remover"
                ButtonType="Button" />

        </columns>

    </asp:GridView>

    <br />

    <hr />

    <br />

    <!-- ===================================================== -->
    <!-- REGIÕES DE ATENDIMENTO -->
    <!-- ===================================================== -->

    <h5>Regiões de atendimento</h5>

    <p>
        Selecione os locais onde você oferece seus serviços.
    </p>

    <br />

    <!-- ESTADO -->

    <div class="mb-3">

        <label class="form-label">
            Estado
        </label>

        <asp:DropDownList
            ID="ddlEstado"
            runat="server"
            CssClass="form-select"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
        </asp:DropDownList>

    </div>

    <!-- CIDADE -->

    <div class="mb-3">

        <label class="form-label">
            Cidade
        </label>

        <asp:DropDownList
            ID="ddlCidade"
            runat="server"
            CssClass="form-select"
            Enabled="false">
        </asp:DropDownList>

    </div>

    <asp:Button
        ID="btnSalvarCidade"
        runat="server"
        Text="Adicionar Cidade"
        CssClass="btn cor-roxa btn-dark"
        OnClick="btnSalvarCidade_Click" />

    <br />
    <br />

    <!-- CIDADES ESCOLHIDAS -->

    <asp:GridView
        ID="gvRegiao"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        CssClass="table table-striped table-bordered"
        OnRowDeleting="gvRegiao_RowDeleting">

        <columns>

            <asp:BoundField
                DataField="Estado"
                HeaderText="Estado" />

            <asp:BoundField
                DataField="Cidade"
                HeaderText="Cidade" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Remover"
                ButtonType="Button" />

        </columns>

    </asp:GridView>

    <!-- ===================================================== -->
    <!-- PORTIFÓLIO -->
    <!-- ===================================================== -->

    <center>
        <h1>Portifólio</h1>
    </center>

    <asp:Button
        ID="buttonEnviar"
        runat="server"
        Text="Enviar portfólio"
        CssClass="btn btn-dark cor-roxa"
        OnClick="btnEnviar_Click" />

    <br />

    <!-- Modal -->
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Mostre um pouco de você</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <asp:Label ID="lblMensagem" CssClass="fonte-vermelha" runat="server" Text=""></asp:Label>

                    <label class="form-label">
                        Comente um pouco sobre o serviço que você realizou
                    </label>

                    <div class="mb-3">
                        <asp:TextBox ID="tbxDescricao" class="form-control" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                    <br />
                    <label class="form-label">
                        Fotos do serviço (até 5 fotos)
                    </label>

                    <asp:FileUpload
                        ID="fuFotos"
                        runat="server"
                        AllowMultiple="true"
                        CssClass="form-control"
                        accept=".jpg,.jpeg,.png,.webp" />

                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-dark" data-bs-dismiss="modal" style="width: 200px;">Fechar</button>

                    <asp:Button runat="server" ID="btnEnviar" type="button" class="btn btn-dark cor-roxa" OnClick="btnEnviar_Click" Text="Enviar portifólio" Width="200px" />

                    <br />
                </div>
            </div>
        </div>
    </div>

    <!-- PORTIFÓLIOS CADASTRADOS -->

    <h4 class="mt-4 mb-3">Meus portfólios</h4>

    <asp:Repeater
        ID="rptPortifolios"
        runat="server"
        OnItemCommand="rptPortifolios_ItemCommand">

        <itemtemplate>

            <div class="card-portifolio">

                <div class="portifolio-cabecalho">
                    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                        <asp:LinkButton
                            ID="btnExcluirPortifolio"
                            runat="server"
                            CssClass="btn btn-danger"
                            Text="Excluir"
                            CommandName="Excluir"
                            CommandArgument='<%# Eval("ID") %>'
                            OnClientClick="return confirm('Deseja realmente excluir este portfólio?');" />
                    </div>


                </div>

                <p class="portifolio-descricao">
                    <%#: Eval("Descricao") %>
                </p>

                <div class="portifolio-fotos">

                    <asp:Repeater
                        ID="rptFotos"
                        runat="server"
                        DataSource='<%# Eval("Fotos") %>'>

                        <itemtemplate>

                            <asp:Image
                                ID="imgPortifolio"
                                runat="server"
                                CssClass="foto-avaliacao img-thumbnail"
                                ImageUrl='<%# Eval("Caminho") %>' />

                        </itemtemplate>

                    </asp:Repeater>

                </div>

            </div>

        </itemtemplate>

    </asp:Repeater>

</asp:Content>
