$(document).ready(function () {

    if ($("#tabela tr").length > 1) {
        $("#msgSemItens").hide();
    } else {
        $("#msgSemItens").show();
        $("#tabela").hide()
    }

    $("#descricaoPesquisa").keyup(function () {
        if (this.value == "") {
            $("#filtrar").click();
        }
    });

    $("#Delete").click(function confirmarDelete() {
        return confirm("Tem certeza que deseja excluir este produto?");
    });
    
 });